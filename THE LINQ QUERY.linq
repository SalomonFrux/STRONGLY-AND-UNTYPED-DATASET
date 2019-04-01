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
