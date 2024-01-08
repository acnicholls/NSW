namespace NSW.Data.Validation
{
	public class ValidPostalCodes
	{
		private static List<PostalCode> _naganoPostalCodes = new List<PostalCode>();
		public static List<PostalCode> NaganoPostalCodes
		{
			get
			{
				return _naganoPostalCodes;
			}
			set
			{
				if (value == null)
				{
					return;
				}
				if(_naganoPostalCodes.Any())
				{
					return;
				}
				_naganoPostalCodes = value;
			}
		}
	}
}
