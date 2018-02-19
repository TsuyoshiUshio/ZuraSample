using System;
using System.Reflection;

namespace AttributesSample
{
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method,
        AllowMultiple = true,
        Inherited = false)]
    public class AuthorAttribute : Attribute
    {
        private string name;
        public AuthorAttribute(string name) { this.name = name; }
        public string Name { get { return this.name;  } }
    }

    [Author("Tsuyoshi Ushio")]
    [Author("Kanio Dimitrov")]
    class AuthorTest
    {
        [Author("Jimmy Page")]
        public static void A() { }
        [Author("Robert Plant")]
        public static void B() { }
        [Author("Jhon Paul Jones")]
        public static void C() { }
        [Author("Jhon Bohnam")]
        public static void D() { }
    }

    class AttributeTest
    {
        static void Main()
        {
            GetAllAuthors(typeof(AuthorTest));
            Console.ReadLine();
        }

        static void GetAllAuthors(Type t)
        {
            Console.Write("type name: {0}\n", t.Name);
            GetAuthors(t);

            foreach(MethodInfo info in t.GetMethods())
            {
                Console.Write(" method name: {0}\n", info.Name);
                GetAuthors(info);
            }
        }

        static void GetAuthors(MemberInfo info)
        {
            Attribute[] authors = Attribute.GetCustomAttributes(
                info, typeof(AuthorAttribute));
            foreach(Attribute att in authors)
            {
                AuthorAttribute author = att as AuthorAttribute;
                if (author != null)
                {
                    Console.Write("  author name: {0}\n", author.Name);

                }
            }
        }

    }

}
