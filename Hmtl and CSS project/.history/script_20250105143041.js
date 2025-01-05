document.getElementById("nav-toggle").addEventListener("click", function () {
    const navMenu = document.getElementById("nav-menu");
    const navToggle = document.getElementById("nav-toggle");
    
    navMenu.classList.toggle("show");
    navToggle.classList.toggle("hidden");
});
// Close the navigation menu
document.getElementById("close-btn").addEventListener("click", function () {
    const navMenu = document.getElementById("nav-menu");
    const navToggle = document.getElementById("nav-toggle");

    // Hide the menu
    navMenu.classList.remove("show");
    
    // Show the toggle button and change its text back to "Click me!"
    navToggle.classList.remove("hidden");
    navToggle.textContent = "Click me!"; // Reset the text to "Click me!"
});