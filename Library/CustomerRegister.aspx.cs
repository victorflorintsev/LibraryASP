using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library
{
    public partial class CustomerRegister : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
     
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=den1.mssql4.gear.host;Database=cosc3380;Uid=cosc3380;Pwd=vfegaf$;";
            conn.Open();
            try
            {
                
                string checkuser = "select count(*) from LIBDB.USERS where UserName='" + TextBox12.Text + "'";
                SqlCommand cmd = new SqlCommand(checkuser, conn);
                int temp = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (temp == 1)
                {
                    Response.Write("Username already exists! Please try a new Username.");
                    conn.Close();

                }

                else
                {
                    string insertUser = "Insert into LIBDB.USERS (PasswordHash,UserName,User_Type)" +
                        " values(@Password,@Username, 1) ";
                    SqlCommand cmd1 = new SqlCommand(insertUser, conn);
                    cmd1.Parameters.AddWithValue("@Username", TextBox12.Text);
                    cmd1.Parameters.AddWithValue("@Password", TextBox13.Text);

                    string insertCustomer = "Insert into LIBDB.Customer (First_Name, Middle_Initial, Last_Name, Birth_Date," +
                        "Phone_Number,Membership_Issued,Address_Street,Address_City,Address_State,Address_Zipcode,Customer_Type,Username) " +
                        "values(@FN,@MI,@LN,@BD,@PN,@MbI,@ASt,@AC,@AS,@AZ,@CT,@Username)";
                    SqlCommand cmd2 = new SqlCommand(insertCustomer, conn);
                    cmd2.Parameters.AddWithValue("@FN", TextBox1.Text);
                    cmd2.Parameters.AddWithValue("@MI", TextBox2.Text);
                    cmd2.Parameters.AddWithValue("@LN", TextBox3.Text);
                    cmd2.Parameters.AddWithValue("@BD", TextBox4.Text);
                    cmd2.Parameters.AddWithValue("@PN", TextBox5.Text);
                    cmd2.Parameters.AddWithValue("@MbI", TextBox6.Text);
                    cmd2.Parameters.AddWithValue("@ASt", TextBox7.Text);
                    cmd2.Parameters.AddWithValue("@AC", TextBox8.Text);
                    cmd2.Parameters.AddWithValue("@AS", TextBox9.Text);
                    cmd2.Parameters.AddWithValue("@AZ", TextBox10.Text);

                    string checkcustomertype = "select Customer_Type_ID from LIBDB.CUSTOMER_TYPE where Customer_Type_Name='" + DropDownList1.Text + "'";
                    SqlCommand query1 = new SqlCommand(checkcustomertype, conn);
                    int customertype = Convert.ToInt32(query1.ExecuteScalar().ToString());
                    cmd2.Parameters.AddWithValue("@CT", customertype);
                    cmd2.Parameters.AddWithValue("@Username", TextBox12.Text);

                    //Run Querries
                    cmd1.ExecuteNonQuery();
                    try
                    { cmd2.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        //if there's an error on cmd2, delete the user added 
                        Response.Write("error" + ex.ToString());
                        string deleteUser = "delete from LIBDB.USERS where UserName = @Username";
                        SqlCommand cmd3 = new SqlCommand(deleteUser, conn);
                        cmd3.Parameters.AddWithValue("@Username", TextBox12.Text);
                        cmd3.ExecuteNonQuery();
                    }

                    Response.Write("Customer Registration Complete!");
                    conn.Close();
                }
            }

            catch (Exception ex)
            {
                Response.Write("error" + ex.ToString());
            }

        }

      
    }
}  
        