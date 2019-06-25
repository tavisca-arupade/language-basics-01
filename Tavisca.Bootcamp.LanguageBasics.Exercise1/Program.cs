using System;
using System.Text.RegularExpressions;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class Program
    {
        static string missing = "?";
        static int impossible = -1;

        static void Main(string[] args)
        {

            Test("42*47=1?74", 9);
            Test("4?*47=1974", 2);
            Test("42*?7=1974", 4);
            Test("42*?47=1974", -1);
            Test("2*12?=247", -1);
            
            try
            {
                Test("23*1k3=628", 4);
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.GetType().FullName);
                Console.WriteLine(fe.Message);
            }
            try
            { 
                Test("", 0);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.GetType().FullName);
                Console.WriteLine(ae.Message);
            }
            try
            {
                Test("23*89*45=78?9", 1);
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine(ae.GetType().FullName);
                Console.WriteLine(ae.Message);
            }
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

            //check for null string

            if (String.IsNullOrEmpty(equation))
                throw new ArgumentException("argument cannot be null or empty", "equation");

            //Splitting string a*b=c into a,b,c
            string[] splitEquation = equation.Split(new Char[] { '*', '=' });

            //check for equation form , i.e. a*b=c
            if (splitEquation.Length != 3)
                throw new ArgumentException("argument must be of the form a * b = c", "equation");

            string number1 = splitEquation[0];
            string number2 = splitEquation[1];
            string result = splitEquation[2];

            // check for only one ? mark and no other characters
            if (!Regex.IsMatch(splitEquation[0], @"^[0-9?0-9]+$" ) || !Regex.IsMatch(splitEquation[1], @"^[0-9?0-9]+$") || !Regex.IsMatch(splitEquation[2], @"^[0-9?0-9]+$"))
                throw new FormatException("enter string in correct format(just one ? mark and all other digits)");

            //Case1: num1 has ?

            if (number1.Contains(missing))
            {
                return FindNumberBeforeAssignment(number1, number2, result);
            }

            //Case2: num2 has ?

            else if (number2.Contains(missing))
            {
                return FindNumberBeforeAssignment(number2, number1, result);
            }

            //Case3: res has ?

            else
            {
                int num1 = 0, num2 = 0;

                Int32.TryParse(number1, out num1);
                Int32.TryParse(number2, out num2);

                //calculating actual result
                int temp = num1 * num2;
                string temp2 = temp.ToString();

                return GetMissingNumber(result, temp2);
            }
        }

        public static int FindNumberBeforeAssignment(string number1, string number2, string result)
        {
            int num2 = 0, res = 0;

            //Converting String to Integer

            Int32.TryParse(number2, out num2);
            Int32.TryParse(result, out res);

            double tempNumber = (double) res / num2;

            //Checking for decimal in result(c)
            if (tempNumber - Convert.ToInt32(tempNumber) == 0)
            {

                string temp = tempNumber.ToString();

                return GetMissingNumber(number1, temp);
            }
            return impossible;
        }

        public static int GetMissingNumber(string equationValue, string calculatedValue)
        {
            //checking whether equation number and temporary number are of same magnitude
            if (equationValue.Length == calculatedValue.Length)
            {
                //Finding ? to replace missing number 
                int index = equationValue.IndexOf('?');

                // this is to avoid returning ASCII value of the string instead it returns the number in int type
                return calculatedValue[index] - '0';
            }
            return impossible;
        }
    }
}
