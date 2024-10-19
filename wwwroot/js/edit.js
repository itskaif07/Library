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

//Remove button


function removeImage() {

    // Set the image source to default
    uploadImage.src = "/images/uploadImage.png";

    // Clear the file input
    uploadImageButton.value = "";

    // Set the hidden field to null or empty
    document.querySelector("input[name='BookCoverImage']").value = "";
}
