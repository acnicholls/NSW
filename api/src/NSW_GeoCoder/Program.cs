using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Xml;
using System.Configuration;

namespace NSW.GeoCoder
{
    class Program
    {
        /// <summary>
        /// this struct is used to hold both lat and long values in one parameter
        /// </summary>
        public class coords
        {
            public double longitude { get; set; }
            public double latitude { get; set; }
        }

        /// <summary>
        /// this is the connection to the database
        /// </summary>
        static SqlConnection nswConn = new SqlConnection(ConfigurationManager.ConnectionStrings[NSW.Info.ConnectionInfo.ConnectionString].ConnectionString);

        /// <summary>
        /// bool containing the true/false result of the Google API call.  
        /// if coords were found value stays true
        /// if no coords are returned, value changes to false
        /// </summary>
        static bool validCoords = true;

        /// <summary>
        /// main function of this program.
        /// reads postal codes from a text file
        /// sends a call to a google api that returns xml containing latitude/longitude within that postal code
        /// parses the returned xml into a struct
        /// reads the struct and commits the values to a database
        /// </summary>
        /// <param name="args">commandline arguments (unused)</param>
        static void Main(string[] args)
        {
            try
            {
                #region first remove the FK constraint on the users table
                NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Main", "Removing Constraint...", NSW.LogEnum.Message);
                SqlCommand fkComm = nswConn.CreateCommand();
                fkComm.CommandType = CommandType.Text;
                string sqlString = "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsers_tblPostalCodes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsers]'))";
                sqlString += "  ALTER TABLE [dbo].[tblUsers] DROP CONSTRAINT [FK_tblUsers_tblPostalCodes]";
                fkComm.CommandText = sqlString;
                nswConn.Open();
                fkComm.ExecuteNonQuery();
                nswConn.Close();
                NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Main", "Constraint Removed!", NSW.LogEnum.Message);
                #endregion
                #region now add all the new postal codes.
                // load the file 
                string fileLocation = Environment.CurrentDirectory.ToString() + @"\postalcodes.txt";
                StreamReader sr = new StreamReader(fileLocation);
                string line = "";
                string code = "";
                // first we pull ALL the zones into the database from the text file line by line
                Console.WriteLine("Press enter if you're ready to read file?");
                Console.Read();
                // read the file line by line
                while (sr.Peek() > -1)
                {
                    line = sr.ReadLine().Trim();
                    NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Main", "checking for : " + line, NSW.LogEnum.Message);
                    code = line.Remove(3, 1);
                    // check for duplicate postal code with the hyphen in the postal code
                    bool with = CheckForDuplicate(line);
                    NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Main", "with :" + with.ToString(), NSW.LogEnum.Debug);
                    // check for duplicate postal code without the hyphen
                    bool without = CheckForDuplicate(code);
                    NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Main", "without :" + without.ToString(), NSW.LogEnum.Debug);
                    // if it doesn't exist in the database at all
                    if (!with & !without)
                    {
                        #region add to db
                        // now take those values and put together a google maps api call to find a lat/long
                        coords postalCoords = APICALL(code);
                        if (validCoords)
                        {
                            NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Main", "Coords found, inserting new code : " + line, NSW.LogEnum.Message);
                            //Console.WriteLine("Latitude : {0}", postalCoords.latitude);
                            //Console.WriteLine("Longitude : {0}", postalCoords.longitude);
                            SqlCommand nswComm = nswConn.CreateCommand();
                            nswComm.CommandType = CommandType.StoredProcedure;
                            nswComm.CommandText = "insertPostalCode";
                            // declare SQL parameters
                            SqlParameter param = new SqlParameter("@code", line);
                            nswComm.Parameters.Add(param);
                            param = new SqlParameter("@lat", postalCoords.latitude);
                            nswComm.Parameters.Add(param);
                            param = new SqlParameter("@long", postalCoords.longitude);
                            nswComm.Parameters.Add(param);
                            // now send that to the db
                            if (nswConn.State == ConnectionState.Closed)
                                nswConn.Open();
                            int result = nswComm.ExecuteNonQuery();
                            nswConn.Close();
                            if (result == 1)
                                Console.WriteLine("Postal Code Inserted : {0}", code);
                            else
                            {
                                Console.WriteLine("Error Encountered");
                                NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Main", "Error encountered Inserting new code with coords", NSW.LogEnum.Error);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error Encountered");
                            NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Main", "Error getting coords from google", NSW.LogEnum.Error);
                        }
                        #endregion
                    }
                    else
                    {
                        if (without)
                        {
                            Console.WriteLine("Found duplicate........................updating.");
                            SqlCommand updComm = nswConn.CreateCommand();
                            updComm.CommandType = CommandType.Text;
                            updComm.CommandText = "Update tblPostalCodes set fldPostal_Code = '" + line + "' where fldPostal_Code='" + code + "'";
                            nswConn.Open();
                            int result = updComm.ExecuteNonQuery();
                            nswConn.Close();
                            NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Main", "update duplicate result:" + result.ToString(), NSW.LogEnum.Message);
                            if (result != 1)
                            {
                                Console.WriteLine("Error updating duplicate....");
                                Exception x = new Exception("Error updating duplicate..." + code);
                                throw x;
                            }
                        }
                        if (with)
                        {
                            Console.WriteLine("Found duplicate........................continuing.");
                            NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Main", "found duplicate :" + line, NSW.LogEnum.Message);
                        }
                    }
                }
                sr.Close();
                #endregion
                #region next modify all the postal codes in tblusers
                // testing data had no hypen in the postal codes.  hyphens were found to be required.
                NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Main", "Starting tblUser data modification...", NSW.LogEnum.Message);
                fkComm = nswConn.CreateCommand();
                fkComm.CommandType = CommandType.Text;
                fkComm.CommandText = "SELECT * from tblUsers";
                SqlDataAdapter adap = new SqlDataAdapter(fkComm);
                DataSet ds = new DataSet();
                nswConn.Open();
                adap.Fill(ds);
                nswConn.Close();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string oldCode = dr["fldUser_PostalCode"].ToString().Trim();
                    string newCode = "";
                    int x = 0;
                    foreach(char c in oldCode)
                    {
                        newCode += c.ToString();
                        if(x == 2)
                            newCode += "-";
                        x++;
                    }
                   // now put the new code back
                    fkComm.CommandText = "UPDATE tblUsers set fldUser_PostalCode='" + newCode + "' where fldUser_id=" + dr["fldUser_id"].ToString();
                    nswConn.Open();
                    fkComm.ExecuteNonQuery();
                    nswConn.Close();
                }
                NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Main", "tblUser data modified!", NSW.LogEnum.Message);
                #endregion
                #region last, re-add the FK contraint on tblUsers
                NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Main", "Re-adding Constraint...", NSW.LogEnum.Message);
                fkComm = nswConn.CreateCommand();
                fkComm.CommandType = CommandType.Text;
                sqlString = "ALTER TABLE [dbo].[tblUsers]  WITH CHECK ADD  CONSTRAINT [FK_tblUsers_tblPostalCodes] FOREIGN KEY([fldUser_PostalCode])";
                sqlString += " REFERENCES [dbo].[tblPostalCodes] ([fldPostal_Code])";
                fkComm.CommandText = sqlString;
                nswConn.Open();
                fkComm.ExecuteNonQuery();
                sqlString = "ALTER TABLE [dbo].[tblUsers] CHECK CONSTRAINT [FK_tblUsers_tblPostalCodes]";
                fkComm.CommandText = sqlString;
                fkComm.ExecuteNonQuery(); 
                nswConn.Close();
                NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Main", "Constraint Re-added!", NSW.LogEnum.Message);
                Console.WriteLine("Finished...Press Enter to close.");
                Console.ReadLine();
                #endregion
            }
            catch (Exception x)
            {
                NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Main", x, NSW.LogEnum.Critical);
            }
        }

        /// <summary>
        /// reads the API password/key from a local text file
        /// saves my key from being overused by others with this code
        /// </summary>
        private static string APIKey
        {
            get
            {
                StreamReader sr = new StreamReader(Environment.CurrentDirectory.ToString() + @"\APIKEY.txt");
                string returnValue = sr.ReadLine().Trim();
                sr.Close();
                return returnValue;
            }
        }

        /// <summary>
        /// sends a call to a google maps API that returns an xml based latitude/longitude set of coordinates
        /// which are somewhere within the specified postal code
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        private static coords APICALL(string searchString)
        {
            try
            {
                string output = "xml";
                string URL = "https://maps.googleapis.com/maps/api/geocode/";
                string parameters = "components=country:jp|postal_code:" + searchString;  
                string requestString = URL + output + "?" + parameters + "&sensor=false&key=" + APIKey;
                NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "APICALL", "Request : " + requestString, NSW.LogEnum.Debug);
                // now send the request and get back the XML dataset.
                var client = new WebClient();
                return ParseResult(client.DownloadString(requestString));
            }
            catch (Exception x)
            {
                NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "APICALL", x, NSW.LogEnum.Critical);
            }
            return null;
        }

        /// <summary>
        /// parses the xml returned from Google maps API containing geocoordinates within the postal code sent
        /// </summary>
        /// <param name="result">xml string returned from google maps</param>
        /// <returns>coordinates struct</returns>
        private static coords ParseResult(string result)
        {
            NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "ParseResult", "Response:" + result, NSW.LogEnum.Debug);
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
                            validCoords = true;
                            var resultNode = geocodeResponseNode.SelectSingleNode("result");
                            var geometryNode = resultNode.SelectSingleNode("geometry");
                            var locationNode = geometryNode.SelectSingleNode("location");
                            coordValues.longitude = Convert.ToDouble(locationNode.SelectSingleNode("lng").InnerText);
                            coordValues.latitude = Convert.ToDouble(locationNode.SelectSingleNode("lat").InnerText);
                            return coordValues;
                        }
                        else
                        {
                            validCoords = false;
                            Console.WriteLine("Status Not OK");
                            NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "ParseResult", "Status not OK, result :" + result, NSW.LogEnum.Debug);
                        }
                    }
                }
            }
            catch (Exception x)
            {
                NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "ParseResult", x, NSW.LogEnum.Critical);
            }
            return null;
        }

        /// <summary>
        /// checks to see if the postal code passed in is already in the database
        /// </summary>
        /// <param name="code">postal code to check for</param>
        /// <returns>true/false</returns>
        private static bool CheckForDuplicate(string code)
        {
            try
            {
                SqlCommand nswComm = nswConn.CreateCommand();
                nswComm.CommandType = CommandType.Text;
                nswComm.CommandText = "Select fldPostal_Code from tblPostalCodes where fldPostal_code='" + code + "'";
                // declare SQL parameters
                // now send that to the db
                if (nswConn.State == ConnectionState.Closed)
                    nswConn.Open();
                object result = nswComm.ExecuteScalar();
                nswConn.Close();
                if (result != null)
                {
                    string foundCode = result.ToString();
                    if (foundCode == code)
                        return true;
                }
                else
                    return false;
            }
            catch (Exception x)
            {
                NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Main", x, NSW.LogEnum.Critical);
                if (nswConn.State == ConnectionState.Open)
                    nswConn.Close();
            }
            return true;
        }

    }
}
