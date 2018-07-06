//var testdata = [{ "Id": "Congo Democratic Republic", "Fertility1960": 6, "Fertility2016": 6.1, "Iq": 78 }, { "Id": "Nepal", "Fertility1960": 6, "Fertility2016": 2.1, "Iq": 78 }, { "Id": "Qatar", "Fertility1960": 7, "Fertility2016": 1.9, "Iq": 78 }, { "Id": "Guatemala", "Fertility1960": 6.9, "Fertility2016": 3, "Iq": 79 }, { "Id": "Sri Lanka", "Fertility1960": 5.5, "Fertility2016": 2, "Iq": 79 }, { "Id": "Zambia", "Fertility1960": 7.1, "Fertility2016": 5, "Iq": 79 }, { "Id": "Barbados", "Fertility1960": 4.3, "Fertility2016": 1.8, "Iq": 80 }];
var fertilityChartData = viewModel.fertilityModels;
var filteredData = [];

var fertilityRegressionArray = viewModel.Statistics.regressionFertility;
var filteredFertilityRegressionArray = [];

var iqRegressionArray = viewModel.Statistics.regressionIq;
var filteredIqRegressionArray = [];

fertilityChartData.forEach(
    (item, index) => {
        if (item["Fertility2016"] !== null && item["Iq"] !== null) {
            filteredData.push(item);
            filteredFertilityRegressionArray.push(fertilityRegressionArray[index]);
            filteredIqRegressionArray.push(iqRegressionArray[index]);
        }
    }
);

let labelArray = [];
let labelString = "";
filteredData.forEach(
        (item, index) => {
            labelArray.push(item["Id"]);
            labelString += item["Id"] + ",";
        }
);
console.log("LABELS: " + labelArray.length);
console.log(labelString);

let FertilityArray = [];
let FertilityString = "";
filteredData.forEach(
        (item, index) => {
            FertilityArray.push(item["Fertility2016"]);
            FertilityString += item["Fertility2016"] + ",";
        }
);
console.log("FERTS: " + FertilityArray.length);
console.log(FertilityString);

let IqArray = [];
let IqString = "";
filteredData.forEach(
        (item, index) => {
            IqArray.push(item["Iq"]);
            IqString += item["Iq"] + ",";
        }
);
console.log("IQs: " + IqArray.length);
console.log(IqArray.toString());



let fertilityChartElement = document.getElementById('fertilityChart').getContext('2d');
let FertilityChartObject = new Chart(fertilityChartElement, {
    type: 'line',
    data: {
        labels: labelArray,
        datasets: [
            {
                label: 'Fertility, 2016',
                borderColor: 'rgba(0, 255, 153, 0.25)',
                backgroundColor: 'rgba(0, 255, 153, 0.25)',
                fill: false,
                data: FertilityArray,
                yAxisID: 'FertilityAxis'
            },
            {
                label: 'Fertility, linear regression',
                borderColor: 'rgba(0, 255, 153, 0.6)',
                backgroundColor: 'rgba(0, 255, 153, 0.6)',
                fill: false,
                data: filteredFertilityRegressionArray,
                yAxisID: 'FertilityAxis'
            },
                
            {
                label: 'Average Iq',
                borderColor: 'rgba(153, 51, 255, 0.25)',
                backgroundColor: 'rgba(153, 51, 255, 0.25)',
                fill: false,
                data: IqArray,
                yAxisID: 'IqAxis'
            },
            {
                label: 'Average Iq, linear regression',
                borderColor: 'rgba(153, 51, 255, 0.6)',
                backgroundColor: 'rgba(153, 51, 255, 0.6)',
                fill: false,
                data: filteredIqRegressionArray,
                yAxisID: 'IqAxis'
            }
        ]
    },
    options: {
        scales: {
            yAxes: [{
                id: 'FertilityAxis',
                type: 'linear',
                position: 'left',
                autoskip: true
            }, {
                id: 'IqAxis',
                type: 'linear',
                position: 'right',
                autoskip: false
            }]
        }
    }

});

