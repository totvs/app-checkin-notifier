using System.Text.RegularExpressions;

namespace App.CheckIn.Domain.ValuesObjects
{
    public class Email
    {
        private static readonly Regex _emailRegex = new Regex(@"[^@]+@[^\.]+\..+");

        /// <summary>
        /// Answer if the giving <see cref="string"/> is a valid email
        /// </summary>
        /// <param name="email">the <see cref="string"/> to validate</param>
        /// <returns>True if is valid, false is not</returns>
        public static bool IsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return _emailRegex.IsMatch(email);
        }
    }
}
