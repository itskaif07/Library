let uploadImage = document.querySelector("#uploadImage");
let uploadImageButton = document.querySelector("#uploadImageButton");

document.addEventListener("DOMContentLoaded", function () {

    if (uploadImage && uploadImageButton) {
        uploadImage.addEventListener("click", function () {
            uploadImageButton.click();
        })

        uploadImageButton.addEventListener('change', function () {
            if (this.files && this.files[0]) {
                uploadImage.src = URL.createObjectURL(this.files[0])
            }
        })
    }

})