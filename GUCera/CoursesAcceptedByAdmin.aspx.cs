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
    public partial class CoursesAcceptedByAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["user_login"]) != "")
            {

                string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            int session_id = Int16.Parse(Convert.ToString(Session["user_login"]));

            SqlCommand accepted_courses = new SqlCommand("availableCourses2", conn);
            accepted_courses.CommandType = CommandType.StoredProcedure;

            accepted_courses.Parameters.Add(new SqlParameter("@sid", session_id));



            conn.Open();
            accepted_courses.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            SqlDataReader rdr = accepted_courses.ExecuteReader(CommandBehavior.CloseConnection);

            while (rdr.Read())
            {
                int cid = rdr.GetInt32(rdr.GetOrdinal("id"));
                int instructor_id = rdr.GetInt32(rdr.GetOrdinal("instructorId"));
                int credit_hours = rdr.GetInt32(rdr.GetOrdinal("creditHours"));
                string course_name = rdr.GetString(rdr.GetOrdinal("name"));
                var course_desc = "";
                if ( rdr.IsDBNull(rdr.GetOrdinal("courseDescription"))){
                    course_desc = "";
                }
                else
                {
                    course_desc = rdr.GetString(rdr.GetOrdinal("courseDescription"));
                }
                var course_content = "";
                if (rdr.IsDBNull(rdr.GetOrdinal("content")))
                {
                    course_content = "";
                }
                else
                {
                    course_content = rdr.GetString(rdr.GetOrdinal("content"));
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
                var td_5 = new HtmlGenericControl("td");
                var td_6 = new HtmlGenericControl("td");
                var td_7 = new HtmlGenericControl("td");
                var td_8 = new HtmlGenericControl("td");

                var b = new MyButton();
                //b.ID = cid.ToString();
                b.course_id = cid.ToString();
                b.inst_id = instructor_id.ToString();
                
                b.Attributes["class"] = "mdl-button mdl-js-button mdl-button--fab mdl-button--colored";
                b.InnerHtml = "<i class=\"material-icons\">view course</i>";
                b.ServerClick += new EventHandler(ViewCourse);
                

                td_1.InnerText = course_name.ToString();
                td_2.InnerText = cid.ToString();
                td_3.InnerText = credit_hours.ToString();
                td_4.InnerText = course_desc.ToString();
                td_5.InnerText = course_content.ToString();
                td_6.InnerText = inst_name.ToString();
                td_7.InnerText = price.ToString();
                td_8.Controls.Add(b);
               

                tr_0.Controls.Add(td_1);
                tr_0.Controls.Add(td_2);
                //tr_0.Controls.Add(td_3);
                //tr_0.Controls.Add(td_4);
                //tr_0.Controls.Add(td_5);
                tr_0.Controls.Add(td_6);
                //tr_0.Controls.Add(td_7);
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

        private void ViewCourse(object sender, EventArgs e)
        {
            MyButton b = (MyButton)sender;
            int cid = int.Parse(b.course_id);
            Response.Redirect("CourseInfo.aspx?cid="+cid);
        }


        public void Enroll(object sender, EventArgs e)
        {
            
            
            int student_id = int.Parse(Convert.ToString(Session["user_login"]));
            int cid = Int32.Parse(Convert.ToString(Session["course_id"]));
            int inst_id = Int32.Parse(Convert.ToString(Session["inst_id"]));


            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand enrollInCourse = new SqlCommand("enrollInCourse",conn);
            enrollInCourse.CommandType = CommandType.StoredProcedure;
            
            enrollInCourse.Parameters.Add(new SqlParameter("@sid", student_id));
            enrollInCourse.Parameters.Add(new SqlParameter("@cid", cid));
            enrollInCourse.Parameters.Add(new SqlParameter("@instr", inst_id));

            conn.Open();
            Boolean err = false;
            try
            {
                enrollInCourse.ExecuteNonQuery();
                MessageBox.Show("Enrolled successfully!");
            }
            catch (SqlException)
            {
                err = true;
                MessageBox.Show("Can not enroll in this course, this course has pre-requisite!");
            }
            //if (err)
            //{
            //    Response.Redirect("StudentProfile.aspx");
            //}
            //Session.Remove("promocode");


            conn.Close();

            Session.Remove("inst_id");



        }
        public class MyButton : HtmlButton
        {
            public String course_id { get; set; }
            public String inst_id { get; set; }
        }
    }
}