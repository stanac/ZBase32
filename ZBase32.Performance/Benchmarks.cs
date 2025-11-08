using BenchmarkDotNet.Attributes;

namespace ZBase32.Performance;

[MemoryDiagnoser]
public class Benchmarks
{
    private const string Source = "3zdkxpb8ngbqhj4wp1ux5pkx9ko8ukje8gj4877kk618bmg575ys88i34uc5ikah3sim1qxhhbbrs7mrcaihgtrhqgobdwqt5q1bfe17t3sqfc96kcmmzi4bpcpifksc6fatx5fxq5z7e7s5hupx3n6x8tjy7ftbw95zwb5bsbriko19rfw59nfpay1o4671dxjniy89np9xkjbwstwx4knrssbbzwwkby94nrtf7b1y";

    [Benchmark]
    public bool EncodeDecode1000()
    {
        bool areEqual = false;

        for (int i = 0; i < 1000; i++)
        {
            byte[] decoded = ZBase32Encoder.Decode(Source);
            string encoded = ZBase32Encoder.Encode(decoded);

            areEqual &= encoded == Source;
        }

        return areEqual;
    }
}