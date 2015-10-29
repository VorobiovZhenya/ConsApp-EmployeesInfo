using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DZ_3
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "options.txt";
            string option = " ";           
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    option = sr.ReadToEnd().ToLower();
                    Console.WriteLine("Фрмат сериализации - " + option);
                }
            }
            catch (Exception e)
            {                
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
           if ((option == "bin")|| (option == "xml"))
           {
                new Menu().OpenBd(option);
           }
           else   Console.WriteLine("Не верный формат сериализации!");            
        }
    }
}
