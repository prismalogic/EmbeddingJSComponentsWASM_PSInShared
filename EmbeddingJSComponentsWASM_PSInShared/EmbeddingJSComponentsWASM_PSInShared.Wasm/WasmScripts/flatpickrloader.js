(function () {
    const head = document.getElementsByTagName("head")[0];

    // Load Flatpickr CSS from CDN
    const link = document.createElement("link");
    link.rel = "stylesheet";
    link.href = "https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css";
    head.appendChild(link);
})();