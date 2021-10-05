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
    public partial class AddCourse : System.Web.UI.Page
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


        protected void Submit_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);
            Int32 credit_hours = Int32.Parse(CreditHours.Text);
            String course_name = CourseName.Text;
            Int32 pre_cid = -1;
            if (prerequisite.Text != "")
            {
                pre_cid = Int32.Parse(prerequisite.Text);
            }
            decimal price = Decimal.Parse(Price.Text);
            

            int session_id = Int16.Parse(Convert.ToString(Session["user_login"]));
            String session_id_string = session_id.ToString();

            //MessageBox.Show(Price.Text);
            if (credit_hours.ToString() == "")
            {
                MessageBox.Show("You have to enter the Credit Hours");
                return;
            }


            if (course_name.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter the Course Name");
                return;
            }
            if (price.ToString() == "")
            {
                MessageBox.Show("You have to enter the price ");
                return;
            }

            SqlCommand instructor_add_course = new SqlCommand("InstAddCourse", conn);
            instructor_add_course.CommandType = CommandType.StoredProcedure;

            instructor_add_course.Parameters.Add(new SqlParameter("@creditHours", credit_hours));
            instructor_add_course.Parameters.Add(new SqlParameter("@name", course_name));
            instructor_add_course.Parameters.Add(new SqlParameter("@price", price));
            instructor_add_course.Parameters.Add(new SqlParameter("@instructorId", session_id));

            conn.Open();
            instructor_add_course.ExecuteNonQuery();
            conn.Close();

            if (prerequisite.Text != "")
            {

                SqlCommand cmd1 = new SqlCommand("courseIdUsingName", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@name", course_name));

                SqlParameter cidd = cmd1.Parameters.Add("@id", SqlDbType.Int);
                cidd.Direction = ParameterDirection.Output;

                //int created_course = int.Parse(cidd.Value.ToString());
                //int created_course = int.Parse(cidd.ToString());
                

                conn.Open();
                cmd1.ExecuteNonQuery();
                conn.Close();

                SqlCommand pre_req = new SqlCommand("DefineCoursePrerequisites", conn);
                pre_req.CommandType = CommandType.StoredProcedure;

                pre_req.Parameters.Add(new SqlParameter("@prerequsiteId", pre_cid));
                pre_req.Parameters.Add(new SqlParameter("@cid", cidd.Value)); // should be created_id


                conn.Open();
                pre_req.ExecuteNonQuery();
                conn.Close();
            }




            
            MessageBox.Show("YOU HAVE ADDED A COURSE SUCCESSFULLY!");

            Response.Redirect("Instructorprofile.aspx");






        }
       
    }
}