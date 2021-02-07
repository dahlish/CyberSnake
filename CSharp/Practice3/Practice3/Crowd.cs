using System;
using System.Collections.Generic;
using System.Text;

namespace Practice3
{
    public class Crowd
    {
        private Person[] people;

        public Crowd()
        {
            people = new Person[0];
        }

        public void Add(Person p)
        {
            Person[] people = new Person[this.people.Length + 1];

            for (int i = 0; i < people.Length; i++)
            {
                if (i == people.Length - 1)
                {
                    people[i] = p;
                }
                else
                {
                    people[i] = this.people[i];
                }
            }

            this.people = people;
        }

        public int Count()
        {
            return people.Length;
        }
    }
}
