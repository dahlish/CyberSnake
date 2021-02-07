using System;

namespace Practice3
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] people =
            {
                new Person("Christopher", 26, "Blue", false),
                new Person("Truc", 21, "Orange", false),
                new Person("Mona", 63, "Green", true),
                new Person("Annika", 38, "Red", false)
            };


            Crowd crowd = new Crowd();

            foreach (var person in people)
            {
                crowd.Add(person);
            }

            Console.WriteLine(crowd.Count());

            int num = 99;
            Clamp(ref num);
            Console.WriteLine(num);
            num = 105;
            Clamp(ref num);
            Console.WriteLine(num);

            GoodNumbers(out int num1, out int num2);
            Console.WriteLine($"{num1}, {num2}");


            Person p1 = people[3].Copy();
            Console.WriteLine(p1.Name);

            Console.WriteLine(PerhapsGetInt());


            Square square = new Square(3);
            Rectangle rect = new Rectangle(3, 4);
            Square square2 = new Square(3);

            Console.WriteLine(square2 != square);
        }

        public static void Clamp(ref int number)
        {
            if (number > 100)
            {
                number = 100;
            }
        }

        public static void GoodNumbers(out int x, out int y)
        {
            Random rand = new Random();
            x = rand.Next(1, 100);
            y = rand.Next(1, 100);
        }

        public static int? PerhapsGetInt()
        {
            try
            {
                int number = int.Parse(Console.ReadLine());
                return number;
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }
}
