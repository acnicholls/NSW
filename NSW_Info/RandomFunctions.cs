using NSW.Info.Interfaces;
using System.Text;
using ILog = NSW.Info.Interfaces.ILog;

namespace NSW.Info
{
	/// <summary>
	/// Creates random numbers, strings, passwords, and verify codes
	/// Useful for membership functions
	/// </summary>
	public class RandomFunctions : IRandomFunctions
    {

        private  Random rand;
		private readonly ILog Log;

        public RandomFunctions(ILog log)
        {
			this.rand = new Random();
			this.Log = log;
        }

        public  int RandomNumber(int min, int max)
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

        public  string RandomString(int size, bool lowerCase)
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
        public  string BuildVerifyCode()
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
        public  string BuildNewPassword()
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
