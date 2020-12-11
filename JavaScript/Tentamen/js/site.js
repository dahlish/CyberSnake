$("document").ready(function(){

    $("#checkButton").click(function(event){
        event.preventDefault();
        let patientMax = $("#patientMax").val();
        let doctorMin = $("#doctorMin").val();

        if(patientMax.length >= doctorMin.length)
        {
            $("#resultLabel").text("go");
        }
        else
        {
            $("#resultLabel").text("no");
        }

        $("#patientMax").val("");
        $("#doctorMin").val("");
    });


    $("#testButton").click(function(event)
    {
        event.preventDefault();

        let testCaseCounter = 1;
        let weekDays = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
        let currentDay = 6;
        let splitTextArea = $("#testCases").val().split("\n");
        let numberOfTestCases = splitTextArea[0];
        let numberOfLuckyDays = 0;
        let numberOfLuckyDaysArray = [];
        let hasError = false;
        let errorMessage = "";
        let daysInEachMonthCombined = 0;

        if (numberOfTestCases < 1 || numberOfTestCases > 20)
        {
            hasError = true;
            errorMessage = "Antal testcase måste vara mellan 1-20";
        }

        for (i = 1; i < numberOfTestCases * 2; i+=2)
        {
            if (splitTextArea[i] == "" || splitTextArea[i] == undefined)
            {
                hasError = true;
                errorMessage = "Du måste fylla i alla testcase.";
            }
        }
        if (!hasError)
        {
            for (i = 1; i <= numberOfTestCases; i++)
            {
                let numberOfDays = splitTextArea[testCaseCounter].split(" ")[0];
                let numberOfMonths = splitTextArea[testCaseCounter].split(" ")[1];
                let daysInEachMonth = splitTextArea[testCaseCounter + 1].split(" ");

                for (x = 0; x < daysInEachMonth.length; x++)
                {
                    daysInEachMonthCombined += Number(daysInEachMonth[x]);
                }

                if (daysInEachMonthCombined > numberOfDays)
                {
                    hasError = true;
                    errorMessage = "Totalt antal dagar i månader får ej överstiga totalt antal dagar per år. Testcase: " + i;
                    break;
                }
                else if (numberOfDays < 1 || numberOfDays > 1000)
                {
                    hasError = true;
                    errorMessage = "Totalt antal dagar måste vara mellan 1-1000. Testcase: " + i;
                    break;
                }
                
                for (y = 0; y < daysInEachMonth.length; y++)
                {
                    for (j = 1; j <= daysInEachMonth[y]; j++)
                    {
                        if (j == 13 && weekDays[currentDay] == "Friday")
                        {
                            numberOfLuckyDays++;
                        }

                        currentDay++;
                        if (currentDay > 6)
                        {
                            currentDay = 0;
                        } 
                    }
                }

                numberOfLuckyDaysArray.push(numberOfLuckyDays);
                numberOfLuckyDays = 0;
                testCaseCounter += 2;
                currentDay = 6;
                daysInEachMonthCombined = 0;
            }
        }

        if (hasError)
        {
            $("#testResultLabel").text(errorMessage);
            $("#testResultLabel").css("color", "red");
        }

        if (!hasError)
        {
            $("#testResultLabel").css("color", "black");
            $("#testResultLabel").text(numberOfLuckyDaysArray[0]);

            for (i = 1; i < numberOfLuckyDaysArray.length; i++)
            {
                $("#testResultLabel").append("\n" + numberOfLuckyDaysArray[i]);
            }
        }
    });

});