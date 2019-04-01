using System;
using System.Linq;

namespace StronglyTypeDataset
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StudentDataSetTableAdapters.StudentsTableAdapter studentsTableAdapter =
                    new StudentDataSetTableAdapters.StudentsTableAdapter();
                StudentDataSet.StudentsDataTable studentDatatable = new StudentDataSet.StudentsDataTable();
                studentsTableAdapter.Fill(studentDatatable);

                Session["studentDatatable"] = studentDatatable;
                GridView1.DataSource = from Student in studentDatatable
                                       select new
                                       {
                                           Student.ID,
                                           Student.Name,
                                           Student.Gender,
                                           Student.TotalMarks
                                       };
                GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox1.Text))
            {
                if (Session["studentDatatable"] != null)

                {
                    StudentDataSet.StudentsDataTable studentDatatable = (StudentDataSet.StudentsDataTable)Session["studentDatatable"];
                    GridView1.DataSource = studentDatatable;
                    GridView1.DataBind();
                }
            }
            else
            {
                StudentDataSet.StudentsDataTable studentDatatable = (StudentDataSet.StudentsDataTable)Session["studentDatatable"];
                
                GridView1.DataSource = from Student in studentDatatable
                                       where Student.Name.ToUpper().StartsWith(TextBox1.Text.ToUpper())
                                       select new
                                       {
                                           Student.ID,
                                           Student.Name,
                                           Student.Gender,
                                           Student.TotalMarks
                                       };
                GridView1.DataBind();
            }
        }
    }
}
SOLVED
