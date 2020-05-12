function currentSelectOnChange() {
    index = $("select[id='Current'] option:selected").index();
    document.getElementById(
        "Power").selectedIndex = index;
}

function powerSelectOnChange() {
    index = $("select[id='Power'] option:selected").index();
    document.getElementById(
        "Current").selectedIndex = index;
}

var url_string = window.location.href;
var url = new URL(url_string);
var powerValue = url.searchParams.get("Power");
var currentValue = url.searchParams.get("Current");
var currentTypeValue = url.searchParams.get("CurrentType");
var voltageValue = url.searchParams.get("Voltage");

if(powerValue != null && currentValue != null && currentTypeValue != null && voltageValue != null){
    var powerSelect = document.getElementById("Power");
    var index = $(`select[id='Power'] option[value='${powerValue}']`).index();
    document.getElementById("Power").selectedIndex = index;

    index = $(`select[id='Current'] option[value='${currentValue}']`).index();
    document.getElementById("Current").selectedIndex = index;

    index = $(`select[id='CurrentType'] option[value='${currentTypeValue}']`).index();
    document.getElementById("CurrentType").selectedIndex = index;

    index = $(`select[id='Voltage'] option[value='${voltageValue}']`).index();
    document.getElementById("Voltage").selectedIndex = index;
}