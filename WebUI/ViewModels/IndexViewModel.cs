using FertilityModelNamespace;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.ViewModels
{
    public class IndexViewModel
    {        
        public SearchParameters SearchParameters { get; set; }
        public IEnumerable<FertilityModel> fertilityModels { get; set; }
        public Statistics Statistics { get; set; }

        public IndexViewModel()
        {
            this.SearchParameters = new SearchParameters();
            this.Statistics = new Statistics();
        }
    }

    public class SearchParameters {
        // searchparameters
        // order by: radio button (Fert or IQ)
        [Display(AutoGenerateField = true, AutoGenerateFilter = true, Name = "Order by")]
        public bool orderByFertility { get; set; }
        // search for country
        public string searchCountry { get; set; }
        // search for range (FERT OR IQ)
        [Display(Name = "Search by fertility from (minimum value)", GroupName = "Search by fertility")]
        public double? searchFertilityMin { get; set; }
        [Display(Name = "Search by fertility to (maximum value)", GroupName = "Search by fertility")]
        public double? searchFertilityMax { get; set; }



        [Display(Name = "Search by IQ from (minimum value)", GroupName = "Search by IQ")]
        public double? searchIqMin { get; set; }
        [Display(Name = "Search by IQ to (maximum value)", GroupName = "Search by IQ")]
        public double? searchIqMax { get; set; }

        public SearchParameters()
        {
            this.orderByFertility = true;            
        }
    }

    public class Statistics {
        // statistics
        // filtered dataset is used
        // linear regression data collection (Fert AND IQ)
        public IEnumerable<double> regressionFertility { get; set; }
        public IEnumerable<double> regressionIq { get; set; }
        // stat measures
        public double r { get; set; }
        public double rSquared { get; set; }

        public Statistics()
        {
            this.regressionFertility = new List<double>();
            this.regressionIq = new List<double>();
            this.r = 0;
            this.rSquared = 0;
        }
    }
}