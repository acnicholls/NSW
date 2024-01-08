namespace NSW.Api
{
	public static class Encryption
	{

		/// <summary>
		/// this method converts the Base64 content of the key file to a machine readable RSA private key.
		/// </summary>
		/// <param name="fileName">the location of the key</param>
		/// <returns>a byte array.</returns>
		public static byte[] PemBytes(string fileName) =>
			Convert.FromBase64String(
				File.ReadAllLines(fileName)
				.Where(l => !l.Contains('-'))
				.Aggregate("", (current, next) => current + next));
	}
}
