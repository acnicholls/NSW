using Microsoft.Extensions.Logging;
using NSW.Data.Internal.Interfaces;
using NSW.Data.Validation.Interfaces;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;


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
					var token = _internalDataTransferService.GetTokenStringAsync(_accessType).GetAwaiter().GetResult();
					var validPostalCodes = _internalDataTransferService.GetDataFromApiAsync<List<PostalCode>>("/api/PostalCode", token).GetAwaiter().GetResult();
					//var validPostalCodes = _internalDataTransferService.GetDataFromApiAsync<List<PostalCode>>("/api/PostalCode", _accessType).GetAwaiter().GetResult();
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

		//private async Task<List<PostalCode>> GetPostalCodesBasedOnAccessTypeAsync(ApiAccessType accessType)
		//{
		//	var returnValue = new List<PostalCode>();
		//	switch (accessType)
		//	{
		//		case ApiAccessType.Client:
		//			{
		//				break;
		//			}
		//		case ApiAccessType.Idp:
		//			{
		//				var token = await _internalDataTransferService.GetTokenStringAsync(accessType);
		//				returnValue = await _internalDataTransferService.GetDataFromApiAsync<List<PostalCode>>("/api/PostalCode", token);
		//				break;
		//			}
		//		default:
		//			{
		//				break;
		//			}

		//	}
		//}
	}
}
