document.getElementById("nav-toggle").addEventListener("click", function () {
    const navMenu = document.getElementById("nav-menu");
    const navToggle = document.getElementById("nav-toggle");
    
    navMenu.classList.toggle("show");
    navToggle.classList.toggle("hidden"); // Add or remove the "hidden" class
});