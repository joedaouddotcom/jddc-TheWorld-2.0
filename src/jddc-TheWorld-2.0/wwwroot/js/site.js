// site.js

var ele = document.getElementById('userName');
ele.innerHTML = "Joe Dizzy Daoud"

var main = document.getElementById('main');
main.onmouseenter = function () {
    main.style.backgroundColor = "#888";
};

main.onmouseleave = function () {
    main.style.backgroundColor = "";
};