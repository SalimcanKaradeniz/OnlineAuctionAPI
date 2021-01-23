using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using OnlineAuction.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

namespace Core.Extensions
{
    public static class LogExtensions
    {
        public static void InsertLog(this Exception exception, int userId = -99999, HttpRequest request = null, IServiceProvider serviceProvider = null, AppSettings _appSettings = null)
        {
            StringBuilder s = new StringBuilder();
            s.AppendLine("Exception type: " + exception.GetType().FullName);
            s.AppendLine();
            s.AppendLine("Message       : " + exception.Message);
            s.AppendLine();
            s.AppendLine("Stacktrace:");
            s.AppendLine(exception.StackTrace);
            s.AppendLine();
            string message = s.ToString();

            var path = request == null ? "-" : request.Path.Value;
            if (request != null && request.QueryString != null && request.QueryString.HasValue)
            {
                path = $"{path}/{request.QueryString.Value}";
            }

            string connStr = _appSettings.ConnectionStrings.SQLConnection;

            if (!string.IsNullOrEmpty(connStr))
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"INSERT INTO LogSystem (UserId,Title, Url,ShortDescription,Detail,CreatedAt) VALUES(@UserId, @Title, @Url,@ShortDescription,@Detail,@CreatedAt)";

                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@Title", "Hata");
                        cmd.Parameters.AddWithValue("@Url", path);
                        cmd.Parameters.AddWithValue("@ShortDescription", exception.Message);
                        cmd.Parameters.AddWithValue("@Detail", message);
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();

                            if (conn.State == ConnectionState.Open)
                                conn.Close();
                        }
                        catch (SqlException e)
                        {
                            if (conn.State == ConnectionState.Open)
                                conn.Close();
                        }
                    }
                }
            }
        }
    }
}