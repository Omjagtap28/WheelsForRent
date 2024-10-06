function showCarInfo(carName) {
        var carInfoContent = document.getElementById('carInfoContent');
        carInfoContent.innerHTML = "<h3>" + carName + "</h3><p>Details about " + carName + "...</p>";

        var carInfoModal = document.getElementById('carInfoModal');
        carInfoModal.style.display = 'block';
    }

    function closeCarInfoModal() {
        var carInfoModal = document.getElementById('carInfoModal');
        carInfoModal.style.display = 'none';
    }

    // Close modal if the user clicks outside the modal content
    window.onclick = function (event) {
        var carInfoModal = document.getElementById('carInfoModal');
        if (event.target === carInfoModal) {
            carInfoModal.style.display = 'none';
        }
}


//------------------------------------------- search.aspx
// Smooth scaling on hover
function smoothScaleOnHover(carItem) {
    carItem.style.transition = "transform 0.3s ease-in-out";
    carItem.style.transform = "scale(1.05)";
}

function resetScale(carItem) {
    carItem.style.transition = "transform 0.3s ease-in-out";
    carItem.style.transform = "scale(1)";
}

// Display description below the car on hover
function showDescriptionOnHover(carItem) {
    var description = carItem.getAttribute('data-description');
    var carInfoContent = document.createElement('div');
    carInfoContent.classList.add('car-description');
    carInfoContent.innerText = description;
    carItem.appendChild(carInfoContent);
}

function hideDescriptionOnLeave(carItem) {
    var carInfoContent = carItem.querySelector('.car-description');
    if (carInfoContent) {
        carInfoContent.remove();
    }
}



/* Search aspx page */
// Get the number of car items returned in the search results
var carItems = document.querySelectorAll('.car-item');
var searchForm = document.querySelector('#SearchForm');
// Check if only one car item is returned
if (carItems.length === 1 || carItems.length === 0) {
    // Set the width of the single car item to 100%
    searchForm.style.width = '100%';
}

