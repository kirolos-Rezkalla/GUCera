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
    public partial class nonacceptedcourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["user_login"]) != "")
            {




                string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
                //create a new connection
                SqlConnection conn = new SqlConnection(connStr);

                // to read the table one by one
                SqlCommand AdminViewNonAcceptedCourses = new SqlCommand("AdminViewNonAcceptedCourses", conn);
                AdminViewNonAcceptedCourses.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader rdr = AdminViewNonAcceptedCourses.ExecuteReader(CommandBehavior.CloseConnection);




                while (rdr.Read())
                {
                    String name = rdr.GetString(rdr.GetOrdinal("name"));
                    Label n = new Label();
                    n.Text = "<br>" + "name: " + name;
                    form1.Controls.Add(n);

                    if (!rdr.IsDBNull(1))
                    {
                        int c = rdr.GetInt32(rdr.GetOrdinal("creditHours"));
                        Label x = new Label();
                        x.Text = "<br>" + "credit hours: " + c;
                        form1.Controls.Add(x);
                    }


                    if (!rdr.IsDBNull(2))
                    {
                        decimal p = rdr.GetDecimal(rdr.GetOrdinal("price"));
                        Label l = new Label();
                        l.Text = "<br>" + "price: " + p;
                        form1.Controls.Add(l);
                    }
                    if (!rdr.IsDBNull(3))
                    {
                        String con = rdr.GetString(rdr.GetOrdinal("content"));
                        Label y = new Label();
                        y.Text = "<br>" + "content: " + con;
                        form1.Controls.Add(y);
                    }
                    Label s = new Label();
                    s.Text = "<br>";
                    form1.Controls.Add(s);
                }
                conn.Close();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }


        }
    }
}