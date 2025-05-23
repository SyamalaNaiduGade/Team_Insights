﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    const options = document.querySelectorAll(".option");

    options.forEach(option => {
        option.addEventListener("click", function () {
            let parent = this.parentElement;
            parent.querySelectorAll(".option").forEach(opt => opt.classList.remove("selected"));
            this.classList.add("selected");
        });
    });
});


document.addEventListener("DOMContentLoaded", function () {
    const profile = document.querySelector(".user-profile");
    const btn = document.querySelector(".profile-btn");

    if (profile && btn) {
        btn.addEventListener("click", function (event) {
            event.stopPropagation(); // Prevents closing when clicking inside
            const isActive = profile.classList.toggle("active");

            // Toggle button style as well
            if (isActive) {
                btn.classList.add("active");
            } else {
                btn.classList.remove("active");
            }
        });

        // Close when clicking outside
        document.addEventListener("click", function (event) {
            if (!profile.contains(event.target)) {
                profile.classList.remove("active");
                btn.classList.remove("active"); // Also reset the button style
            }
        });
    }
});