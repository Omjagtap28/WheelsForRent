// ... your existing script ...
var backgroundImages = [
    'Bg/car4.jpg',
    'Bg/car5.jpg',
    'Bg/car1.jpg',
    'Bg/car3.jpg',
    'Bg/car13.jpg'
    // Add more image URLs as needed
];

var currentImageIndex = 0; // Initialize the index of the current background image

// Event listener for button b1
document.querySelector('#b1').addEventListener('click', function () {
    changeBackgroundImage('prev');
});

// Event listener for button b2
document.querySelector('#b2').addEventListener('click', function () {
    changeBackgroundImage('next');
});

// Function to change the background image
function changeBackgroundImage(direction) {
    if (direction === 'prev') {
        currentImageIndex = (currentImageIndex - 1 + backgroundImages.length) % backgroundImages.length;
    } else {
        currentImageIndex = (currentImageIndex + 1) % backgroundImages.length;
    }

    // Set the new background image
    document.querySelector('.Container').style.backgroundImage = 'url(' + backgroundImages[currentImageIndex] + ')';
}
const preloadImages = (images) => {
    images.forEach((image) => {
        const img = new Image();
        img.src = image;
    });
};

// Call preloadImages with your array of backgroundImages
preloadImages(backgroundImages);
window.addEventListener('load', function () {
    // Start your transition logic here
    setInterval(function () {
        changeBackgroundImage('next');
    }, 5000);
});



// courousel --------------------------------------------------------------------------------
// Add this JavaScript code after your existing script
/*
document.addEventListener('DOMContentLoaded', function () {
    const carousel = document.querySelector('.carousel');
    const prevButton = document.getElementById('prevCar');
    const nextButton = document.getElementById('nextCar');

    let currentCarIndex = 0;

    nextButton.addEventListener('click', function () {
        currentCarIndex = (currentCarIndex + 1) % carousel.children.length;
        updateCarousel();
    });

    prevButton.addEventListener('click', function () {
        currentCarIndex = (currentCarIndex - 1 + carousel.children.length) % carousel.children.length;
        updateCarousel();
    });

    function updateCarousel() {
        const transformValue = -currentCarIndex * 100 + '%';
        carousel.style.transform = 'translateX(' + transformValue + ')';
    }


    

});
*/
// booking -------------------------------------------
function handleCheckboxClick() {
    console.log('Checkbox clicked!');
    var chkSameReturnLocation = document.getElementById('<%= chkSameReturnLocation.ClientID %>');
    var txtPickupLocation = document.getElementById('<%= txtPickupLocation.ClientID %>');
    var txtReturnLocation = document.getElementById('<%= txtReturnLocation.ClientID %>');

    if (chkSameReturnLocation.checked) {
        // If checkbox is checked, set Return Location to Pickup Location
        console.log('Setting Return Location to Pickup Location');
        txtReturnLocation.value = txtPickupLocation.value;
    } else {
        // If checkbox is unchecked, clear Return Location
        console.log('Clearing Return Location');
        txtReturnLocation.value = '';
    }
}

