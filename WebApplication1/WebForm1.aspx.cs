using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string n = FirstName.Value;
            SqlConnection sqlConn = new SqlConnection(Properties.Settings.Default.dbConn);

            if (IsPostBack)
            {
                return;               
            }
            else
            {
                Label4.Text = DateTime.Now.ToString("dd.mm.yyyy hh:mm:ss");
            }
        }
        protected void Button1_Click(object sender,EventArgs e)
        {
            Label3.Text = DropDownList1.SelectedItem.Text;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString);
            SqlCommand sqlcmd = new SqlCommand("select * from [13th Aug Cloud PT Immersive].TeamA.Products",sqlConnection);
            sqlcmd.CommandType = CommandType.Text;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
           
            sqlDataAdapter.SelectCommand = sqlcmd;

            DataSet dtset = new DataSet();
            sqlDataAdapter.Fill(dtset);

            GridViewProducts.DataSource = dtset.Tables[0];
            GridViewProducts.DataBind();

        }

    }
}