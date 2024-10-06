function createSlideshow(otherImages) {
    var slideshowContainer = document.createElement('div');
    slideshowContainer.classList.add('slideshow-container');

    var images = otherImages.split(',');

    images.forEach(function (image) {
        var slide = document.createElement('div');
        slide.classList.add('mySlides');
        slide.style.display = 'none';

        var img = document.createElement('img');
        img.src = 'Car_Images/' + image + '.jpg';

        slide.appendChild(img);
        slideshowContainer.appendChild(slide);
    });

    document.getElementsByClassName('img_container')[0].appendChild(slideshowContainer);

    var slideIndex = 0;
    showSlides();

    function showSlides() {
        var slides = document.getElementsByClassName('mySlides');
        if (slides.length === 0) return;
        for (var i = 0; i < slides.length; i++) {
            slides[i].style.display = 'none';
        }
        slideIndex++;
        if (slideIndex > slides.length) {
            slideIndex = 1;
        }
        slides[slideIndex - 1].style.display = 'block';
        setTimeout(showSlides, 5000); // Change image every 5 seconds
    }
}
