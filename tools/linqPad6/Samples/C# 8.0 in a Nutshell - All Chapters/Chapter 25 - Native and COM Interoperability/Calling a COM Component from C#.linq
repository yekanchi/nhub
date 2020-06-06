<Query Kind="Statements">
  <NuGetReference>Bundle.Microsoft.Office.Interop</NuGetReference>
  <Namespace>Excel = Microsoft.Office.Interop.Excel</Namespace>
  <Namespace>Outlook = Microsoft.Office.Interop.Outlook</Namespace>
  <Namespace>PowerPoint = Microsoft.Office.Interop.PowerPoint</Namespace>
  <Namespace>Word = Microsoft.Office.Interop.Word</Namespace>
</Query>

var excel = new Excel.Application();
excel.Visible = true;
Excel.Workbook workBook = excel.Workbooks.Add();
((Excel.Range)excel.Cells [1, 1]).Font.FontStyle = "Bold";
((Excel.Range)excel.Cells [1, 1]).Value2 = "Hello World";
workBook.SaveAs (Path.GetTempFileName() + ".xlsx");

