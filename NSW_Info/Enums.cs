﻿
using System;

namespace NSW
{
	[Obsolete]
    /// <summary>
    /// user role enums
    /// </summary>
    public enum Role
    {
        Admin,
        Member
    }

	[Obsolete]
	/// <summary>
	/// log importance enum
	/// </summary>
	public enum LogEnum
    {
        Debug = 0,
        Message = 1,
        Important = 2,
        Access = 3,
        Warning = 4,
        Error = 5,
        Critical = 6
    }

	[Obsolete]
	/// <summary>
	/// log location enum
	/// </summary>
	public enum LogTypeEnum
    {
        File,
        Database
    }

	[Obsolete]
	/// <summary>
	/// wizard step navigation container enum
	/// </summary>
	public enum WizardNavigationTempContainer
    {
        StartNavigationTemplateContainerID = 1,
        StepNavigationTemplateContainerID = 2,
        FinishNavigationTemplateContainerID = 3
    }

	[Obsolete]
	/// <summary>
	/// user language option enum
	/// </summary>
	public enum LanguagePreference
    {
        English = 2,
        Japanese = 1
    }
}
