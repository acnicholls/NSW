namespace NSW.Data.Validation.Interfaces
{
	public interface IPostalCodeTask
	{
		void StartBackgroundPostalCodeWorker(ApiAccessType accessType);
	}
}
