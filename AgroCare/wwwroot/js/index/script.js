// start header
let navbar = document.querySelector('.navbar');
window.onscroll = () => {

  if (window.scrollY >= 800) {
    navbar.classList.add('background');
  }
  else {
    navbar.classList.remove('background');
  }

};

// document.addEventListener("DO MContentLoaded", function () { 

//   var element = document.getElementById("s");
//   element.addEventListener("mousedown", function () {
//     this.style.borderColor = "green"
//   });
//    element.addEventListener("mouseup", function () {
//     this.style.borderColor = "green"
//   });
// })