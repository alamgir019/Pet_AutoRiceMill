using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Reporting.WebForms;
using System.Web;

namespace Johan.Repository
{
    public class ReportViewModel
    {
         public enum ReportFormat { PDF=1,Word=2,Excel=3}
        public ReportViewModel()
          {
              //initation for the data set holder
               ReportDataSets = new List<ReportDataSet>();
               ReportParams = new List<ReportParameter>();
         }
         //Name of the report
          public string Name { get; set; }
  
         //Language of the report
          public string ReportLanguage { get; set; }
  
          //Reference to the RDLC file that contain the report definition
        public string FileName { get; set; }                   
            public string LeftMainTitle { get; set; }
            public string LeftSubTitle { get; set; }
            public string ReportTitle { get; set; }
     
            //the url for the logo, 
            public string ReportLogo { get; set; }
     
            //dataset holder
            public List<ReportDataSet> ReportDataSets { get; set; }
            public List<ReportParameter> ReportParams { get; set; }
       
            //report format needed
            public ReportFormat Format { get; set; }
            public bool ViewAsAttachment { get; set; }
     
             //an helper class to store the data for each report data set
            public class ReportDataSet
            {
                public string DatasetName { get; set; }
                public List<object> DataSetData { get; set; }
            }
     
            public string ReporExportFileName { get {
                return string.Format("attachment; filename={0}.{1}", this.ReportTitle, ReporExportExtention);
            } }
            public string ReporExportExtention
            {
                get
                {
                    switch (this.Format)
                    {
                        case ReportViewModel.ReportFormat.Word: return  ".doc"; 
                        case ReportViewModel.ReportFormat.Excel: return ".xls"; 
                        default:
                            return ".pdf";
                    }
                }
            }
     
            public string LastmimeType
            {
                get
                {
                    return mimeType;
                }
            }
            private string mimeType;
            public byte[] RenderReport()
            {
                try
                {
                    //creating a new report and setting its path
                    LocalReport localReport = new LocalReport();
                    localReport.ReportPath = this.FileName;

                    //adding the reort datasets with there names
                    foreach (var dataset in this.ReportDataSets)
                    {
                        ReportDataSource reportDataSource = new ReportDataSource(dataset.DatasetName, dataset.DataSetData);
                        localReport.DataSources.Add(reportDataSource);
                    }
                    //enabeling external images
                    localReport.EnableExternalImages = true;

                    foreach (var item in this.ReportParams)
                    {
                        localReport.SetParameters(item);
                    }

                    //preparing to render the report

                    string reportType = this.Format.ToString();

                    string encoding;
                    string fileNameExtension;

                    //The DeviceInfo settings should be changed based on the reportType
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx
                    string deviceInfo =
                    "<DeviceInfo>" +
                    "  <OutputFormat>" + this.Format.ToString() + "</OutputFormat>" +  
                "  <PageWidth>9in</PageWidth>" +
                "  <PageHeight>14in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>0.25in</MarginLeft>" +
                "  <MarginRight>0.25in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;

                    //Render the report
                    renderedBytes = localReport.Render(
                         reportType,
                         deviceInfo,
                         out mimeType,
                         out encoding,
                        out fileNameExtension,
                         out streams,
                         out warnings);

                    return renderedBytes;

                }
                catch (Exception exc)
                {
                    throw exc;
                }
           }
    }
}
