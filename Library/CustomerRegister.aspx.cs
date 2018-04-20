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
            if (IsPostBack)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LibraryConn"].ConnectionString);
                conn.Open();
                string checkuser = "select count(*) from LIBDB.Customer where Customer_ID='" + TextBox12.Text + "'";
                SqlCommand cmd = new SqlCommand(checkuser, conn);
                int temp = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (temp == 1)
                {
                    Response.Write("Customer Already Exist");
                }

                conn.Close();
            }

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            try
            {

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LibraryConn"].ConnectionString);
                conn.Open();
                string insertQuery = "Insert into LIBDB.Customer (First_Name, Middle_Initial, Last_Name, Birth_Date,Customer_ID," +
                    "Phone_Number,Membership_Issued,Address_Street,Address_City,Address_State,Address_Zipcode,Customer_Type) " +
                    "values(@FN,@MI,@LN,@BD,@CID,@PN,@MbI,@ASt,@AC,@AS,@AZ,@CT)";
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@FN", TextBox1.Text);
                cmd.Parameters.AddWithValue("@MI", TextBox2.Text);
                cmd.Parameters.AddWithValue("@LN", TextBox3.Text);
                cmd.Parameters.AddWithValue("@BD", TextBox4.Text);
                cmd.Parameters.AddWithValue("@CID", TextBox12.Text);
                cmd.Parameters.AddWithValue("@PN", TextBox5.Text);
                cmd.Parameters.AddWithValue("@MbI", TextBox6.Text);
                cmd.Parameters.AddWithValue("@ASt", TextBox7.Text);
                cmd.Parameters.AddWithValue("@AC", TextBox8.Text);
                cmd.Parameters.AddWithValue("@AS", TextBox9.Text);
                cmd.Parameters.AddWithValue("@AZ", TextBox10.Text);
                cmd.Parameters.AddWithValue("@CT", TextBox11.Text);
                cmd.ExecuteNonQuery();

                Response.Write("Customer registeration Successfully!!!thank you");

                conn.Close();

            }
            catch (Exception ex)
            {
                Response.Write("error" + ex.ToString());
            }
        }
    }
}  
        