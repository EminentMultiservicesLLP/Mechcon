using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BISERP.Models.Login
{
    public partial class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        /// <summary>
        /// Checks if user with given password exists in the database
        /// </summary>
        /// <param name="_username">User name</param>
        /// <param name="_password">User password</param>
        /// <returns>True if user exist and password is correct</returns>
        public bool IsValid(string _username, string _password)
        {
            using (var cn = new SqlConnection(ConnectionString))
            {
                string _sql = @"SELECT * FROM [dbo].[User_Mst] WHERE [UserName] = @u";
                var cmd = new SqlCommand(_sql, cn);
                cmd.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = _username;
                
                cn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Dispose();
                    cmd.Dispose();
                    if (reader["password"] == _password)
                        return true;
                    else
                        return false;
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return false;
                }
            }
        }

        public static string ConnectionString
        {
            get
            {
                // return "Server=192.168.2.212;Database=BISNowERP;User Id=sa;Password=optimal$2009;";
                return ConfigurationManager.ConnectionStrings["BISConnection"].ConnectionString;
            }
        }
    }
}