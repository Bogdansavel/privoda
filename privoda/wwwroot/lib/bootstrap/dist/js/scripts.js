function myFunction() {
    var moreText = document.getElementById("more");
    var btnText = document.getElementById("myBtn");
  
    if (moreText.style.display === "inline") {
        btnText.innerHTML = "Подробнее";
        moreText.style.display = "none";
    } else {
      btnText.innerHTML = "Короче";
      moreText.style.display = "inline";
    }
  }