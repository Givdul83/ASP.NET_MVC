//var isDarkMode = false;
//var logoElement = document.getElementById("logo");
//var logoImage = "images/full-siliconlogo.svg";
//logoElement.innerHTML = "<img src='"+logoImage + "' alt='Silicon Logo'>";

//function toggleBothSwitches() {
//    isDarkMode = !isDarkMode;

//    var desktopSwitch = document.getElementById("switch");
//    desktopSwitch.checked = isDarkMode;

//    var mobileSwitch = document.getElementById("switch-mobile");
//    mobileSwitch.checked = isDarkMode;

//    if (isDarkMode) {
//        activateDarkMode();
//    } else {
//        deactivateDarkMode();
//    }

//    updateLogo(); 
//}

//function activateDarkMode() {
//    document.body.classList.add("dark-mode");
//}

//function deactivateDarkMode() {
//    document.body.classList.remove("dark-mode");
//}

//function updateLogo() {
//    logoImage = isDarkMode ? "images/fullDark-Siliconlogo.svg" : "images/full-siliconlogo.svg";
//    logoElement.innerHTML = "<img src='" + logoImage + "' alt='Silicon Logo'>";
//}

var isDarkMode = false;
var logoElement = document.getElementById("logo");
var logoImage = "/images/full-siliconlogo.svg";
logoElement.innerHTML = "<img src='" + logoImage + "' alt='Silicon Logo'>";

function toggleBothSwitches() {
    isDarkMode = !isDarkMode;
    var desktopSwitch = document.getElementById("switch");
    desktopSwitch.checked = isDarkMode;


    var mobileSwitch = document.getElementById("switch-mobile");
    mobileSwitch.checked = isDarkMode;
    if (isDarkMode) {
        activateDarkMode();
    } else {
        deactivateDarkMode();
    }

    updateLogo();
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