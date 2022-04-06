using Bistec.ATF.Refit.Models;
using System;
using TechTalk.SpecFlow;

namespace Bistec.ATF.Refit.StepDefinitions
{
    [Binding]
    internal class Transformations
    {
        [StepArgumentTransformation]
        public RandomisedValue ToRandomisedValue(string initialInput)
        {
            if (initialInput == "$$randomstring")
            {
                return new RandomisedValue { StringValue = GenerateName(10) };
            }
            else
            {
                return new RandomisedValue { StringValue = initialInput };
            }
        }

        public static string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;


        }
    }
}
