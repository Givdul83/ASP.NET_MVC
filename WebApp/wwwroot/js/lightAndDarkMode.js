
var isDarkMode = localStorage.getItem("darkMode") ==="true" ? true : false;
var logoElement = document.getElementById("logo");
var logoImage = "/images/full-siliconlogo.svg";
logoElement.innerHTML = "<img src='" + logoImage + "' alt='Silicon Logo'>";
var notFoundElement = document.getElementById("notFoundId");
var notFoundImage = isDarkMode ? "/images/404-Dark.svg" : "/images/404.svg";
var desktopSwitch = document.getElementById("switch");
var mobileSwitch = document.getElementById("switch-mobile");
desktopSwitch.checked = isDarkMode;
mobileSwitch.checked = isDarkMode;


function toggleBothSwitches() {
    isDarkMode = !isDarkMode;
    localStorage.setItem("darkMode", isDarkMode);
   
   

    if (isDarkMode) {
        activateDarkMode();
    } else {
        deactivateDarkMode();
    }

    updateLogo();
    update404();
}


function activateDarkMode() {
    document.body.classList.add("dark-mode");
}

function deactivateDarkMode() {
    document.body.classList.remove("dark-mode");
}


function updateLogo() {
    logoImage = isDarkMode ? "/images/fullDark-Siliconlogo.svg" : "/images/full-siliconlogo.svg";
    logoElement.innerHTML = "<img src='" + logoImage + "' alt='Silicon Logo'>";
}

function update404()
{
    notFoundImage = isDarkMode ? "/images/404-Dark.svg" : "/images/404.svg";
    notFoundElement.innerHTML = "<img src='" + notFoundImage + "' alt='Page not found image'>";
}

window.onload = function () {
    if (isDarkMode) {
        activateDarkMode();
    }
    else {
        deactivateDarkMode();
    }
    updateLogo();
    update404();
}