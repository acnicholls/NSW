namespace NSW
{
	/// <summary>
	/// user language option enum
	/// </summary>
	public enum LanguagePreference
	{
		Japanese = 1,
		English = 2,
	}
	/// <summary>
	/// user role enums
	/// </summary>
	public enum Role
	{
		Admin,
		Member
	}

	public enum CustomClaimType
	{
		PostalCode,
		LanguagePreference
	}

	public enum DataTransferVaraintEnum
	{
		Tools,
		NoTools
	}

	public enum ApiAccessType
	{
		Client,
		User,
		Idp
	}
}


