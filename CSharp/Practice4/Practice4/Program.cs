using System;
using System.Collections.Generic;

namespace Practice4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Compute(3, 5, Operation.Multiplication));

            List<User> users = new List<User>();

            users.Add(new AdminUser("Moffe94", "oy3ndori"));
            users.Add(new NormalUser("Truckibo.3110", "truc1234"));
            users.Add(new NormalUser("jonas", "1234"));
            users.Add(new AdminUser("Villewonka", "9a9sd993"));

            foreach(var user in users)
            {
                Console.WriteLine($"{user}, privilege: {user.Privilege()}");
            }

            List<IUser> users2 = new List<IUser>();

            users2.Add(new AdminUserStruct("Moffe", "oy3ndori"));
            users2.Add(new NormalUserStruct("Moffis", "loleksde"));

            foreach(var user in users2)
            {
                Console.WriteLine($"{user}, privilege: {user.Privilege()}");
            }
        }

        public static float Compute(float num1, float num2, Operation op)
        {
            switch (op)
            {
                case Operation.Addition:
                    return num1 + num2;
                case Operation.Subtraction:
                    return num1 - num2;
                case Operation.Division:
                    if (num2 == 0)
                    {
                        throw new DivideByZeroException("Can't divide by zero.");
                    }
                    else
                    {
                        return num1 / num2;
                    }
                case Operation.Multiplication:
                    return num1 * num2;
                default:
                    throw new FormatException("An invalid operator was used. Please use +, -, * or /");
            }
        }

        public enum Operation
        {
            Addition,
            Subtraction,
            Division,
            Multiplication
        }
    }
}
