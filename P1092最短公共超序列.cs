using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/5/14
    // rank: 1977
    // 最长公共子序列(ref from 1143)
    internal class P1092最短公共超序列
    {
        // ver4 终于AC了
        public string ShortestCommonSupersequence(string str1, string str2)
        {
            (int i, int j, int len)[,] dp = new (int i, int j, int len)[str1.Length + 1, str2.Length + 1];
            for (int i = 0; i <= str1.Length; ++i)
                for (int j = 0; j <= str2.Length; ++j)
                {
                    if (i == 0 && j == 0) dp[i, j] = new();
                    else if (i == 0) dp[i, j] = new() { len = j };
                    else if (j == 0) dp[i, j] = new() { len = i };
                    else // i > 0 && j > 0
                    {
                        dp[i, j] = new()
                        {
                            i = 1,
                            j = 1,
                            len = dp[i - 1, j - 1].len + (str1[i - 1] == str2[j - 1] ? 1 : 2)
                        };
                        if (dp[i - 1, j].len + 1 < dp[i, j].len)
                        {
                            dp[i, j].j = 0;
                            dp[i, j].len = dp[i - 1, j].len + 1;
                        }
                        if (dp[i, j - 1].len + 1 < dp[i, j].len)
                        {
                            dp[i, j].i = 0;
                            dp[i, j].len = dp[i, j - 1].len + 1;
                        }
                    }
                    //Console.WriteLine("[{0}, {1}] ({2} {3}) => {4}",
                    //    i, j,
                    //    str1.Substring(0, i + 1),
                    //    str2.Substring(0, j + 1),
                    //    dp[i, j]);
                }

            StringBuilder BuildString(StringBuilder build, int i, int j)
            {
                if (i == 0 && j == 0) return build;
                else if (i == 0) return build.Append(str2, 0, j);
                else if (j == 0) return build.Append(str1, 0, i);
                else build = BuildString(build, i - dp[i, j].i, j - dp[i, j].j);
                if (dp[i, j].i == 0) return build.Append(str2[j - 1]);
                if (dp[i, j].j == 0) return build.Append(str1[i - 1]);
                return build.Append(str1[i - 1] == str2[j - 1] ? str1[i - 1] : $"{str1[i - 1]}{str2[j - 1]}");
            }

            return BuildString(new(), str1.Length, str2.Length).ToString();
        }

        // ver3 最长公共子序列(ref from 1143)
        // 做出来了，但是OutOfMemory
        //public string ShortestCommonSupersequence(string str1, string str2)
        //{
        //    string[,] dp = new string[str1.Length + 1, str2.Length + 1];
        //    for (int i = 0; i <= str1.Length; ++i)
        //        for (int j = 0; j <= str2.Length; ++j)
        //        {
        //            if (i == 0 && j == 0) dp[i, j] = "";
        //            else if (i == 0) dp[i, j] = str2.Substring(0, j);
        //            else if (j == 0) dp[i, j] = str1.Substring(0, i);
        //            else // i > 0 && j > 0
        //            {
        //                dp[i, j] = dp[i - 1, j - 1] + (str1[i - 1] == str2[j - 1] ? str1[i - 1] : $"{str1[i - 1]}{str2[j - 1]}");
        //                if (dp[i - 1, j].Length + 1 < dp[i, j].Length) dp[i, j] = dp[i - 1, j] + str1[i - 1];
        //                if (dp[i, j - 1].Length + 1 < dp[i, j].Length) dp[i, j] = dp[i, j - 1] + str2[j - 1];
        //            }
        //            //Console.WriteLine("[{0}, {1}] ({2} {3}) => {4}",
        //            //    i, j,
        //            //    str1.Substring(0, i + 1),
        //            //    str2.Substring(0, j + 1),
        //            //    dp[i, j]);
        //        }
        //    return dp[str1.Length, str2.Length];
        //}

        // ver2 逻辑考虑未充分，应该考虑空字串的情况
        //public string ShortestCommonSupersequence(string str1, string str2)
        //{
        //    string max = str1 + str2;
        //    string[,] dp = new string[str1.Length, str2.Length];
        //    for (int i = 0; i < str1.Length; ++i)
        //        for (int j = 0; j < str2.Length; ++j)
        //        {
        //            if (i == 0 && j == 0) dp[i, j] = str1[i] == str2[j] ? $"{str1[i]}" : $"{str1[i]}{str2[j]}";
        //            else dp[i, j] = max;
        //            if (i > 0 && j > 0) dp[i, j] = dp[i - 1, j - 1] + (str1[i] == str2[j] ? str1[i] : $"{str1[i]}{str2[j]}");
        //            if (i > 0 && dp[i - 1, j].Length + 1 < dp[i, j].Length) dp[i, j] = dp[i - 1, j] + str1[i];
        //            if (j > 0 && dp[i, j - 1].Length + 1 < dp[i, j].Length) dp[i, j] = dp[i, j - 1] + str2[j];
        //        }
        //    return dp[str1.Length - 1, str2.Length - 1];
        //}

        // ver1: 理解错题意了，以为str1和str2都必须是ans的substring
        //string Kmp(string s1, string s2)
        //{
        //    Span<int> next = stackalloc int[s2.Length];
        //    next[0] = -1;
        //    for (int i = 1; i < next.Length; ++i)
        //    {
        //        int nexti = next[i - 1] + 1;
        //        while (nexti >= 0 && s2[nexti] != s2[i])
        //            nexti = nexti > 0 ? next[nexti - 1] + 1 : -1;
        //        next[i] = nexti;
        //    }
        //    int p = -1;
        //    for (int i = 0; i < s1.Length; ++i)
        //    {
        //        ++p;
        //        while (p >= 0)
        //        {
        //            if (s2[p] == s1[i]) break;
        //            if (p > 0) p = next[p - 1] + 1;
        //            else p = -1;
        //        }
        //    }
        //    return s1 + s2.Substring(p + 1);
        //}

        //public string ShortestCommonSupersequence(string str1, string str2)
        //{
        //    string s = Kmp(str1, str2), rs = Kmp(str2, str1);
        //    if (rs.Length < s.Length) s = rs;
        //    return s;
        //}

        internal static void Run()
        {
            //string str1 = "abac", str2 = "cab";

            // out of memory
            string str1 = "atdznrqfwlfbcqkezrltzyeqvqemikzgghxkzenhtapwrmrovwtpzzsyiwongllqmvptwammerobtgmkpowndejvbuwbporfyroknrjoekdgqqlgzxiisweeegxajqlradgcciavbpgqjzwtdetmtallzyukdztoxysggrqkliixnagwzmassthjecvfzmyonglocmvjnxkcwqqvgrzpsswnigjthtkuawirecfuzrbifgwolpnhcapzxwmfhvpfmqapdxgmddsdlhteugqoyepbztspgojbrmpjmwmhnldunskpvwprzrudbmtwdvgyghgprqcdgqjjbyfsujnnssfqvjhnvcotynidziswpzhkdszbblustoxwtlhkowpatbypvkmajumsxqqunlxxvfezayrolwezfzfyzmmneepwshpemynwzyunsxgjflnqmfghsvwpknqhclhrlmnrljwabwpxomwhuhffpfinhnairblcayygghzqmotwrywqayvvgohmujneqlzurxcpnwdipldofyvfdurbsoxdurlofkqnrjomszjimrxbqzyazakkizojwkuzcacnbdifesoiesmkbyffcxhqgqyhwyubtsrqarqagogrnaxuzyggknksrfdrmnoxrctntngdxxechxrsbyhtlbmzgmcqopyixdomhnmvnsafphpkdgndcscbwyhueytaeodlhlzczmpqqmnilliydwtxtpedbncvsqauopbvygqdtcwehffagxmyoalogetacehnbfxlqhklvxfzmrjqofaesvuzfczeuqegwpcmahhpzodsmpvrvkzxxtsdsxwixiraphjlqawxinlwfspdlscdswtgjpoiixbvmpzilxrnpdvigpccnngxmlzoentslzyjjpkxemyiemoluhqifyonbnizcjrlmuylezdkkztcphlmwhnkdguhelqzjgvjtrzofmtpuhifoqnokonhqtzxmimp";
            string str2 = "xjtuwbmvsdeogmnzorndhmjoqnqjnhmfueifqwleggctttilmfokpgotfykyzdhfafiervrsyuiseumzmymtvsdsowmovagekhevyqhifwevpepgmyhnagjtsciaecswebcuvxoavfgejqrxuvnhvkmolclecqsnsrjmxyokbkesaugbydfsupuqanetgunlqmundxvduqmzidatemaqmzzzfjpgmhyoktbdgpgbmjkhmfjtsxjqbfspedhzrxavhngtnuykpapwluameeqlutkyzyeffmqdsjyklmrxtioawcrvmsthbebdqqrpphncthosljfaeidboyekxezqtzlizqcvvxehrcskstshupglzgmbretpyehtavxegmbtznhpbczdjlzibnouxlxkeiedzoohoxhnhzqqaxdwetyudhyqvdhrggrszqeqkqqnunxqyyagyoptfkolieayokryidtctemtesuhbzczzvhlbbhnufjjocporuzuevofbuevuxhgexmckifntngaohfwqdakyobcooubdvypxjjxeugzdmapyamuwqtnqspsznyszhwqdqjxsmhdlkwkvlkdbjngvdmhvbllqqlcemkqxxdlldcfthjdqkyjrrjqqqpnmmelrwhtyugieuppqqtwychtpjmloxsckhzyitomjzypisxzztdwxhddvtvpleqdwamfnhhkszsfgfcdvakyqmmusdvihobdktesudmgmuaoovskvcapucntotdqxkrovzrtrrfvoczkfexwxujizcfiqflpbuuoyfuoovypstrtrxjuuecpjimbutnvqtiqvesaxrvzyxcwslttrgknbdcvvtkfqfzwudspeposxrfkkeqmdvlpazzjnywxjyaquirqpinaennweuobqvxnomuejansapnsrqivcateqngychblywxtdwntancarldwnloqyywrxrganyehkglbdeyshpodpmdchbcc";
            
            var sln = new P1092最短公共超序列();
            Console.WriteLine("ans=" + sln.ShortestCommonSupersequence(str1, str2));
        }
    }
}
