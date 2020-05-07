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