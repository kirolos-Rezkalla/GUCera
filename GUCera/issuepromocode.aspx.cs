using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GUCera
{
    public partial class issuepromocode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["user_login"]) != "")
            {
                string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand promocode = new SqlCommand("checkpromo", conn);
            promocode.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader rdr = promocode.ExecuteReader(CommandBehavior.CloseConnection);
            Label b = new Label();
            b.Text = "ALL PROMOCODES" + "<br>";
            form1.Controls.Add(b);



            while (rdr.Read())
            {
                String code = rdr.GetString(rdr.GetOrdinal("code"));
                Label c = new Label();
                c.Text = code + "<br>";
                form1.Controls.Add(c);




            }
            conn.Close();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }


        }

        protected void IssuePromocde(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);
            string c = code.Text;
            if (c.Trim() == string.Empty)
            {
                System.Windows.Forms.MessageBox.Show("You have to enter your desired code");
                return;
            }
            if (studentid.Text.Trim() == string.Empty)
            {
                System.Windows.Forms.MessageBox.Show("You have to enter id");
                return;
            }
            int id;
            if (!int.TryParse(studentid.Text, out id))
            {
                System.Windows.Forms.MessageBox.Show("Student Id must be numeric");
                return;
            }

            SqlCommand idsandpromo = new SqlCommand("IdsAndPromoCodes", conn);
            idsandpromo.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader rdr = idsandpromo.ExecuteReader(CommandBehavior.CloseConnection);




            while (rdr.Read())
            {
                if (c == rdr.GetString(rdr.GetOrdinal("code")) && id == rdr.GetInt32(rdr.GetOrdinal("sid")))
                {
                    System.Windows.Forms.MessageBox.Show("this promo was issued to this student before");
                    return;



                }
            }
                conn.Close();
                SqlCommand AdminIssuePromoCode = new SqlCommand("AdminIssuePromoCodeToStudent", conn);
                AdminIssuePromoCode.CommandType = CommandType.StoredProcedure;
                AdminIssuePromoCode.Parameters.Add(new SqlParameter("@sid", id));
                AdminIssuePromoCode.Parameters.Add(new SqlParameter("@pid", c));
                conn.Open();
                AdminIssuePromoCode.ExecuteNonQuery();
                conn.Close();
                System.Windows.Forms.MessageBox.Show("promo was issued to this student");
            }
        } 
    
    }