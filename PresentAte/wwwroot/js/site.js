// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.addEventListener('DOMContentLoaded', event => {
    const listHoursArray = document.body.querySelectorAll('.list-hours li');
    const todayIndex = new Date().getDay();
    if (listHoursArray[todayIndex]) {
        listHoursArray[todayIndex].classList.add('today');
    }
});
