using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Excercises
{
    class Program
    {
        static void Main(string[] args)
        {
            var bContinue = true;
            while (bContinue)
            {
                DisplayMenu();
                switch (Console.ReadLine())
                {
                    case "1":
                        try
                        {
                            LeapYear();
                        }
                        catch(Exception ex)
                        {
                            DisplayMessage(ex.Message);
                        }
                        break;
                    case "2":
                        try
                        {
                            RepeatedCharacters();
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage(ex.Message);
                        }
                        break;
                    case "3":
                        try
                        {
                            StringCompression();
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage(ex.Message);
                        }
                        break;
                    case "4":
                        try
                        {
                            BracketsValidation();
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage(ex.Message);
                        }
                        break;
                    case "5":
                        try
                        {
                            BoundingBox();
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage(ex.Message);
                        }
                        break;
                    case "6":
                        bContinue = false;
                        break;
                    default:
                        DisplayMessage("Please select a valid option.");
                        break;
                }
            }
        }

        /// <summary>
        /// Given a finite number of discrete points in a cartesian plane, write a function that computes the area of 
        /// the smallest bounding box which includes all provided points.
        /// </summary>
        private static void BoundingBox()
        {
            var pPoints = new Point[5];
            Console.Clear();
            Console.WriteLine("Introduce 5 points to calculate bonding box:");
            for (int i = 0; i < pPoints.Length; i++)
            {
                var validX = false;
                var validY = false;
                pPoints[i] = new Point();
                do
                {
                    if (!validX)
                    {
                        Console.Write($"X{(i + 1).ToString()}:");
                        validX = int.TryParse(Console.ReadLine(), out int x);
                        if (!validX)
                        {
                            Console.Write("Please provide a valid int.");
                            continue;
                        }
                        pPoints[i].X = x;
                    }

                    if (!validY)
                    {
                        Console.Write($"Y{(i + 1).ToString()}:");
                        validY = int.TryParse(Console.ReadLine(), out int y);
                        if (!validY)
                        {
                            Console.Write("Please provide a valid int.");
                            continue;
                        }
                        pPoints[i].Y = y;
                    }
                }
                while (!(validX && validY));
            }
            var area = GetBase(pPoints) * GetHeight(pPoints);
            DisplayMessage($"Smallest bounding box area which includes all points is {area.ToString()}");
        }

        /// <summary>
        /// Gets the magnitude of the height of the smallest bounding box for a Points array
        /// </summary>
        /// <param name="pPoints"></param>
        /// <returns></returns>
        private static int GetHeight(Point[] pPoints)
        {
         
            return Math.Abs(pPoints.GetYArray().Min()) + Math.Abs(pPoints.GetYArray().Max());
        }

        /// <summary>
        /// Gets the magnitude of the base of the smallest bounding box for a Points array
        /// </summary>
        /// <param name="pPoints"></param>
        /// <returns></returns>
        private static int GetBase(Point[] pPoints)
        {
            return Math.Abs(pPoints.GetXArray().Min()) + Math.Abs(pPoints.GetXArray().Max());
        }

        /// <summary>
        /// Write a function that receives a string and validates if all the next brackets '[', '(' are properly closed '), ']'
        /// </summary>
        private static void BracketsValidation()
        {
            Console.Clear();
            Console.Write("Introduce an expression containing brackets and parentheses[]():");
            var s = Console.ReadLine();
            //I was going to put a regex to accept only parentheses and brackets
            //but in the end we only care about then and not the content
            //var matched = Regex.Matches(s, @"[a-zA-Z0-9]", RegexOptions.IgnoreCase).Count;
            //if (matched > 0)
            //throw new Exception("Please use only brackets and parentheses[]()");
            if (Regex.Matches(s, @"[\(\)\[\]]", RegexOptions.IgnoreCase).Count < 1)
                throw new Exception("Expression does not contain brackets");

            if (CheckBracketsBalance(s.ToArray()))
            {
                DisplayMessage("Expression's brackets are valid and balanced");
                return;
            }
            DisplayMessage("Expression's brackets are NOT valid or balanced");
        }

        private static bool CheckBracketsBalance(char[] cBrackets)
        {
            var stack = new Stack();
            for (int i = 0; i < cBrackets.Length; i++)
            {
                if (cBrackets[i] == '(' || cBrackets[i] == '[')
                {
                    stack.Push(cBrackets[i]);
                }
                else
                {
                    switch (cBrackets[i])
                    {
                        case ')':
                            if ((char)stack.Peek() == '(')
                            {
                                stack.Pop();
                            }
                            break;
                        case ']':
                            if ((char)stack.Peek() == '[')
                            {
                                stack.Pop();
                                
                            }
                            break;
                    }
                }
            }
            if (stack.Count == 0)
                return true;
            return false;
        }

        /// <summary>
        /// Write a function to perform basic string compression using the counts of repeated characters; e.g "aabcccccaaa"
        ///would become "a2b1c5a3". If the compressed string would not become smaller than the original string, just print
        ///the original string.
        /// </summary>
        private static void StringCompression()
        {
            Console.Clear();
            Console.Write("Introduce string to compress:");
            var s = Console.ReadLine().ToArray();
            int cont = 1;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {


                if (i + 1 < s.Length && s[i] == s[i + 1])
                {
                    cont++;
                }
                else
                {
                    sb.Append(s[i]);
                    if (cont > 1)
                    {
                        sb.Append(cont.ToString());
                        cont = 1;
                    }
                }

            }
            if (sb.ToString().Length < s.Length)
            {
                DisplayMessage(sb.ToString());
                return;
            }
            DisplayMessage(new string(s));
        }

        /// <summary>
        /// Given 2 strings of unknown characters (but it cannot be repeated) create a function that returns an array of the
        ///characters that are repeated in both strings in the most efficient way.
        /// </summary>
        private static void RepeatedCharacters()
        {
            Console.Clear();
            Console.Write("Please provide a string:");
            var s1 = Console.ReadLine();
            Console.Write("Please provide a different string:");
            var s2 = Console.ReadLine();
            if (s1.ToUpper() == s2.ToUpper())
                throw new Exception("Strings must be different");
            s1 = new string(s1.Distinct().OrderBy(c => c).ToArray());
            s2 = new string(s2.Distinct().OrderBy(c => c).ToArray());
            var sIndex = s1.Length < s2.Length ? s1 : s2;
            var sCompare = s1.Length > s2.Length ? s1 : s2;
            StringBuilder sbResult = new StringBuilder();
            for (int i = 0; i < sIndex.Length; i++)
            {
                if (sCompare.Contains(sIndex.Substring(i, 1)))
                    sbResult.Append(sIndex.Substring(i, 1));
            }
            DisplayMessage($"Repeated characters in both strings are:{sbResult.ToString()}");


        }

        private static void DisplayMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }

        /// <summary>
        /// Write a function that receives a string, if possible transform it to a date and return whether the year of the date
        ///is a leap year.If the string can't be transformed to a date throw an exception. (Datetime.IsLeapYear() can’t be
        ///used)
        /// </summary>
        private static void LeapYear()
        {
            Console.Clear();
            Console.Write("Please provide a date to check:");
            LeapYearCheck(Console.ReadLine());
        }

        private static void LeapYearCheck(string sDate)
        {
            if (!DateTime.TryParse(sDate, out DateTime dDate))
            {
                throw new Exception("Invalid date.");
            }

            var isLeapYear = dDate.Year % 4 == 0 && (dDate.Year % 100 != 0 || dDate.Year % 400 == 0);
            DisplayMessage($"{dDate.Year.ToString()} is {(isLeapYear ? "" : "NOT ")}Leap year.");
        }

        private static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Anexinet C#/.NET Technical Test");
            Console.WriteLine("");
            Console.WriteLine("1) Leap year test");
            Console.WriteLine("2) Repeated characters");
            Console.WriteLine("3) String compression");
            Console.WriteLine("4) Brackets and parentheses validation");
            Console.WriteLine("5) Bounding box");
            Console.WriteLine("6) Exit");
            Console.Write("Option:");
        }
    }

    
    
}
