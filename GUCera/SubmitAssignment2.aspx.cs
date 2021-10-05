using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class SubmitAssignment2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["user_login"]) != "")
            {
                if (!Page.IsPostBack)
            {
                string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
                //create a new connection
                SqlConnection conn = new SqlConnection(connStr);

                int session_id = Int16.Parse(Convert.ToString(Session["user_login"]));
                String session_id_string = session_id.ToString();

                SqlCommand cmd = new SqlCommand("submitAssign", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlCommand cmd1 = new SqlCommand("coursesEnrolledIn", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@sid", session_id));

                conn.Open();
                SqlDataReader rdr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read())
                {
                    String courseName = rdr.GetString(rdr.GetOrdinal("name"));

                    select_course.Items.Add(new ListItem(courseName, courseName));
                }

            }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void Submit(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            if (assign_number.Text == "")
            {
                MessageBox.Show("You have to enter the Assignment Number");
                return;
            }

            int assignment_number = Int16.Parse(assign_number.Text);
            String course_name = select_course.Value;
            String assignment_type = assignment_Type.SelectedValue;
            int flag1 = 0;
            int flag2 = 0;

            int session_id = Int16.Parse(Convert.ToString(Session["user_login"]));
            String session_id_string = session_id.ToString();

            SqlCommand cmd = new SqlCommand("submitAssign", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd1 = new SqlCommand("courseIdUsingName", conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@name", course_name));

            SqlParameter cid3 = cmd1.Parameters.Add("@id", SqlDbType.Int);
            cid3.Direction = ParameterDirection.Output;

            SqlCommand assignment_check = new SqlCommand("assignmentCheck", conn);
            assignment_check.CommandType = CommandType.StoredProcedure;

            SqlCommand assignment_check2 = new SqlCommand("assignmentCheck2", conn);
            assignment_check2.CommandType = CommandType.StoredProcedure;

            conn.Open();
            cmd1.ExecuteNonQuery();
            conn.Close();

            cmd.Parameters.Add(new SqlParameter("@sid", session_id));
            cmd.Parameters.Add(new SqlParameter("@cid", cid3.Value));
            cmd.Parameters.Add(new SqlParameter("@assignnumber", assignment_number));
            cmd.Parameters.Add(new SqlParameter("@assignType", assignment_type));

            conn.Open();
            // to read the table one by one
            SqlDataReader rdr2 = assignment_check.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr2.Read())
            {
                String type = rdr2.GetString(rdr2.GetOrdinal("type"));
                Int32 cid = rdr2.GetInt32(rdr2.GetOrdinal("cid"));
                Int32 number = rdr2.GetInt32(rdr2.GetOrdinal("number"));

                if (type.Trim() == assignment_type.Trim() && cid.ToString() == cid3.Value.ToString()
                    && number.ToString().Trim() == assignment_number.ToString().Trim())
                {
                    flag1 = 1;
           
                }
            }
            conn.Close();

            

            if (flag1 == 0)
            {
                MessageBox.Show("This Assignment is not valid!");
                return;
            }

            else
            {
                conn.Open();
                // to read the table one by one
                SqlDataReader rdr1 = assignment_check2.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr1.Read())
                {
                    String type = rdr1.GetString(rdr1.GetOrdinal("assignmenttype"));
                    Int32 cid = rdr1.GetInt32(rdr1.GetOrdinal("cid"));
                    Int32 number = rdr1.GetInt32(rdr1.GetOrdinal("assignmentNumber"));

                    if (type.Trim() == assignment_type.Trim() && cid.ToString() == cid3.Value.ToString()
                        && number.ToString().Trim() == assignment_number.ToString().Trim())
                    {
                        flag2 = 1;

                    }
                }
                conn.Close();

                if (flag2 == 1)
                {
                    MessageBox.Show("This Assignment is already submitted!");
                    return;
                }

                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Assignment submitted Successfully!");
                    conn.Close();
                }
            }

        }
    }
    }
