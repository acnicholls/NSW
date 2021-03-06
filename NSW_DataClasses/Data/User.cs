﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace NSW.Data
{

    public class User
    {
        SqlConnection userConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);

        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }

        /// <summary>
        /// gets the role of the current user
        /// </summary>
        private string userRole
        {
            get
            {
                try
                {
                    string strSql = "Select fldUser_Role from tblUsers where fldUser_id=" + this.ID;
                    SqlConnection roleConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
                    SqlCommand roleComm = roleConn.CreateCommand();
                    roleComm.CommandType = CommandType.Text;
                    roleComm.CommandText = strSql;
                    roleConn.Open();
                    object result = roleComm.ExecuteScalar();
                    roleConn.Close();
                    // if value is admin then user is admin
                    string strResult = result.ToString();
                    //Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "User.userRole", strResult, LogEnum.Debug);
                    return strResult;
                }
                catch (Exception x)
                {
                    Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "User.userRole", x, LogEnum.Critical);
                    return null;
                }
            }
        }
        public int LanguagePreference { get; set; }

        public Role UserRole
        {
            get
            {
                    switch (userRole)
                    {
                        case "ADMIN":
                            {
                                return Role.Admin;
                            }
                        case "MEMBER":
                            {
                                return Role.Member;
                            }
                        default:
                            {
                                return Role.Member;
                            }
                    }
                }
            }
        

        public User()
        {
        }

        /// <summary>
        /// builds a user object based on the ID of the user row
        /// </summary>
        /// <param name="id">integer ID of the user row</param>
        public User(int id)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand userComm = userConn.CreateCommand();
                SqlDataAdapter adap = new SqlDataAdapter(userComm);
                userComm.CommandType = CommandType.Text;
                userComm.CommandText = "Select * from tblUsers where fldUser_id=" + id.ToString();
                // first find the user row in the database
                userConn.Open();
                adap.Fill(ds);
                userConn.Close();
                // assign values
                DataRow dr = ds.Tables[0].Rows[0];
                this.ID = id;
                this.Name = dr["fldUser_Name"].ToString();
                this.Password = dr["fldUser_Password"].ToString();
                this.Phone = dr["fldUser_Phone"].ToString();
                this.PostalCode = dr["fldUser_PostalCode"].ToString();
                this.Email = dr["fldUser_Email"].ToString();
                this.LanguagePreference = Convert.ToInt32(dr["fldUser_langPref"]);
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "User.ByID", x, LogEnum.Critical);
            }
            }

        /// <summary>
        /// builds a user object based on the email and password of the desired user
        /// </summary>
        /// <param name="email">email of user</param>
        /// <param name="password">password of user</param>
        public User(string email, string password)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand userComm = userConn.CreateCommand();
                SqlDataAdapter adap = new SqlDataAdapter(userComm);
                userComm.CommandType = CommandType.Text;
                userComm.CommandText = "Select * from tblUsers where fldUser_Email='" + email + "' and fldUser_Password='" + password + "'";
                // first find the user row in the database
                userConn.Open();
                adap.Fill(ds);
                userConn.Close();
                if (ds.Tables[0].Rows.Count == 1)
                {
                    // assign values
                    DataRow dr = ds.Tables[0].Rows[0];
                    this.ID = Convert.ToInt32(dr["fldUser_id"]);
                    this.Name = dr["fldUser_Name"].ToString();
                    this.Password = dr["fldUser_Password"].ToString();
                    this.Phone = dr["fldUser_Phone"].ToString();
                    this.PostalCode = dr["fldUser_PostalCode"].ToString();
                    this.Email = dr["fldUser_Email"].ToString();
                    this.LanguagePreference = Convert.ToInt32(dr["fldUser_langPref"]);
                }
                else
                    this.ID = 0;
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "User.ByEmailAndPassword", x, LogEnum.Critical);
            }
            }

        /// <summary>
        /// builds user object based solely on user email
        /// </summary>
        /// <param name="email">email of desired user</param>
        public User(string email)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand userComm = userConn.CreateCommand();
                SqlDataAdapter adap = new SqlDataAdapter(userComm);
                userComm.CommandType = CommandType.Text;
                userComm.CommandText = "Select * from tblUsers where fldUser_Email='" + email + "'";
                // first find the user row in the database
                userConn.Open();
                adap.Fill(ds);
                userConn.Close();
                if (ds.Tables[0].Rows.Count == 1)
                {
                    // assign values
                    DataRow dr = ds.Tables[0].Rows[0];
                    this.ID = Convert.ToInt32(dr["fldUser_id"]);
                    this.Name = dr["fldUser_Name"].ToString();
                    this.Password = dr["fldUser_Password"].ToString();
                    this.Phone = dr["fldUser_Phone"].ToString();
                    this.PostalCode = dr["fldUser_PostalCode"].ToString();
                    this.Email = dr["fldUser_Email"].ToString();
                    this.LanguagePreference = Convert.ToInt32(dr["fldUser_langPref"]);
                }
                else
                    this.ID = 0;
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "User.ByEmail", x, LogEnum.Critical);
            }
            }

        /// <summary>
        /// saves new values to user data row
        /// </summary>
        public void modifyUser()
        {
            try
            {
                SqlCommand userComm = userConn.CreateCommand();
                userComm.CommandType = CommandType.StoredProcedure;
                userComm.CommandText = "modifyUser";
                // set all the parameters
                SqlParameter param = new SqlParameter();
                // assign values
                param = new SqlParameter("@ID", this.ID);
                userComm.Parameters.Add(param);
                param = new SqlParameter("@name", this.Name);
                userComm.Parameters.Add(param);
                param = new SqlParameter("@pass", this.Password);
                userComm.Parameters.Add(param);
                param = new SqlParameter("@email", this.Email);
                userComm.Parameters.Add(param);
                param = new SqlParameter("@phone", this.Phone);
                userComm.Parameters.Add(param);
                param = new SqlParameter("@postalcode", this.PostalCode);
                userComm.Parameters.Add(param);
                param = new SqlParameter("@langPref", this.LanguagePreference);
                userComm.Parameters.Add(param);
                // execute the command
                userConn.Open();
                userComm.ExecuteNonQuery();
                userConn.Close();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "User.modifyUser", x, LogEnum.Critical);
            }
            }

        /// <summary>
        /// deletes the current user data row
        /// </summary>
        public void deleteUser()
        {
            try
            {
                SqlCommand userComm = userConn.CreateCommand();
                userComm.CommandType = CommandType.StoredProcedure;
                userComm.CommandText = "deleteUser";
                // set all the parameters
                SqlParameter param = new SqlParameter();
                // assign values
                param = new SqlParameter("@ID", this.ID);
                userComm.Parameters.Add(param);
                // execute the command
                userConn.Open();
                userComm.ExecuteNonQuery();
                userConn.Close();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "User.deleteUser", x, LogEnum.Critical);
            }
            }

        /// <summary>
        /// saves a new user row
        /// </summary>
        public void insertUser()
        {
            try
            {
                SqlCommand userComm = userConn.CreateCommand();
                userComm.CommandType = CommandType.StoredProcedure;
                userComm.CommandText = "insertUser";
                // set all the parameters
                SqlParameter param = new SqlParameter();
                // assign values
                param = new SqlParameter("@name", this.Name);
                userComm.Parameters.Add(param);
                param = new SqlParameter("@pass", this.Password);
                userComm.Parameters.Add(param);
                param = new SqlParameter("@email", this.Email);
                userComm.Parameters.Add(param);
                param = new SqlParameter("@phone", this.Phone);
                userComm.Parameters.Add(param);
                param = new SqlParameter("@postalcode", this.PostalCode);
                userComm.Parameters.Add(param);
                param = new SqlParameter("@langPref", this.LanguagePreference);
                userComm.Parameters.Add(param);
                // execute the command
                userConn.Open();
                userComm.ExecuteNonQuery();
                userConn.Close();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "User.insertUser", x, LogEnum.Critical);
            }
            }

        /// <summary>
        /// checks to see if the input email already exists in the current dataset
        /// </summary>
        /// <param name="email">email to check for</param>
        /// <returns>true if found, false if not</returns>
        public static bool Exists(string email)
        {
            try
            {
                // check for username or email.
                string strSql = "Select * from tblUsers where fldUser_Email='" + email + "'";
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "User.Exists", strSql, LogEnum.Debug);
                SqlConnection existsConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
                SqlCommand existsComm = existsConn.CreateCommand();
                existsComm.CommandType = CommandType.Text;
                existsComm.CommandText = strSql;
                SqlDataAdapter adap = new SqlDataAdapter(existsComm);
                DataSet ds = new DataSet();
                existsConn.Open();
                adap.Fill(ds);
                existsConn.Close();
                // if any row then the username or email already exists
                int result = ds.Tables[0].Rows.Count;
                if (result > 0)
                    return true;
                else if (result == 0)
                    return false;
            }
            catch   (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "User.Exists", x, LogEnum.Critical);
            }
            return false;
        }

        /// <summary>
        /// changes user password to new value
        /// </summary>
        /// <param name="newPassword">new password to use</param>
        public void changePassword(string newPassword)
        {
            try
            {
                SqlCommand userComm = userConn.CreateCommand();
                userComm.CommandType = CommandType.StoredProcedure;
                userComm.CommandText = "modifyUserPassword";
                // set all the parameters
                SqlParameter param = new SqlParameter();
                // assign values
                param = new SqlParameter("@ID", this.ID);
                userComm.Parameters.Add(param);
                param = new SqlParameter("@newPass", newPassword);
                userComm.Parameters.Add(param);
                // execute the command
                userConn.Open();
                userComm.ExecuteNonQuery();
                userConn.Close();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "User.changePassword", x, LogEnum.Critical);
            }
            }
        }
    }
