const strEndpoint = "https://localhost:7264/weather";
let strResponse;

let elemTable;

window.onload = (event) => {

    elemTable = document.getElementById('tblWeather');

    // Get response from backend
    fetch(strEndpoint)
        .then(res => res.json())
        .then((out) => {
            strResponse = out;
            fnDisplay();
        }).catch(err => console.error(err));
};


function fnDisplay() {

    // Clear tbl
    var tableHeaderRowCount = 1;
    var rowCount = elemTable.rows.length;
    
    for (var i = tableHeaderRowCount; i < rowCount; i++) {
        elemTable.deleteRow(tableHeaderRowCount);
    }


    // Da Loop
    var iCurrent = 0;

    for (var i = 0; i < strResponse.list.length; i++) {

            iCurrent = iCurrent + 1;
            var row = elemTable.insertRow(iCurrent);
            row.insertCell(0).innerHTML = strResponse.list[i].dt_txt;

            //row.insertCell(1).innerHTML = '';
            //row.insertCell(2).innerHTML = '';
            //row.insertCell(3).innerHTML = '';
            //row.insertCell(4).innerHTML = '';

            row.insertCell(1).innerHTML = strResponse.list[i].main.temp_min + ' / ' + strResponse.list[i].main.temp_max;
            row.insertCell(2).innerHTML = GetIconUrl(strResponse.list[i].weather[0].icon, strResponse.list[i].weather[0].description);
    }
}


function GetIconUrl(strIcon, strTitle) {
    return '<img src="https://openweathermap.org/img/wn/' + strIcon + '.png" title="' + strTitle + '"/>';
}
