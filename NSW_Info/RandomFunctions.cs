using System;
using System.Text;

namespace NSW.Info
{
    /// <summary>
    /// Creates random numbers, strings, passwords, and verify codes
    /// Useful for membership functions
    /// </summary>
    public class RandomFunctions
    {

        private static Random rand;

        public RandomFunctions()
        {

        }

        public static int RandomNumber(int min, int max)
        {
            int returnValue = 0;
            try
            {
                rand = new Random();
                returnValue = rand.Next(min, max);
            }
            catch (Exception x)
            {
                Log.WriteToLog(LogTypeEnum.File, "RandomNumber", x, LogEnum.Critical);
            }
            return returnValue;
        }

        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * rand.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        /// <summary>
        /// builds a random code to send to a new user
        /// </summary>
        /// <returns></returns>
        public static string BuildVerifyCode()
        {
            string verify = "";
            string randNum = RandomNumber(2, 210).ToString();
            string randString = RandomString(2, false);
            verify = randNum + randString;
            randNum = RandomNumber(845, 100374).ToString();
            randString = RandomString(3, true);
            verify += randNum;
            verify += randString;
            return verify;
        }
        /// <summary>
        /// builds a new password
        /// </summary>
        /// <returns></returns>
        public static string BuildNewPassword()
        {
            string password = "";
            string randNum = RandomNumber(4, 210).ToString();
            string randString = RandomString(2, false);
            password = randNum + randString;
            randNum = RandomNumber(845, 100374).ToString();
            randString = RandomString(5, true);
            password += randNum;
            password += randString;
            return password;
        }
    }

}
