
$("document").ready(function(){

    let movieData;
    let request = new XMLHttpRequest();
    request.open("GET", "https://ghibliapi.herokuapp.com/films", true);
    request.onload = function () {
        let data = JSON.parse(this.response);
        if (request.status >= 200 && request.status < 400) {
        movieData = data;
        }
        else {
        console.log("Something went wrong!");
        }   
    }       
    request.send();


    $("#changeData").click(function(){
        for (i = 0; i < movieData.length; i++)
        {
            $("#filmList").append($(`<li id="${movieData[i].id}">`).html(`<a href="javascript:">${movieData[i].title}</a>`));
            $("#filmList").append($(`<li id="${movieData[i].id}-desc" style="display: none;">`).html(`${movieData[i].description}<br><br>Directed by: ${movieData[i].director}<br>Producer: ${movieData[i].producer}<br>
            Released: ${movieData[i].release_date}`));
        }
    });

    $("#filmList").on("click","li",function() {    
        displayRest(this.id);
    });
});

function displayRest(movieId)
{
    $(`#${movieId}-desc`).toggle("fast");
}

