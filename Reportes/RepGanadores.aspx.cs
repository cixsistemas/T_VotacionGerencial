using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;

using OfficeOpenXml;
using OfficeOpenXml.Style;

public partial class Reportes_RepGanadores : System.Web.UI.Page
{
    private string Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
    policia.clsaccesodatos servidor = new policia.clsaccesodatos();
    Lista _Lista = new Lista();
    //DataTable tabla_;
    static DataTable dt_1;
    static string UsuarioGeneraReporte;
    public bool Consultar_Formulario_Derecho_Autorizacion(int Usuario, string Formulario, string Derecho, string msg)
    {
        bool _ok = false;
        __mensaje.Value = "";
        __pagina.Value = "";
        try
        {
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true)
            {
                DataTable dt = servidor.consultar("[dbo].[__consultar_formulario_derecho_autorizacion]", Usuario, Formulario, Derecho).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    servidor.cerrarconexion();
                    if (msg.Trim().Length > 0)
                    {
                        __mensaje.Value = msg;
                        __pagina.Value = "";
                    }
                    _ok = false;
                }
                else
                {
                    _ok = Convert.ToBoolean(dt.Rows[0].ItemArray[3]);//"Aprobacion  
                    servidor.cerrarconexion();
                }
            }
            else
            {
                servidor.cerrarconexion();
                __mensaje.Value = servidor.getMensageError();
                __pagina.Value = "../CerrarSession.aspx";
            }
        }
        catch (Exception)
        {
            __mensaje.Value = "Error inesperado al intentar conectarnos con el servidor.";
            __pagina.Value = "../CerrarSession.aspx";
        }
        return _ok;
    }

    private void Lista(string Opcion, string Cargo, string Periodo, string Candidato, string Socio)
    {
        _Lista.ShowMessage(__mensaje, __pagina, "", "");
        //try
        //{
        btnexportar.Visible = false;
        policia.clsaccesodatos servidor = new policia.clsaccesodatos();
        servidor.cadenaconexion = Ruta;
        if (servidor.abrirconexion() == true)
        {
            dt_1 = servidor.consultar("[dbo].[Rep_Votaciones]"
                , Opcion, Cargo, Periodo, Candidato, Socio).Tables[0];

            if (dt_1.Rows.Count == 0)
            {
                servidor.cerrarconexion();
                _Lista.ShowMessage(__mensaje, __pagina, "No hay datos para mostrar", "");
                GvReporte.DataBind();
            }
            else
            {
                //dt_1 = _Lista.Generar_Totales_VentasPorMes(dt_1, 1);
                GvReporte.DataSource = dt_1;
                GvReporte.DataBind();
                servidor.cerrarconexion();
                btnexportar.Visible = true;
            }
        }
        else
        {
            servidor.cerrarconexion();
            _Lista.ShowMessage(__mensaje, __pagina, servidor.getMensageError(), "../CerrarSession.aspx");
        }
        //}
        //catch (Exception)
        //{
        //    //_Lista.ShowMessage(__mensaje, __pagina, "Error inesperado al intentar conectarnos con el servidor.", "../CerrarSession.aspx");
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ////=============================================================================================================
        ////Verificamos si el usuario ha iniciado sesion.
        //__mensaje.Value = "";
        //__pagina.Value = "";
        //string[] Datos = (string[])Session["__JSAR__"];
        //if (Datos == null)
        //{
        //    __mensaje.Value = "Ud. no esta autorizado para ingresar a esta página, inicie sesion por favor.";
        //    __pagina.Value = "../CerrarSession.aspx";
        //    return;
        //}
        //UsuarioGeneraReporte = Datos[1];
        ////=============================================================================================================

    }
    protected void Page_init(object sender, EventArgs e)
    {
        //try{

        Obtener_Cargo("1");
        Obtener_Periodo("2");

        DdlCargo.BorderStyle = BorderStyle.None;
        DdlCargo.BackColor = System.Drawing.Color.WhiteSmoke;

        Obtener_Periodo("2");
        DdlPeriodo.BorderStyle = BorderStyle.None;
        DdlPeriodo.BackColor = System.Drawing.Color.WhiteSmoke;

        //DateTime Hoy = DateTime.Today;
        //TxtAño.Text = Hoy.ToString("yyyy");

        //}catch (Exception){
        //    //throw;
        //}

    }


    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        __mensaje.Value = "";
        __pagina.Value = "";
        try
        {
            bool ok;
            ////==========================================================================================================================================================================================================================
            //ok = Consultar_Formulario_Derecho_Autorizacion(Convert.ToInt32(((object[])Session["__JSAR__"])[0]), "LIQUIDACION DIARIA", "ACEPTAR", "Ud. no tiene derecho y permisos necesarios para generar reporte en esta pagina web.\n\nComuniquese con el administrador del sistema");
            //if (ok == false)
            //{
            //    ////trabajadores.Visible = false;
            //    return;
            //}
            ////==========================================================================================================================================================================================================================

        }
        catch (Exception)
        {
            __mensaje.Value = "Ud. no esta autorizado para ingresar a esta página, inicie sesion por favor.";
            __pagina.Value = "../CerrarSession.aspx";
        }

        //bool ok1 = TxtAño.Text.Trim() != "";//validamos informacion.
        //if (ok1 == false)//verificamos si hay fundo y lote.
        //{
        //    _Lista.ShowMessage(__mensaje, __pagina, "Específica Año a Buscar, por favor.", "");
        //    this.TxtAño.Focus();
        //    return;
        //}

        DateTime Hoy = DateTime.Today;
        string fecha_actual = Hoy.ToString("dd-MM-yyyy");
        //TxtFechaActual.Text = fecha_actual;
        //TxtHoraActual.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;

        Lista("1", DdlCargo.Items[DdlCargo.SelectedIndex].Text.Trim()
        , DdlPeriodo.Items[DdlPeriodo.SelectedIndex].Text.Trim(), "", "");
    }


    protected void btnexportar_Click(object sender, EventArgs e)
    {
        //string excelName = "RepVentasMes";
        //CreateExcelFile();
        GenerateExcel();
    }

    protected void GenerateExcel()
    {
        //https://www.gopiportal.in/2017/08/creating-excel-reports-using-epplus.html
        //https://desarrolladores.me/2014/02/c-creando-archivo-de-excel-con-epplus/

        DataTable dtData = GetExcelData();

        ExcelPackage p = new ExcelPackage();
        var workSheet = p.Workbook.Worksheets.Add("Ventas");
        workSheet.TabColor = System.Drawing.Color.Black;
        workSheet.DefaultRowHeight = 12;

        //using (ExcelPackage p = new ExcelPackage())
        //{
        ////Document properties
        p.Workbook.Properties.Author = "Modulo Gerencial";
        p.Workbook.Properties.Title = "Modulo Gerencial";

        //p.Workbook.Worksheets.Add("Asistencia");
        ExcelWorksheet ws = p.Workbook.Worksheets[1];
        ws.Cells.Style.Font.Size = 9; //Default font size for whole sheet
        ws.Cells.Style.Font.Name = "Tahoma"; //Default Font name for whole sheet

        int i = 1, j = 1;
        // First Row
        ws.Cells[i, j].Value = "REPORTE DE VENTAS POR MES";
        // Merging the columns
        ws.Cells[i, j, i, dtData.Columns.Count].Merge = true;
        ws.Cells[i, j, i, dtData.Columns.Count].Style.Font.Bold = true;
        ws.Cells[i, j, i, dtData.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws.Cells[i, j, i, dtData.Columns.Count].Style.Font.Color.SetColor(System.Drawing.Color.Red);
        //ws.Cells[i, j, i, dtData.Columns.Count + 1].Merge = true;
        //ws.Cells[i, j, i, dtData.Columns.Count + 1].Style.Font.Bold = true;
        //ws.Cells[i, j, i, dtData.Columns.Count + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        i++;
        j = 1;

        // Heading row
        foreach (DataColumn col in dtData.Columns)
        {
            ws.Cells[i, j].Value = col.Caption;
            ws.Cells[i, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            j++;
        }

        //ws.Cells[i, j].Value = "Earned";
        //ws.Cells[i, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //// Adding comment
        //ws.Cells[i, j].AddComment("Earned= Basic + HRA + DA", "");

        // Header row formatting
        ExcelRange range = ws.Cells[i, 1, i, j];
        range.Style.Font.Bold = true;
        // Header Background color
        range.Style.Fill.PatternType = ExcelFillStyle.None;
        //range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Green);
        // Header Text color
        range.Style.Font.Color.SetColor(System.Drawing.Color.White);
        ws.Cells["A2:N2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        ws.Cells["A2:N2"].Style.Fill.BackgroundColor.SetColor(Color.Green);
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
                //ws.Cells[i, j].Style.Numberformat.Format = "#,##0.00";
                //Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#f7f5de");
                //ws.Cells[i, j].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                //////ws.Cells[i, j, i, 8].Style.Numberformat.Format = "#,##0.00";
                j++;
            }

            ////**********************************************************************************
            ////***** FUNCIONA PERO HAY QUE DETERMINAR EL RANGO AUTOMATICAMENTE *****
            //foreach (var cell in ws.Cells[i, j - 9])
            //{
            //    cell.Value = Convert.ToDecimal(cell.Value);
            //}
            //foreach (var cell in ws.Cells[i, j - 8])
            //{
            //    cell.Value = Convert.ToDecimal(cell.Value);
            //}
            //foreach (var cell in ws.Cells[i, j - 7])
            //{
            //    cell.Value = Convert.ToDecimal(cell.Value);
            //}
            //foreach (var cell in ws.Cells[i, j - 6])
            //{
            //    cell.Value = Convert.ToDecimal(cell.Value);
            //}
            //foreach (var cell in ws.Cells[i, j - 5])
            //{
            //    cell.Value = Convert.ToDecimal(cell.Value);
            //}
            //foreach (var cell in ws.Cells[i, j - 4])
            //{
            //    cell.Value = Convert.ToDecimal(cell.Value);
            //}
            //foreach (var cell in ws.Cells[i, j - 3])
            //{
            //    cell.Value = Convert.ToDecimal(cell.Value);
            //}
            //foreach (var cell in ws.Cells[i, j - 2])
            //{
            //    cell.Value = Convert.ToDecimal(cell.Value);
            //}
            //foreach (var cell in ws.Cells[i, j - 1])
            //{
            //    cell.Value = Convert.ToDecimal(cell.Value);
            //}
            ws.Cells[i, j - 13].Style.Numberformat.Format = "#,##0.00";
            ws.Cells[i, j - 12].Style.Numberformat.Format = "#,##0.00";
            ws.Cells[i, j - 11].Style.Numberformat.Format = "#,##0.00";
            ws.Cells[i, j - 10].Style.Numberformat.Format = "#,##0.00";
            ws.Cells[i, j - 9].Style.Numberformat.Format = "#,##0.00";
            ws.Cells[i, j - 8].Style.Numberformat.Format = "#,##0.00";
            ws.Cells[i, j - 7].Style.Numberformat.Format = "#,##0.00";
            ws.Cells[i, j - 6].Style.Numberformat.Format = "#,##0.00";
            ws.Cells[i, j - 5].Style.Numberformat.Format = "#,##0.00";
            ws.Cells[i, j - 4].Style.Numberformat.Format = "#,##0.00";
            ws.Cells[i, j - 3].Style.Numberformat.Format = "#,##0.00";
            ws.Cells[i, j - 2].Style.Numberformat.Format = "#,##0.00";
            ws.Cells[i, j - 1].Style.Numberformat.Format = "#,##0.00";
            ////********************************************************************************

            //// Formula for Earned: Sum(B4:D4)
            //ws.Cells[i, j].Formula = "Sum(" + ws.Cells[i, j - 3].Address + ":" + ws.Cells[i, j - 1].Address + ")";
            i++;
            j = 1;

            //Apply style to Individual Cells of Alternating Row
            if (i % 2 != 0)
            {
                Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#f7f5de");
                ws.Cells[i, j, i, 14].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells[i, j, i, 14].Style.Fill.BackgroundColor.SetColor(colFromHex);
            }

            ////***** FUNCIONA PERO HAY QUE DETERMINAR EL RANGO AUTOMATICAMENTE *****
            //foreach (var cell in ws.Cells["F3:N3"])
            //{
            //    cell.Value = Convert.ToDecimal(cell.Value);
            //}
            //ws.Cells["F3:N3"].Style.Numberformat.Format = "#,##0.00";

        }

        // Footer row
        //ws.Cells[i, j].Value = "";
        //ws.Cells[i, j, i, 13].Merge = true;
        //ws.Cells[i, j, i, 13].Style.Font.Bold = true;
        //ws.Cells[i, j, i, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
        //ws.Cells[i, j, i, 13].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        ws.Cells[i, j, i, 13].Style.Border.Left.Style = ExcelBorderStyle.None;
        ws.Cells[i, j, i, 13].Style.Border.Right.Style = ExcelBorderStyle.None;
        ws.Cells[i, j, i, 13].Style.Border.Bottom.Style = ExcelBorderStyle.None;
        j = j + 13;

        //// Formula for Basic Total
        //ws.Cells[i, j].Formula = "Sum(" + ws.Cells[iStartingRow, j].Address + ":" + ws.Cells[i - 1, j].Address + ")";
        //j++;
        //// Formula for HRA Total
        //ws.Cells[i, j].Formula = "Sum(" + ws.Cells[iStartingRow, j].Address + ":" + ws.Cells[i - 1, j].Address + ")";
        //j++;
        //// Formula for DA Total
        //ws.Cells[i, j].Formula = "Sum(" + ws.Cells[iStartingRow, j].Address + ":" + ws.Cells[i - 1, j].Address + ")";
        //j++;
        //// Formula for Sum of Total
        //ws.Cells[i, j].Formula = "Sum(" + ws.Cells[iStartingRow, j].Address + ":" + ws.Cells[i - 1, j].Address + ")";

        //// Footer row formatting
        //range = ws.Cells[i, 1, i, j];
        ////range.Style.Font.Bold = true;
        //// Footer background color
        //range.Style.Fill.PatternType = ExcelFillStyle.None;
        ////range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
        //// Footer Text color
        //range.Style.Font.Color.SetColor(System.Drawing.Color.Blue);

        // now we resize the columns
        range = ws.Cells[1, 1, i, j]; // complete document range
        range.AutoFitColumns();
        // Border formatting
        Border border = range.Style.Border;
        border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

        string excelName = "RepVentasPorMes";
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
    private DataTable GetExcelData()
    {
        DataTable dtData = new DataTable();
        //  Lista("5", "", "", "", DdlSucursal.Items[DdlSucursal.SelectedIndex].Text.Trim()
        //, CboPtoVenta.Items[CboPtoVenta.SelectedIndex].Text.Trim(), "", "", "", TxtAño.Text.Trim());
        DataRow dr = dtData.NewRow();
        dtData.Rows.Add(dr);
        return dt_1;
    }


    protected void GvReporte_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string Mes;
            Mes = e.CommandName;

            if (e.CommandName == "TOTAL")
            {
                // Selecciona el indice de la fila del boton en el que se hizo clic
                int index = Convert.ToInt32(e.CommandArgument);
                // retorna el row en que se hizo clic
                GridViewRow row = GvReporte.Rows[index];
                // asigna el valor de la celda de la columna 2 y la fila en que se hizo clic  
                string Item = Server.HtmlDecode(row.Cells[0].Text); //[CANDIDATO]

               //CUANDO ITEM SEA "TOTAL" DEBE APARECER VACIO
                if (Item == "TOTAL"){
                    Item = "";
                }

                OpenNewWindow("RepGanadores2.aspx?Opcion=2"
                 + "&Cargo=" + DdlCargo.Items[DdlCargo.SelectedIndex].Text.Trim()
                 + "&Periodo=" + DdlPeriodo.Items[DdlPeriodo.SelectedIndex].Text.Trim()
                 + "&Candidato=" + Item + "&Socio="
                 );

                ////=========== SI VA ANTES VARIABLE "Mes" LA CONVIERTE A NUMERO ================

                //if (Mes == "TOTAL")
                //{
                //    //Mes = "12";
                //    GvReporte.Rows[row.RowIndex].Cells[Convert.ToInt32(Mes)].Controls[0].Focus();
                //    GvReporte.Rows[row.RowIndex].Cells[Convert.ToInt32(Mes)].ForeColor = Color.Red;
                //    //GvReporte.Rows[row.RowIndex].BackColor = Color.Ivory;
                //}
                ////===========================================================================

            }
        }catch (Exception){
            throw;
        }

    }
    public void OpenNewWindow(string url)
    {
        ClientScript.RegisterStartupScript(
        GetType(), "newWindow", string.Format("<script>window.open('{0}');</script>", url));
    }
    protected void GvReporte_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try{
            if (e.Row.RowType == DataControlRowType.DataRow){
                e.Row.Cells[4].ToolTip = "TOTAL";
                //e.Row.Cells[0].BackColor = Color.Ivory;
            }

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    decimal d ;
            //    decimal.TryParse(e.Row.Cells[4].Text, out d);
            //    e.Row.Cells[4].Text = d.ToString("N2");
            //}

            if (e.Row.RowType == DataControlRowType.DataRow){
                string _estado = DataBinder.Eval(e.Row.DataItem, "CANDIDATO").ToString();
                if (_estado == "TOTAL"){
                    e.Row.BackColor = System.Drawing.Color.DarkSeaGreen;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Font.Bold = true;
                }

                //MANTENER EL ORDEN ESTABLECIDO, YA QUE SINO SE HACE NO FUNCIONA
                string _13 = DataBinder.Eval(e.Row.DataItem, "TOTAL").ToString();
                if (_13 == "0"){
                    e.Row.Cells[4].Enabled = false;
                    //e.Row.Cells[1].Text = "";
                }
            }

            DataRowView drv = (DataRowView)e.Row.DataItem;
            for (int i = 0; i < drv.DataView.Table.Columns.Count; i++)
            {
                if (drv.DataView.Table.Columns[i].ColumnName.Equals("TOTAL"))
                {
                    e.Row.Cells[i].BackColor = System.Drawing.Color.WhiteSmoke;
                    e.Row.Font.Bold = true;
                }
            }
        }
        catch (Exception)
        {
            //throw;
        }

    }

    protected void cbPeriodo_CheckedChanged(object sender, EventArgs e)
    {
        __mensaje.Value = "";
        __pagina.Value = "";

        if (cbPeriodo.Checked == false)
        {
            DdlPeriodo.Enabled = false;
            //string txt_sucursal = DdlSucursal.SelectedItem.ToString();
            Obtener_Periodo("2");
            DdlPeriodo.BorderStyle = BorderStyle.None;
            DdlPeriodo.BackColor = System.Drawing.Color.WhiteSmoke;
        }

        if (cbPeriodo.Checked == true)
        {
            DdlPeriodo.Enabled = true;
            DdlPeriodo.Focus();
            // AGREGADO EL DIA 12-08-2018
            //string txt_sucursal = DdlSucursal.SelectedItem.ToString();
            //Obtener_PtoVenta("2");
        }
    }
    private void Obtener_Periodo(string Opcion)
    {
        try
        {
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true)
            {
                System.Data.DataTable dt = servidor.consultar("[dbo].[_pr_Obtener_Varios_Votaciones]", Opcion).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    servidor.cerrarconexion();
                    _Lista.ShowMessage(__mensaje, __pagina, "Error, al intentar registro.", "CerrarSession.aspx");
                }
                else
                {
                    DdlPeriodo.DataSource = dt;
                    DdlPeriodo.DataTextField = "NOMBRE";
                    DdlPeriodo.DataValueField = "CODIGO";
                    DdlPeriodo.DataBind();
                    servidor.cerrarconexion();
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
            servidor.cerrarconexion();
            _Lista.ShowMessage(__mensaje, __pagina, "Error inesperado al intentar conectarnos con el servidor.", "../CerrarSession.aspx");
        }
    }
    protected void CbCargo_CheckedChanged(object sender, EventArgs e)
    {
        __mensaje.Value = "";
        __pagina.Value = "";

        if (CbCargo.Checked == false)
        {
            DdlCargo.Enabled = false;
            Obtener_Cargo("1");
            DdlCargo.BorderStyle = BorderStyle.None;
            DdlCargo.BackColor = System.Drawing.Color.WhiteSmoke;
            //string txt_sucursal = DdlSucursal.SelectedItem.ToString();
            //Obtener_Persona("1", txt_sucursal);
        }

        if (CbCargo.Checked == true)
        {
            DdlCargo.Enabled = true;
            DdlCargo.Focus();
        }
    }

    private void Obtener_Cargo(string Opcion)
    {
        try
        {
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true)
            {
                System.Data.DataTable dt = servidor.consultar("[dbo].[_pr_Obtener_Varios_Votaciones]", Opcion).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    servidor.cerrarconexion();
                    _Lista.ShowMessage(__mensaje, __pagina, "Error, al intentar recuperar Clientes.", "../CerrarSession.aspx");
                }
                else
                {
                    DdlCargo.DataSource = dt;
                    DdlCargo.DataTextField = "NOMBRE";
                    DdlCargo.DataValueField = "CODIGO";
                    DdlCargo.DataBind();
                    servidor.cerrarconexion();
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
            servidor.cerrarconexion();
            _Lista.ShowMessage(__mensaje, __pagina, "Error inesperado al intentar conectarnos con el servidor.", "../CerrarSession.aspx");
        }
    }
}