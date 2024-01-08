
using Microsoft.Extensions.Configuration;
using NSW.Info.Interfaces;
using System.Net;
using System.Xml;
using NSW.GeoCoder.Data;
using NSW.GeoCoder.Interfaces;

namespace NSW.GeoCoder;

public class ApiCall : IApiCall
{
	private readonly IConfiguration _config;
	private readonly IProjectInfo _projectInfo;
	private readonly ILog _log;

	public ApiCall(
		IConfiguration configuration,
		IProjectInfo projectInfo,
		ILog log
		)
	{
		_config = configuration;
		_projectInfo = projectInfo;
		_log = log;
	}
	/// <summary>
	/// reads the API password/key from a local text file
	/// saves my key from being overused by others with this code
	/// </summary>
	private string APIKey
	{
		get
		{
			return _config.GetSection("ApiKey").Value;
			//StreamReader sr = new StreamReader(Environment.CurrentDirectory.ToString() + @"\APIKEY.txt");
			//string returnValue = sr.ReadLine().Trim();
			//sr.Close();
			//return returnValue;
		}
	}

	/// <summary>
	/// sends a call to a google maps API that returns an xml based latitude/longitude set of coordinates
	/// which are somewhere within the specified postal code
	/// </summary>
	/// <param name="searchString"></param>
	/// <returns></returns>
	public coords APICALL(string searchString)
	{
		try
		{
			string output = "xml";
			string URL = "https://maps.googleapis.com/maps/api/geocode/";
			string parameters = "components=country:jp|postal_code:" + searchString;
			string requestString = URL + output + "?" + parameters + "&sensor=false&key=" + APIKey;
			_log.WriteToLog(_projectInfo.ProjectLogType, "APICALL", "Request : " + requestString, NSW.LogEnum.Debug);
			// now send the request and get back the XML dataset.
			var client = new WebClient();
			return ParseResult(client.DownloadString(requestString));
		}
		catch (Exception x)
		{
			_log.WriteToLog(_projectInfo.ProjectLogType, "APICALL", x, NSW.LogEnum.Critical);
		}
		return null;
	}

	/// <summary>
	/// parses the xml returned from Google maps API containing geocoordinates within the postal code sent
	/// </summary>
	/// <param name="result">xml string returned from google maps</param>
	/// <returns>coordinates struct</returns>
	private coords? ParseResult(string result)
	{
		_log.WriteToLog(_projectInfo.ProjectLogType, "ParseResult", "Response:" + result, NSW.LogEnum.Debug);
		var coordValues = new coords();
		try
		{
			var xmlDoc = new XmlDocument { InnerXml = result };
			if (xmlDoc.HasChildNodes)
			{
				var geocodeResponseNode = xmlDoc.SelectSingleNode("GeocodeResponse");
				if (geocodeResponseNode != null)
				{
					var statusNode = geocodeResponseNode.SelectSingleNode("status");
					if (statusNode != null && statusNode.InnerText.Equals("OK"))
					{
						var resultNode = geocodeResponseNode.SelectSingleNode("result");
						var geometryNode = resultNode.SelectSingleNode("geometry");
						var locationNode = geometryNode.SelectSingleNode("location");
						coordValues.longitude = Convert.ToDouble(locationNode.SelectSingleNode("lng").InnerText);
						coordValues.latitude = Convert.ToDouble(locationNode.SelectSingleNode("lat").InnerText);
						return coordValues;
					}
					else
					{
						Console.WriteLine("Status Not OK");
						_log.WriteToLog(_projectInfo.ProjectLogType, "ParseResult", "Status not OK, result :" + result, NSW.LogEnum.Debug);
					}
				}
			}
		}
		catch (Exception x)
		{
			_log.WriteToLog(_projectInfo.ProjectLogType, "ParseResult", x, NSW.LogEnum.Critical);
		}
		return null;
	}

}