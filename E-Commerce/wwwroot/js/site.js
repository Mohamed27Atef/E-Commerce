// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


const favoriteIcons = document.querySelectorAll(".toggle-favorite")

favoriteIcons.forEach(icon => {
    icon.addEventListener("click", () => {
        icon.classList.toggle("fas")
    })
})

function updateStarRating() {
    const productRatingElements =
        document.querySelectorAll(".product-rating")

    productRatingElements.forEach(ratingElement => {
        const ratingText = ratingElement.textContent.trim().split(" ")[0]
        const rating = parseFloat(ratingText)

        const starsElement = ratingElement.querySelector(".stars")
        starsElement.innerHTML = ""

        for (let i = 1; i <= rating; i++) {
            const starIcon = document.createElement("i")
            starIcon.classList.add("fas", "fa-star")
            starsElement.appendChild(starIcon)
        }

        if (rating % 1 == 0.5) {
            const halfStarIcon = document.createElement("i")
            halfStarIcon.classList.add("fas", "fa-star-half-alt")
            starsElement.appendChild(halfStarIcon)
        }

        const emptyStarsCount = 5 - Math.ceil(rating)
        for (let i = 0; i < emptyStarsCount; i++) {
            const starIcon = document.createElement("i")
            starIcon.classList.add("far", "fa-star")
            starsElement.appendChild(starIcon)
        }
    })
}

// details view
document.addEventListener("DOMContentLoaded", function () {
    var thumbnailImages = document.querySelectorAll('.thumbnail');
    var mainImage = document.querySelector('.main-image img');

    thumbnailImages.forEach(function (thumbnail) {
        thumbnail.addEventListener('mouseenter', function () {
            var imgSrc = thumbnail.getAttribute('src');
            mainImage.setAttribute('src', imgSrc);
        });
    });

});

updateStarRating()