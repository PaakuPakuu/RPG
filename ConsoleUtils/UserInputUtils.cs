using System;

namespace GeneralUtils
{
    public static class UserInputUtils
    {
        private static readonly string NOT_A_NUMBER_M = "Veuillez entrer un nombre.";
        private static readonly string NOT_IN_SCOPE_NUMBER_M = "La valeur doit être comprise entre {0} et {1}.";
        private static readonly string NOT_IN_SCOPE_OR_ZERO_NUMBER_M = "La valeur doit être comprise entre {0} et {1}, ou doit être nulle.";

        public static bool GetNumber(out int number, out string message)
        {
            if (!int.TryParse(Console.ReadLine(), out number))
            {
                message = NOT_A_NUMBER_M;
                return false;
            } else
            {
                message = "";
                return true;
            }
        }

        public static bool GetNumberIn(int min, int max, out int number, out string message)
        {
            if (!GetNumber(out number, out message))
            {
                return false;
            }

            if (number < min || number > max)
            {
                message = String.Format(NOT_IN_SCOPE_NUMBER_M, min, max);
                return false;
            }

            return true;
        }

        public static bool GetNumberInOrZero(int min, int max, out int number, out string message)
        {
            if (!GetNumber(out number, out message))
            {
                return false;
            }

            if (number != 0 && (number < min || number > max))
            {
                message = String.Format(NOT_IN_SCOPE_OR_ZERO_NUMBER_M, min, max);
                return false;
            }

            return true;
        }

        public static string GetInputAt(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.CursorVisible = true;
            string input = Console.ReadLine();
            Console.CursorVisible = false;

            return input;
        }
    }
}
