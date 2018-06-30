using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityModelNamespace;

namespace SecondPhase
{
    class Program

    {
        static void Main(string[] args)
        {
            Console.WriteLine("Projects original Main function disabled to protect database from being overwritten.");
            Console.ReadKey();
        }

            static void DisabledMain(string[] args)
        {
            try
            {
                //StreamReader srFertility = new StreamReader(@"_fertility.csv", Encoding.UTF8);
                //StreamReader srIq = new StreamReader(@"_iq.csv", Encoding.UTF8);

                List<string> rowListFertility = RowListFromFile(new StreamReader(@"xfe.csv", Encoding.UTF8));
                List<string> rowListIq = RowListFromFile(new StreamReader(@"xiq.csv", Encoding.UTF8));

                Repository repository = new Repository();

                foreach (string row in rowListFertility) {
                    string[] data = row.Split(',');
                    FertilityModel fertilityModel = new FertilityModel(data[0], ConvertOrNull(data[1]), ConvertOrNull(data[2]), null);
                    repository.AddData(fertilityModel);
                }

                foreach (string row in rowListIq)
                {
                    string[] data = row.Split(',');
                    FertilityModel fertilityModel = new FertilityModel(data[0], null, null, ConvertOrNull(data[1]));
                    repository.AddData(fertilityModel);
                }

                Console.WriteLine("Ultimate success. Check database");
            }
            catch { Console.WriteLine("Something went wrong, Boss..."); }
            finally
            {
                Console.ReadKey();
            }
        }

        static List<string> RowListFromFile(StreamReader streamReader)
        {
            
            List<string> outputRows = new List<string>();
            while (!streamReader.EndOfStream)
            {
                outputRows.Add(streamReader.ReadLine());
            }
            streamReader.Close();
            return outputRows;
        }

        static double? ConvertOrNull(string input) {
            if (!input.Equals("")) { return Convert.ToDouble(input); }
            else { return null; }
        }
    }
}
