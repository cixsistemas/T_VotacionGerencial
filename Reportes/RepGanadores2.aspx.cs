using System;
using System.Web.UI;
using System.Data;
using System.Drawing;
using System.IO;

using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Web.UI.WebControls;

public partial class Reportes_RepGanadores2 : System.Web.UI.Page
{
    private String Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
    policia.clsaccesodatos servidor = new policia.clsaccesodatos();
    DataTable dt;
    Lista _Lista = new Lista();
    double dTotal = 0;
    //double dTotal2 = 0;
    private void Lista(string Opcion, string Cargo, string Periodo, string Candidato, string Socio)
    {
        __mensaje.Value = "";
        __pagina.Value = "";
        btnexportar.Visible = false;
        try
        {
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true)
            {
                dt = servidor.consultar("[Rep_Votaciones]", Opcion, Cargo, Periodo, Candidato, Socio).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    servidor.cerrarconexion();
                    _Lista.ShowMessage(__mensaje, __pagina, "No hay datos para mostrar.", "");
                }
                else
                {
                    dt.Columns.Remove("VOTO");
                    //dt.Columns.Remove("TOT. HRAS");
                    //dt.Columns.Remove("CONVERTIR");
                    // dt = _Lista.Generar_Totales_Filas_3(dt, 0, 7);
                    GvReporte2.DataSource = dt;
                    GvReporte2.DataBind();
                    btnexportar.Visible = true;
                }
            }
            else
            {
                servidor.cerrarconexion();
                _Lista.ShowMessage(__mensaje, __pagina, servidor.getMensageError(), "../CerrarSession.aspx");
            }
        }
        catch (Exception)
        {
            _Lista.ShowMessage(__mensaje, __pagina, "Error inesperado al intentar conectarnos con el servidor.", "../CerrarSession.aspx");
        }
    }

   
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            GvReporte2.DataSource = BindGrid();
        }
    }
    protected DataTable BindGrid()
    {

        DateTime Hoy = DateTime.Today;
        string fecha_actual = Hoy.ToString("dd-MM-yyyy");
        //TxtFechaActual.Text = fecha_actual;
        //TxtHoraActual.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
        string dato1 = Request.QueryString["Opcion"]; // OPcION
        string dato2 = Request.QueryString["Cargo"]; //Cargo
        string dato3 = Request.QueryString["Periodo"]; // Periodo
        string dato4 = Request.QueryString["Candidato"]; // Candidato
        string dato5 = Request.QueryString["Socio"]; // Socio
        //string dato6 = Request.QueryString["Tipo"]; // Socio

        //if (dato6 == "Marcas")
        //{
            //string dato6 = Request.QueryString["datos6"]; // ONVERTIR
            //this.Title = dato1 + " " + dato2;
            Lista(dato1, dato2, dato3, dato4, dato5);
        //}
        //if (dato6 == "Descanso")
        //{
        //    ListaDescanso(dato1, dato2, dato3, dato4, dato5, "");
        //}

        return dt;

    }

    private DataTable GetExcelData()
    {
        DataTable dtData = new DataTable();

        string dato1 = Request.QueryString["Opcion"]; // OPcION
        string dato2 = Request.QueryString["Cargo"]; //Cargo
        string dato3 = Request.QueryString["Periodo"]; // Periodo
        string dato4 = Request.QueryString["Candidato"]; // Candidato
        string dato5 = Request.QueryString["Socio"]; // Socio

        Lista(dato1, dato2, dato3, dato4, dato5);

        DataRow dr = dtData.NewRow();
        dtData.Rows.Add(dr);
        return dt;
    }
    protected void GenerateExcel()
    {
        //https://www.gopiportal.in/2017/08/creating-excel-reports-using-epplus.html
        //https://desarrolladores.me/2014/02/c-creando-archivo-de-excel-con-epplus/
        DataTable dtData = GetExcelData();

        ExcelPackage p = new ExcelPackage();
        var workSheet = p.Workbook.Worksheets.Add("Votacion");
        workSheet.TabColor = System.Drawing.Color.Black;
        workSheet.DefaultRowHeight = 12;

        p.Workbook.Properties.Author = "Modulo Gerencial";
        p.Workbook.Properties.Title = "Modulo Gerencial";

        ExcelWorksheet ws = p.Workbook.Worksheets[1];
        ws.Cells.Style.Font.Size = 9; //Default font size for whole sheet
        ws.Cells.Style.Font.Name = "Tahoma"; //Default Font name for whole sheet

        int i = 1, j = 1;
        // First Row
        ws.Cells[i, j].Value = "VOTACIONES";
        // Merging the columns
        ws.Cells[i, j, i, dtData.Columns.Count].Merge = true;
        ws.Cells[i, j, i, dtData.Columns.Count].Style.Font.Bold = true;
        ws.Cells[i, j, i, dtData.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws.Cells[i, j, i, dtData.Columns.Count].Style.Font.Color.SetColor(System.Drawing.Color.Red);
        i++;
        j = 1;

        // Heading row
        foreach (DataColumn col in dtData.Columns)
        {
            ws.Cells[i, j].Value = col.Caption;
            ws.Cells[i, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            j++;
        }


        // Header row formatting
        ExcelRange range = ws.Cells[i, 1, i, j];
        range.Style.Font.Bold = true;
        // Header Background color
        range.Style.Fill.PatternType = ExcelFillStyle.None;
        //range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Green);
        // Header Text color
        range.Style.Font.Color.SetColor(System.Drawing.Color.White);
        ws.Cells["A2:E2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        ws.Cells["A2:E2"].Style.Fill.BackgroundColor.SetColor(Color.Green);
        //range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        i++;
        j = 1;

        int iStartingRow = i;
        foreach (DataRow row in dtData.Rows)
        {
            foreach (DataColumn col in dtData.Columns)
            {
                ws.Cells[i, j].Value = row[col];
                ws.Cells[i, j].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[i, j].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[i, j].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                j++;
            }

            i++;
            j = 1;

            //Apply style to Individual Cells of Alternating Row
            if (i % 2 == 0)
            {
                Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#f7f5de");
                ws.Cells[i, j, i, 5].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells[i, j, i, 5].Style.Fill.BackgroundColor.SetColor(colFromHex);
            }

        }

        // Footer row
        //ws.Cells[i, j].Value = "";
        //ws.Cells[i, j, i, 13].Merge = true;
        //ws.Cells[i, j, i, 13].Style.Font.Bold = true;
        //ws.Cells[i, j, i, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
        //ws.Cells[i, j, i, 13].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        ws.Cells[i, j, i, 4].Style.Border.Left.Style = ExcelBorderStyle.None;
        ws.Cells[i, j, i, 4].Style.Border.Right.Style = ExcelBorderStyle.None;
        ws.Cells[i, j, i, 4].Style.Border.Bottom.Style = ExcelBorderStyle.None;
        j = j + 4;


        // now we resize the columns
        range = ws.Cells[1, 1, i, j]; // complete document range
        range.AutoFitColumns();
        // Border formatting
        Border border = range.Style.Border;
        border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

        string excelName = "Resumen";
        using (var memoryStream = new MemoryStream())
        {
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
            p.SaveAs(memoryStream);
            memoryStream.WriteTo(Response.OutputStream);
            Response.Flush();
            Response.End();
        }
    }

    protected void btnexportar_Click(object sender, EventArgs e)
    {
        GenerateExcel();
        //DataTable dt = null;
        //try
        //{
        //    dt = BindGrid();
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(dt, "Detallado");
        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.Charset = "";
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", "attachment;filename=RepResumenCuentas_Detallado.xlsx");
        //        using (MemoryStream MyMemoryStream = new MemoryStream())
        //        {
        //            wb.SaveAs(MyMemoryStream);
        //            MyMemoryStream.WriteTo(Response.OutputStream);
        //            Response.Flush();
        //            Response.End();
        //        }

        //    }

        //}
        //catch (Exception Ex)
        //{
        //}
        //finally
        //{
        //    dt = null;
        //}
    }

    protected void GvReporte2_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal d = 0;
            decimal.TryParse(e.Row.Cells[4].Text, out d);
            e.Row.Cells[4].Text = d.ToString("N2");

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            dTotal += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TOTAL"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "TOTAL";
            e.Row.Cells[4].Text = dTotal.ToString("N2");
            //e.Row.Cells[6].Text = dTotal.ToString("c"); //SIMBOLO DE SOLES
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Font.Bold = true;
        }
    }
}