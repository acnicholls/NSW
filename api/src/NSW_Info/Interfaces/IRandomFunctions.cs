namespace NSW.Info.Interfaces
{
	public interface IRandomFunctions
	{
		int RandomNumber(int min, int max);
		string RandomString(int size, bool lowerCase);
		string BuildVerifyCode();
		string BuildNewPassword();
	}
}
