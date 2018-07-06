using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityModelNamespace;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;


namespace FertilityModelNamespace
{
    public class Repository : IDisposable
    {
        Context _context = new Context();

        public Repository()
        {
            _context = new Context();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<FertilityModel> GetFertilityModels() {
            List<FertilityModel> fertilityModels = _context.FertilityModels.ToList();
            return fertilityModels;
        }

        public FertilityModel GetFertilityModel(string Id)
        {
            FertilityModel fertilityModel = _context.FertilityModels.SingleOrDefault(fm => fm.Id == Id);
            return fertilityModel;
        }


        public void Edit(FertilityModel fertilityModel)
        {
            FertilityModel fertilityModelInDb = _context.FertilityModels.Single(fm => fm.Id == fertilityModel.Id);

            //  fertilityModelInDb.Id = fertilityModel.Id ?? fertilityModelInDb.Id;
            fertilityModelInDb.Fertility1960 = fertilityModel.Fertility1960 ?? fertilityModelInDb.Fertility1960;
            fertilityModelInDb.Fertility2016 = fertilityModel.Fertility2016 ?? fertilityModelInDb.Fertility2016;
            fertilityModelInDb.Iq = fertilityModel.Iq ?? fertilityModelInDb.Iq;

            _context.SaveChanges();
        }

        public void AddNew(FertilityModel fertilityModel)
        {
            _context.FertilityModels.Add(fertilityModel);
            _context.SaveChanges();
        }

        public void Delete(FertilityModel fertilityModel)
        {
            FertilityModel fertilityModelInDb = _context.FertilityModels.Single(fm => fm.Id == fertilityModel.Id);
            _context.FertilityModels.Remove(fertilityModelInDb);

            _context.SaveChanges();
        }

        //public void AddData(FertilityModel fertilityModel)
        //{
        //    FertilityModel fertilityModelInDb;
        //    try
        //    {
        //        fertilityModelInDb = _context.FertilityModels.Single(fm => fm.Id == fertilityModel.Id);

        //        fertilityModelInDb.Fertility1960 = fertilityModel.Fertility1960 ?? fertilityModelInDb.Fertility1960;
        //        fertilityModelInDb.Fertility2016 = fertilityModel.Fertility2016 ?? fertilityModelInDb.Fertility2016;
        //        fertilityModelInDb.Iq = fertilityModel.Iq ?? fertilityModelInDb.Iq;
        //        Console.Write("*** Updated " + fertilityModel.Id + " !!! ");
        //    }
        //    catch { _context.FertilityModels.Add(fertilityModel); Console.Write("*** Added " + fertilityModel.Id + " !!! "); }
        //    finally { _context.SaveChanges(); }
        //}

        public void SaveBackupToFile() {
            List<FertilityModel> fertilityModels = GetFertilityModels();
            StreamWriter streamWriter;
#if DEBUG
            streamWriter = new StreamWriter(@"C:\Users\bradib0y\source\repos\TextConverter\FertilityModel\BACKUP\backup" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv", false, Encoding.UTF8);
#endif
#if (!DEBUG)
            StreamWriter streamWriter = new StreamWriter(@"backup"+ DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv", false, Encoding.UTF8);
#endif

            foreach (FertilityModel fertilityModel in fertilityModels)
            {
                streamWriter.WriteLine(fertilityModel.Id + ";" 
                    + fertilityModel.Fertility1960 + ";"
                    + fertilityModel.Fertility2016 + ";"
                    + fertilityModel.Iq);
                
            }
            
            streamWriter.Close();
        }
    }
}
