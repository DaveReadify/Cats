using System;
using System.Collections.Generic;

namespace Cats.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public List<Pet> Pets { get; set; }

        public Gender PersonGender => (Gender)Enum.Parse(typeof(Gender), Gender, true);
    }

    public enum Gender
    {
        Male,
        Female
    }
}