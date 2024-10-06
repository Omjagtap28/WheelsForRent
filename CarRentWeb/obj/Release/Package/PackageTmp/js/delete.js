var backgroundImages = [
    'Bg/Car1.jpg',
    'Bg/Car2.jpg',
];

var currentImageIndex = 0;

document.querySelector('#b1').addEventListener('click', function () {
    changeBackgroundImage('prev');
});
document.querySelector('#b2').addEventListener('click', function () {
    changeBackgroundImage('next');
});


function changeBackgroundImage(direction) {
    if (direction == 'prev') {
        currentImageIndex = (currentImageIndex - 1 + backgroundImages.length) % backgroundImages.length;
    }
    else {
        currentImageIndex = (currentImageIndex + 1) % backgroundImages.length;
    }
}

document.querySelector('Container').style.backgroundImage = 'url(' + backgroundImages[currentImageIndex] + ')';

const preloadImages = (images) => {
    images.forEach((images) => {
        const img = new Image();
        img.src = image;
    });
};

preloadImages(backgroundImages);

window.addEventListener('load', function () {
    setInterval(function () {
        changeBackgroundImage('next');
    },5000);
});