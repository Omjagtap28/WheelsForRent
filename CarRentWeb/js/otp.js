var seconds = 120; // Set the timer duration in seconds

function startTimer() {
    var timer = setInterval(function () {
        document.getElementById("timer").innerHTML = "Time remaining: " + seconds + " seconds";
        seconds--;

        if (seconds < 0) {
            clearInterval(timer);
            // Get the CarID from the query string
            var queryString = window.location.search;
            var urlParams = new URLSearchParams(queryString);
            var carID = urlParams.get('carID');

            // Redirect to payment.aspx after 2 minutes with CarID parameter
            window.location.href = "payment.aspx?paymentStatus=failed&carID=" + carID;
        }
    }, 1000); // Update the timer every second
}

// Call the startTimer function when the page loads
window.onload = startTimer;
