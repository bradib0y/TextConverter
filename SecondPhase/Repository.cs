using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityModelNamespace;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace FertilityModelNamespace
{
    class Repository : IDisposable
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

        public void AddData(FertilityModel fertilityModel) {
            FertilityModel fertilityModelInDb;
            try
            {
                fertilityModelInDb = _context.FertilityModels.Single(fm => fm.Id == fertilityModel.Id);

                fertilityModelInDb.Fertility1960 = fertilityModel.Fertility1960 ?? fertilityModelInDb.Fertility1960;
                fertilityModelInDb.Fertility2016 = fertilityModel.Fertility2016 ?? fertilityModelInDb.Fertility2016;
                fertilityModelInDb.Iq = fertilityModel.Iq ?? fertilityModelInDb.Iq;
                Console.Write("*** Updated " + fertilityModel.Id + " !!! ");
            }
            catch { _context.FertilityModels.Add(fertilityModel); Console.Write("*** Added " + fertilityModel.Id + " !!! "); }
            finally { _context.SaveChanges(); }            
        }
    }
}
