using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
public partial class Votacion : System.Web.UI.Page
{
    private string Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
    policia.clsaccesodatos servidor = new policia.clsaccesodatos();

    Lista _Lista = new Lista();
    DataTable TablaCandidatos_;
    DataTable TablaListaGanador;
    DataTable T_ListaSociosVotaron;
    DataTable T_ListaSociosCapital;
    static string CapitalSocios;
    static string ProporcionSocios;
    int ConteoSociosVotaron;
    static string Cargo ;
    static string Periodo;
    private void ShowMessage(string msg, string paginaweb)
    {
        __mensaje.Value = msg;
        __pagina.Value = paginaweb;
    }
   
    private void Matenimiento(int Id_, bool Voto, int Id_Socio, string capital, string proporcion
        , int Id_Candidato ,DateTime fecha , string valido, string operacion)
    {
        //try
        //{
            ////string Cargo = Decrypt(HttpUtility.UrlDecode(Request.QueryString["C"]));

            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true){
                servidor.ejecutar("[dbo].[pa_mantenimiento_Votacion_Candidato]",
                                    false,
                                    Id_,
                                    Voto,
                                    Id_Socio,
                                    capital.Trim(),
                                    proporcion.Trim(),
                                    Id_Candidato,
                                    fecha,
                                    valido,
                                    operacion,
                                    0, "");
                if (servidor.getRespuesta() == 1){
                    servidor.cerrarconexiontrans();
                    __mensaje.Value = servidor.getMensaje();
                    __pagina.Value = "VotacionCandidato.aspx?C=" + Cargo + "&P=" + Periodo;
                //NO MOSTRAR BOTON REGISTRAR
                btnRegistrar.Visible = false;
                }
                else
                {
                    servidor.cancelarconexiontrans();
                    __mensaje.Value = servidor.getMensaje();
                    __pagina.Value = "";
                //MOSTRAR BOTON REGISTRAR
                btnRegistrar.Visible = true;
                //this.__pagina.Value = "_Asistencia.aspx";
            }
            }
            else
            {
                servidor.cancelarconexiontrans();
                __mensaje.Value = servidor.getMensageError();
                __pagina.Value = "CerrarSession.aspx";
            }

        //}
        //catch (Exception)
        //{
        //    __mensaje.Value = "Seleccione Candidato, por el cual va a votar.  ";
        //    __pagina.Value = "";
        //}
    }

  

    protected void Page_Load(object sender, EventArgs e)
    {

        //Cargo = Decrypt(HttpUtility.UrlDecode(Request.QueryString["C"]));
        Cargo = ((Request.QueryString["C"]));
        Periodo = ((Request.QueryString["P"]));


        //if (!IsPostBack)
        //{
        //    Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
        //    Response.Cache.SetAllowResponseInBrowserHistory(false);
        //    Response.Cache.SetNoStore();
        //}

        if (Session["INDICE"] == null)
        {
            Session["INDICE"] = -1;
        }

        ListaCandidatos(Cargo, Periodo, "2");
        ListaGanador(Cargo, Periodo, "", "2");
    }



    protected void Page_init(object sender, EventArgs e)
    {
        __mensaje.Value = "";
        __pagina.Value = "";
        //this.Listar_("", "1");
        
        Obtener_Socio("3");
        FechaActual.Text = DateTime.Now.ToLongDateString();
        //ListaCandidatos(Cargo, Periodo, "2");
        //ListaGanador(Cargo, Periodo, "", "2");
        ////Cargo = Decrypt(HttpUtility.UrlDecode(Request.QueryString["C"]));
    }

    private bool Validar_Datos()
    {
        Boolean ok = Convert.ToInt32(this.DdlSocio.SelectedValue) != -1;
        if (ok == false)//verificamos si el usuario selecciono empacadora.
        {
            _Lista.ShowMessage(__mensaje, __pagina, "Seleccione Socio, por favor.", "");
            this.DdlSocio.Focus();
            return false;
        }

        //ok = ok && Convert.ToString(this.ConteoSociosVotaron) != "";
        //if (ok == true)
        //{
        //    ok = ok && Convert.ToDouble(this.ConteoSociosVotaron) >= 1;
        //}
        //if (ok == false)//verificamos si hay cantidad de cajas para exportacion.
        //{
        //    _Lista.ShowMessage(__mensaje, __pagina,
        //        "Ingrese por favor cantidad de cajas para exportacion.\n Socio ya voto para este cargo", "");
        //    this.btnRegistrar.Focus();
        //    return false;
        //}

        //ok = ok && this.Id_Candidato.Value != "";
        //if (ok == false)//verificamos si hay produccion.
        //{
        //    _Lista.ShowMessage(__mensaje, __pagina, "Seleccione Candidato, por el cual va a votar..", "");
        //    this.GrVoto.Focus();
        //    return false;
        //}
        return ok;
    }
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        __mensaje.Value = "";
        __pagina.Value = "";

    
        Id_Candidato.Value = Convert.ToString(Request.Form["RadioButton1"]);
        //String mobilename = GrVoto.Rows[sno - 1].Cells[1].Text;
        //String companyname = GrVoto.Rows[sno - 1].Cells[2].Text;
        //String price = GrVoto.Rows[sno - 1].Cells[3].Text;
        //Label2.Text = "You Have Selected Mobile " + mobilename + " with Company Name " + companyname + " Price " + price;
        Label2.Text = "You Have Selected Mobile " + Id_Candidato.Value + " with Company Name " + " Price ";

        //Title = Id_Candidato.Value;

        if (Id_Candidato.Value == "")
        {
            _Lista.ShowMessage(__mensaje, __pagina, "Seleccione Candidato, por el cual va a votar..", "");
            GrVoto.Focus();
        }
        DateTime Hoy = DateTime.Today;
        string fecha_actual = Hoy.ToString("dd-MM-yyyy");

        bool ok = this.Validar_Datos();//validar informacion
        if (ok == false)
        {
            return;
        }

        //ListaSociosVotaron(Cargo, Periodo, Convert.ToString(DdlSocio.Items[DdlSocio.SelectedIndex].Text.Trim()), "3");

     
        try
        {
            Matenimiento(Convert.ToInt32(Id_.Value.Trim()),
                      true, //VOTO
                      Convert.ToInt32(DdlSocio.SelectedValue), // SOCIO
                      CapitalSocios, //CAPITAL
                      ProporcionSocios, //PROPORCION
                      Convert.ToInt32(Id_Candidato.Value), //CANDIDATO
                      Convert.ToDateTime(fecha_actual),//FECHA_MODIFICACION
                  "SI", "N");
        }
        catch (Exception)
        {

            //throw;
        }
      

        //ListaCandidatos(DdlCargo.Items[DdlCargo.SelectedIndex].Text.Trim(), "2");
    }
   
    private void Obtener_Socio(string Opcion)
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
                    _Lista.ShowMessage(__mensaje, __pagina, "Error, al intentar recuperar datos.", "CerrarSession.aspx");
                }
                else
                {
                    DdlSocio.DataSource = dt;
                    DdlSocio.DataTextField = "NOMBRE";
                    DdlSocio.DataValueField = "CODIGO";
                    DdlSocio.DataBind();
                    servidor.cerrarconexion();
                }
            }
            else
            {
                servidor.cerrarconexion();
                _Lista.ShowMessage(__mensaje, __pagina, servidor.getMensageError(), "CerrarSession.aspx");
            }
        }
        catch (Exception)
        {
            servidor.cerrarconexion();
            _Lista.ShowMessage(__mensaje, __pagina, "Error inesperado al intentar conectarnos con el servidor.", "CerrarSession.aspx");
        }
    }

    public void ListaCandidatos(string criterio, string periodo, string Opcion)
    {
        //btnexportar.Visible = false;
        try
        {
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true)
            {
                if (servidor.consultar("[dbo].[Pa_Listar_Candidato]", criterio, periodo, Opcion).Tables.Count > 0)
                {
                    TablaCandidatos_ = servidor.consultar("[dbo].[Pa_Listar_Candidato]", criterio, periodo, Opcion).Tables[0];
                }
                int NroFilas = TablaCandidatos_.Rows.Count;
                if (NroFilas == 0)
                {
                    servidor.cerrarconexion();
                    MensajeCandidatos.Visible = true;
                    btnRegistrar.Visible = false;
                    DdlSocio.Enabled = false;
                    MensajeCandidatos.Text = "Votacion esta Cerrada";
                    __pagina.Value = "VotacionCandidato.aspx?C=" + Cargo + "&P=" + Periodo;
                    
                    //_Lista.ShowMessage(__mensaje, __pagina, "No hay datos para mostrar con el criterio ingresado...", "");
                    GrVoto.DataBind();
                }
                else
                {
                    ////OCULTAR COLUMNAS
                    //TablaLote_.Columns.Remove("ID");
                    //TablaLote_.Columns.Remove("PTO_VENTA");

                    GrVoto.DataSource = TablaCandidatos_;
                    GrVoto.DataBind();
                    servidor.cerrarconexion();

                    MensajeCandidatos.Visible = false;
                    btnRegistrar.Visible = true;
                    DdlSocio.Enabled = true;
                }
            }
            else
            {
                servidor.cerrarconexion();
                __mensaje.Value = servidor.getMensageError();
                __pagina.Value = "CerrarSession.aspx";
            }
        }
        catch (Exception)
        {
            throw;
            //__mensaje.Value = "Vuelva a Generar Reporte";
        }
    }
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        //ListaCandidatos(DdlCargo.Items[DdlCargo.SelectedIndex].Text.Trim(), "2");
        int sno = Convert.ToInt32(Request.Form["RadioButton1"]);
        //String mobilename = GrVoto.Rows[sno - 1].Cells[1].Text;
        //String companyname = GrVoto.Rows[sno - 1].Cells[2].Text;
        //String price = GrVoto.Rows[sno - 1].Cells[3].Text;
        Label2.Text = "Id " + sno + " with Company Name " + " Price ";
        Title = Id_.Value+" "+ Id_CargoVoto.Value+ " "+Id_Periodo.Value;
    }

    protected void GrVoto_SelectedIndexChanging(object sender, System.Web.UI.WebControls.GridViewSelectEventArgs e)
    {
        Session["INDICE"] = e.NewSelectedIndex;
        int indice = Convert.ToInt32(Session["INDICE"]);
        if (indice == -1)
        {
            _Lista.ShowMessage(__mensaje, __pagina, "Seleccione registro a modificar", "");
            return;
        }
        GrVoto.SelectedIndex = indice;
        //gvReporte.SelectedRow.Focus();
        Id_.Value = Convert.ToString(TablaCandidatos_.Rows[indice].ItemArray[1]);
        Id_CargoVoto.Value = Convert.ToString(TablaCandidatos_.Rows[indice].ItemArray[4]);
        Id_Periodo.Value = Convert.ToString(TablaCandidatos_.Rows[indice].ItemArray[5]);

        Title = Id_.Value;
    }

    public void ListaGanador(string Cargo, string Periodo, string datos3, string Opcion)
    {
        //btnexportar.Visible = false;
        try
        {
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true)
            {
                if (servidor.consultar("[dbo].[Pa_Listar_Votacion_Candidato]", Cargo, Periodo, datos3, Opcion).Tables.Count > 0)
                {
                    TablaListaGanador = servidor.consultar("[dbo].[Pa_Listar_Votacion_Candidato]", Cargo, Periodo, datos3, Opcion).Tables[0];
                }
                int NroFilas = TablaListaGanador.Rows.Count;
                if (NroFilas == 0)
                {
                    servidor.cerrarconexion();
                    //_Lista.ShowMessage(__mensaje, __pagina, "No hay datos para mostrar con el criterio ingresado...3", "");
                    GvListado.DataBind();
                    BtnCerrarVotacion.Visible = false;
                }
                else
                {
                    ////OCULTAR COLUMNAS
                    //tabla_.Columns.Remove("PTO_VENTA");

                    GvListado.DataSource = TablaListaGanador;
                    GvListado.DataBind();
                    BtnCerrarVotacion.Visible = true;
                    servidor.cerrarconexion();
                }
            }
            else
            {
                servidor.cerrarconexion();
                __mensaje.Value = servidor.getMensageError();
                __pagina.Value = "CerrarSession.aspx";
            }
        }
        catch (Exception)
        {
            //throw;
            __mensaje.Value = "Vuelva a Generar Reporte";
        }
    }

    private void Matenimiento_CerrarVotacion(string IdCandidato, string nombreprofesion, string IdPeriodo, string IdCargo, string Valido,
      string Cerrada, string operacion)
    {
        //try
        //{
        policia.clsaccesodatos servidor = new policia.clsaccesodatos();
        servidor.cadenaconexion = Ruta;
        if (servidor.abrirconexiontrans() == true)
        {
            servidor.ejecutar("[dbo].[pa_mantenimiento_Candidato]",
                                false,
                                IdCandidato.Trim(),
                                nombreprofesion.Trim(),
                                 IdPeriodo,
                                 IdCargo,
                                 Valido,
                                 Cerrada.Trim(),
                                operacion,
                                0, "");
            if (servidor.getRespuesta() == 1)
            {
                servidor.cerrarconexiontrans();
                __mensaje.Value = servidor.getMensaje();
                __pagina.Value = "VotacionCandidato.aspx?C=" + Cargo + "&P=" + Periodo;
                //__pagina.Value = "Candidato.aspx";
                //NO MOSTRAR BOTON REGISTRAR
                btnRegistrar.Visible = false;
            }
            else
            {
                servidor.cancelarconexiontrans();
                __mensaje.Value = servidor.getMensaje();
                //__pagina.Value = "Candidato.aspx";
                //MOSTRAR BOTON REGISTRAR
                btnRegistrar.Visible = true;
            }
        }
        else
        {
            servidor.cancelarconexiontrans();
            __mensaje.Value = servidor.getMensageError();
            __pagina.Value = "CerrarSession.aspx";
        }

        //}
        //catch (Exception)
        //{
        //    __mensaje.Value = "Error inesperado al intentar conectarnos con el servidor.";
        //    //this.__pagina.Value = "CerrarSession.aspx";
        //}
    }

   
    protected void BtnCerrarVotacion_Click(object sender, EventArgs e)
    {
        __mensaje.Value = "";
        __pagina.Value = "";

         ListaCandidatos(Cargo, Periodo, "2");
        string IdCandidato, IdCargo, IdPeriodo;
        for (int j = 0; j <= TablaCandidatos_.Rows.Count - 1; j++)
        {
            IdCandidato = TablaCandidatos_.Rows[j].ItemArray[0].ToString();
            IdCargo = TablaCandidatos_.Rows[j].ItemArray[6].ToString();
            IdPeriodo = TablaCandidatos_.Rows[j].ItemArray[7].ToString();

            Matenimiento_CerrarVotacion(IdCandidato, "", IdCargo, IdPeriodo, "", "", "C");  
        }     
    }

    public void ListaSociosVotaron(string Cargo, string Periodo, string IdSocio, string Opcion)
    {
        try
        {
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true)
            {
                if (servidor.consultar("[dbo].[Pa_Listar_Votacion_Candidato]", Cargo, Periodo, IdSocio, Opcion).Tables.Count > 0)
                {
                    T_ListaSociosVotaron = servidor.consultar("[dbo].[Pa_Listar_Votacion_Candidato]", Cargo, Periodo, IdSocio, Opcion).Tables[0];
                }
                int NroFilas = T_ListaSociosVotaron.Rows.Count;
                if (NroFilas == 0)
                {
                    servidor.cerrarconexion();
                    MensajeSociosVotaron.Visible = false;
                    MensajeSociosVotaron.Text = "";
                    //MOSTRAR BOTON REGISTRAR
                    btnRegistrar.Visible = true;
                    //__pagina.Value = "VotacionCandidato.aspx?C=" + Cargo + "&P=" + Periodo;
                }
                else
                {
                    MensajeSociosVotaron.Visible = true;
                    MensajeSociosVotaron.Text = "Socio ya ha votado para este cargo";
                    //NO MOSTRAR BOTON REGISTRAR
                    btnRegistrar.Visible = false;
                    //__pagina.Value = "VotacionCandidato.aspx?C=" + Cargo + "&P=" + Periodo;
                    ConteoSociosVotaron = Convert.ToInt32(T_ListaSociosVotaron.Rows[0].ItemArray[4].ToString());
                   
                    ////OCULTAR COLUMNAS
                    //TablaLote_.Columns.Remove("ID");
                    //TablaLote_.Columns.Remove("PTO_VENTA");

                    //GrVoto.DataSource = TablaCandidatos_1;
                    //GrVoto.DataBind();
                    servidor.cerrarconexion();
                }
            }
            else
            {
                servidor.cerrarconexion();
                __mensaje.Value = servidor.getMensageError();
                __pagina.Value = "CerrarSession.aspx";
            }
        }
        catch (Exception)
        {
            throw;
            //__mensaje.Value = "Vuelva a Generar Reporte";
        }
    }

    public void ListaSociosCapital(string Cargo, string Periodo, string IdSocio, string Opcion)
    {
        //MUESTRA el CAPITAL Y PROPORCION DE SOCIOS 
        try{
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true){
                if (servidor.consultar("[dbo].[Pa_Listar_Votacion_Candidato]", Cargo, Periodo, IdSocio, Opcion).Tables.Count > 0)
                {
                    T_ListaSociosCapital = servidor.consultar("[dbo].[Pa_Listar_Votacion_Candidato]", Cargo, Periodo, IdSocio, Opcion).Tables[0];
                }
                int NroFilas = T_ListaSociosCapital.Rows.Count;
                if (NroFilas == 0){
                    servidor.cerrarconexion();
                    //MensajeSociosVotaron.Visible = false;
                    //MensajeSociosVotaron.Text = "";
                    //btnRegistrar.Visible = true;
                    //__pagina.Value = "VotacionCandidato.aspx?C=" + Cargo + "&P=" + Periodo;
                }else{
                    //MensajeSociosVotaron.Visible = false;
                    //MensajeSociosVotaron.Text = "Socio ya ha votado para este cargo";
                    //btnRegistrar.Visible = false;
                    //__pagina.Value = "VotacionCandidato.aspx?C=" + Cargo + "&P=" + Periodo;
                    CapitalSocios = Convert.ToString(T_ListaSociosCapital.Rows[0].ItemArray[2].ToString());
                    ProporcionSocios = Convert.ToString(T_ListaSociosCapital.Rows[0].ItemArray[3].ToString());

                    ////OCULTAR COLUMNAS
                    //TablaLote_.Columns.Remove("ID");
                    //TablaLote_.Columns.Remove("PTO_VENTA");

                    //GrVoto.DataSource = TablaCandidatos_1;
                    //GrVoto.DataBind();
                    servidor.cerrarconexion();
                }
            }else{
                servidor.cerrarconexion();
                __mensaje.Value = servidor.getMensageError();
                __pagina.Value = "CerrarSession.aspx";
            }
        }
        catch (Exception)
        {
            throw;
            //__mensaje.Value = "Vuelva a Generar Reporte";
        }
    }

    protected void DdlSocio_SelectedIndexChanged(object sender, EventArgs e)
    {
        __mensaje.Value = "";
        __pagina.Value = "";
        ListaSociosVotaron(Cargo, Periodo, Convert.ToString(DdlSocio.Items[DdlSocio.SelectedIndex].Text.Trim()), "3");
        ListaSociosCapital("", "", Convert.ToString(DdlSocio.Items[DdlSocio.SelectedIndex].Text.Trim()), "4");
        //Title= Convert.ToString(T_ListaSociosCapital.Rows[0].ItemArray[3].ToString());

        //Title = Convert.ToString(ConteoSociosVotaron);
        //if (ConteoSociosVotaron >= 1)
        //{
        //    btnRegistrar.Visible = false;
        //}
        //else if (ConteoSociosVotaron == 0)
        //{
        //    btnRegistrar.Visible = true;
        //}
    }

    protected void GvListado_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        try
        {
               //==================================================================         
           if (e.Row.RowType == DataControlRowType.DataRow){
            string _estado = DataBinder.Eval(e.Row.DataItem, "ORDEN").ToString();
            if (_estado == "1"){
                e.Row.BackColor = System.Drawing.Color.LemonChiffon;
                //e.Row.BackColor = System.Drawing.Color.Black;
                e.Row.ForeColor = System.Drawing.Color.Black;
                e.Row.Font.Bold = true;
                e.Row.ToolTip = "CANDIDATO GANADOR";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal d = 0;
                decimal.TryParse(e.Row.Cells[4].Text, out d);
                e.Row.Cells[4].Text = d.ToString("N2");

            }
            //if (_estado == "TOTAL GENERAL")
            //{
            //    e.Row.BackColor = System.Drawing.Color.DarkSeaGreen;
            //    e.Row.ForeColor = System.Drawing.Color.Black;
            //    e.Row.Font.Bold = true;
            //}
        }
            //==================================================================
        }
        catch (Exception)
        {
            
            //throw;
        }
    }
    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }


   
}