// Ensure the DOM content is fully loaded before executing JavaScript
window.onload = function () {
    // Define the feedbackData variable
    var feedbackData = [];

    // Function to draw the tape graph using feedbackData
    function drawTapeGraph() {
        // Your code to draw the tape graph using the feedback data

        // Log feedbackData to the console to inspect its structure and contents
        console.log(feedbackData);

        // Ensure Chart.js is loaded before executing this code
        if (feedbackData) {
            // Prepare data for Chart.js
            var labels = Object.keys(feedbackData);
            var data = Object.values(feedbackData);

            // Render tape graph using Chart.js
            var ctx = document.getElementById('feedbackTapeGraph').getContext('2d');
            var feedbackChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Feedback Ratings',
                        data: data,
                        backgroundColor: 'rgba(54, 162, 235, 0.5)',
                        borderColor: 'rgba(54, 162, 235, 1)',
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true // Ensure the y-axis starts at 0
                            }
                        }]
                    }
                }
            });
        }
    }

    // Call the drawTapeGraph function when the page loads
    drawTapeGraph();
};
