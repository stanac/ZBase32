# ZBase32

ZBase32 is a lightweight, dependency-free C# implementation of the [Z-Base-32](https://philzimmermann.com/docs/human-oriented-base-32-encoding.txt) encoding — a human-friendly variant of Base32 designed by Zooko Wilcox-O’Hearn.

It provides simple methods to encode and decode binary data using the Z-Base-32 alphabet, avoiding visually confusing characters like `0`, `O`, `l`, and `I`.

---

## Installation

Install via [NuGet](https://www.nuget.org/packages/ZBase32/):

```bash
dotnet add package ZBase32
```

---

## Quick Start

```csharp
using System;
using ZBase32;

class Program
{
    static void Main()
    {
        string encoded = ZBase32Encoder.EncodeUtf8String("Hello ZBase32!");
        Console.WriteLine($"Encoded: {encoded}");

        string decoded = ZBase32Encoder.DecodeToUtf8String(encoded);
        Console.WriteLine($"Decoded: {decoded}");
    }
}
```

**Output:**
```
Encoded: jb1sa5dxr1hgbrmfwx4c1p1
Decoded: Hello ZBase32!
```

---

## Features

- Full Z-Base-32 alphabet support  
- Fast and allocation-efficient implementation  
- Static class — thread-safe and dependency-free  
- UTF-8 string convenience methods  
- Built for .NET 8, 9 and 10

---

## Z-Base-32 Alphabet

| Index | Char | Index | Char | Index | Char | Index | Char |
|:------|:-----|:------|:-----|:------|:-----|:------|:-----|
| 0 | y | 8 | e | 16 | o | 24 | 3 |
| 1 | b | 9 | j | 17 | t | 25 | 4 |
| 2 | n | 10 | k | 18 | 1 | 26 | 5 |
| 3 | d | 11 | m | 19 | u | 27 | h |
| 4 | r | 12 | c | 20 | w | 28 | 7 |
| 5 | f | 13 | p | 21 | i | 29 | 6 |
| 6 | g | 14 | q | 22 | s | 30 | 9 |
| 7 | 8 | 15 | x | 23 | z | 31 | a |

---

## API Reference

| Method | Description |
| ------ | ------ |
| `ZBase32Encoder.Encode(byte[] data)` | Encodes a byte array into a Z-Base-32 string. |
| `ZBase32Encoder.Decode(string input)` | Decodes a Z-Base-32 string back into a byte array.  Throws a `FormatException` if the input contains invalid characters.|
| `ZBase32Encoder.EncodeUtf8String(string text)` | Equivalent to `Encode(Encoding.UTF8.GetBytes(text))`. |
| `ZBase32Encoder.DecodeToUtf8String(string encoded)` | Decodes a Z-Base-32 string back into a UTF-8 string. Equivalent to `Encoding.UTF8.GetString(Decode(encoded))`. |

---

## License

This project is licensed under the MIT License.  
