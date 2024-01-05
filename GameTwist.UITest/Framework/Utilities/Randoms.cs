using System;
using System.Linq;

namespace GTAutomation.Framework.Utilities
{
    public class Randoms
    {
        private static Random random = new Random();

        /// <summary>
        /// To get random string for given length
        /// </summary>
        /// <param name="length"> Length of String</param>
        /// <returns>Random String</returns>
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(str => str[random.Next(str.Length)]).ToArray());
        }

        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();

        /// <summary>
        /// To get random number between min to max
        /// </summary>
        /// <param name="min">minimum number</param>
        /// <param name="max">maximum number</param>
        /// <returns></returns>
        public static string GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            {   // synchronize
                return Convert.ToString(getrandom.Next(min, max));
            }
        }
    }
}
