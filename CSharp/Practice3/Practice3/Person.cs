using System;
using System.Collections.Generic;
using System.Text;

namespace Practice3
{
    public class Person
    {
        private string name;
        private int age;
        private string favoriteColor;
        private bool isMarried;

        public string Name { get { return name; } }
        public int Age { get { return age; } }
        public string FavoriteColor { get { return favoriteColor; } }
        public bool IsMarried { get { return isMarried; } }

        public Person(string name, int age, string favoriteColor, bool isMarried)
        {
            this.name = name;
            this.age = age;
            this.favoriteColor = favoriteColor;
            this.isMarried = isMarried;
        }
        
        public string Description()
        {
            string marriedText = "I am not married";
            if (IsMarried)
            {
                marriedText = "I am married";
            }

            return $"Hello, my name is {name} and I am {age} years old. {marriedText} and my favorite color is {favoriteColor}.";
        }

        public Person Copy()
        {
            //return this fungerar ej pga reference type. att returna this är att returna samma minnesreferens som det gamla objektet. således blir det inget "new" object.
            return new Person(name, age, favoriteColor, isMarried);
        }
    }
}
