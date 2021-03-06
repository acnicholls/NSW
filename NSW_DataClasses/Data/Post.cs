﻿using System;
using System.Data;
using System.Data.SqlClient;


namespace NSW.Data
{
    public class Post
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Expiry { get; set; }
        public int UserID { get; set; }
        public string Status { get; set; }
        public bool DeleteFlag { get; set; }

        private SqlConnection postConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);

        public Post()
        { }

        /// <summary>
        /// builds a post based on the integer ID
        /// </summary>
        /// <param name="identifier">ID of the post</param>
        public Post(int identifier)
        {
            try
            {
                SqlCommand postComm = postConn.CreateCommand();
                postComm.CommandType = CommandType.Text;
                postComm.CommandText = "Select * from tblPosts where fldPost_id=" + identifier.ToString();
                SqlDataAdapter adap = new SqlDataAdapter(postComm);
                DataSet ds = new DataSet();
                postConn.Open();
                adap.Fill(ds);
                postConn.Close();
                if (ds.Tables[0].Rows.Count == 1)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    this.ID = Convert.ToInt32(dr["fldPost_id"]);
                    this.CategoryID = Convert.ToInt32(dr["fldPost_CategoryID"]);
                    this.Title = dr["fldPost_Title"].ToString();
                    this.Description = dr["fldPost_Description"].ToString();
                    this.Price = Convert.ToDecimal(dr["fldPost_Price"]);
                    this.Expiry = Convert.ToDateTime(dr["fldPost_Expiry"]);
                    this.UserID = Convert.ToInt32(dr["fldPost_UserID"]);
                    this.Status = dr["fldPost_Status"].ToString();
                    this.DeleteFlag = Convert.ToBoolean(dr["fldPost_DeleteFlag"]);
                }
                else
                {
                    Exception ex = new Exception("There are either no rows, or too many rows with the same ID " + identifier.ToString());
                    throw ex;
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Post.ByID", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// saves a new post to the database
        /// </summary>
        public void insertPost()
        {
            try
            {
                SqlCommand postComm = postConn.CreateCommand();
                postComm.CommandType = CommandType.StoredProcedure;
                postComm.CommandText = "insertPost";
                SqlParameter param = new SqlParameter("@catID", this.CategoryID);
                postComm.Parameters.Add(param);
                param = new SqlParameter("@title", this.Title);
                postComm.Parameters.Add(param);
                param = new SqlParameter("@desc", this.Description);
                postComm.Parameters.Add(param);
                param = new SqlParameter("@price", this.Price);
                postComm.Parameters.Add(param);
                param = new SqlParameter("@userID", this.UserID);
                postComm.Parameters.Add(param);
                param = new SqlParameter("@ID", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                postComm.Parameters.Add(param);
                postConn.Open();
                int result = postComm.ExecuteNonQuery();
                postConn.Close();
                this.ID = Convert.ToInt32(param.Value);
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Post.insertPost", "Post inserted result : " + result.ToString(), LogEnum.Debug);
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Post.insertPost", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// saves new values to the current post
        /// </summary>
        public void modifyPost()
        {
            try
            {
                SqlCommand postComm = postConn.CreateCommand();
                postComm.CommandType = CommandType.StoredProcedure;
                postComm.CommandText = "modifyPost";
                SqlParameter param = new SqlParameter("@ID", this.ID);
                postComm.Parameters.Add(param);
                param = new SqlParameter("@title", this.Title);
                postComm.Parameters.Add(param);
                param = new SqlParameter("@desc", this.Description);
                postComm.Parameters.Add(param);
                param = new SqlParameter("@price", this.Price);
                postComm.Parameters.Add(param);
                param = new SqlParameter("@status", this.Status);
                postComm.Parameters.Add(param);
                postConn.Open();
                int result = postComm.ExecuteNonQuery();
                postConn.Close();
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Post.insertPost", "Post modified result : " + result.ToString(), LogEnum.Debug);
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Post.modifyPost", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// deletes the current post
        /// </summary>
        public void deletePost()
        {
            try
            {
                SqlCommand postComm = postConn.CreateCommand();
                postComm.CommandType = CommandType.StoredProcedure;
                postComm.CommandText = "deletePost";
                SqlParameter param = new SqlParameter("@ID", this.ID);
                postComm.Parameters.Add(param);
                postConn.Open();
                postComm.ExecuteNonQuery();
                postConn.Close();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Post.deletePost", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// gets the user object for the current post
        /// </summary>
        /// <returns>user object</returns>
        public NSW.Data.User PostUser()
        {
            try
            {
                if (this.UserID != 0)
                {
                    NSW.Data.User postUser = new User(this.UserID);
                    return postUser;
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Post.PostUser", x, LogEnum.Critical);
            }
            return new NSW.Data.User();
        }

        /// <summary>
        /// sends an email to the current posts user indicating post expiry
        /// </summary>
        public void SendExpiryEmail()
        {
            try
            {
                NSW.Info.EmailMessage email = new Info.EmailMessage();
                NSW.Data.User thisUser = PostUser();
                email.To.Add(thisUser.Email);
                email.Subject = NSW.Data.LabelText.Text("ExpiryEmail.Subject", thisUser);
                string strBody = NSW.Data.LabelText.Text("ExpiryEmail.Line1", thisUser) + " " + Title + "\r\n\r\n";
                strBody += "\r\n";
                strBody += NSW.Data.LabelText.Text("ExpiryEmail.Line2", thisUser);
                strBody += "\r\n\r\n";
                string strLink = NSW.Info.ProjectInfo.protocol + NSW.Info.ProjectInfo.webServer + "/Posts/RenewPost.aspx?postID=" + ID.ToString();
                strBody += strLink;
                strBody += "\r\n\r\n";
                strBody += NSW.Data.LabelText.Text("ExpiryEmail.Line3", thisUser) + "\r\n";
                strBody += NSW.Data.LabelText.Text("ExpiryEmail.Line4", thisUser);
                email.Body = strBody;
                email.Send();
                SetEmailSent();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Post.SendExpiryEmail", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// if email is successfully sent, sets flag in the database
        /// </summary>
        private void SetEmailSent()
        {
            try
            {
                SqlConnection sesConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
                SqlCommand sesComm = sesConn.CreateCommand();
                sesComm.CommandType = CommandType.Text;
                sesComm.CommandText = "update tblPosts set fldPost_emailSent=1 where fldPost_id=" + this.ID.ToString();
                sesConn.Open();
                sesComm.ExecuteNonQuery();
                sesConn.Close();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Post.SetEmailSent", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// checks to see if the current post should be active
        /// </summary>
        public bool IsActive
        {
            get
            {
                if (Status == "ACTIVE")
                    return true;
                else
                    return false;
            }
        }
    }
}
