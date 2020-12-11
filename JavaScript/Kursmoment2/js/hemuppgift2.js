$(document).ready(function(){

    //UPPGIFT 1 DEL 1 NEDAN---------------------------------------------

    let answer = 0;
    let numberOfGuesses = 0;
    let correctAnswer = false;
    GenerateGuessGame();

    $("#guessButton").click(function() { Guess($("#guessField").val())});
    $("#playAgainButton").click(function() { GenerateGuessGame()});

    function Guess(guessedNumber)
    {
        guessedNumber = Number(guessedNumber);
        if (!correctAnswer)
        {
            if (guessedNumber > 0 && guessedNumber <= 100)
            {
                numberOfGuesses++;
                if (guessedNumber == answer)
                {
                    $("#guessResultLabel").text("Rätt gissat. Du gissade " + numberOfGuesses + " gånger.")
                    correctAnswer = true;
                    $("#playAgainButton").css("display", "inline");
                }
                else if (guessedNumber < answer)
                {
                    $("#guessResultLabel").text("För lågt!");
                }
                else
                {
                    $("#guessResultLabel").text("För högt!");
                }
            }
            else
            {
                $("#guessResultLabel").text("Vänligen skriv in ett nummer mellan 1-100.");
            }
        }
        else
        {
            $("#guessResultLabel").text("Du har redan gissat rätt! Klicka på Spela igen om du vill spela igen.");
        }
    }
    function GenerateGuessGame()
    {
        $("#guessResultLabel").text("");
        $("#playAgainButton").css("display", "none");
        $("#guessField").val(undefined);
        correctAnswer = false;
        numberOfGuesses = 0;
        answer = Math.floor(Math.random() * 100 + 1);
    }

    //UPPGIFT 1 DEL 2 NEDAN---------------------------------------------


    $("#translateButton").click(function() {
        ($("#translatedTextLabel")).text(Translate($("#translateField").val()));
    })


    function Translate(input)
    {
        let translatedText = "";
        let konsonanter = "BbCcDdFfGgHhJjKkLlMmNnPpQqRrSsTtVvWwXxZz";
        for(i = 0; i < input.length; i++)
        {
            let isKonsonant = false;
            for (y = 0; y < konsonanter.length; y++)
            {
                if (input[i] == konsonanter[y])
                {
                    translatedText+= input[i];
                    isKonsonant = true;
                }
            }
            if (isKonsonant)
            {
                translatedText += "o";
                translatedText += input[i];
            }
            else {
                translatedText+= input[i];
            }

        }

        return translatedText;
    }

        //UPPGIFT 2 NEDAN---------------------------------------------
        let firstNameCorrect, lastNameCorrect, emailCorrect, passwordCorrect, addressCorrect, postcodeCorrect, cityCorrect, datesCorrect;
        let arrivalDate = new Date();
        let leaveDate = new Date();
        $("#sendButton").attr("disabled", true);


        $("#firstName").bind("change", function() {
            let regEx = /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/u;
    
            if (!regEx.test($("#firstName").val()))
            {
                $("#firstName").css("border-color", "red");
                firstNameCorrect = false;
            }
            else
            {
                $("#firstName").css("border-color", "green");
                firstNameCorrect = true;
            }
            EvaluateForm();
         });
         $("#lastName").bind("change", function() {
            let regEx = /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/u;
    
            if (!regEx.test($("#lastName").val()))
            {
                $("#lastName").css("border-color", "red");
                lastNameCorrect = false;
            }
            else
            {
                $("#lastName").css("border-color", "green");
                lastNameCorrect = true;
            }
            EvaluateForm();
         });
         $("#email").bind("change", function() {
            let regEx = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/g;
    
            if (!regEx.test($("#email").val()))
            {
                $("#email").css("border-color", "red");
                emailCorrect = false;
            }
            else
            {
                $("#email").css("border-color", "green");
                emailCorrect = true;
            }
            EvaluateForm();
         });
    
         $("#password").bind("change", function() {
            let regEx = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/g;
    
            if (!regEx.test($("#password").val()))
            {
                $("#password").css("border-color", "red");
                $("#passwordInfoLabel").text("Lösenordet måste innehålla minst 8 tecken, innehålla en stor respektive liten bokstav, en siffra samt ett tecken.")
                $("#passwordInfoLabel").css("color", "red");
                passwordCorrect = false;
            }
            else
            {
                $("#password").css("border-color", "green");
                $("#passwordInfoLabel").text("");
                passwordCorrect = true;
    
            }
            EvaluateForm();
         });
    
         $("#address").bind("change", function() {
            let regEx = /^[0-9a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/u;
    
            if (!regEx.test($("#address").val()))
            {
                $("#address").css("border-color", "red");
                addressCorrect = false;
            }
            else
            {
                $("#address").css("border-color", "green");
                addressCorrect = true;
    
            }
            EvaluateForm();
         });

         $("#postcode").bind("change", function() {
            let regEx = /^\d{3} \d{2}$|^\d{5}$/g;
    
            if (!regEx.test($("#postcode").val()))
            {
                $("#postcode").css("border-color", "red");
                postcodeCorrect = false;
            }
            else
            {
                $("#postcode").css("border-color", "green");
                postcodeCorrect = true;
    
            }
            EvaluateForm();
         });

         
         $("#city").bind("change", function() {
            let regEx = /^[0-9a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/u;
    
            if (!regEx.test($("#city").val()))
            {
                $("#city").css("border-color", "red");
                cityCorrect = false;
            }
            else
            {
                $("#city").css("border-color", "green");
                cityCorrect = true;
    
            }
            EvaluateForm();
         });

         $("#arrivalDate").bind("change", function() {
            
            arrivalDate = $("#arrivalDate").val();
            EvaluateDates(arrivalDate, leaveDate);
            EvaluateForm();
         });

         $("#leaveDate").bind("change", function() {
            
            leaveDate = $("#leaveDate").val();
            EvaluateDates(arrivalDate, leaveDate);
            EvaluateForm();
         });

         function EvaluateDates(arrivalDate, leaveDate)
         {
             if (arrivalDate > leaveDate)
             {
                $("#arrivalDate").css("border-color", "red");
                $("#leaveDate").css("border-color", "red");
                 $("#evaluatedDatesLabel").text("Ankomstdatumet måste vara före hemresedatumet.");
                 $("#evaluatedDatesLabel").css("color", "red");
                 datesCorrect = false;
             }
             else
             {
                $("#evaluatedDatesLabel").text("");
                $("#arrivalDate").css("border-color", "black");
                $("#leaveDate").css("border-color", "black");
                datesCorrect = true;
             }
         }


         function EvaluateForm()
         {
             if (firstNameCorrect && lastNameCorrect && emailCorrect && passwordCorrect && addressCorrect && postcodeCorrect && cityCorrect && datesCorrect)
             {
                 $("#sendButton").attr("disabled", false);
             }
             else
             {
                $("#sendButton").attr("disabled", true);

             }
         }

         function Send()
         {
            $("#sendLabel").text("Tack! Ditt formulär är skickat.");
            $("#sendButton").attr("disabled", true);
         }

         $("#sendButton").click(function() {
            event.preventDefault(); 
            Send();
         });

         //UPPGIFT 3 NEDAN

         let possibleWords = ["Nationalencyclopedin", "Bensinmack", "Tesla", "Julmust", "Tomte", "Dörr", "Programmerare", "Häst", "Boll", "Viking", "Isterband", "Stockholm", "Bank", "Kalas", "Kålpudding", "Matta", "Vinstmarginal", "Företag",
         "Trisslott", "Ryssland", "Pannkaka", "Hallonsylt", "Vispgrädde", "Vinöppnare", "Restaurang", "Pizzabagare", "Doktor", "Onlinespel", "Barn", "Kärlek", "Familj", "Nyheter", "Tidning", "Systemutveckling", "Juldekorationer", "Nyårsafton"];
         let canvas = document.getElementById("gameview");
         let context = canvas.getContext('2d');
         let gameAnswer = "";
         let gameIsRunning = false;
         let progress = "";
         let currentStage = 1;
         function StartGame()
         {
            progress = "";
            currentStage = 1;
            $("#progressLabel").text(progress);
            $("#gameInfoLabel").text("");
            $(".letterButton").attr("disabled", false);

            gameAnswer = possibleWords[Math.floor(Math.random() * possibleWords.length)];
            PlaceImage("1");
            gameIsRunning = true;
            $("#startGame").attr("disabled", true);

            for (i = 0; i < gameAnswer.length; i++)
            {
                progress += "_";
            }

            $("#progressLabel").text(progress);
            

         }
         function PlaceImage(stage)
         {
            let image = new Image();
            
            image.src = "./img/stage_" + stage + ".png";
            image.onload = function() {
            context.drawImage(image, 0, 0);
         }

         function GuessLetter(letter)
         {
             let matchFound = false;
            for (i = 0; i < gameAnswer.length; i++)
            {
                if (letter.toLowerCase() == gameAnswer[i].toLowerCase())
                {
                    UpdateWord(letter);
                    matchFound = true;
                    break;
                }
            }

            if (!matchFound)
            {
                currentStage++;
                PlaceImage(currentStage);
            }

            if (currentStage >= 13)
            {
                $("#gameInfoLabel").text("Tyvärr så klarade du inte matchen. Försök gärna igen.");
                $("#gameInfoLabel").css("color", "red");
                EndGame();
            }
         }

         function UpdateWord(letter)
         {
            for (i = 0; i < gameAnswer.length; i++)
            {
                if (letter.toLowerCase() == gameAnswer[i].toLowerCase())
                {
                    progress = ReplaceAt(progress, i, letter);
                }
            }

            $("#progressLabel").text(progress);

            EvaluateWords();
         }

         function EvaluateWords()
         {
             let hasUnderscore = false;
             for(i = 0; i < progress.length; i++)
             {
                if (progress[i] == "_")
                {
                    hasUnderscore = true;
                }
             }

             if (!hasUnderscore)
             {
                $("#gameInfoLabel").text("Bra jobbat, du klarade det!");
                $("#gameInfoLabel").css("color", "green");
                 EndGame();
             }
         }

         function EndGame()
         {
             $("#progressLabel").text(gameAnswer.toUpperCase());
            $(".letterButton").attr("disabled", true);
            gameIsRunning = false;
            $("#startGame").attr("disabled", false);
         }

         $(".letterButton").unbind().click(function() {
            GuessLetter($(this).text());
        });

        function ReplaceAt(string, index, replace) {
            return string.substring(0, index) + replace + string.substring(index + 1);
          }
    }

    $("#startGame").click(function(){
        StartGame();
        });

    $(".letterButton").attr("disabled", true);



    //UPPGIFT 4 NEDAN

    $(".landingDiv").hover(function(){
        $(this).animate({width: "500px", opacity: "0.7"}, "fast");
        $("#homeImg").animate({right: "390px"}, "fast");
        $("#homeText").fadeIn("slow");
    });
    $(".landingDiv").mouseleave(function(){
        $(this).stop(true).fadeTo("fast",1);
        $("#homeImg").stop(true).fadeTo("fast", 1);
        $("#homeText").stop(true).fadeTo("fast", 1);
        $(this).animate({width: "100px", opacity: "1"}, "fast");
        $("#homeImg").animate({right: "0"}, "fast");
        $("#homeText").fadeOut("medium")
    });

    $(".accountDiv").hover(function(){
        $(this).animate({width: "500px", opacity: "0.7"}, "fast");
        $("#accountImg").animate({right: "390px"}, "fast");
        $("#accountText").fadeIn("slow");
    });
    $(".accountDiv").mouseleave(function(){
        $(this).stop(true).fadeTo("fast",1);
        $("#accountImg").stop(true).fadeTo("fast", 1);
        $("#accountText").stop(true).fadeTo("fast", 1);
        $(this).animate({width: "100px", opacity: "1"}, "fast");
        $("#accountImg").animate({right: "0"}, "fast");
        $("#accountText").fadeOut("medium")
    });

    $(".settingsDiv").hover(function(){
        $(this).animate({width: "500px", opacity: "0.7"}, "fast");
        $("#settingsImg").animate({right: "390px"}, "fast");
        $("#settingsText").fadeIn("slow");
    });
    $(".settingsDiv").mouseleave(function(){
        $(this).stop(true).fadeTo("fast",1);
        $("#settingsImg").stop(true).fadeTo("fast", 1);
        $("#settingsText").stop(true).fadeTo("fast", 1);
        $(this).animate({width: "100px", opacity: "1"}, "fast");
        $("#settingsImg").animate({right: "0"}, "fast");
        $("#settingsText").fadeOut("medium")
    });

    $(".privacyDiv").hover(function(){
        $(this).animate({width: "500px", opacity: "0.7"}, "fast");
        $("#privacyImg").animate({right: "390px"}, "fast");
        $("#privacyText").fadeIn("slow");
    });
    $(".privacyDiv").mouseleave(function(){
        $(this).stop(true).fadeTo("fast",1);
        $("#privacyImg").stop(true).fadeTo("fast", 1);
        $("#privacyText").stop(true).fadeTo("fast", 1);
        $(this).animate({width: "100px", opacity: "1"}, "fast");
        $("#privacyImg").animate({right: "0"}, "fast");
        $("#privacyText").fadeOut("medium")
    });

    $("a.navigation").hover(function(){
        $(this).animate({color:"white"}, "fast");
    });
    $("a.navigation").mouseleave(function(){
        $(this).stop(true).fadeTo("fast", 1);
        $(this).animate({color:"black", fontSize: "18px"}, "slow");
    });

    $("a.navigation").click(function(){
        $(this).animate({color:"white", fontSize: "24px"}, 75).promise().done(function(){
            $(this).animate({color:"white", fontSize: "18px"}, 75);
        });

    });

    function ToggleDropDown(){
        $(".list").slideToggle(250, "linear");
    }

    $(".dropdownButton").on("click", function() {
        ToggleDropDown();
    })


});