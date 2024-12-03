using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using RegistrationMVC.Models;

namespace RegistrationMVC.DAL
{
    public class Register_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["RegisterDB"].ToString();

        public List<RegisterModel> GetAllApplications() 
        {
            List<RegisterModel> applicationList = new List<RegisterModel>(); 
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_GetAllApplications";

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
               

                con.Open();
                adapter.Fill(dt);
                con.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    applicationList.Add(new RegisterModel
                    {
                        UserID = Convert.ToInt32(dr["UserID"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Email = dr["Email"].ToString(),
                        PasswordHash = dr["PasswordHash"].ToString(),
                        City = dr["City"].ToString(),
                        PhoneNumber = dr["PhoneNumber"].ToString(),
                        DOB = Convert.ToDateTime(dr["DOB"]).Date,
                        Gender = dr["Gender"].ToString(),
                    });
                }
            }

            return applicationList;
        }

        // Register Applications
        public bool RegisterUser(RegisterModel register)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SPI_Register", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", register.FirstName);
                cmd.Parameters.AddWithValue("@LastName", register.LastName);
                cmd.Parameters.AddWithValue("@Email", register.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", register.PasswordHash);
                cmd.Parameters.AddWithValue("@City", register.City);
                cmd.Parameters.AddWithValue("@PhoneNumber", register.PhoneNumber);
                cmd.Parameters.AddWithValue("@DOB", register.DOB.Date);
                cmd.Parameters.AddWithValue("@Gender", register.Gender);

                con.Open();
                id = cmd.ExecuteNonQuery();
                con.Close();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Get User By ID

        public List<RegisterModel> GetUserByID(int UserID) // function name with list type
        {
            List<RegisterModel> applicationList = new List<RegisterModel>(); // object created for List<product>
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_GetElemntById";
                cmd.Parameters.AddWithValue("UserID",UserID);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                adapter.Fill(dt);
                con.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    applicationList.Add(new RegisterModel
                    {
                        UserID = Convert.ToInt32(dr["UserID"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Email = dr["Email"].ToString(),
                        PasswordHash = dr["PasswordHash"].ToString(),
                        City = dr["City"].ToString(),
                        PhoneNumber = dr["PhoneNumber"].ToString(),
                        DOB = Convert.ToDateTime(dr["DOB"]).Date,
                        Gender = dr["Gender"].ToString(),
                    });
                }
            }

            return applicationList;
        }

        // Update 
        public bool UpdateUser(RegisterModel register)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SPU_Register", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", register.FirstName);
                cmd.Parameters.AddWithValue("@LastName", register.LastName);
                cmd.Parameters.AddWithValue("@Email", register.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", register.PasswordHash);
                cmd.Parameters.AddWithValue("@City", register.City);
                cmd.Parameters.AddWithValue("@PhoneNumber", register.PhoneNumber);
                cmd.Parameters.AddWithValue("@DOB", register.DOB.Date);
                cmd.Parameters.AddWithValue("@Gender", register.Gender);


                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Delete
        public string DeleteUser(int Userid)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SPD_Register", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", Userid);
                cmd.Parameters.Add("@ReturnMessage", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["@ReturnMessage"].Value.ToString();
                con.Close();
            }
            return result;
        }
    }
}
