using System.Buffers;
using System.Text;

namespace ZBase32;

/// <summary>
/// Provides methods to encode and decode data using the Z-Base-32 alphabet.
/// This implementation is optimized for performance and low memory allocation.
/// </summary>
public static class ZBase32Encoder
{
    private const string Alphabet = "ybndrfg8ejkmcpqxot1uwisza345h769";
    private static readonly byte[] SDecodeMap = new byte[128];

    static ZBase32Encoder()
    {
#if NETSTANDARD2_0
        for (int i = 0; i < SDecodeMap.Length; i++)
        {
            SDecodeMap[i] = 255;
        }
#else
        Array.Fill(SDecodeMap, (byte)255);
#endif
        for (byte i = 0; i < Alphabet.Length; i++)
        {
            SDecodeMap[Alphabet[i]] = i;
        }
    }

    /// <summary>
    /// Writes the Z-Base-32 encoded version of the source data into a destination span.
    /// </summary>
    /// <param name="source">The binary data to encode.</param>
    /// <param name="destination">The span to write the encoded characters into.</param>
    private static void EncodeTo(ReadOnlySpan<byte> source, Span<char> destination)
    {
        if (source.IsEmpty)
        {
            return;
        }

        int buffer = 0;
        int bitsLeft = 0;
        int charIndex = 0;

        foreach (byte b in source)
        {
            buffer = (buffer << 8) | b;
            bitsLeft += 8;

            while (bitsLeft >= 5)
            {
                bitsLeft -= 5;
                int index = (buffer >> bitsLeft) & 0x1F;
                destination[charIndex++] = Alphabet[index];
            }
        }

        if (bitsLeft > 0)
        {
            int index = (buffer << (5 - bitsLeft)) & 0x1F;
            destination[charIndex] = Alphabet[index];
        }
    }

    /// <summary>
    /// Encodes a byte span into a Z-Base-32 string.
    /// </summary>
    /// <param name="data">The byte span to encode.</param>
    /// <returns>The Z-Base-32 encoded string.</returns>
    public static string Encode(ReadOnlySpan<byte> data)
    {
        if (data.IsEmpty)
        {
            return string.Empty;
        }

        int encodedLength = (data.Length * 8 + 4) / 5;

        // For small strings, use stack allocation to avoid heap allocation.
        if (encodedLength <= 256)
        {
            Span<char> buffer = stackalloc char[encodedLength];
            EncodeTo(data, buffer);
#if NETSTANDARD2_0
            // The string(ReadOnlySpan<char>) constructor is not available on netstandard2.0.
            return new string(buffer.ToArray());
#else
            return new string(buffer);
#endif
        }

        // For larger strings, rent a buffer from the array pool.
        char[] bufferFromPool = ArrayPool<char>.Shared.Rent(encodedLength);
        try
        {
            Span<char> bufferSpan = bufferFromPool.AsSpan(0, encodedLength);
            EncodeTo(data, bufferSpan);
            return new string(bufferFromPool, 0, encodedLength);
        }
        finally
        {
            ArrayPool<char>.Shared.Return(bufferFromPool);
        }
    }

    /// <summary>
    /// Decodes a Z-Base-32 string back into a byte array.
    /// This method is case-insensitive and ignores leading/trailing whitespace.
    /// </summary>
    public static byte[] Decode(ReadOnlySpan<char> input)
    {
        input = input.Trim();
        if (input.IsEmpty)
        {
            return [];
        }

        int decodedLength = input.Length * 5 / 8;
        byte[] output = new byte[decodedLength];

        int buffer = 0;
        int bitsLeft = 0;
        int outputIndex = 0;

        foreach (char c in input)
        {
            char lowerChar = char.ToLowerInvariant(c);

            if (lowerChar >= SDecodeMap.Length)
            {
                throw new FormatException($"Invalid Z-Base-32 character: '{c}'");
            }

            byte value = SDecodeMap[lowerChar];
            if (value == 255)
            {
                throw new FormatException($"Invalid Z-Base-32 character: '{c}'");
            }

            buffer = (buffer << 5) | value;
            bitsLeft += 5;

            if (bitsLeft >= 8)
            {
                bitsLeft -= 8;
                output[outputIndex++] = (byte)(buffer >> bitsLeft);
            }
        }

        return output;
    }

    /// <summary>Encodes a byte array into a Z-Base-32 string.</summary>
    public static string Encode(byte[] data) => Encode(data.AsSpan());

    /// <summary>Decodes a Z-Base-32 string back into a byte array.</summary>
    public static byte[] Decode(string input) => Decode(input.AsSpan());

    /// <summary>Encodes a UTF-8 string into a Z-Base-32 string.</summary>
    public static string EncodeUtf8String(string text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;

        int maxByteCount = Encoding.UTF8.GetMaxByteCount(text.Length);

        // Use a stack buffer for small strings to avoid heap allocation.
        if (maxByteCount <= 256)
        {
#if NETSTANDARD2_0
            // Encoding.GetBytes(string, Span<byte>) is not available on netstandard2.0.
            byte[] bytes = new byte[maxByteCount];
            int byteCount = Encoding.UTF8.GetBytes(text, 0, text.Length, bytes, 0);
            return Encode(bytes.AsSpan(0, byteCount));
#else
            Span<byte> bytes = stackalloc byte[maxByteCount];
            int actualByteCount = Encoding.UTF8.GetBytes(text, bytes);
            return Encode(bytes.Slice(0, actualByteCount));
#endif
        }

        // For larger strings, use the classic GetBytes method.
        byte[] bytesFromHeap = Encoding.UTF8.GetBytes(text);
        return Encode(bytesFromHeap);
    }

    /// <summary>Decodes a Z-Base-32 string back into a UTF-8 string.</summary>
    public static string DecodeToUtf8String(string encoded)
    {
        if (string.IsNullOrWhiteSpace(encoded)) return string.Empty;

        byte[] bytes = Decode(encoded.AsSpan());
        return Encoding.UTF8.GetString(bytes);
    }
}
