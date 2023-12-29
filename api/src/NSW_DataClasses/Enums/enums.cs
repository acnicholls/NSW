﻿namespace NSW
{
	/// <summary>
	/// user language option enum
	/// </summary>
	public enum LanguagePreference
	{
		English = 2,
		Japanese = 1
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

	public enum ApiAccessType
	{
		Client,
		User
	}
}


