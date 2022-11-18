namespace SipaaScriptInterpreter
{
    internal class Program
    {
        static bool CheckFile(string file)
        {
            if (File.Exists(file))
            {
                var fi = new FileInfo(file);
                if (fi.Extension.EndsWith("ss") || fi.Extension.EndsWith("sipaascript"))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        static void Main(string[] args)
        {
            Console.Title = "Sipaa Script Interpreter (BETA)";
            Console.Clear();
            if (args.Length == 0)
            {
                Console.Write("What file do you want parse (a .ss or a .sipaascript file) ? ");
                var f = Console.ReadLine();
                if (CheckFile(f))
                {
                    Parser.Parse(f);
                }
            }
            else
            {
                if (CheckFile(args[0]))
                {
                    Parser.Parse(args[0]);
                }
            }
        }
    }
}