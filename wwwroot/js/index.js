const boxContainer = document.getElementById('boxContainer');
const next = document.querySelector("#next");

const Genres = ['fiction', 'adventure', 'romance', 'mystery', 'fantasy', 'science',
    'history', 'biography', 'comics', 'thriller', 'horror', 'self-help',
    'technology', 'travel', 'philosophy', 'psychology', 'art', 'poetry',
    'business', 'sports', 'health', 'spirituality', 'education', 'war',
    'nature', 'classics', 'short stories', 'memoir', 'music', 'drama', 'anime']

const randomQuery = Genres[Math.floor(Math.random() * Genres.length)]

document.addEventListener('DOMContentLoaded', function () {

    fetch(`https://www.googleapis.com/books/v1/volumes?q=${randomQuery}`)
        .then(response => response.json())
        .then(data => {
            boxContainer.innerHTML = '';  // Clear any previous content

            // Check if 'items' exists in the response
            if (data.items && data.items.length > 0) {
                // Loop through all the books
                data.items.forEach(book => {
                    const volumeInfo = book.volumeInfo;

                    // Constructing book item
                    const bookItem = `
                            <a id="box" href="/Apis/Details/${book.id}" class="block hover:text-white opacity-90 hover:opacity-100 w-full md:w-[70%] lg:w-[45%] 2xl:w-[30%] border-2 border-gray-600 rounded-lg flex items-center justify-between h-60">
                                <div class="w-1/2 h-full">
                                   <img src="${volumeInfo.imageLinks ? volumeInfo.imageLinks.thumbnail : '/images/noImage.jpg'}" alt="No Image Available" class="w-full h-full object-cover">

                                </div>
                                <div class="w-1/2 px-2 space-y-2">
                                    <h1 class="text-xl font-medium pl-4">${volumeInfo.title ? volumeInfo.title.substring(0, 50) : 'No Title'}</h1>
                                    <h3 class="text-md pl-4">${volumeInfo.authors ? volumeInfo.authors.join(', ').substring(0, 40) : 'Unknown Author'}</h3>
                                    <h5 class="text-sm pl-4 ">${volumeInfo.categories && volumeInfo.categories.length > 0 ? volumeInfo.categories.join(',') : 'Unknown genre'}</h5>
                                </div>
                            </a>
                        `;
                    console.log(volumeInfo.categories);

                    // Appending book item to the book list
                    boxContainer.innerHTML += bookItem;
                });
                next.classList.remove('hidden')

            } else {
                // If no books found
                boxContainer.innerHTML = '<p>No books found.</p>';
            }
        })
        .catch(error => console.error('Error:', error));

})

//search bar

let searchInput = document.querySelector("#searchInput")

document.addEventListener("DOMContentLoaded", function () {
    searchInput.addEventListener("input", (e) => {
        let searchValue = e.target.value.toLowerCase();

        fetch(`https://www.googleapis.com/books/v1/volumes?q=${searchValue}`)
            .then(response => response.json())
            .then(data => {
                boxContainer.innerHTML = '';

                if (data.items && data.items.length > 0) {

                    data.items.forEach(book => {
                        let volumeInfo = book.volumeInfo;

                        let boxItem = `
                            <a id="box" href="/Apis/Details/${book.id}" class="block hover:text-white opacity-90 hover:opacity-100 w-full md:w-[70%] lg:w-[45%] 2xl:w-[30%] border-2 border-gray-600 rounded-lg flex items-center justify-between h-60">
                                <div class="w-1/2 h-full">
                                    <img src="${volumeInfo.imageLinks ? volumeInfo.imageLinks.thumbnail : '/images/noImage.jpg'}" alt="Book Cover" class="w-full h-full object-cover">
                                </div>
                                <div class="w-1/2 px-2 space-y-2">
                                 <h1 class="text-xl font-medium pl-4">${volumeInfo.title ? volumeInfo.title.substring(0, 50) : 'No Title'}</h1>
                                 <h3 class="text-md pl-4">${volumeInfo.authors ? volumeInfo.authors.join(', ') : 'Unknown Author'}</h3>
                                 <h5 class="text-sm pl-4 ">${volumeInfo.categories && volumeInfo.categories.length > 0 ? volumeInfo.categories.join(',') : 'Unknown genre'}</h5>

                                </div>
                            </a>
                        `;

                        boxContainer.innerHTML += boxItem;
                    })

                }
                else {
                    boxContainer.innerHTML += `<p>No Book Found</p>`
                }
            }).catch(error => console.log('Error: ' + error))


    })
})
