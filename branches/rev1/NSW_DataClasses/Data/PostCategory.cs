﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace NSW.Data
{
    public class PostCategory
    {
        public int ID { get; set; }
        public string EnglishTitle { get; set; }
        public string JapaneseTitle { get; set; }
        public string EnglishDescription { get; set; }
        public string JapaneseDescription { get; set; }

        SqlConnection catConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);

        public PostCategory()
        {

        }

        public PostCategory(int ID)
        {
            try
            {
                SqlCommand catComm = catConn.CreateCommand();
                catComm.CommandType = CommandType.Text;
                catComm.CommandText = "Select * from tblPostCategories where fldPostCategory_id=" + ID.ToString();
                SqlDataAdapter adap = new SqlDataAdapter(catComm);
                DataSet ds = new DataSet();
                catConn.Open();
                adap.Fill(ds);
                catConn.Close();
                // now fill the values
                if (ds.Tables[0].Rows.Count == 1)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    this.ID = Convert.ToInt32(dr["fldPostCategory_id"]);
                    this.EnglishTitle = dr["fldPostCategory_English"].ToString();
                    this.JapaneseTitle = dr["fldPostCategory_Japanese"].ToString();
                    this.EnglishDescription = dr["fldPostCategory_DescEnglish"].ToString();
                    this.JapaneseDescription = dr["fldPostCategory_DescJapanese"].ToString();
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "PostCategory.ByID", x, LogEnum.Critical);
            }
        }

        public static string Title(int ID)
        {
            SqlConnection textConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
            try
            {
                DataSet ds = new DataSet();
                SqlCommand labelComm = textConn.CreateCommand();
                SqlDataAdapter adap = new SqlDataAdapter(labelComm);
                labelComm.CommandType = CommandType.Text;
                labelComm.CommandText = "Select * from tblPostCategories where fldPostCategory_id=" + ID;
                // first find the user row in the database
                textConn.Open();
                adap.Fill(ds);
                textConn.Close();
                // assign values
                DataRow dr = ds.Tables[0].Rows[0];
                switch (LabelText.DisplayLanguage)
                {
                    case "English":
                        {
                            return dr["fldPostCategory_English"].ToString();
                        }
                    case "Japanese":
                        {
                            return dr["fldPostCategory_Japanese"].ToString();
                        }
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "PostCategory.Title", x, LogEnum.Critical);
            }
            return string.Empty;
        }

        public static string Description(int ID) 
        {
            SqlConnection textConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
            try
            {
                DataSet ds = new DataSet();
                SqlCommand labelComm = textConn.CreateCommand();
                SqlDataAdapter adap = new SqlDataAdapter(labelComm);
                labelComm.CommandType = CommandType.Text;
                labelComm.CommandText = "Select * from tblPostCategories where fldPostCategory_id=" + ID;
                // first find the user row in the database
                textConn.Open();
                adap.Fill(ds);
                textConn.Close();
                // assign values
                DataRow dr = ds.Tables[0].Rows[0];
                switch (LabelText.DisplayLanguage)
                {
                    case "English":
                        {
                            return dr["fldPostCategory_DescEnglish"].ToString();
                        }
                    case "Japanese":
                        {
                            return dr["fldPostCategory_DescJapanese"].ToString();
                        }
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "PostCategory.Description", x, LogEnum.Critical);
            }
            return string.Empty;
        }

        public void insertCategory()
        {
            try
            {
                SqlCommand catComm = catConn.CreateCommand();
                catComm.CommandType = CommandType.StoredProcedure;
                catComm.CommandText = "insertPostCategory";
                // now declare parameters
                SqlParameter param = new SqlParameter("@english", this.EnglishTitle);
                catComm.Parameters.Add(param);
                if (this.EnglishDescription.Length > 0)
                    param = new SqlParameter("@descEnglish", this.EnglishDescription);
                else
                    param = new SqlParameter("@descEnglish", string.Empty);
                catComm.Parameters.Add(param);
                if (this.JapaneseTitle.Length > 0)
                    param = new SqlParameter("@japanese", this.JapaneseTitle);
                else
                    param = new SqlParameter("@japanese", string.Empty);
                catComm.Parameters.Add(param);
                if (this.JapaneseDescription.Length > 0)
                    param = new SqlParameter("@descJapanese", this.JapaneseDescription);
                else
                    param = new SqlParameter("@descJapanese", string.Empty);
                catComm.Parameters.Add(param);
                // now execute the procedure
                catConn.Open();
                int result = catComm.ExecuteNonQuery();
                catConn.Close();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "PostCategory.insertCategory", x, LogEnum.Critical);
            }
        }

        public void modifyCategory()
        {
            try
            {
                SqlCommand catComm = catConn.CreateCommand();
                catComm.CommandType = CommandType.StoredProcedure;
                catComm.CommandText = "modifyPostCategory";
                // now declare parameters
                SqlParameter param = new SqlParameter("@id", this.ID);
                catComm.Parameters.Add(param);
                param = new SqlParameter("@english", this.EnglishTitle);
                catComm.Parameters.Add(param);
                if (this.EnglishDescription.Length > 0)
                    param = new SqlParameter("@descEnglish", this.EnglishDescription);
                else
                    param = new SqlParameter("@descEnglish", string.Empty);
                catComm.Parameters.Add(param);
                if (this.JapaneseTitle.Length > 0)
                    param = new SqlParameter("@japanese", this.JapaneseTitle);
                else
                    param = new SqlParameter("@japanese", string.Empty);
                catComm.Parameters.Add(param);
                if (this.JapaneseDescription.Length > 0)
                    param = new SqlParameter("@descJapanese", this.JapaneseDescription);
                else
                    param = new SqlParameter("@descJapanese", string.Empty);
                catComm.Parameters.Add(param);
                // now execute the procedure
                catConn.Open();
                int result = catComm.ExecuteNonQuery();
                catConn.Close();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "PostCategory.modifyCategory", x, LogEnum.Critical);
            }
        }
    }
}
