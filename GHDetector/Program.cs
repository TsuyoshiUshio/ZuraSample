using System;
using System.Reflection;

namespace GHDetector
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method,
        AllowMultiple = true, Inherited = false)]
    public class GHDetectAttribute : Attribute
    {
        private string maker;
        private bool isWig;
        public GHDetectAttribute(bool isWig, string maker)
        {
            this.maker = maker;
            this.isWig = isWig;
        }
        public string Maker {  get { return this.maker; } }
        public bool IsWig { get { return this.isWig; } }
    }

    public interface Accused
    {
        void Execute();
    }

    [GHDetect(true, "アデランス")]
    public class Takeru : Accused
    {
        public void Execute()
        {
            Console.WriteLine("[Takeru] 私ははげていません。");
        }
    }
    [GHDetect(false, null)]
    public class Tsuyoshi : Accused
    {
        public void Execute()
        {
            Console.WriteLine("[Tsuyoshi] 私ははげていません。");
        }
    }


    class Program
    {

        static Attribute DetectGH(MemberInfo info)
        {
            Attribute detector = Attribute.GetCustomAttribute(
                info, typeof(GHDetectAttribute));
            return detector;
        }

        static void Main(string[] args)
        {
            Accused tsuyoshi = new Tsuyoshi();
            Execute(tsuyoshi);
            Console.ReadLine();
            Accused takeru = new Takeru();
            Execute(takeru);
            Console.ReadLine();
            
        }

        static void Execute(Accused accused)
        {
            var accusedAttribute = DetectGH(accused.GetType()) as GHDetectAttribute;
            if (accusedAttribute.IsWig)
            {
                accused.Execute();
                Console.WriteLine($"カツラが検出されました。　メーカー: {accusedAttribute.Maker}");
            } else
            {
                accused.Execute();
                Console.WriteLine("本当に地毛のようです。");
            }
        }
    }
}
