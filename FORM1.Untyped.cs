using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace StronglyTypeDataset
{
    // WHEN USING NOT STRONGLY TYPED DATASET 
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(! IsPostBack)
            {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);
                string cmdText = "select * from tblStudents";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmdText, con);

                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "tblStudent");

                Session["dataSet"] = dataSet;

                GridView1.DataSource = from datarow in dataSet.Tables["tblStudent"].AsEnumerable()
                                       select new Student
                                       {
                                           ID = (int)datarow["ID"],
                                           Name = datarow["Name"].ToString(),
                                           Gender = datarow["Gender"].ToString(),
                                           TotalMarks = (int)datarow["TotalMarks"]
                                       };
                GridView1.DataBind();
                                       
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(TextBox1.Text))
            {
                DataSet dataSet = (DataSet)Session["dataSet"];
                GridView1.DataSource = dataSet;
                GridView1.DataBind();


            }
            else
            {
                DataSet dataSet = (DataSet)Session["dataSet"];
                GridView1.DataSource = from datarow in dataSet.Tables["tblStudent"].AsEnumerable()
                                       where datarow["Name"].ToString().ToUpper().StartsWith(TextBox1.Text.ToUpper())
                                       select new Student
                                       {
                                           ID = (int)datarow["ID"],
                                           Name = datarow["Name"].ToString(),
                                           Gender = datarow["Gender"].ToString(),
                                           TotalMarks = (int)datarow["TotalMarks"]
                                       };
               
               GridView1.DataBind();
            }

        }
    }
}
