﻿
namespace NSW
{
    public enum Role
    {
        Admin,
        Member
    }
    public enum ModeEnum
    {
        Add,
        Edit,
        View
    }
    public enum DataStorageType
    {
        xml,
        mssql,
        mysql
    }
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

    public enum LogTypeEnum
    {
        File,
        Database
    }

    public enum WizardNavigationTempContainer
    {
        StartNavigationTemplateContainerID = 1,
        StepNavigationTemplateContainerID = 2,
        FinishNavigationTemplateContainerID = 3
    }

    public enum LanguagePreference
    {
        English = 2,
        Japanese = 1
    }
}
