using System;
namespace Förberedande_Kurs
{
    class Person
    {


        //internal int health;
        //internal int strength;
        //internal int lucky;

        private int health = new Random().Next(50, 100);
        private int strength = new Random().Next(50, 100);
        private int luck = new Random().Next(50, 100);
        private string name;

        public Person(string name)
        {
            this.name = name;  
        }

        // metod för att klassen ska kunna skrivas ut på något informativt sätt.
        public override string ToString() {
            return name + " har hälsa " + health + ", styrka " + strength + " och tur " + luck + "." ;
        }

    }
}
