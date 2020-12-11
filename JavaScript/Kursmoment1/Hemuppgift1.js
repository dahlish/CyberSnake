function SkrivUtMultiplikationsTabell() //FOR-LOOP
{
    for (i = 0; i <= 10; i++)
    {
        let line = i + "*" + i + " = " + i * i;
        var paragraph = document.getElementById("multiplikationsTabell");
        paragraph.innerHTML += line + "<br>";    
    }
}

function SkrivUtNestadMultiplikationsTabell() //NESTAD FOR-LOOP
{
    for (i = 0; i <= 30; i+= 10)
    {
        for (j = 1; j <= i; j++)
        {
            var paragraph = document.getElementById("nestadMultiplikationsTabell");
            let line = j + "*" + i + " = " + j * i;
            paragraph.innerHTML += line + "<br>";    
        }
    }
}

function GissaSpelet() //GISSA-SPELET
{
    let answer = Math.floor(Math.random() * 100 + 1);
    let numberOfGuesses = 0;
    let correctAnswer = false;

    do
    {
        var input = prompt("Gissa ett nummer mellan 1-100.\nSkriv X för att avbryta.");

        if (input > 0 && input <= 100)
        {
            if (input == answer)
            {
                numberOfGuesses++;
                alert("Rätt gissat. Du gissade " + numberOfGuesses + " gånger.")
                correctAnswer = true;
            }
            else if (input < answer)
            {
                numberOfGuesses++;
                alert("För lågt!");
            }
            else
            {
                numberOfGuesses++;
                alert("För högt!");
            }
        }
        else if (input.toLowerCase() == "x")
        {
            break;
        }
        else
        {
            alert("Vänligen skriv in ett tal mellan 1-100. Du angav: " + input);
        }
    } while (!correctAnswer);

}

function Sibice() //VG-UPPGIFT -> https://open.kattis.com/problems/sibice
{
    let inputSplit = [];
    let input;
    let n = 0;
    let w = 0;
    let h = 0;
    let length = [];
    let lengthInput;
    let sibiceParagraph = document.getElementById("sibiceParagraph");
    let correctInput = false;

    do
    {
        input = prompt("Enter number of matches (1-50), width(1-100) and height(1-100) separated with a space.");
        inputSplit = input.split(" ");
        n = Number(inputSplit[0]);
        w = Number(inputSplit[1]);
        h = Number(inputSplit[2]);

        if ((n >= 1 && n <= 50) && (w >= 1 && w <= 100) && (h >= 1 && h <= 100))
        {
            correctInput = true;
        }
        else 
        {
            alert("You haven't followed the required boundaries. Please re-enter the variables");
        }
    }while (!correctInput);

    for (i = 0; i < n; i++)
    {
        lengthInput = prompt("Length of match " + (i + 1) + "? 1-1000\nWrite X to exit.");
        lengthInput = Number(lengthInput);
        if (lengthInput > 0 && lengthInput <= 1000)
        {
            length.push(lengthInput);
        }
        else if (lengthInput.toLowerCase() == "x")
        {
            break;
        }
        else 
        {
            i--;
        }
    }

    for (i = 0; i < length.length; i++)
    {
        
        if ((Math.pow(w, 2) + Math.pow(h, 2)) >= Math.pow(length[i], 2))
        {
            sibiceParagraph.innerHTML += "Match " + (i + 1) + ": DA<br>";
        }
        else 
        {
            sibiceParagraph.innerHTML += "Match " + (i + 1 ) + ": NE<br>";
        }
    }
}

function translateToPirateLanguage() //ÖVERSÄTT TILL RÖVARSPRÅKET
{
    let input = prompt("Skriv in det du vill översätta: ");
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

    alert(translatedText);
}

function studerande()
{
    function Person(namn, efternamn, alder) //UTVIDGAR DEN PROTOTYPBASERADE ARVSHIERARKIN
    {
        this.namn = namn;
        this.efternamn = efternamn;
        this.alder = alder;

        this.greet = function() {
            alert("Hej, jag heter " + this.namn);
        }

        this.eat = function()
        {
            alert(this.namn + " äter mat.");
        }
        this.legitimation = function()
        {
            alert("Legitimation\n" + namn + " " + efternamn + "\n" + alder + " år gammal.");
        }
    }

    function Student(namn, efternamn, alder, betyg, skola, klass) 
    {
        Person.call(this, namn, efternamn, alder);
        this.skola = skola;
        this.klass = klass;
        this.betyg = betyg;
        this.study = function() {
            alert(this.namn + " sitter och pluggar");
        }

        this.studentInformation = function()
        {
            alert("Elev: " + this.namn + " " + this.efternamn + "\nSkola: " + this.skola + "\nKlass: " + this.klass + "\nBetyg: " + this.betyg);
        }
        this.bytBetyg = function(nyttBetyg)
        {
            this.betyg = nyttBetyg;
            this.studentInformation();
        }
    }


    Student.prototype = new Person();
    Student.prototype.constructor = Student;
    var std = new Student("Christopher", "Dahlborg", 26, "G", "TUC Yrkeshögskola", "SYNED20JÖN");
    console.log(std.namn); // Holger
    console.log(std instanceof Student); 
    console.log(std instanceof Person); 
    std.greet(); 
    std.study(); 
    std.eat();
    std.legitimation();
    std.studentInformation();
    std.bytBetyg("VG");
}

function vgArvsHierarki() //VG UPPGIFT 2 - ARVSHIERARKI
{
    function Animal(name, age, weight)
    {
        if (this.constructor === Animal)
        {
            throw new Error("Abstract classes can't be instantiated");
        }

        this.name = name;
        this.age = age;
        this.weight = weight;
        this.say = function(){
            alert("Animal can't speak as you haven't defined the language.");
        }
        this.eat = function(){
            alert("Animal is eating");
        }
        this.printAnimalInfo = function()
        {
            alert("This is an animal.\n" + name + "\nAge: " + age + "\nWeight: " + weight + "kg");
        }
    }

    function Dog(name, age, breed, weight)
    {
        Animal.call(this, name, age, weight);
        this.breed = breed;
        this.say = function()
        {
            alert("Woof!");
        }
        this.eat = function()
        {
            alert("A dog is eating.");
        }

        this.printAnimalInfo = function()
        {
            alert(this.name + "\nAge: " + this.age + "\nWeight: " + this.weight + "kg\nBreed: " + this.breed);
        }
    }
    function Cat(name, age, breed, weight)
    {
        Animal.call(this, name, age, weight);
        this.breed = breed;
        this.say = function()
        {
            alert("Meow!");
        }

        this.printAnimalInfo = function()
        {
            alert(this.name + "\nAge: " + this.age + "\nWeight: " + this.weight + "kg\nBreed: " + this.breed);
        }
    }
    function Monkey(name, age, breed, weight, iq)
    {
        Animal.call(this, name, age, weight);
        this.breed = breed;
        this.iq = iq;

        this.printAnimalInfo = function()
        {
            alert(this.name + "\nAge: " + this.age + "\nWeight: " + this.weight + "kg\nBreed: " + this.breed + "\nIQ: " + this.iq);
        }

    }

    //var animal = new Animal(); //Uncommenta du vill se att den inte går att instantiera.

    Dog.prototype = Object.create(Animal.prototype);
    Dog.prototype.constructor = Dog;
    var dog = new Dog("Aski", 1,"German Shepherd", 36);

    dog.eat();
    dog.say();
    dog.printAnimalInfo();
    
    Cat.prototype = Object.create(Animal.prototype);
    Cat.prototype.constructor = Cat;
    var cat = new Cat("Titzie", 14, "Norsk Skogskatt", 6);

    cat.eat();
    cat.say();
    cat.printAnimalInfo();

    Monkey.prototype = Object.create(Animal.prototype);
    Monkey.prototype.constructor = Monkey;
    var monkey = new Monkey("Oogabooga", 25, "Gorilla", 92, 31);

    monkey.say();
    monkey.eat();
    monkey.printAnimalInfo();
}


function vgArvsHierarki2() //DEL 2 AV ARVSHIARKINS VG UPPGIFT (2.4.4)
{
    function Shape()
    {
        this.area = function()
        {
            return 0;
        }

        this.toString = function()
        {
            return Object.getPrototypeOf(this).constructor.name;
        }
    }
    function Circle(radius)
    {
        Shape.call(this);
        this.radius = radius;
        this.area = function()
        {
            return Math.PI * this.radius ** 2;
        }
    }
    function Rectangle(width, height)
    {
        Shape.call(this);
        this.width = width;
        this.height = height;

        this.area = function()
        {
            return this.width * this.height;
        }
    }
    function Triangle(base, height)
    {
        Shape.call(this);
        this.base = base;
        this.height = height;

        this.area = function()
        {
            return this.base * this.height / 2;
        }
    }

    function cumulateShapes(shapes)
    {
        return shapes.reduce((sum, shape) => {
            if (shape instanceof Shape)
            {
                alert("Shape: " + shape.toString() + " - area: " + shape.area());
                return sum + shape.area();
            }
            alert("Bad argument shape.");
        }, 0);
    }

        Rectangle.prototype = Object.create(Shape.prototype);
        Rectangle.prototype.constructor = Rectangle;
        Circle.prototype = Object.create(Shape.prototype);
        Circle.prototype.constructor = Circle;
        Triangle.prototype = Object.create(Shape.prototype);
        Triangle.prototype.constructor = Triangle;
        
        const shapes = [new Circle(3), new Rectangle(2, 3), new Triangle(3,
        4), new Circle(2)];
        console.log(cumulateShapes(shapes));
}
