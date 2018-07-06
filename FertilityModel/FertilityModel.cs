using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityModelNamespace
{
    public class FertilityModel
    {
        [StringLength(200)]
        [Required]
        [Display(AutoGenerateField = true, AutoGenerateFilter = true)]        
        public string Id { get; set; }
        public double? Fertility1960 { get; set; }
        public double? Fertility2016 { get; set; }
        public double? Iq { get; set; }

        public FertilityModel(string id, double? fertility1960, double? fertility2016, double? iq)
        {
            Id = id;
            Fertility1960 = fertility1960;
            Fertility2016 = fertility2016;
            Iq = iq;
        }

        public FertilityModel() { }
    }
}
