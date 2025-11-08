# ZBase32

A modern, high-performance .NET library for Z-Base-32 encoding and decoding.

This implementation is optimized for speed and low memory allocation by leveraging `Span<T>` and other modern .NET features.

## What is Z-Base-32?

Z-Base-32 is a Base32 encoding variant designed by Zooko Wilcox-O'Hearn to be more human-friendly. It uses a custom alphabet that minimizes visual ambiguity between characters (e.g., `1`, `l`, `i`) and is case-insensitive. It is a great choice for generating short, memorable, and easily transcribable identifiers.

## Features

*   **Fast and Efficient:** Optimized for high throughput and minimal memory usage.
*   **Modern API:** Uses `Span<T>` and `ReadOnlySpan<T>` for zero-allocation operations where possible.
*   **Simple Static Methods:** A straightforward `ZBase32Encoder` class with static `Encode` and `Decode` methods.
*   **Case-Insensitive Decoding:** Adheres to the Z-Base-32 specification by treating uppercase and lowercase characters as equivalent during decoding.
*   **Convenience Methods:** Includes helpers for encoding/decoding UTF-8 strings directly.
*   **Supports:** .NET 8, 9 and 10

## Installation

Install the package from NuGet using the .NET CLI:

```sh
dotnet add package ZBase32
```

## Usage

The library exposes a single static class `ZBase32.ZBase32Encoder`.

### Basic Encoding and Decoding

To encode a byte array into a Z-Base-32 string:

```csharp
using ZBase32;

byte[] data = { 0xF8, 0x3E, 0x7A, 0x13, 0x8D, 0x95, 0x3E, 0x56 };
string encoded = ZBase32Encoder.Encode(data);

Console.WriteLine(encoded);
```

To decode a Z-Base-32 string back into a byte array:

```csharp
using ZBase32;

string encoded = "64385t78983xyr4o";
byte[] decoded = ZBase32Encoder.Decode(encoded);
```

### Working with UTF-8 Strings

The library includes helper methods to work directly with UTF-8 strings.

```csharp
using System.Text;
using ZBase32;

// Encoding a string
string originalText = "Hello, World!";
string encodedText = ZBase32Encoder.EncodeUtf8String(originalText);

Console.WriteLine(encodedText);

// Decoding back to a string
string decodedText = ZBase32Encoder.DecodeToUtf8String(encodedText);
Console.WriteLine(decodedText);
```

### High-Performance Usage with Span&lt;T&gt;

For performance-critical scenarios, you can use the `Span<T>`-based overloads to avoid array allocations.

```csharp
using ZBase32;

// Encoding from a ReadOnlySpan<byte>
ReadOnlySpan<byte> dataSpan = new byte[] { 0xDE, 0xAD, 0xBE, 0xEF };
string encoded = ZBase32Encoder.Encode(dataSpan);

Console.WriteLine(encoded);

// Decoding to a byte[] from a ReadOnlySpan<char>
ReadOnlySpan<char> encodedSpan = "tuex3z61".AsSpan();
byte[] decoded = ZBase32Encoder.Decode(encodedSpan);
```

## Performance

This library was designed with performance as a primary goal.
*   **Span-Based Core:** The core logic operates on spans to eliminate unnecessary memory allocations and copies.
*   **Array Pooling:** For encoding larger data sets, `ArrayPool<T>` is used to rent and return buffers, reducing GC pressure.
*   **Optimized Lookups:** Decoding uses a pre-computed array for O(1) character-to-value lookups.

## License

This library is licensed under the MIT License.