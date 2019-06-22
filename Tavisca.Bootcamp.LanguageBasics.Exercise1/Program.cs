using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            Test("42*47=1?74", 9);
            Test("4?*47=1974", 2);
            Test("42*?7=1974", 4);
            Test("42*?47=1974", -1);
            Test("2*12?=247", -1);
            Console.ReadKey(true);
        }

        private static void Test(string args, int expected)
        {
            var result = FindDigit(args).Equals(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"{args} : {result}");
        }

        public static int FindDigit(string equation)
        {
            // Author : Aditi Rupade.

            //Splitting string a*b=c into a,b,c
            string[] splitEquation = equation.Split(new Char[] { '*', '=' });

            //Case1: num1 has ?

            if (splitEquation[0].Contains('?'))
            {
                 int num2 = 0, res = 0;

                //Converting String to Integer
                Int32.TryParse(splitEquation[1], out num2);
                Int32.TryParse(splitEquation[2], out res);

               //Checking for factors of result(c)
                if (res % num2 == 0)
                {
                    //calculating factor with missing number
                    int temp = res / num2;
                    string temp2 = temp.ToString();

                    return FindIndex(splitEquation[0], temp2);
                }

            }

            //Case2: num2 has ?

            else if (splitEquation[1].Contains('?'))
            {
                //Works in the Same way as above function just values different
                int num1 = 0, res = 0;

                Int32.TryParse(splitEquation[0], out num1);
                Int32.TryParse(splitEquation[2], out res);

                if (res % num1 == 0)
                {
                    int temp = res / num1;
                    string temp2 = temp.ToString();

                    return FindIndex(splitEquation[1], temp2);
                }

            }

            //Case3: res has ?

            else
            {
                int num1 = 0, num2 = 0;

                Int32.TryParse(splitEquation[0], out num1);
                Int32.TryParse(splitEquation[1], out num2);

                //calculating actual result
                int temp = num1 * num2;
                string temp2 = temp.ToString();

                return FindIndex(splitEquation[2], temp2);
            }
            Console.WriteLine($"-1");
            return -1;
        }
        
         public static int FindIndex(string equationVar, string tempVar)
        {
            //checking whether equation number and temporary number are of same magnitude
            if (equationVar.Length == tempVar.Length)
            {
                //Finding ? to replace missing number 
                int index = equationVar.IndexOf('?');
                Console.WriteLine(tempVar[index]);

                return tempVar[index] - '0';
            }
            Console.WriteLine($"-1");
            return -1;
        }
    }
}
