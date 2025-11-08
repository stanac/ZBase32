using System.Collections;
using AwesomeAssertions;

namespace ZBase32.Tests;

public class ZBase32EncoderTests
{
    [Fact]
    public void Encode_Decode_GivesEquivalentValues()
    {
        for (int i = 0; i < 100; i++)
        {
            byte[] bytes = GetRandomBytes();

            string data = ZBase32Encoder.Encode(bytes);
            byte[] decoded = ZBase32Encoder.Decode(data);

            decoded.Should().BeEquivalentTo(bytes);
        }
    }

    [Theory]
    [ClassData(typeof(TheorySource))]
    public void Encoded_Decode__GivesEquivalentValues(string encoded, byte[] source)
    {
        byte[] decoded = ZBase32Encoder.Decode(encoded);
        decoded.Should().BeEquivalentTo(source);

        string secondEncode = ZBase32Encoder.Encode(decoded);
        secondEncode.Should().Be(encoded);
    }

    [Fact]
    public void EncodeUtf8String_DecodeToUtf8String_GivesSameString()
    {
        for (int i = 0; i < 100; i++)
        {
            string source = GetRandomString();
            string encoded = ZBase32Encoder.EncodeUtf8String(source);
            string decoded = ZBase32Encoder.DecodeToUtf8String(encoded);

            decoded.Should().BeEquivalentTo(source);
        }
    }

    private static byte[] GetRandomBytes()
    {
        int len = Random.Shared.Next(0, 148);
        byte[] bytes = new byte[len];
        Random.Shared.NextBytes(bytes);

        return bytes;
    }

    private static string GetRandomString()
    {
        const string alphabet = "qwertyuiopasdfghjklzxcvbnm1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
        int len = Random.Shared.Next(0, 148);
        char[] data = new char[len];

        for (int i = 0; i < len; i++)
        {
            data[i] = alphabet[Random.Shared.Next(alphabet.Length)];
        }

        return new string(data);
    }
}

public class TheorySource : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return ["7xq41aw6h33g1i4ryn9qasghsgxhfgqid63n8qa39qggmb9nqc", new byte[] { 235, 221, 169, 98, 158, 230, 114, 105, 87, 68, 0, 190, 236, 88, 220, 177, 159, 194, 153, 213, 31, 178, 35, 187, 25, 251, 140, 101, 135, 226, 115 }];
        yield return ["yj7xu5uhf9jozcxq9y44tag6ie1jzrn8h84hr37mfgmypyhci5xyraka3aqkemng816wjgrrdkh6asfznqi94gc7w7pnfuxc8sdne", new byte[] { 2, 122, 249, 238, 124, 47, 211, 11, 177, 238, 248, 53, 168, 224, 222, 170, 36, 155, 144, 71, 225, 245, 194, 103, 171, 41, 150, 6, 131, 140, 174, 222, 2, 97, 88, 206, 28, 164, 44, 70, 60, 189, 68, 152, 132, 26, 185, 236, 88, 183, 19, 171, 253, 25, 157, 167, 90, 34, 205, 236, 61, 134, 36 }];
        yield return ["53bah6x1fk9udgz7hfed6eny8msp8dnafs3rtbz35hn4rirznxrrbzf5sj99fyegi6txdd3o4cfdfm8x", new byte[] { 222, 67, 142, 121, 242, 42, 191, 49, 154, 253, 225, 80, 63, 32, 64, 58, 236, 211, 140, 88, 45, 178, 72, 134, 249, 223, 5, 162, 84, 151, 19, 200, 64, 220, 187, 178, 127, 242, 129, 6, 175, 162, 241, 143, 48, 211, 10, 50, 172, 239 }];
        yield return ["najqmy8j49bk4aedhtnktjd9t3njpwk6ierhco4cdfagb588m14nkgx76j8pjwiqahq8q9gbn8fib1h4nksy3kmimr8aqgihxi337jnnyp6exurniu796j81jzkoahe86du17qejgdfbear1qm4kyqco57kqh9nntsnyqdjb9npw5iec7cychooidxhjfnpf99h4qsfdzxii7oy", new byte[] { 22, 18, 229, 128, 233, 215, 194, 173, 97, 3, 228, 68, 168, 164, 127, 142, 68, 150, 209, 94, 170, 9, 198, 67, 76, 25, 112, 96, 236, 231, 92, 180, 37, 25, 253, 242, 78, 212, 210, 174, 199, 28, 119, 124, 193, 17, 203, 80, 203, 154, 18, 172, 12, 169, 117, 89, 15, 135, 26, 188, 125, 115, 158, 164, 66, 3, 124, 135, 204, 130, 172, 251, 255, 36, 242, 77, 213, 12, 113, 7, 240, 231, 46, 185, 9, 48, 202, 20, 96, 146, 114, 244, 160, 57, 144, 223, 84, 238, 124, 66, 141, 132, 7, 13, 33, 248, 155, 77, 213, 12, 235, 0, 206, 66, 21, 27, 248, 146, 137, 165, 255, 249, 167, 88, 163, 187, 235, 94, 192 }];
        yield return ["g8austwbt5g4jts3iqknd6dkxf7igjmep6u4znb34q814b5pe1r3nc66c1qbh", new byte[] { 49, 241, 59, 70, 129, 142, 205, 164, 198, 217, 171, 148, 33, 248, 106, 121, 123, 83, 37, 104, 111, 167, 171, 136, 57, 211, 143, 45, 7, 109, 68, 137, 145, 51, 222, 100, 156, 30 }];
        yield return ["q4ibhdbaafmrjyrgghey", new byte[] { 118, 170, 30, 12, 56, 193, 86, 68, 128, 134, 55, 16 }];
        yield return ["it5wf139844x5csb9twupifsadoo6km9y5ejqbknt4ho5kuqthrbbapbmihmtyzzdxmndm3a3ghpzj3t6chqktoddrxybbpugmd9wdeuy4ubg6uf3h6qa5bymufip1hasnxwpkd3jaci6ajpaw5o3nqaeou5q3y", new byte[] { 172, 119, 66, 203, 63, 62, 180, 253, 178, 193, 252, 105, 54, 212, 182, 192, 225, 15, 41, 127, 6, 208, 151, 5, 66, 142, 185, 13, 170, 110, 143, 8, 16, 225, 161, 93, 120, 184, 130, 247, 27, 214, 33, 175, 56, 201, 184, 219, 167, 49, 243, 56, 229, 70, 3, 25, 30, 0, 133, 179, 50, 199, 250, 13, 19, 6, 166, 19, 122, 101, 207, 60, 236, 108, 32, 92, 203, 86, 203, 152, 176, 159, 70, 168, 121, 78, 25, 95, 97, 45, 197, 55, 12, 137, 216, 68, 39, 183, 100 }];
        yield return ["zfmfjyma1ru8m7ygkjfupffjpg8omjhwfnafhtpbhtqxwayczstsk11xtmdc6gagc8zz6kry3g1crexp59wwmsyd84m37g6p9daai9r7yrnxwfefg16g8tkzn1njqxm8bwu66ke", new byte[] { 185, 86, 84, 129, 120, 145, 38, 117, 244, 6, 82, 75, 54, 148, 169, 105, 143, 5, 167, 148, 40, 176, 94, 69, 161, 228, 92, 250, 96, 12, 189, 163, 101, 74, 79, 138, 198, 207, 27, 6, 97, 239, 127, 40, 128, 201, 164, 194, 33, 237, 223, 233, 69, 216, 3, 62, 151, 158, 155, 205, 248, 241, 138, 252, 157, 1, 4, 250, 21, 5, 52, 188, 99, 197, 87, 20, 132, 151, 61, 103, 13, 39, 239, 41 }];
        yield return ["uxfwogm3ceg1ynra8msst7h4augj1cir5wggg9rjdwjy67p3gg6oby17z3t6woa4qzrq76thsqjcf1z9qt76gnf8u1un8mgjsj9e6j1bjgijc1wqb3ah48gxpossz554ks6wi4zeuw8tukcjbb4ph6o5hqqdh74xa1hiu58uq6bc9eg1s1mqp5tx7toxjrcdwtac71endtsr1b6oc6jsg", new byte[] { 155, 203, 72, 25, 121, 98, 13, 32, 8, 152, 58, 237, 104, 247, 154, 196, 204, 153, 50, 164, 221, 12, 99, 124, 137, 29, 18, 15, 117, 185, 49, 189, 0, 130, 93, 190, 99, 234, 67, 26, 117, 200, 238, 250, 60, 179, 146, 194, 202, 255, 116, 123, 227, 8, 167, 156, 166, 35, 172, 201, 178, 126, 143, 38, 65, 73, 170, 150, 74, 142, 14, 113, 205, 28, 207, 108, 45, 107, 239, 122, 85, 189, 74, 234, 232, 157, 15, 25, 169, 137, 8, 116, 222, 122, 27, 227, 156, 62, 119, 79, 196, 185, 89, 236, 243, 119, 130, 207, 160, 210, 180, 150, 230, 238, 47, 236, 96, 244, 145, 131, 164, 112, 206, 201, 2, 28, 108, 73, 7, 208, 103, 147, 99 }];
        yield return ["y6jsn1wisss5h7bp4qbj56zmm9rjsz9bz6cxkrxsjhcxbmut5rm119yb7ocf7znb939w3uho9wxj8k6yjqsae5xjeczz9d7wz4qg6usc5x4y", new byte[] { 7, 147, 97, 74, 149, 181, 173, 190, 116, 45, 211, 130, 157, 250, 235, 95, 200, 155, 95, 225, 191, 152, 245, 17, 246, 79, 24, 240, 174, 113, 217, 23, 41, 124, 1, 236, 24, 94, 220, 65, 254, 127, 76, 207, 144, 253, 30, 147, 171, 192, 75, 173, 132, 109, 233, 67, 47, 127, 143, 180, 190, 156, 111, 78, 204, 219, 244 }];
        yield return ["6c", new byte[] { 243 }];
        yield return ["bdfhh6gw483rhcur54nqp38fizsgxmb3qf4qecbq6doicxsfpdrz3gimssubofe4rihxx8hyoc1tstmjghg7d66pgfsmo9ban1ir136jf6jiap4u6ee64ooy5rjp78z7i5mo", new byte[] { 8, 203, 206, 120, 212, 209, 242, 78, 50, 100, 222, 132, 230, 228, 229, 173, 236, 103, 172, 57, 113, 116, 228, 48, 46, 240, 225, 86, 62, 197, 104, 201, 124, 154, 171, 181, 166, 24, 21, 26, 37, 120, 247, 159, 128, 131, 37, 27, 69, 105, 55, 13, 209, 251, 205, 49, 108, 184, 124, 56, 20, 170, 73, 103, 201, 47, 147, 92, 55, 83, 242, 17, 237, 66, 0, 217, 18, 222, 158, 253, 174, 215 }];
        yield return ["anym3y3z53espg4bo9re5hkzqt8ksazmtdxady8z5htsse4bu8f6picjq7imidu5yz9ucpqmxcdqqctm1za3wdtaou78wjp4gdpfneyrit7uoo1ykefcf83q7w", new byte[] { 192, 128, 188, 131, 55, 222, 81, 102, 155, 65, 135, 200, 141, 241, 87, 116, 78, 171, 98, 235, 136, 223, 129, 128, 247, 223, 35, 107, 35, 65, 153, 203, 230, 213, 137, 119, 106, 186, 142, 123, 5, 255, 54, 53, 203, 123, 6, 231, 50, 43, 149, 241, 154, 14, 56, 132, 250, 122, 37, 186, 48, 218, 81, 32, 4, 172, 123, 56, 66, 64, 82, 10, 194, 159, 46, 237 }];
        yield return ["3zdkxpb8ngbqhj4wp1ux5pkx9ko8ukje8gj4877kk618bmg575ys88i34uc5ikah3sim1qxhhbbrs7mrcaihgtrhqgobdwqt5q1bfe17t3sqfc96kcmmzi4bpcpifksc6fatx5fxq5z7e7s5hupx3n6x8tjy7ftbw95zwb5bsbriko19rfw59nfpay1o4671dxjniy89np9xkjbwstwx4knrssbbzwwkby94nrtf7b1y", new byte[] { 205, 198, 167, 180, 39, 17, 130, 238, 39, 84, 108, 166, 253, 181, 79, 250, 160, 121, 169, 40, 57, 147, 163, 247, 170, 87, 164, 112, 172, 219, 238, 193, 99, 158, 185, 212, 217, 186, 171, 28, 205, 170, 185, 57, 252, 224, 66, 75, 117, 100, 102, 43, 195, 68, 156, 113, 160, 17, 209, 209, 219, 164, 18, 162, 93, 142, 108, 226, 179, 254, 83, 22, 187, 215, 65, 107, 27, 82, 170, 204, 241, 113, 23, 236, 175, 118, 239, 212, 118, 219, 228, 218, 252, 139, 207, 60, 82, 14, 150, 33, 167, 247, 122, 7, 97, 176, 73, 85, 66, 95, 33, 105, 191, 136, 173, 192, 37, 13, 123, 178, 27, 210, 42, 128, 255, 19, 126, 245, 36, 52, 180, 104, 253, 40, 68, 181, 130, 27, 210, 138, 8, 63, 161, 18, 37, 232, 100 }];
        yield return ["f4z6oriemp5ykd8h4sn3x5wmzo9qbpfbfhyfknggeth7qu6nrb7wbaoeitqx1rdnpq7cexgracpo7h4yi1apykbimxt9bxzx4nzbr6gt5uxna", new byte[] { 46, 175, 232, 18, 168, 91, 118, 5, 12, 252, 213, 133, 151, 238, 139, 188, 62, 224, 180, 161, 47, 0, 85, 8, 198, 68, 121, 215, 79, 194, 32, 123, 64, 226, 8, 172, 92, 249, 16, 98, 107, 186, 196, 60, 196, 195, 27, 14, 243, 64, 172, 176, 208, 40, 53, 91, 227, 240, 190, 239, 208, 174, 18, 120, 209, 220, 222, 44 }];
        yield return ["7zo4m13opqnykhnx8sktjucdhqu8sm5fa6tmr7rs", new byte[] { 237, 225, 165, 203, 48, 107, 132, 5, 112, 79, 61, 149, 20, 205, 131, 227, 166, 123, 47, 101, 199, 162, 178, 116, 150 }];
        yield return ["jmajaiiw5wtuo", new byte[] { 74, 240, 156, 86, 180, 221, 35, 56 }];
        yield return ["sbz5qe6kw5r468c93hs593og6dpd7sa", new byte[] { 176, 111, 183, 35, 202, 166, 201, 175, 29, 159, 207, 45, 191, 230, 6, 240, 218, 62, 219 }];
        yield return ["xdjrre4p8f6uzmdpzgm6nsigmrpzwonga1wat5fb7izzf7aa189dh6g5pyz9z86e9qxseh5nm5pr6j5i", new byte[] { 120, 210, 66, 35, 77, 57, 125, 59, 172, 109, 185, 151, 225, 90, 166, 89, 27, 122, 64, 70, 196, 169, 136, 236, 161, 237, 111, 114, 247, 24, 145, 254, 62, 120, 219, 104, 47, 251, 159, 200, 251, 159, 100, 115, 98, 94, 218, 79, 39, 117 }];
        yield return ["1d11uac9nuwu14czx37y4q7fnbd31jxpm67qg791g1bhuygm39emzx1u6kauqdidg4fp4ofd6yfb63puossedjrriou3yc7qbwid3h3au5n1ekyn7omomyfq44xmb5ryw34xnkzzh3dt5nyzz57huekgsbwt3ikncmiarw9yuq8ntxn6crxfu7f8qdsc89qoh3ao4seu1gmy", new byte[] { 144, 229, 41, 225, 159, 20, 233, 57, 105, 151, 126, 122, 13, 59, 165, 16, 71, 153, 37, 237, 95, 186, 227, 119, 242, 52, 131, 201, 128, 203, 207, 208, 187, 190, 83, 242, 177, 55, 14, 163, 54, 138, 221, 64, 163, 240, 10, 31, 101, 179, 133, 172, 129, 164, 132, 172, 39, 144, 51, 174, 13, 42, 60, 243, 56, 158, 197, 36, 40, 2, 236, 23, 5, 128, 174, 214, 158, 176, 236, 128, 166, 116, 241, 42, 247, 230, 71, 29, 136, 23, 190, 251, 201, 161, 70, 176, 105, 28, 213, 66, 98, 235, 130, 83, 224, 155, 142, 40, 188, 94, 97, 30, 89, 244, 167, 112, 236, 195, 253, 208, 230, 113, 13, 89, 19, 145, 150 }];
        yield return ["zhpbmg1t17u755y9gciewbnuuurejdhytf1bc5ops4bz5zznpdm8gy9tnm384nrwz6xoaufzrdj7sk7m45644wp5yfrkmc1z73xtphxisfky3jxfboamy1d5ry97iz6pfxm1xgs6qyammhpeyk85qh9o3cge4ycfo4eu68jt3se1hbepkp48s8jwjw1h61ywrwi7m46p6zhbz5ta", new byte[] { 191, 26, 21, 154, 81, 151, 103, 221, 236, 31, 51, 42, 138, 4, 83, 156, 200, 132, 143, 128, 137, 100, 22, 110, 13, 182, 131, 125, 222, 226, 104, 214, 115, 3, 241, 18, 242, 125, 8, 148, 191, 159, 12, 76, 183, 32, 211, 219, 43, 171, 214, 253, 173, 81, 187, 1, 72, 165, 178, 87, 238, 95, 22, 241, 245, 177, 84, 12, 165, 229, 12, 48, 176, 72, 123, 32, 63, 218, 223, 205, 43, 215, 39, 154, 222, 112, 48, 181, 241, 168, 2, 143, 183, 115, 240, 203, 12, 141, 1, 133, 134, 145, 63, 29, 49, 205, 145, 46, 5, 13, 83, 116, 123, 29, 52, 77, 37, 207, 72, 20, 37, 43, 213, 235, 205, 245, 248, 27, 238, 56 }];
        yield return ["j8d4d9rxzgrktbbhiwcgooow6jf57xeksktdn3ydbiy6s387z4cpy4hum3iitcn9xcjwtofkesmqftezhi5o3p3n7f8j1uxnkuwfix1hy8rcaci7jc13wp5odxss8xkxf4fwaouedk1qwsash3k554wu3gte4", new byte[] { 73, 199, 161, 252, 143, 185, 136, 168, 132, 60, 173, 24, 104, 66, 20, 242, 75, 190, 189, 10, 178, 162, 49, 100, 3, 13, 65, 235, 100, 253, 190, 152, 208, 107, 147, 94, 107, 88, 176, 95, 123, 19, 72, 192, 170, 69, 150, 226, 197, 23, 229, 119, 12, 183, 34, 233, 78, 153, 77, 226, 84, 232, 90, 190, 92, 1, 200, 204, 50, 189, 75, 37, 154, 55, 112, 27, 237, 99, 189, 79, 46, 139, 76, 66, 104, 26, 164, 234, 91, 22, 230, 85, 189, 234, 147, 201, 162, 141 }];
        yield return ["iqw1rhpr7thd1hmkz7k5kh8xikk3iz557h4gy1ib3swijtnmuszxe4i87p6opboh6nmfpp44qk8dq4dzexqypc493kreoyqupr1xnksup85xwy54bmzi51b8hagwzzop4zfjbzbrmx8mg8mjobqymu8bjx53e1sana8uwuq73kedrg6mfeinrcnojzo6u5kdc7iazngoj5tbgae", new byte[] { 171, 169, 34, 113, 164, 236, 120, 57, 113, 106, 191, 85, 181, 112, 239, 170, 149, 154, 223, 123, 239, 52, 96, 74, 161, 205, 169, 84, 196, 75, 157, 174, 244, 106, 167, 235, 125, 6, 134, 28, 240, 150, 86, 183, 90, 114, 142, 55, 104, 119, 67, 220, 6, 179, 95, 202, 136, 136, 1, 211, 105, 36, 241, 42, 211, 105, 246, 250, 3, 122, 10, 239, 93, 200, 39, 230, 13, 75, 222, 13, 213, 202, 144, 220, 36, 91, 206, 179, 29, 105, 128, 92, 5, 204, 225, 75, 247, 148, 74, 216, 22, 15, 58, 77, 221, 202, 144, 50, 27, 203, 42, 42, 34, 48, 80, 77, 225, 233, 237, 67, 103, 107, 139, 136, 208, 78, 226, 19, 97 }];
        yield return ["naa8th9rzw3cpircqxwe7eabokcrtxy3oymx47zbcjhbi63wjxizm8w1mitjicduuk4nrkahqgnmcktw7yw9urtazrsp1as4dpn3o55yf5n46msw1nnskbdor8soirezbk3u6r57tzrj5zemeaa4oethft7tbw186n4wesd13ig6w78iyssj6w4g8yo4i7p7beq8yx31oqfeb3nw5y19p7qjy84f1ib3e6fxh11i", new byte[] { 22, 48, 120, 243, 228, 189, 50, 198, 212, 140, 115, 232, 142, 163, 1, 130, 152, 72, 188, 25, 128, 22, 253, 118, 225, 98, 120, 26, 251, 52, 75, 235, 117, 158, 146, 93, 98, 154, 176, 115, 154, 180, 34, 43, 28, 113, 132, 182, 42, 52, 232, 41, 249, 146, 56, 185, 44, 217, 98, 218, 27, 69, 152, 111, 96, 46, 197, 175, 46, 212, 144, 133, 101, 4, 112, 33, 237, 10, 145, 23, 10, 179, 63, 19, 125, 141, 200, 157, 221, 11, 70, 49, 168, 34, 60, 44, 123, 16, 210, 71, 240, 181, 68, 88, 114, 205, 77, 234, 116, 245, 5, 172, 159, 83, 70, 56, 33, 170, 245, 189, 10, 28, 112, 63, 50, 131, 138, 128, 228, 84, 216, 37, 246, 245, 201, 1, 244, 89, 84, 57, 71, 138, 254, 74, 85 }];
        yield return ["6oceaa5grmfm8caj3x3hacxucbzm17hxcg5gxgyegzt7yiihw7wxsdcoitwwdfxo6pa1po1hjskm5pxxoaeru9xkwat8z8do45qqndq3kf47j3ckhmy8figusjier5eiqopo", new byte[] { 244, 24, 140, 99, 102, 34, 202, 179, 179, 9, 203, 243, 204, 49, 243, 96, 110, 185, 119, 143, 97, 182, 103, 152, 8, 53, 227, 208, 86, 188, 167, 104, 251, 13, 144, 172, 105, 65, 149, 240, 243, 113, 38, 194, 92, 77, 148, 189, 181, 239, 134, 16, 73, 253, 234, 166, 34, 123, 156, 112, 214, 220, 225, 13, 217, 81, 117, 212, 229, 138, 226, 192, 114, 212, 211, 178, 106, 130, 109, 21, 116, 27 }];
        yield return ["pe9qjs3anmandtxgagg4m8pjk5qbb9kfe4d9hrdtpdq6r165jgd93utd3a43j7fpska7k8416padapm5zhghadc5no783inz1umoi1e", new byte[] { 106, 62, 228, 219, 56, 18, 240, 33, 197, 230, 193, 141, 165, 157, 169, 86, 220, 16, 253, 69, 70, 135, 254, 16, 113, 104, 221, 226, 75, 219, 73, 135, 252, 206, 35, 206, 53, 148, 244, 173, 178, 177, 213, 31, 82, 243, 112, 60, 53, 123, 191, 13, 204, 13, 155, 20, 58, 124, 212, 87, 148, 215, 10, 201 }];
        yield return ["g67khg5z1w1z5rfma1kg3ze", new byte[] { 55, 186, 174, 27, 119, 149, 37, 125, 144, 171, 196, 148, 108, 221 }];
        yield return ["cm543xmdxmzwdmwbnr4mu3wg16r55m6z8194ibqnb7qm5ph1yk79ywzw1beoqm6q31zqeb6k98ukpm4pwe91wqx1wzxpkfys8ciayj7c75qeyoo8bqba5dibgyg159tysyw7spemg1dh97ss46izqg3n5z79tghx74zdurf88tmejz74cht4ncdea5e7bkqqwrq6n", new byte[] { 98, 247, 172, 189, 99, 122, 239, 65, 174, 129, 17, 52, 185, 230, 134, 151, 137, 189, 175, 215, 60, 191, 170, 133, 194, 15, 92, 189, 183, 146, 2, 187, 240, 82, 244, 144, 81, 7, 47, 206, 204, 174, 228, 7, 202, 249, 230, 166, 175, 77, 162, 63, 42, 57, 242, 165, 222, 213, 20, 22, 59, 43, 128, 39, 172, 238, 220, 128, 66, 7, 11, 131, 141, 142, 161, 48, 13, 45, 254, 32, 176, 41, 219, 53, 11, 52, 135, 207, 246, 214, 215, 171, 119, 27, 34, 221, 251, 248, 155, 143, 238, 174, 57, 144, 167, 60, 86, 132, 223, 186, 103, 35, 161, 48, 104, 198, 209, 208, 169, 206, 161, 29, 225 }];
        yield return ["co17uca5hf5p6ue7qzwwn846qukdmiu6h9tuujgfuh31ka1jnr6dr9qs3ko18u93fxtbomrhudfjyidsurxd5qnq9o6mg8ogesxprdwqsuznj6j4ccn5d5zzdq61a", new byte[] { 100, 37, 217, 179, 27, 225, 118, 223, 77, 29, 117, 233, 65, 31, 94, 116, 212, 53, 214, 126, 231, 227, 57, 164, 197, 159, 51, 37, 98, 73, 17, 60, 50, 125, 214, 202, 161, 35, 207, 249, 43, 226, 24, 44, 156, 152, 202, 144, 84, 118, 153, 30, 61, 184, 78, 252, 60, 179, 30, 6, 69, 158, 210, 14, 142, 180, 238, 36, 249, 58, 99, 5, 177, 238, 247, 27, 189, 44 }];
        yield return ["mnxantjpxxseaam9phc4fwn7rdtdwjxde5wgek5p8hppn4dhexjm6j4szwa4dcosstn5igxycts5h4kj", new byte[] { 88, 159, 129, 69, 45, 123, 236, 140, 97, 127, 111, 25, 162, 208, 93, 32, 226, 58, 37, 227, 70, 232, 100, 43, 109, 63, 26, 209, 104, 124, 67, 210, 191, 39, 86, 189, 49, 161, 178, 22, 180, 69, 186, 153, 224, 100, 109, 190, 105, 73 }];
        yield return ["po5be11rk6ewuyshfag9gupzg7qyhranjmxj1es8rf1jjx1jwzkphpw5smcrebz979oup1hzf66auix4iymbdtjgmcxkhji5d5pfko1j6ki34kxnq7w8mqx3oo6yusfkxwdzgk6i51xa41kxdw4yred5wonnshiiz9okrfbkjda3wtms1aq8t5zmuw", new byte[] { 108, 54, 20, 74, 68, 87, 145, 73, 130, 220, 46, 13, 243, 77, 183, 55, 92, 14, 19, 2, 74, 222, 153, 34, 199, 33, 100, 148, 190, 73, 165, 212, 222, 54, 155, 178, 216, 68, 6, 255, 239, 225, 54, 203, 151, 47, 189, 137, 213, 250, 168, 22, 17, 197, 38, 91, 30, 174, 38, 187, 30, 218, 85, 66, 73, 242, 171, 157, 41, 226, 119, 104, 117, 185, 249, 132, 60, 9, 216, 170, 125, 7, 115, 43, 213, 220, 159, 141, 73, 79, 29, 52, 2, 32, 123, 164, 4, 43, 114, 181, 191, 224, 162, 20, 42, 72, 241, 154, 69, 118, 150, 28, 120, 238, 235, 157 }];
        yield return ["hp9gusdw7h9e3buj87nrmtp5nopje9zy78nzrdg4tuso", new byte[] { 227, 126, 105, 216, 116, 239, 62, 140, 134, 105, 63, 68, 69, 197, 187, 20, 26, 148, 126, 224, 233, 197, 114, 12, 218, 140, 237 }];
        yield return ["hm7wg", new byte[] { 226, 251, 67 }];
        yield return ["4oww86tyhyhwbtsg8ct84memoido1urr38ricji87cky6jjb6j6b8jwgre3twdp5qxqu58udogfbimy", new byte[] { 212, 41, 67, 250, 32, 224, 57, 64, 198, 198, 59, 34, 125, 45, 11, 133, 71, 9, 76, 132, 201, 201, 86, 38, 167, 235, 20, 15, 37, 33, 242, 124, 19, 166, 134, 34, 51, 26, 13, 187, 115, 221, 61, 158, 99, 129, 138, 26, 172 }];
        yield return ["7j5xomoxxfa9qht38a4q79xxihbiqc61s7mtqmxxk9qp15npbedi3if1duce871ybtje8s1xqcjjnsbr4outgnnuujgf86gn", new byte[] { 234, 118, 248, 46, 15, 121, 113, 247, 114, 57, 62, 52, 238, 253, 239, 175, 3, 87, 51, 210, 183, 87, 23, 45, 239, 87, 220, 217, 108, 77, 10, 7, 92, 212, 178, 28, 216, 131, 246, 64, 12, 82, 131, 218, 79, 115, 18, 145, 88, 36, 212, 39, 19, 8, 83, 154, 76, 83, 248, 194 }];
        yield return ["xb515ecb13dm95z4ocqjx58a4k89m96b1bwo4t6srqbpzpai1kmkhnx5f6sfoet588zya1mqoocbsmqetobwbhqw5hujc9ip7ynfhxihybkm7z8o11dgwjyszw8n3rxdbzk5woemk4fiu96ytzzab9rimpfdejx4x7o3iwewn87rz9q94w9icfc8q5mw4g4c7a8cf4a", new byte[] { 120, 119, 45, 161, 129, 150, 70, 191, 238, 250, 131, 28, 151, 236, 248, 210, 143, 245, 255, 193, 144, 105, 13, 71, 214, 35, 130, 219, 183, 21, 146, 150, 174, 9, 251, 47, 172, 88, 34, 59, 57, 238, 12, 73, 110, 132, 24, 27, 45, 200, 140, 3, 64, 241, 212, 223, 38, 150, 126, 173, 232, 4, 94, 62, 188, 0, 84, 190, 220, 240, 148, 134, 106, 36, 22, 189, 14, 44, 145, 227, 13, 213, 186, 65, 11, 86, 139, 89, 255, 192, 141, 239, 128, 252, 149, 91, 74, 52, 37, 250, 127, 97, 154, 209, 20, 17, 250, 75, 253, 223, 213, 63, 86, 21, 135, 118, 215, 77, 27, 76, 238, 14, 194, 235 }];
        yield return ["qs1i8qtxib6hb56dukiw9qepw5keynoexarxd4qjmnyq9yy9dcu5njf4hmkszafsjpg9pnygmwyzb7ny8mwntrknr13sbs8xp1xy463ddyxu33zduozezaqu4tyekirjco14kxj5o8yrg5jmquixse59oqph6s8shnpxia9k4ot1n5jqxh8uwjahhy5631fhcukxkmprh61k5rms85f7kggkg3qgs", new byte[] { 117, 165, 83, 186, 47, 168, 125, 192, 239, 195, 154, 171, 79, 185, 13, 166, 212, 128, 10, 8, 126, 8, 241, 233, 201, 88, 128, 239, 128, 31, 27, 39, 177, 36, 186, 226, 213, 107, 224, 182, 75, 77, 246, 136, 6, 93, 1, 112, 244, 64, 58, 232, 40, 145, 66, 36, 179, 96, 216, 239, 108, 158, 13, 123, 35, 24, 31, 60, 230, 227, 156, 46, 139, 225, 211, 212, 64, 133, 84, 137, 100, 37, 165, 61, 59, 129, 192, 67, 109, 43, 116, 234, 251, 35, 127, 131, 155, 207, 88, 246, 224, 154, 250, 227, 234, 212, 35, 33, 109, 46, 127, 15, 58, 39, 28, 224, 55, 236, 200, 188, 100, 212, 245, 45, 164, 231, 164, 173, 145, 118, 62, 203, 213, 24, 202, 54, 92, 107 }];
        yield return ["jzd1tdrkcee3ary9ybbfhxddd9fgidkonib6mgjfxr4tk8bdsqsk585wxooui7mgngxokpfa8psxz1ydqpr8fppoj65atmgndygu5kdtszk5i7qb", new byte[] { 77, 199, 40, 140, 138, 98, 17, 156, 16, 31, 0, 66, 94, 60, 99, 31, 202, 106, 141, 80, 21, 67, 229, 153, 37, 121, 53, 21, 28, 35, 179, 172, 173, 159, 116, 124, 33, 58, 245, 102, 17, 159, 5, 52, 184, 59, 108, 251, 200, 3, 115, 72, 114, 181, 176, 79, 183, 136, 172, 194, 24, 13, 61, 168, 113, 181, 213, 186, 245, 193 }];
        yield return ["tcwyzcs4g7cua3oke13bnnbawubw4e7oxs1h9mbbejxf66sqpua7snjzznrzkpo", new byte[] { 139, 40, 11, 178, 218, 55, 89, 60, 102, 10, 68, 178, 17, 8, 56, 164, 195, 77, 35, 176, 125, 165, 207, 172, 33, 66, 94, 95, 122, 206, 108, 241, 219, 9, 55, 184, 137, 117, 54 }];
        yield return ["h9grxc53hu7nnnhhqd1wkk4r8zhkg", new byte[] { 231, 204, 71, 179, 121, 228, 250, 33, 11, 156, 112, 229, 69, 43, 68, 61, 248, 163 }];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}