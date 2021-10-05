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
    public partial class allcourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["user_login"]) != "")
            {




                string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
                //create a new connection
                SqlConnection conn = new SqlConnection(connStr);

                // to read the table one by one
                SqlCommand AdminViewAllCourses = new SqlCommand("AdminViewAllCourses", conn);
                AdminViewAllCourses.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader rdr = AdminViewAllCourses.ExecuteReader(CommandBehavior.CloseConnection);




                while (rdr.Read())
                {
                    String name = rdr.GetString(rdr.GetOrdinal("name"));
                    Label n = new Label();
                    n.Text = "name: " + name + "<br>";
                    form1.Controls.Add(n);
                    if (!rdr.IsDBNull(1))
                    {
                        int c = rdr.GetInt32(rdr.GetOrdinal("creditHours"));
                        Label x = new Label();
                        x.Text = "credit hours: " + c + "<br>";
                        form1.Controls.Add(x);
                    }
                    if (!rdr.IsDBNull(2))
                    {
                        decimal p = rdr.GetDecimal(rdr.GetOrdinal("price"));
                        Label l = new Label();
                        l.Text = "price: " + p + "<br>";
                        form1.Controls.Add(l);
                    }
                    if (!rdr.IsDBNull(3))
                    {
                        String con = rdr.GetString(rdr.GetOrdinal("content"));
                        Label y = new Label();
                        y.Text = "content: " + con + "<br>";
                        form1.Controls.Add(y);
                    }
                    Label isacc = new Label();
                    if (!rdr.IsDBNull(4))
                    {
                        Boolean acc = rdr.GetBoolean(rdr.GetOrdinal("accepted"));

                        if (acc)
                            isacc.Text = "Accepted" + "<br>";
                    }
                    else
                        isacc.Text = "Not Yet Accepted" + "<br>";
                    form1.Controls.Add(isacc);
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