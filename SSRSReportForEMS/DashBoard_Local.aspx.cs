using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
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
    public partial class DashBoard_Local : System.Web.UI.Page
    {
        private DataSet dataSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetData();
                GenerateReport();
            }
        }
        private void GetData()
        {
            
            string connString = ConfigurationManager.ConnectionStrings["EMSReportConnString"].ConnectionString;
            this.dataSet = new DataSet();
           
            using (SqlConnection conn = new SqlConnection(connString))
            {
                FetchData(conn, "usp_RptDashBoard_EmployeePerDepartment", "EMPPERDEPARTMENT");
                FetchData(conn, "usp_RptDashBoard_SalaryPerDepartment", "SALARYPERDEPARTMENT");
                FetchData(conn, "usp_RptSexRation", "SEXRATIO");
            }
            
        }
        private void FetchData(SqlConnection conn,string storedProc, string dataTableName)
        {
            DataTable dt = new DataTable();
            dt.TableName = dataTableName;
            using (SqlCommand cmd = new SqlCommand(storedProc, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            this.dataSet.Tables.Add(dt);
        }
        private void GenerateReport()
        {
            var localReport = ReportViewer1.LocalReport;
            localReport.ReportPath = "./Reports/Dashboard.rdl";
            localReport.DataSources.Clear();

            // Create a report data source for the sales order data  
            ReportDataSource dsEmpPerDepartment = new ReportDataSource();
            dsEmpPerDepartment.Name = "EMPPERDEPARTMENT";
            dsEmpPerDepartment.Value = this.dataSet.Tables["EMPPERDEPARTMENT"];

            ReportDataSource dsSalaryDepartment = new ReportDataSource();
            dsSalaryDepartment.Name = "SALARYPERDEPARTMENT";
            dsSalaryDepartment.Value = this.dataSet.Tables["SALARYPERDEPARTMENT"];

            ReportDataSource dsSexRatio = new ReportDataSource();
            dsSexRatio.Name = "SEXRATIO";
            dsSexRatio.Value = this.dataSet.Tables["SEXRATIO"];

            localReport.DataSources.Add(dsEmpPerDepartment);
            localReport.DataSources.Add(dsSalaryDepartment);
            localReport.DataSources.Add(dsSexRatio);
            // To hide the toolbar of the report.
            ReportViewer1.ShowToolBar = false;
            //Refresh the Report
            localReport.Refresh();
        }
    }
}