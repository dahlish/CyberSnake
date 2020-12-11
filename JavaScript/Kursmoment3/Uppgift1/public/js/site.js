$(document).ready(function(){
    $("#form").submit(function(){
        $("#success").css("display", "block");
        $("#submit").attr("disabled", true);
    })
});