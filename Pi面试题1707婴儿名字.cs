using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/8/5
    // 面试题做一题错一题= =
    // 并查集
    internal class Pi面试题1707婴儿名字
    {
        public string[] TrulyMostPopular(string[] names, string[] synonyms)
        {
            List<string> na = new();
            Dictionary<string, int> dic = new(), idx = new();

            foreach (var s in names)
            {
                var sp = s.TrimEnd(')').Split('(');
                (string name, int cnt) = (sp[0], int.Parse(sp[1]));
                na.Add(name);
                dic[name] = cnt;
            }
            List<(string, string)> cps = new();
            foreach (var s in synonyms)
            {
                var sp = s.Substring(1, s.Length - 2).Split(',');
                foreach (var name in sp)
                    if (!dic.ContainsKey(name))
                    {
                        na.Add(name);
                        dic[name] = 0;
                    }
                cps.Add((sp[0], sp[1]));
            }
            na.Sort();
            for (int i = 0; i < na.Count; ++i) idx[na[i]] = i;
            UnionFind uni = new(na.Count);
            foreach ((string s1, string s2) in cps)
            {
                (int i1, int i2) = (idx[s1], idx[s2]);
                // 惨痛的教训啊！！
                if (uni.Find(i1) > uni.Find(i2)) (i1, i2) = (i2, i1);
                //if (i1 > i2) (i1, i2) = (i2, i1);
                uni.Union(i1, i2);
            }

            return Enumerable.Range(0, na.Count)
                .GroupBy(i => uni.Find(i))
                .Select(g => $"{na[g.Key]}({g.Sum(i => dic[na[i]])})")
                .ToArray();
        }

        internal static void Run()
        {
            string[] na = { "Fcclu(70)", "Ommjh(63)", "Dnsay(60)", "Qbmk(45)", "Unsb(26)", "Gauuk(75)", "Wzyyim(34)", "Bnea(55)", "Kri(71)", "Qnaakk(76)", "Gnplfi(68)", "Hfp(97)", "Qoi(70)", "Ijveol(46)", "Iidh(64)", "Qiy(26)", "Mcnef(59)", "Hvueqc(91)", "Obcbxb(54)", "Dhe(79)", "Jfq(26)", "Uwjsu(41)", "Wfmspz(39)", "Ebov(96)", "Ofl(72)", "Uvkdpn(71)", "Avcp(41)", "Msyr(9)", "Pgfpma(95)", "Vbp(89)", "Koaak(53)", "Qyqifg(85)", "Dwayf(97)", "Oltadg(95)", "Mwwvj(70)", "Uxf(74)", "Qvjp(6)", "Grqrg(81)", "Naf(3)", "Xjjol(62)", "Ibink(32)", "Qxabri(41)", "Ucqh(51)", "Mtz(72)", "Aeax(82)", "Kxutz(5)", "Qweye(15)", "Ard(82)", "Chycnm(4)", "Hcvcgc(97)", "Knpuq(61)", "Yeekgc(11)", "Ntfr(70)", "Lucf(62)", "Uhsg(23)", "Csh(39)", "Txixz(87)", "Kgabb(80)", "Weusps(79)", "Nuq(61)", "Drzsnw(87)", "Xxmsn(98)", "Onnev(77)", "Owh(64)", "Fpaf(46)", "Hvia(6)", "Kufa(95)", "Chhmx(66)", "Avmzs(39)", "Okwuq(96)", "Hrschk(30)", "Ffwni(67)", "Wpagta(25)", "Npilye(14)", "Axwtno(57)", "Qxkjt(31)", "Dwifi(51)", "Kasgmw(95)", "Vgxj(11)", "Nsgbth(26)", "Nzaz(51)", "Owk(87)", "Yjc(94)", "Hljt(21)", "Jvqg(47)", "Alrksy(69)", "Tlv(95)", "Acohsf(86)", "Qejo(60)", "Gbclj(20)", "Nekuam(17)", "Meutux(64)", "Tuvzkd(85)", "Fvkhz(98)", "Rngl(12)", "Gbkq(77)", "Uzgx(65)", "Ghc(15)", "Qsc(48)", "Siv(47)" },
                sa = { "(Gnplfi,Qxabri)","(Uzgx,Siv)","(Bnea,Lucf)","(Qnaakk,Msyr)","(Grqrg,Gbclj)","(Uhsg,Qejo)","(Csh,Wpagta)","(Xjjol,Lucf)","(Qoi,Obcbxb)","(Npilye,Vgxj)","(Aeax,Ghc)","(Txixz,Ffwni)","(Qweye,Qsc)","(Kri,Tuvzkd)","(Ommjh,Vbp)","(Pgfpma,Xxmsn)","(Uhsg,Csh)","(Qvjp,Kxutz)","(Qxkjt,Tlv)","(Wfmspz,Owk)","(Dwayf,Chycnm)","(Iidh,Qvjp)","(Dnsay,Rngl)","(Qweye,Tlv)","(Wzyyim,Kxutz)","(Hvueqc,Qejo)","(Tlv,Ghc)","(Hvia,Fvkhz)","(Msyr,Owk)","(Hrschk,Hljt)","(Owh,Gbclj)","(Dwifi,Uzgx)","(Iidh,Fpaf)","(Iidh,Meutux)","(Txixz,Ghc)","(Gbclj,Qsc)","(Kgabb,Tuvzkd)","(Uwjsu,Grqrg)","(Vbp,Dwayf)","(Xxmsn,Chhmx)","(Uxf,Uzgx)"};
            Console.WriteLine(string.Join("\n", new Pi面试题1707婴儿名字().TrulyMostPopular(na, sa)));

            // WA2: 太贱了，出一个关联是在计数表里不存在的
            //["a(10)","c(13)"]
            //["(a,b)","(c,d)","(b,c)"]
        }
    }
}
