// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function dropNormalSelectFunction() {
    normal_select = document.getElementById('normal');
    normal_select.options[0].selected = true;
}

function dropHardSelectFunction() {
    hard_select = document.getElementById('hard');
    hard_select.options[0].selected = true;
}

function setInputFilter(textbox, inputFilter) {
    ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function(event) {
      textbox.addEventListener(event, function() {
        if (inputFilter(this.value)) {
          this.oldValue = this.value;
          this.oldSelectionStart = this.selectionStart;
          this.oldSelectionEnd = this.selectionEnd;
        } else if (this.hasOwnProperty("oldValue")) {
          this.value = this.oldValue;
          this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
        }
      });
    });
  }
  
  setInputFilter(document.getElementById("power"), function(value) {
    return /^-?\d*[.,]?\d*$/.test(value);
  });

  setInputFilter(document.getElementById("current"), function(value) {
    return /^-?\d*[.,]?\d*$/.test(value);
  });

  setInputFilter(document.getElementById("speed"), function(value) {
    return /^-?\d*[.,]?\d*$/.test(value);
  });

  setInputFilter(document.getElementById("cos"), function(value) {
    return /^-?\d*[.,]?\d*$/.test(value);
  });