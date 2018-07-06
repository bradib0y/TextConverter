using FertilityModelNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.ViewModels;

namespace WebUI
{
    public class Logic
    {
        public void Statistics(ref IndexViewModel indexViewModel) {
            string[] countries = indexViewModel.fertilityModels.Select(fm => fm.Id).ToArray();
            List<double> countryIndicesList = new List<double>();
            double counter = 0;
            foreach (string country in countries) {
                counter++;
                countryIndicesList.Add(counter);
            }
            double[] indices = countryIndicesList.ToArray();
            double[] fertilities = indexViewModel.fertilityModels.Select(fm => Convert.ToDouble(fm.Fertility2016)).ToArray();
            double[] iqs = indexViewModel.fertilityModels.Select(fm => Convert.ToDouble(fm.Iq)).ToArray();

            // linear regression of (x: countryIndex, y: Fertilty2016)
            double r, rsquared, yintercept, slope;
            LinearRegression(indices, fertilities,
                                     0, indices.Length, out r,
                                    out rsquared, out yintercept,
                                    out slope);
            indexViewModel.Statistics.regressionFertility = RegressionSeriesFromModel(indices, yintercept, slope);
            // linear regression of (x: countryIndex, y: Iq)            
            LinearRegression(indices, iqs,
                                     0, indices.Length, out r,
                                    out rsquared, out yintercept,
                                    out slope);
            indexViewModel.Statistics.regressionIq = RegressionSeriesFromModel(indices, yintercept, slope);
            // correlation of (x: fertility , y: Iq)
            LinearRegression(fertilities, iqs,
                         0, indices.Length, out r,
                        out rsquared, out yintercept,
                        out slope);
            indexViewModel.Statistics.r = r;
            indexViewModel.Statistics.rSquared = rsquared;
        }

        private IEnumerable<double> RegressionSeriesFromModel(double[] xVals, double yIntercept, double slope) {
            List<double> result = new List<double>();
            foreach (double x in xVals) {
                result.Add(yIntercept + x * slope);
            }
            return result;
        }

        public void Search(ref IndexViewModel indexViewModel) {
            SearchParameters searchParameters = indexViewModel.SearchParameters;
            // filter by country
            if (!String.IsNullOrEmpty(searchParameters.searchCountry)) {
                indexViewModel.fertilityModels = indexViewModel.fertilityModels.Where(fm => fm.Id.ToLower().Contains(searchParameters.searchCountry.ToLower())).ToList();
            }
            // filter by fertility, minimum
            if (searchParameters.searchFertilityMin != null) {
                indexViewModel.fertilityModels = indexViewModel.fertilityModels.Where(fm => fm.Fertility2016 >= searchParameters.searchFertilityMin).ToList();
            }
            // filter by fertility, max
            if (searchParameters.searchFertilityMax != null)
            {
                indexViewModel.fertilityModels = indexViewModel.fertilityModels.Where(fm => fm.Fertility2016 <= searchParameters.searchFertilityMax).ToList();
            }
            // filter by IQ, minimum
            if (searchParameters.searchIqMin != null)
            {
                indexViewModel.fertilityModels = indexViewModel.fertilityModels.Where(fm => fm.Iq >= searchParameters.searchIqMin).ToList();
            }
            // filter by IQ, max
            if (searchParameters.searchIqMax != null)
            {
                indexViewModel.fertilityModels = indexViewModel.fertilityModels.Where(fm => fm.Iq <= searchParameters.searchIqMax).ToList();
            }
            // order records by fertility or by IQ
            if (searchParameters.orderByFertility)
            {
                indexViewModel.fertilityModels = indexViewModel.fertilityModels.OrderBy(fm => fm.Fertility2016).ThenByDescending(fm => fm.Iq).ToList();
            }
            else {
                indexViewModel.fertilityModels = indexViewModel.fertilityModels.OrderBy(fm => fm.Iq).ThenByDescending(fm => fm.Fertility2016).ToList();
            }            
        }
        public void LinearRegression(double[] xVals, double[] yVals,
                                    int inclusiveStart, int exclusiveEnd, out double r, 
                                    out double rsquared, out double yintercept,
                                    out double slope)
        {
            
            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfXSq = 0;
            double sumOfYSq = 0;
            double ssX = 0;
            double ssY = 0;
            double sumCodeviates = 0;
            double sCo = 0;
            double count = exclusiveEnd - inclusiveStart;

            for (int ctr = inclusiveStart; ctr < exclusiveEnd; ctr++)
            {
                double x = xVals[ctr];
                double y = yVals[ctr];
                sumCodeviates += x * y;
                sumOfX += x;
                sumOfY += y;
                sumOfXSq += x * x;
                sumOfYSq += y * y;
            }
            ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
            ssY = sumOfYSq - ((sumOfY * sumOfY) / count);
            double RNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
            double RDenom = (count * sumOfXSq - (sumOfX * sumOfX))
             * (count * sumOfYSq - (sumOfY * sumOfY));
            sCo = sumCodeviates - ((sumOfX * sumOfY) / count);

            double meanX = sumOfX / count;
            double meanY = sumOfY / count;
            double dblR = RNumerator / Math.Sqrt(RDenom);
            r = dblR;
            rsquared = dblR * dblR;
            yintercept = meanY - ((sCo / ssX) * meanX);
            slope = sCo / ssX;
        }
    }
}