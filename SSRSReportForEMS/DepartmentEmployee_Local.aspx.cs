using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMSSSRS
{
    public partial class DepartmentEmployee_Local : System.Web.UI.Page
    {
        private DataSet dataSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetDatTable();
                GenerateReport();
            }

        }
        private void GetDatTable()
        {
            string storeProc = "usp_RptDepartmentEmployee";
            string connString = ConfigurationManager.ConnectionStrings["EMSReportConnString"].ConnectionString;
            this.dataSet = new DataSet();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(storeProc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataSet);
                }
            }
        }
        private void GenerateReport()
        {
            var localReport = ReportViewer1.LocalReport;
            localReport.ReportPath = "./Reports/DepartmentEmployee.rdl";
            localReport.DataSources.Clear();

            // Create a report data source for the sales order data  
            ReportDataSource dsDepartment = new ReportDataSource();
            dsDepartment.Name = "Employee";
            dsDepartment.Value = this.dataSet.Tables[0];

            localReport.DataSources.Add(dsDepartment);
            // To hide the toolbar of the report.
            //ReportViewer1.ShowToolBar = false;
            //Refresh the Report
            localReport.Refresh();
        }
    }
}