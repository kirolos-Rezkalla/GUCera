using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace GUCera
{
    public partial class AddCreditCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["user_login"]) != "")
            {


                string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
                //create a new connection
                SqlConnection conn = new SqlConnection(connStr);

                int student_id = Int16.Parse(Convert.ToString(Session["user_login"]));


                // ---------------- showing student credit cards ---------------

                SqlCommand student_creditCards = new SqlCommand("student_creditCards", conn);
                student_creditCards.CommandType = CommandType.StoredProcedure;

                student_creditCards.Parameters.Add(new SqlParameter("@sid", student_id));

                conn.Open();
                student_creditCards.ExecuteNonQuery();
                conn.Close();

                conn.Open();
                SqlDataReader rdr2 = student_creditCards.ExecuteReader(CommandBehavior.CloseConnection);



                while (rdr2.Read())
                {
                    string card_number = rdr2.GetString(rdr2.GetOrdinal("number"));
                    var card_name = rdr2.GetString(rdr2.GetOrdinal("cardHolderName"));
                    DateTime expiry_date = rdr2.GetDateTime(rdr2.GetOrdinal("expiryDate"));
                    var cvv = rdr2.GetString(rdr2.GetOrdinal("cvv"));
                    // use this card button

                    MyButton b2 = new MyButton();
                    b2.ID = card_number;
                    b2.card_number = card_number;
                    b2.card_holder_name = card_name;
                    b2.expiry_date = expiry_date;
                    b2.cvv = cvv;
                    b2.Attributes["class"] = "mdl-button mdl-js-button mdl-button--fab mdl-button--colored";
                    b2.InnerHtml = "<i class=\"material-icons\">Use this Card</i>";
                    b2.ServerClick += new EventHandler(Use);

                    // remove card button

                    MyButton removeButton = new MyButton();
                    removeButton.card_number = card_number;
                    removeButton.Attributes["class"] = "mdl-button mdl-js-button mdl-button--fab mdl-button--colored";
                    removeButton.InnerHtml = "<i class=\"material-icons\">Remove</i>";
                    removeButton.ServerClick += new EventHandler(Remove_Card);



                    var tr_0 = new HtmlGenericControl("tr");
                    var td_1 = new HtmlGenericControl("td");
                    var td_2 = new HtmlGenericControl("td");
                    var td_3 = new HtmlGenericControl("td");
                    var td_4 = new HtmlGenericControl("td");
                    var td_5 = new HtmlGenericControl("td");


                    td_1.InnerText = b2.card_number.ToString();
                    td_2.InnerText = b2.card_holder_name.ToString();
                    td_3.InnerText = b2.expiry_date.ToString();
                    td_4.Controls.Add(b2);
                    td_5.Controls.Add(removeButton);

                    tr_0.Controls.Add(td_1);
                    tr_0.Controls.Add(td_2);
                    tr_0.Controls.Add(td_3);
                    tr_0.Controls.Add(td_4);
                    tr_0.Controls.Add(td_5);

                    tabs.Controls.Add(tr_0);

                }
                conn.Close();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private void Remove_Card(object sender, EventArgs e)
        {
            int student_id = int.Parse(Convert.ToString(Session["user_login"]));
            MyButton b = (MyButton)sender;
            string card_number = b.card_number;

            //create a new connection
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand delete_crediCard = new SqlCommand("deleteCreditCard", conn);
            delete_crediCard.CommandType = CommandType.StoredProcedure;

            delete_crediCard.Parameters.Add(new SqlParameter("@sid", student_id));
            delete_crediCard.Parameters.Add(new SqlParameter("@number", card_number));
            

            conn.Open();
            delete_crediCard.ExecuteNonQuery();
            conn.Close();

            Response.Redirect("AddCreditCard.aspx");

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bool flag = true;

            bool flag_number = true;
            bool flag_name = true;
            bool flag_expiryDate = true;
            bool flag_cvv = true;



            int student_id = Int16.Parse(Convert.ToString(Session["user_login"]));
            string card_number = "t";
            var card_name = "t";
            DateTime expiry_date = new DateTime();
            string cvv = "t";
            if (!creditCard_number.Text.All(char.IsDigit) || creditCard_number.Text.Length == 0)
            {
                creditCard_number_error.Text = "Please enter a valid card number";
                flag_number = false;
                flag = false;
            }
            else
            {
                card_number = creditCard_number.Text;
            }
            if (name.Text.Length == 0)
            {
                name_error.Text = "Please enter name on card";
                flag_name = false;
                flag = false;
            }
            else if(!Regex.IsMatch(name.Text, @"^[a-zA-Z]+$"))
            {
                name_error.Text = "Please enter a valid name, card holder name";
                flag_name = false;
                flag = false;
            }
            else
            {
                card_name = name.Text;
            }
            if ((ExpiryDate.Text).ToString() == "")
            {
                ExpiryDate_error.Text = "Please enter the card Expiry date";
                flag_expiryDate = false;
                flag = false;
            }
            else
            {
                expiry_date = DateTime.Parse(ExpiryDate.Text);
            }
            
            

            if (Cvv.Text.Length!=3 || !Cvv.Text.All(char.IsDigit))
            {
                Cvv_error.Text = "Please a 3 digit number";
                flag_cvv = false;
                flag = false;
            }
            else
            {
                cvv = Cvv.Text;
            }


            if (flag)
            {

                string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
                //create a new connection
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand addCreditCard = new SqlCommand("addCreditCard", conn);
                addCreditCard.CommandType = CommandType.StoredProcedure;

                addCreditCard.Parameters.Add(new SqlParameter("@sid", student_id));
                addCreditCard.Parameters.Add(new SqlParameter("@number", card_number));
                addCreditCard.Parameters.Add(new SqlParameter("@cardHolderName", card_name));
                addCreditCard.Parameters.Add(new SqlParameter("@expiryDate", expiry_date));
                addCreditCard.Parameters.Add(new SqlParameter("@cvv", cvv));

                conn.Open();
                addCreditCard.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Card added, Choose a card to pay with");
                Response.Redirect("AddCreditCard.aspx");
            }
            else
            {
                if (flag_number)
                {
                    creditCard_number_error.Text = "";
                }
                if (flag_name)
                {
                    name_error.Text = "";
                }
                else
                {
                    name.Text = "";
                }
                if (flag_expiryDate)
                {
                    ExpiryDate_error.Text = "";
                }
                else
                {
                    //ExpiryDate.Text
                }
                if (flag_cvv)
                {
                    Cvv_error.Text = "";
                }
                else
                {
                    Cvv.Text = Cvv.Text;
                }
                
                
                //MessageBox.Show("can not add credit card!");
            }

            

        }
        public class MyButton : HtmlButton
        {
            

            public String card_number { get; set; }

            public String course_id { get; set; }
            public string card_holder_name { get; internal set; }
            public DateTime expiry_date { get; internal set; }
            public string cvv { get; internal set; }
        }
        private void Use(object sender, EventArgs e)
        {
            MyButton b = (MyButton)sender;
            b.Attributes.Add("class", "buttongreen");
            
            if (Convert.ToString(Session["course_id"]) != "")
            {
                MessageBox.Show(b.card_number + " is in use, you can now checkout");
                Response.Redirect("checkout.aspx");
            }
            else
            {
                MessageBox.Show(b.card_number + " is in use, choose a course to enroll in");
                Response.Redirect("CoursesAcceptedByAdmin.aspx");
            }

        }

    }
}