document.addEventListener("DOMContentLoaded", function () {
    const navToggle = document.getElementById("nav-toggle");
    const closeBtn = document.getElementById("close-btn");

    // Ensure the "Click Me!" button is visible and the "Close" button is hidden
    navToggle.classList.remove("hidden");
    closeBtn.classList.add("hidden");
});
document.getElementById("nav-toggle").addEventListener("click", function () {
    const navMenu = document.getElementById("nav-menu");
    const navToggle = document.getElementById("nav-toggle");
    const closeBtn = document.getElementById("close-btn");

    // Toggle the navigation menu visibility
    navMenu.classList.toggle("show");

    // Toggle visibility of "Click me!" and "Close" button
    navToggle.classList.toggle("hidden");
    closeBtn.classList.toggle("hidden");
});

// Close the navigation menu when "Close" button is clicked
document.getElementById("close-btn").addEventListener("click", function () {
    const navMenu = document.getElementById("nav-menu");
    const navToggle = document.getElementById("nav-toggle");
    const closeBtn = document.getElementById("close-btn");

    // Hide the navigation menu
    navMenu.classList.remove("show");

    // Show the "Click me!" button and hide the "Close" button
    navToggle.classList.remove("hidden");
    closeBtn.classList.add("hidden");
});