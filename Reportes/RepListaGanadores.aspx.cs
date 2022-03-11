using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Reportes_RepListaGanadores : System.Web.UI.Page
{
    private String Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
    policia.clsaccesodatos servidor = new policia.clsaccesodatos();
    private string __mesajeerror = "";


    private DataTable dt = null;
    private DataTable dt_enfunde = null;
    Lista _Lista = new Lista();
    private void Lista(string Opcion, string Cargo, string Periodo, string Candidato, string Socio)
    {
        _Lista.ShowMessage(__mensaje, __pagina, "", "");

        btnexportar.Enabled = false;
        try
        {
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true)
            {
                if (servidor.consultar("[dbo].[Rep_Votaciones]", Opcion, Cargo, Periodo, Candidato, Socio).Tables.Count > 0)
                {
                    dt = servidor.consultar("[dbo].[Rep_Votaciones]", Opcion, Cargo, Periodo, Candidato, Socio).Tables[0];

                }
                int NroFilas = dt.Rows.Count;
                if (NroFilas == 0)
                {
                    servidor.cerrarconexion();

                    __mensaje.Value = "No hay datos para mostrar con el criterio ingresado";
                    __pagina.Value = "";
                    gvRep_EnfxColor.DataBind();

                }
                else
                {
                    dt.Columns.Remove("FILA");

                    //dt.Columns.Remove("CUENTA_SUELDO");
                    //dt.Columns.Remove("ENTIDAD_FINANCIERA");
                    //dt.Columns.Remove("CUENTA_CTS");
                    //dt.Columns.Remove("ENTIDAD_FINANCIERA2");
                    ////dt.Columns.Remove("MES");
                    ////dt.Columns.Remove("MES");
                    ////dt.Columns.Remove("MES");
                    ////dt.Columns.Remove("MES");
                    ////dt.Columns.Remove("MES");

                    gvRep_EnfxColor.DataSource = dt;
                    gvRep_EnfxColor.DataBind();
                    servidor.cerrarconexion();
                    btnexportar.Enabled = true;
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
            _Lista.ShowMessage(__mensaje, __pagina, "Intente Buscar Nuevamente.", "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_init(object sender, EventArgs e)
    {
        Obtener_Periodo("2");
        DdlPeriodo.BorderStyle = BorderStyle.None;
        DdlPeriodo.BackColor = System.Drawing.Color.WhiteSmoke;
        Title = "Lista de Ganadores";
    }



    protected void bf_Click(object sender, EventArgs e)
    {
        __mensaje.Value = "";
        __pagina.Value = "";
        string txt_persona = DdlPeriodo.SelectedItem.ToString();

        //VALIDAR CLIENTE
        if (cbPeriodo.Checked == false)
        {
            _Lista.ShowMessage(__mensaje, __pagina, "Complete datos formulario.\n\nSeleccione Periodo a buscar por favor.", "");
            cbPeriodo.Focus();
            return;
        }
        if (cbPeriodo.Checked == true & txt_persona == "")
        {
            _Lista.ShowMessage(__mensaje, __pagina, "Complete datos formulario.\n\nSeleccione Periodo a buscar por favor.", "");
            DdlPeriodo.Focus();
            return;
        }

        DateTime Hoy = DateTime.Today;
        string fecha_actual = Hoy.ToString("dd-MM-yyyy");
        TxtFechaActual.Text = fecha_actual;

        TxtHoraActual.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
        Lista("3", ""
       , DdlPeriodo.Items[DdlPeriodo.SelectedIndex].Text.Trim(), "", "");
    }

    public string HTML()
    {
        //string txt_concepto = CboConcepto.SelectedItem.ToString();
        string txt_persona = DdlPeriodo.SelectedItem.ToString();

        Page page1 = new Page();
        HtmlForm form1 = new HtmlForm();

        gvRep_EnfxColor.EnableViewState = false;
        if (gvRep_EnfxColor.DataSource != null)
        {
            gvRep_EnfxColor.DataBind();
        }

        gvRep_EnfxColor.EnableViewState = false;
        page1.EnableViewState = false;

        page1.Controls.Add(form1);
        form1.Controls.Add(gvRep_EnfxColor);

        System.Text.StringBuilder builder1 = new System.Text.StringBuilder();
        System.IO.StringWriter writer1 = new System.IO.StringWriter(builder1);
        HtmlTextWriter writer2 = new HtmlTextWriter(writer1);

        writer2.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\n<head>\n<title>Datos</title>\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\" />\n<style>\n</style>\n</head>\n<body>\n");

        //writer2.Write("<img src=/imagenes/eliminar.png");

        writer2.Write("<table><tr><td colspan=8 style =border:none color: blue><Font-Names=Courier New size=4><center><b><u>LISTA DE GANADORES</u></b></center></font></td><td style =border:none><Font-Names=Courier New size=2>Fecha:</td><td style =border:none><Font-Names=Courier New size=2>" + TxtFechaActual.Text.Trim() + "</td></tr></table>");
        writer2.Write("<table><tr><td style =border:none><Font-Names=Courier New size=2>PERIODO:</td><td style =border:none><Font-Names=Courier New size=2>" + txt_persona + "</td><td style =border:none><Font-Names=Courier New size=2></td><td style =border:none><Font-Names=Courier New size=2>" +
        "</td><td></td><td style =border:none><Font-Names=Courier New size=2></td><td style =border:none><Font-Names=Courier New size=2>" + "</td><td></td><td style =border:none><Font-Names=Courier New size=2>Hora:</td><td style =border:none><Font-Names=Courier New size=2>" + TxtHoraActual.Text.Trim() + "</td></tr></table>");

        page1.DesignerInitialize();
        page1.RenderControl(writer2);
        writer2.Write("\n</body>\n</html>");
        page1.Dispose();
        page1 = null;
        return builder1.ToString();
    }
    protected void btnexportar_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=ListaGanadores.xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(HTML()); //Llamada al procedimiento HTML
        Response.End();
    }

    protected override void Render(HtmlTextWriter writer)
    {
        EnsureChildControls();
        base.Render(writer);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
    }
    protected void gvRep_EnfxColor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal d;
            decimal.TryParse(e.Row.Cells[4].Text, out d);
            e.Row.Cells[4].Text = d.ToString("N2");
        }
    }
    protected void BtnListaTrabajador_Click(object sender, EventArgs e)
    {

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
}
