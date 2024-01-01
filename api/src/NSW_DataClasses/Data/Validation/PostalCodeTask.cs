using Microsoft.Extensions.Logging;
using NSW.Data.Internal.Interfaces;
using NSW.Data.Validation.Interfaces;
using System.ComponentModel;


namespace NSW.Data.Validation
{
	public class PostalCodeTask : IPostalCodeTask
	{

		private readonly BackgroundWorker _worker;
		private readonly IInternalDataTransferService _internalDataTransferService;
		private readonly ILogger<PostalCodeTask> _logger;
		private ApiAccessType _accessType;


		public PostalCodeTask(
			IInternalDataTransferService dataService,
			ILogger<PostalCodeTask> logger)
		{
			_logger = logger;
			_internalDataTransferService = dataService;
			_worker = new BackgroundWorker();
			_worker.DoWork += DoBackgroundWorkerWork;
		}



		public void StartBackgroundPostalCodeWorker(ApiAccessType accessType)
		{
			_accessType = accessType;
			_worker.RunWorkerAsync();
		}


		private void DoBackgroundWorkerWork(object? sender, DoWorkEventArgs e)
		{
			_logger.LogTrace("Starting DoBackgroundWorkerWork.");
			while (!ValidPostalCodes.NaganoPostalCodes.Any())
			{
				try
				{
					_logger.LogTrace("Starting another attempt to acquire Valid Postal Code list.");
					// do the API call to get the postalcode list.
					var validPostalCodes = _internalDataTransferService.GetDataFromApiAsync<List<PostalCode>>("/api/PostalCode", _accessType).GetAwaiter().GetResult();
					// set the variable the validator will be looking at.
					ValidPostalCodes.NaganoPostalCodes = validPostalCodes;
					_logger.LogTrace("Valid Postal Codes successfully set.");
				}
				catch (Exception ex)
				{
					// log
					_logger.LogTrace(ex, "Acquiring Valid Postal Code list failed, trying again in 60 seconds....");
				}
				Thread.Sleep(60000);
			}
			_logger.LogTrace("Completing DoBackgroundWorkerWork.");
		}
	}
}
