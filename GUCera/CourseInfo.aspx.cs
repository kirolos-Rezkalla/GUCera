using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace GUCera
{
    public partial class CourseInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["user_login"]) != "")
            {

                string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
                //create a new connection
                SqlConnection conn = new SqlConnection(connStr);

                int session_id = Int16.Parse(Convert.ToString(Session["user_login"]));
                //int course_id = int.Parse(Convert.ToString(Session["course_id"]));
                int course_id = int.Parse(Request.QueryString["cid"]);
                

                SqlCommand accepted_courses = new SqlCommand("courseInformation", conn);
                accepted_courses.CommandType = CommandType.StoredProcedure;

                accepted_courses.Parameters.Add(new SqlParameter("@id", course_id));

                conn.Open();
                accepted_courses.ExecuteNonQuery();
                conn.Close();

                conn.Open();
                SqlDataReader rdr = accepted_courses.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read())
                {
                    int cid = rdr.GetInt32(rdr.GetOrdinal("id"));
                    int instruct_id = rdr.GetInt32(rdr.GetOrdinal("instructorId"));
                    int credit_hours = rdr.GetInt32(rdr.GetOrdinal("creditHours"));
                    string course_name = rdr.GetString(rdr.GetOrdinal("name"));
                    var course_desc = "";
                    if (rdr.IsDBNull(rdr.GetOrdinal("courseDescription")))
                    {
                        course_desc = "";
                    }
                    else
                    {
                        course_desc = rdr.GetString(rdr.GetOrdinal("courseDescription"));
                    }
                    
                    decimal price = rdr.GetDecimal(rdr.GetOrdinal("price"));
                    string ints_fname = rdr.GetString(rdr.GetOrdinal("firstName"));
                    string ints_lname = rdr.GetString(rdr.GetOrdinal("lastName"));
                    string inst_name = ints_fname + " " + ints_lname;


                    var tr_0 = new HtmlGenericControl("tr");
                    var td_1 = new HtmlGenericControl("td");
                    var td_2 = new HtmlGenericControl("td");
                    var td_3 = new HtmlGenericControl("td");
                    var td_4 = new HtmlGenericControl("td");
                    
                    var td_6 = new HtmlGenericControl("td");
                    var td_7 = new HtmlGenericControl("td");
                    var td_8 = new HtmlGenericControl("td");

                    var b = new MyButton();
                    b.ID = cid.ToString();
                    b.course_id = cid.ToString();
                    b.inst_id = instruct_id.ToString();

                    b.Attributes["class"] = "mdl-button mdl-js-button mdl-button--fab mdl-button--colored";
                    b.InnerHtml = "<i class=\"material-icons\">Enroll</i>";
                    b.ServerClick += new EventHandler(GoToCheckOut);


                    td_1.InnerText = course_name.ToString();
                    td_2.InnerText = cid.ToString();
                    td_3.InnerText = credit_hours.ToString();
                    td_4.InnerText = course_desc.ToString();
                    
                    td_6.InnerText = inst_name.ToString();
                    td_7.InnerText = price.ToString();
                    td_8.Controls.Add(b);


                    tr_0.Controls.Add(td_1);
                    tr_0.Controls.Add(td_2);
                    tr_0.Controls.Add(td_3);
                    tr_0.Controls.Add(td_4);
                    
                    tr_0.Controls.Add(td_6);
                    tr_0.Controls.Add(td_7);
                    tr_0.Controls.Add(td_8);


                    tabs.Controls.Add(tr_0);



                }
                conn.Close();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }
        private void GoToCheckOut(object sender, EventArgs e)
        {
            // adding course id to the session


            MyButton b = (MyButton)sender;
            int course_id = int.Parse(b.course_id);
            int instructor_id = int.Parse(b.inst_id);
            Session["course_id"] = course_id.ToString();
            Session["inst_id"] = instructor_id.ToString();


            //create a new connection
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            // proc to get if the user already have saved credit cards or not

            int student_id = int.Parse(Convert.ToString(Session["user_login"]));

            SqlCommand student_creditCards = new SqlCommand("student_creditCards", conn);
            student_creditCards.CommandType = CommandType.StoredProcedure;

            student_creditCards.Parameters.Add(new SqlParameter("@sid", student_id));

            conn.Open();
            student_creditCards.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            SqlDataReader rdr = student_creditCards.ExecuteReader(CommandBehavior.CloseConnection);

            int count = 0;
            while (rdr.Read())
            {
                count++;
            }
            conn.Close();

            if (count == 0)
            {
                MessageBox.Show("You don't have any saved credit cards, please add one");
                Response.Redirect("addCreditCard.aspx");
            }
            else
            {
                MessageBox.Show("choose a credit card to use or add one to enroll");
                Response.Redirect("addCreditCard.aspx");
            }
        }
        public class MyButton : HtmlButton
        {
            public String course_id { get; set; }
            public String inst_id { get; set; }
        }
    }
}