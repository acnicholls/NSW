using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace NSW.Data.Validation
{
	[AttributeUsage(AttributeTargets.Property |
  AttributeTargets.Field, AllowMultiple = false)]
	sealed public class NaganoPostalCodeValidationAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			bool result = false;
			// an API call at the startup of the IDP is used to capture
			// the list of PostalCodes from the DB.  since this is a small list of only a few thousand items
			// memory seems to be the best place to keep it.  Should the App grow bigger
			// something else can be designed here.
			if (ValidPostalCodes.NaganoPostalCodes is not null 
				&& ValidPostalCodes.NaganoPostalCodes.Any() 
				&& ValidPostalCodes.NaganoPostalCodes.Contains(value))
			{
				result = true;
			}

			return result;
		}


		public override string FormatErrorMessage(string name)
		{
			return string.Format(CultureInfo.CurrentCulture,
			  ErrorMessageString, name);
		}
	}
}
