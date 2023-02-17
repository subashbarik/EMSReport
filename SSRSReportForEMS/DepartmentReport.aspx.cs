using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace EMSSSRS
{
    public partial class DepartmentReport : System.Web.UI.Page
    {
        private DataSet dataSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                GetDatTable();
                GenerateReport();
            }

        }
        private void GetDatTable()
        {
            string query = "Select * from Departments";
            string connString = ConfigurationManager.ConnectionStrings["EMSReportConnString"].ConnectionString;
            this.dataSet = new DataSet();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using(SqlCommand cmd = new SqlCommand(query,conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataSet);
                }
            }
        }
        private void GenerateReport()
        {
            var localReport = RDLCDepartment.LocalReport;
            localReport.ReportPath = "./Reports/Department.rdlc";
            localReport.DataSources.Clear();

            // Create a report data source for the sales order data  
            ReportDataSource dsDepartment = new ReportDataSource();
            dsDepartment.Name = "Department";
            dsDepartment.Value = this.dataSet.Tables[0];

            localReport.DataSources.Add(dsDepartment);
            // To hide the toolbar of the report.
            RDLCDepartment.ShowToolBar = false;
            //Refresh the Report
            localReport.Refresh();
        }
    }
}