using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextConverter
{
    class Program
    {
        static void Main(string[] args)

        {
            try
            {
                StreamReader streamReader = new StreamReader(@"fertility.txt", Encoding.UTF8);
                List<string> outputRows = new List<String>();

                while (!streamReader.EndOfStream)
                {

                    string outputRow = "";

                    for (int i = 0; i < 3; i++)
                    {
                        outputRow += streamReader.ReadLine();
                        outputRow += ";";
                    }

                    outputRows.Add(outputRow);

                    streamReader.ReadLine();
                }

                streamReader.Close();

                StreamWriter streamWriter = new StreamWriter(@"output.csv", false, Encoding.UTF8);

                foreach (string row in outputRows)
                {
                    streamWriter.WriteLine(row);
                    Console.WriteLine(row);
                }

                streamWriter.Close();
                Console.WriteLine("Ultimate success. Check output.csv");
            }
            catch { Console.WriteLine("File not found, Boss..."); }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
