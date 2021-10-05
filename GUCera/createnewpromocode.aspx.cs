using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace GUCera
{
    public partial class createnewpromocode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["user_login"]) != "")
            {
            }
            else
            {
                Response.Redirect("Login.aspx");
            }


        }
        protected void createpromo(object sender, EventArgs e) {
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);
            string Code = code.Text;
            DateTime issue = calendar1.SelectedDate;
            DateTime expiry = calendar2.SelectedDate;
            int id= Int16.Parse(Convert.ToString(Session["user_login"]));
            if (Code.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter your code");
                return;
            }
            if(Code.Length >6)
            {
                MessageBox.Show("Code length must be less than 6 charecters");
                return;
            }
            if (discount.Text.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter your discount");
                return;
            }
            Decimal dis;
            if (!Decimal.TryParse(discount.Text, out dis))
            {
                MessageBox.Show("Discount must be numeric");
                return;
            }

            if (issue.Equals(null))
            {
                MessageBox.Show("You have to enter your issue date");
                return;
            }
            if (expiry.Equals(null))
            {
                MessageBox.Show("You have to enter your expiry date");
                return;
            }
            if(expiry< issue)
            {
                MessageBox.Show("Issue date must be before expiry date");
                return;
            }


            SqlCommand checkpromo = new SqlCommand("checkpromo", conn);
            checkpromo.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader rdr = checkpromo.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String promo = rdr.GetString(rdr.GetOrdinal("code"));
             
                 if (Code == promo)
                {
                    MessageBox.Show("The code is already taken!" + "\n" + "\n" + "Please enter another one");
                    return;
                }
            }
            conn.Close();




            SqlCommand createpromocode = new SqlCommand("AdminCreatePromocode", conn);
            createpromocode.CommandType = CommandType.StoredProcedure;
            createpromocode.Parameters.Add(new SqlParameter("@code", Code) );
            createpromocode.Parameters.Add(new SqlParameter("@isuueDate", issue));
            createpromocode.Parameters.Add(new SqlParameter("@expiryDate", expiry));
            createpromocode.Parameters.Add(new SqlParameter("@adminId", id));
            createpromocode.Parameters.Add(new SqlParameter("@discount", dis));
            conn.Open();
            createpromocode.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Promocode Created");

        }
    }
}