using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Data;

public partial class VotacionCargo : System.Web.UI.Page
{
    private string Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
    policia.clsaccesodatos servidor = new policia.clsaccesodatos();
    Lista _Lista = new Lista();
    DataTable TablaCandidatos_1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Obtener_Cargo("1");
            Obtener_Periodo("2");
        }
    }

    protected void BtnEmpezar_Click(object sender, EventArgs e)
    {
        Boolean ok;
        ok = RvDdlCargo.IsValid;
        ok = ok && RFVDdlCargo.IsValid;
        ok = ok && RvDdlPeriodo.IsValid;
        ok = ok && RfvDdlPeriodo.IsValid;

        //string Cargo = HttpUtility.UrlEncode(Encrypt(Convert.ToString(DdlCargo.SelectedItem.ToString()))); ;
        string Cargo = Convert.ToString(this.DdlCargo.Items[this.DdlCargo.SelectedIndex].Text.Trim());
        string Periodo = Convert.ToString(this.DdlPeriodo.Items[this.DdlPeriodo.SelectedIndex].Text.Trim());

        Response.Clear();
        Response.Redirect("VotacionCandidato.aspx?C=" + Cargo.Trim()+ "&P=" + Periodo);
        Response.Flush();
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
                    TablaCandidatos_1 = servidor.consultar("[dbo].[Pa_Listar_Candidato]", criterio, periodo, Opcion).Tables[0];
                }
                int NroFilas = TablaCandidatos_1.Rows.Count;
                if (NroFilas == 0)
                {
                    servidor.cerrarconexion();
                    MensajeCandidatos.Visible = true;
                    MensajeCandidatos.Text = "Votacion esta Abierta";
                    __pagina.Value = "ElegirCargo.aspx";
                    //_Lista.ShowMessage(__mensaje, __pagina, "No hay datos para mostrar con el criterio ingresado...", "");
                    //GrVoto.DataBind();
                }
                else
                {
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
  
    private void Matenimiento_AbrirVotacion(string IdCandidato, string nombreprofesion, string IdPeriodo, string IdCargo, string Valido,
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
                __pagina.Value = "ElegirCargo.aspx";
            }
            else
            {
                servidor.cancelarconexiontrans();
                __mensaje.Value = servidor.getMensaje();
                //__pagina.Value = "Candidato.aspx";
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
    protected void BtnAperturar_Click(object sender, EventArgs e)
    {
        __mensaje.Value = "";
        __pagina.Value = "";

        // VA A CARGAR VOTACIONES QUE ESTAN CERRADAS
        ListaCandidatos(Convert.ToString(this.DdlCargo.Items[this.DdlCargo.SelectedIndex].Text.Trim())
            , Convert.ToString(this.DdlPeriodo.Items[this.DdlPeriodo.SelectedIndex].Text.Trim()), "3");
        string IdCandidato, IdCargo, IdPeriodo;
        for (int j = 0; j <= TablaCandidatos_1.Rows.Count - 1; j++)
        {
            IdCandidato = TablaCandidatos_1.Rows[j].ItemArray[0].ToString();
            IdCargo = TablaCandidatos_1.Rows[j].ItemArray[6].ToString();
            IdPeriodo = TablaCandidatos_1.Rows[j].ItemArray[7].ToString();


            Matenimiento_AbrirVotacion(IdCandidato, "", IdCargo, IdPeriodo, "", "", "A");

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
                    _Lista.ShowMessage(__mensaje, __pagina, "Error, al intentar recuperar datos.", "CerrarSession.aspx");
                }
                else
                {
                    this.DdlCargo.DataSource = dt;
                    this.DdlCargo.DataTextField = "NOMBRE";
                    this.DdlCargo.DataValueField = "CODIGO";
                    this.DdlCargo.DataBind();
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
                _Lista.ShowMessage(__mensaje, __pagina, servidor.getMensageError(), "CerrarSession.aspx");
            }

        }
        catch (Exception)
        {
            servidor.cerrarconexion();
            _Lista.ShowMessage(__mensaje, __pagina, "Error inesperado al intentar conectarnos con el servidor.", "CerrarSession.aspx");
        }
    }
    private string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
}