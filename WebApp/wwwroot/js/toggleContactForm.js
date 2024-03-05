document.addEventListener("DOMContentLoaded", function () {
    var showFormBtn = document.getElementById("toggleFormBtn");
    var formBox = document.querySelector(".form-box");

    showFormBtn.addEventListener("click", function () {


        formBox.classList.toggle('hide')

    });
});