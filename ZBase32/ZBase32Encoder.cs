using System.Text;

namespace ZBase32;

/// <summary>
/// Provides methods to encode and decode data using the Z-Base-32 alphabet.
/// Z-Base-32 is a human-friendly variant of Base32, designed by Zooko Wilcox-O'Hearn.
/// </summary>
public static class ZBase32Encoder
{
    private const string Alphabet = "ybndrfg8ejkmcpqxot1uwisza345h769";
    private static readonly Dictionary<char, byte> CharToValue = new();

    static ZBase32Encoder()
    {
        for (byte i = 0; i < Alphabet.Length; i++)
            CharToValue[Alphabet[i]] = i;
    }

    /// <summary>
    /// Encodes a byte array into a Z-Base-32 string.
    /// </summary>
    public static string Encode(byte[] data)
    {
        if (data.Length == 0)
        {
            return string.Empty;
        }

        StringBuilder result = new StringBuilder();
        int buffer = 0;
        int bitsLeft = 0;

        foreach (byte b in data)
        {
            buffer = (buffer << 8) | b;
            bitsLeft += 8;

            while (bitsLeft >= 5)
            {
                int index = (buffer >> (bitsLeft - 5)) & 0x1F;
                bitsLeft -= 5;
                result.Append(Alphabet[index]);
            }
        }

        if (bitsLeft > 0)
        {
            int index = (buffer << (5 - bitsLeft)) & 0x1F;
            result.Append(Alphabet[index]);
        }

        return result.ToString();
    }

    /// <summary>
    /// Decodes a Z-Base-32 string back into a byte array.
    /// </summary>
    public static byte[] Decode(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return [];
        }

        input = input.Trim().ToLowerInvariant();
        List<byte> output = [];

        int buffer = 0;
        int bitsLeft = 0;

        foreach (char c in input)
        {
            if (!CharToValue.TryGetValue(c, out byte value))
            {
                throw new FormatException($"Invalid Z-Base-32 character: '{c}'");
            }

            buffer = (buffer << 5) | value;
            bitsLeft += 5;

            if (bitsLeft >= 8)
            {
                bitsLeft -= 8;
                output.Add((byte)((buffer >> bitsLeft) & 0xFF));
            }
        }

        return output.ToArray();
    }

    /// <summary>
    /// Encodes a UTF-8 string into a Z-Base-32 string.
    /// </summary>
    public static string EncodeUtf8String(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        byte[] bytes = Encoding.UTF8.GetBytes(text);
        return Encode(bytes);
    }

    /// <summary>
    /// Decodes a Z-Base-32 string back into a UTF-8 string.
    /// </summary>
    public static string DecodeToUtf8String(string encoded)
    {
        if (string.IsNullOrWhiteSpace(encoded))
        {
            return string.Empty;
        }

        byte[] bytes = Decode(encoded);
        return Encoding.UTF8.GetString(bytes);
    }
}
