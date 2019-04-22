using System.Reflection;

namespace TestsRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Assembly.GetExecutingAssembly().Location;
        }
    }
}
