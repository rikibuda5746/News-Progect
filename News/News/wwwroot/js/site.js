
// Function to render news titles using Handlebars.js
function renderTitles() {

    //Ajex call
    $.ajax({
        type: "GET",
        url: "/News/GetNews",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        //Use JQuery and JS instead of repeater control.
        success: function (data) {
            console.log({data})
            const titlesList = document.getElementById("Titles");
            var source = "";
            if (document.getElementById("titles-template").innerHTML != null) {
                source = document.getElementById("titles-template").innerHTML;
            }
            const template = Handlebars.compile(source);

            //Present the titles
            data.forEach(newsItem => {
                const titleHTML = template(newsItem);
                const titleItem = document.createElement("div");
                titleItem.innerHTML = titleHTML;
                titleItem.addEventListener("click", function () {
                    renderNewsContent(newsItem);
                });
                titlesList.appendChild(titleItem);
            });
        },
        error: function () {
            alert("Error in fetching news from url.");
        }
    });
   
}
// Functiom that close post.
function closePost() {
    const newsContainer = document.getElementById("news-container");
    const template = Handlebars.compile("");
    newsContainer.innerHTML = template(null);
}
// Function to render news content for selected news item
function renderNewsContent(newsItem) {
    const newsContainer = document.getElementById("news-container");

    const source = document.getElementById("content-template").innerHTML;
    const template = Handlebars.compile(source);

    newsContainer.innerHTML = template(newsItem);

    const close = document.createElement("p");
    close.innerHTML = 'x'
    close.addEventListener("click", function () {
        closePost();
    });
    newsContainer.appendChild(close);
}

// Execute the renderTitles function when the document is ready
$(document).ready(function () {
    renderTitles();
});





