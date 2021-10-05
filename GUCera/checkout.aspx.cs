using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace GUCera
{
    public partial class Checkout : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Convert.ToString(Session["user_login"]) != "")
            //{

                //create a new connection
                string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
                SqlConnection conn = new SqlConnection(connStr);

                int student_id = Int16.Parse(Convert.ToString(Session["user_login"]));


                //--------- showing the student promocodes ---------------


                SqlCommand student_promocodes = new SqlCommand("viewPromocode", conn);
                student_promocodes.CommandType = CommandType.StoredProcedure;

                student_promocodes.Parameters.Add(new SqlParameter("@sid", student_id));

                conn.Open();
                student_promocodes.ExecuteNonQuery();
                conn.Close();

                conn.Open();
                SqlDataReader rdr = student_promocodes.ExecuteReader(CommandBehavior.CloseConnection);


                int counter = 0;
                while (rdr.Read())
                {
                    string code = rdr.GetString(rdr.GetOrdinal("code"));
                    DateTime issue_date = rdr.GetDateTime(rdr.GetOrdinal("isuueDate"));
                    DateTime expiry_date = rdr.GetDateTime(rdr.GetOrdinal("expiryDate"));
                    decimal discount = rdr.GetDecimal(rdr.GetOrdinal("discount"));

                    // redeem button
                    RedeemButton btn = new RedeemButton();
                    btn.ID = "redeem-click" + counter;
                    counter++;
                    btn.code = code;
                    btn.admin_id = rdr.GetInt32(rdr.GetOrdinal("adminId"));
                    btn.discount = rdr.GetDecimal(rdr.GetOrdinal("discount"));
                    //btn.expiry_date = rdr.GetDateTime(rdr.GetOrdinal("expiryDate"));
                    //DateTime exp_date = (DateTime)btn.expiry_date;
                    btn.Attributes["class"] = "mdl-button mdl-js-button mdl-button--fab mdl-button--colored";
                    btn.InnerHtml = "<i class=\"material-icons\">Redeem</i>";
                if (Convert.ToString(Session["course_id"]) != "" )
                {
                    btn.ServerClick += new EventHandler(Redeem_clicked);
                    
                }
                else
                {
                    if (!IsPostBack)
                        redeem_error.Visible = true;
                }


                    var tr0 = new HtmlGenericControl("tr");
                    var td1 = new HtmlGenericControl("td");
                    var td2 = new HtmlGenericControl("td");
                    var td3 = new HtmlGenericControl("td");
                    var td4 = new HtmlGenericControl("td");
                    var td5 = new HtmlGenericControl("td");


                    td1.InnerText = code.ToString();
                    td2.InnerText = issue_date.ToString();
                    td3.InnerText = expiry_date.ToString();
                    td4.InnerText = discount.ToString();
                    td5.Controls.Add(btn);

                    tr0.Controls.Add(td1);
                    tr0.Controls.Add(td2);
                    tr0.Controls.Add(td3);
                    tr0.Controls.Add(td4);
                    tr0.Controls.Add(td5);


                    tabs.Controls.Add(tr0);

                }
                conn.Close();


                // ----------------------------- Cart ----------------------------------


                if (Convert.ToString(Session["course_id"]) == "")
                {
                    course_name.Text = "Course";
                    promocode.Text = "";
                    total.Text = "0.00";
                    amount_deducted.Text = "";
                    course_price.Text = "";
                    var b = new MyButton();
                    // to know which the user is paying for
                    b.course_id = Convert.ToString(Session["course_id"]);
                    b.Attributes["class"] = "mdl-button mdl-js-button mdl-button--fab mdl-button--colored";
                    b.InnerHtml = "<i class=\"material-icons\">Pay</i>";
                    b.ServerClick += new EventHandler(EmptyCart);
                    Pay_Button_id.Controls.Add(b);
                }
                else
                {
                    //------------------- proc to get chosen chosen course by course Id -----------------
                    int cid = Int32.Parse(Convert.ToString(Session["course_id"]));



                    SqlCommand course_info = new SqlCommand("courseInformation", conn);
                    course_info.CommandType = CommandType.StoredProcedure;


                    course_info.Parameters.Add(new SqlParameter("@id", cid));


                    conn.Open();
                    course_info.ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    SqlDataReader rdr_course = course_info.ExecuteReader(CommandBehavior.CloseConnection);

                    string c_name = "";
                    decimal c_price = 0;

                    while (rdr_course.Read())
                    {
                        c_name = rdr_course.GetString(rdr_course.GetOrdinal("name"));
                        c_price = rdr_course.GetDecimal(rdr_course.GetOrdinal("price"));
                    }
                    conn.Close();

                    Session["course_price"] = c_price;
                    //--------------------------------------------------

                    course_name.Text = c_name;
                    course_price.Text = c_price.ToString();

               
                
                    if (Convert.ToString(Session["promocode"]) != "")
                    {
                        string s = Convert.ToString(Session["discount_value"]);
                        promocode.Text = Convert.ToString(Session["promocode"]);
                        amount_deducted.Text = "-" + s;
                        decimal total_amount = c_price - Convert.ToDecimal(Session["discount_value"]);
                        total.Text = total_amount.ToString();
                        //Session.Remove("discount_value");
                        //Session.Remove("promocode");
                    }
                    else
                    {
                        total.Text = c_price.ToString();
                    }

                    var pay_button = new MyButton();
                    pay_button.ID = "pay";
                    // to know which the user is paying for
                    pay_button.course_id = Convert.ToString(Session["course_id"]);
                    pay_button.Attributes["class"] = "mdl-button mdl-js-button mdl-button--fab mdl-button--colored";
                    pay_button.InnerHtml = "<i class=\"material-icons\">Pay</i>";
                    CoursesAcceptedByAdmin x = new CoursesAcceptedByAdmin();
                    pay_button.ServerClick += new EventHandler(x.Enroll);
                    Pay_Button_id.Controls.Add(pay_button);

                if (Convert.ToString(Session["promocode"]) != "")
                    {
                        pay_button.ServerClick += new EventHandler(Redeem_promocode_proc);
                        promocode_success.Visible = true;
                }
                    pay_button.ServerClick += new EventHandler(Pay);

                //--------------
                var clear_button = new MyButton();
                clear_button.Attributes["class"] = "mdl-button mdl-js-button mdl-button--fab mdl-button--colored";
                clear_button.InnerHtml = "<i class=\"material-icons\">Clear Cart</i>";
                clear_button.ServerClick += new EventHandler(clear_cart);
                ClearCart_Button_ID.Controls.Add(clear_button);
                
            }
            



        }

        private void clear_cart(object sender, EventArgs e)
        {
            Session.Remove("course_id");
            Session.Remove("promocode");
            Response.Redirect("checkout.aspx");
        }

        private void Redeem_promocode_proc(object sender, EventArgs e)
            {
                //create a new connection
                string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
                SqlConnection conn = new SqlConnection(connStr);



                int student_id = int.Parse(Convert.ToString(Session["user_login"]));
                string promocode = Convert.ToString(Session["promocode"]);

                SqlCommand delete_promocode = new SqlCommand("deletePromoAfterUse", conn);
                delete_promocode.CommandType = CommandType.StoredProcedure;

                delete_promocode.Parameters.Add(new SqlParameter("@sid", student_id));
                delete_promocode.Parameters.Add(new SqlParameter("@code", promocode));

                conn.Open();
                delete_promocode.ExecuteNonQuery();
                conn.Close();

                Session.Remove("promocode");
            

        }

        private void Redeem_clicked(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["course_id"]) != "")
            {
                RedeemButton b = (RedeemButton)sender;
                string code = b.code.ToString();
                decimal discount = b.discount;
                decimal course_price = Convert.ToDecimal(Session["course_price"]);
                
                
                
                decimal discount_value = course_price - (course_price - (course_price * (discount / 100)));
                promocode.Text = code;
                amount_deducted.Text = discount_value.ToString();

                //----session insertions------
                Session["promocode"] = code.ToString();
                //Session["discount"] = discount.ToString();
                Session["discount_value"] = discount_value.ToString();

                //MessageBox.Show("Promocode Added");
                
                
            }
            
                Response.Redirect("checkout.aspx");
            

        }

        private void EmptyCart(object sender, EventArgs e)
        {
            MessageBox.Show("Your cart is empty");
        }

       
     
        private void all_creditCards(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        private void Pay(object sender, EventArgs e)
        {
            //MyButton b = (MyButton)sender;
            int student_id = Int32.Parse(Convert.ToString(Session["user_login"]));
            int cid = Int32.Parse(Convert.ToString(Session["course_id"]));
            

            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand pay_course = new SqlCommand("payCourse", conn);
            pay_course.CommandType = CommandType.StoredProcedure;

            pay_course.Parameters.Add(new SqlParameter("@sid", student_id));
            pay_course.Parameters.Add(new SqlParameter("@cid", cid));
            

            conn.Open();
            pay_course.ExecuteNonQuery();
            conn.Close();

            //------------------ course info -----------------
            

            SqlCommand course_info = new SqlCommand("courseInformation", conn);
            course_info.CommandType = CommandType.StoredProcedure;


            course_info.Parameters.Add(new SqlParameter("@id", cid));


            conn.Open();
            course_info.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            SqlDataReader rdr_course = course_info.ExecuteReader(CommandBehavior.CloseConnection);

            string c_name = "";
            

            while (rdr_course.Read())
            {
                c_name = rdr_course.GetString(rdr_course.GetOrdinal("name"));
            }
            conn.Close();

            //MessageBox.Show("Enrolled in "+c_name.ToUpper()+" course successfully!");

            Session.Remove("course_id");
            Response.Redirect("CoursesAcceptedByAdmin.aspx");
        }

       

        public class MyButton : HtmlButton
        {
            

            public String card_number { get; set; }

            public String course_id { get; set; }
            public string card_holder_name { get; internal set; }
            
            public string cvv { get; internal set; }
        }

        public class RedeemButton : HtmlButton
        {
            public String code { get; set; }
            public int admin_id { get; set; }

            public decimal discount { get; set; }
            public DateTime expiry_date { get; internal set; }

        }


        protected void addCard_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCreditCard.aspx");
        }

    }
}