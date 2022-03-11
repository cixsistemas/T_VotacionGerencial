using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AnularValidarVoto : System.Web.UI.Page
{
    private string Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
    policia.clsaccesodatos servidor = new policia.clsaccesodatos();
    Lista _Lista = new Lista();
    DataTable Tabla_;
    static bool Voto;
    static string CapitalSocios;
    static string ProporcionSocios;
    static string Valido;
    static string ValidoMant;
    static string NumeroMantener;

    //public bool Consultar_Formulario_Derecho_Autorizacion(int Usuario, string Formulario, string Derecho, string msg)
    //{
    //    bool _ok = false;
    //    __mensaje.Value = "";
    //    __pagina.Value = "";
    //    try
    //    {
    //        policia.clsaccesodatos servidor = new policia.clsaccesodatos();
    //        servidor.cadenaconexion = Ruta;
    //        if (servidor.abrirconexion() == true)
    //        {
    //            DataTable dt = servidor.consultar("[dbo].[__consultar_formulario_derecho_autorizacion]", Usuario, Formulario, Derecho).Tables[0];
    //            if (dt.Rows.Count == 0)
    //            {
    //                servidor.cerrarconexion();
    //                if (msg.Trim().Length > 0)
    //                {
    //                    __mensaje.Value = msg;
    //                    __pagina.Value = "";
    //                }
    //                _ok = false;
    //            }
    //            else
    //            {
    //                _ok = Convert.ToBoolean(dt.Rows[0].ItemArray[3]);//"Aprobacion  
    //                servidor.cerrarconexion();
    //            }
    //        }
    //        else
    //        {
    //            servidor.cerrarconexion();
    //            __mensaje.Value = servidor.getMensageError();
    //            __pagina.Value = "CerrarSession.aspx";
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        __mensaje.Value = "Error inesperado al intentar conectarnos con el servidor.";
    //        __pagina.Value = "CerrarSession.aspx";
    //    }
    //    return _ok;
    //}

    public void ListaVotaron(string Cargo, string Periodo, string IdSocio, string Opcion)
    {
        try
        {
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true)
            {
                if (servidor.consultar("[dbo].[Pa_Listar_Votacion_Candidato]", Cargo, Periodo, IdSocio, Opcion).Tables.Count > 0)
                {
                    Tabla_ = servidor.consultar("[dbo].[Pa_Listar_Votacion_Candidato]", Cargo, Periodo, IdSocio, Opcion).Tables[0];
                }
                int NroFilas = Tabla_.Rows.Count;
                if (NroFilas == 0)
                {
                    _Lista.ShowMessage(__mensaje, __pagina, "No hay datos para mostrar", "");
                    gvReporte.DataBind();
                    servidor.cerrarconexion();

                    //MensajeSociosVotaron.Visible = false;
                    //MensajeSociosVotaron.Text = "";
                    //btnRegistrar.Visible = true;
                    //__pagina.Value = "VotacionCandidato.aspx?C=" + Cargo + "&P=" + Periodo;
                }
                else
                {
                    //MensajeSociosVotaron.Visible = true;
                    //MensajeSociosVotaron.Text = "Socio ya ha votado para este cargo";
                    //btnRegistrar.Visible = false;
                    //__pagina.Value = "VotacionCandidato.aspx?C=" + Cargo + "&P=" + Periodo;
                    //ConteoSociosVotaron = Convert.ToInt32(T_ListaSociosVotaron.Rows[0].ItemArray[4].ToString());

                    ////OCULTAR COLUMNAS
                    //TablaLote_.Columns.Remove("ID");
                    //TablaLote_.Columns.Remove("PTO_VENTA");

                    gvReporte.DataSource = Tabla_;
                    gvReporte.DataBind();
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
        //    __pagina.Value = "CerrarSession.aspx";
        //    return;
        //}
        //Usuario = Datos[1];
        ////=============================================================================================================

        if (Session["INDICE"] == null)
        {
            Session["INDICE"] = -1;
        }
        //BUSCAR
        ListaVotaron(TxtSocio.Text.Trim(), DdlEstado.Text.Trim(), "", "1");


    }
    protected void Page_init(object sender, EventArgs e)
    {
        //MANTENER CONTENIDO DE  TxtNumero DESPUES DE ACTUALIZAR
        string NumeroMantener2 = NumeroMantener;
        //Title = NumeroMantener2;
        TxtSocio.Text = NumeroMantener2;
        Title = "Anular o Activar Voto";
    }


    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        __mensaje.Value = "";
        __pagina.Value = "";

        DateTime Hoy = DateTime.Today;
        string fecha_actual = Hoy.ToString("dd-MM-yyyy");
        try
        {
            bool ok;
            ////==========================================================================================================================================================================================================================
            //ok = Consultar_Formulario_Derecho_Autorizacion(Convert.ToInt32(((object[])Session["__JSAR__"])[0]), "ELIMINAR RESERVA", "BUSCAR", "Ud. no tiene derecho y permisos necesarios para buscar los datos ingresados en esta pagina web.\n\nComuniquese con el administrador del sistema");
            ////gvReporte.DataBind();
            //if (ok == false)
            //{

            //    return;
            //}
            ////==========================================================================================================================================================================================================================
            //string txt_TipoDoc = DdlTipoDoc.SelectedItem.ToString();

            //BUSCAR
            ListaVotaron(TxtSocio.Text.Trim(), DdlEstado.Text.Trim(), "", "1");
            btnEliminar.Visible = false;
                //GReporte.DataBind();
        }
        catch (Exception)
        {
            //throw;
            __mensaje.Value = "Su sesion ha expirado";
            __pagina.Value = "CerrarSession.aspx";
        }
    }

  

    protected void LinkLimpiar_Click(object sender, EventArgs e)
    {
        __mensaje.Value = "";
        __pagina.Value = "";

        ////TxtSerie.Text = "-98";
        ////TxtNumero.Text = "";
        TxtSocio.Text = "";
        btnEliminar.Visible = false;
        //GReporte.DataBind();
        gvReporte.DataBind();
    }
    protected void gvReporte_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            __mensaje.Value = "";
            __pagina.Value = "";


            Session["INDICE"] = e.NewSelectedIndex;
            int indice = Convert.ToInt32(Session["INDICE"]);
            if (indice == -1)
            {
                _Lista.ShowMessage(__mensaje, __pagina, "Seleccione registro a modificar", "");
                return;
            }
            gvReporte.SelectedIndex = indice;
            //gvReporte.SelectedRow.Focus();
            Id.Value = Convert.ToString(Tabla_.Rows[indice].ItemArray[0]);
            Voto = Convert.ToBoolean(Tabla_.Rows[indice].ItemArray[5]);
            Valido = Convert.ToString(Tabla_.Rows[indice].ItemArray[6]);
            Id_Socio.Value = Convert.ToString(Tabla_.Rows[indice].ItemArray[7]);
            Id_Candidato.Value = Convert.ToString(Tabla_.Rows[indice].ItemArray[8]);
            CapitalSocios = Convert.ToString(Tabla_.Rows[indice].ItemArray[9]);
            ProporcionSocios = Convert.ToString(Tabla_.Rows[indice].ItemArray[10]);

            if (Valido == "SI")
            {
                ValidoMant = "NO";
                btnEliminar.Text = "  ANULAR  ";
                btnEliminar.ForeColor = System.Drawing.Color.White;
                btnEliminar.BackColor = System.Drawing.Color.IndianRed;
                btnEliminar.BorderColor = System.Drawing.Color.IndianRed;
                btnEliminar.Font.Bold = true;
                btnEliminar.Font.Name = "Tahoma";
            }
            if (Valido == "NO")
            {
                ValidoMant = "SI";
                btnEliminar.Text = "  ACTIVAR  ";
                btnEliminar.ForeColor = System.Drawing.Color.White;
                btnEliminar.BackColor = System.Drawing.Color.SteelBlue;
                btnEliminar.BorderColor = System.Drawing.Color.SteelBlue;
                btnEliminar.Font.Bold = true;
                btnEliminar.Font.Name = "Tahoma";
            }
            //Title = Convert.ToString(Voto);

            btnEliminar.Visible = true;
            btnEliminar.Focus();
            NumeroMantener = TxtSocio.Text;
            //Title = NumeroMantener;
            //Title = Convert.ToString(HfNumero.Value);
        }
        catch (Exception)
        {
            _Lista.ShowMessage(__mensaje, __pagina, "Vuelva a Buscar serie y numero a modificar", "");
            gvReporte.DataBind();
            TxtSocio.Focus();
            //throw;
        }
    }



    private void Matenimiento(int Id_, bool Voto, int Id_Socio, string capital, string proporcion
        , int Id_Candidato, DateTime fecha, string valido, string operacion)
    {
        try
        {
            ////string Cargo = Decrypt(HttpUtility.UrlDecode(Request.QueryString["C"]));

            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true)
            {
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
                if (servidor.getRespuesta() == 1)
                {
                    servidor.cerrarconexiontrans();
                    __mensaje.Value = servidor.getMensaje();
                    __pagina.Value = "AnularActivarVoto.aspx";
                }
                else
                {
                    servidor.cancelarconexiontrans();
                    __mensaje.Value = servidor.getMensaje();
                    __pagina.Value = "";
                    //this.__pagina.Value = "_Asistencia.aspx";
                }
            }
            else
            {
                servidor.cancelarconexiontrans();
                __mensaje.Value = servidor.getMensageError();
                __pagina.Value = "CerrarSession.aspx";
            }

        }
        catch (Exception)
        {
            __mensaje.Value = "Seleccione Candidato, por el cual va a votar.  ";
            __pagina.Value = "";
        }
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            bool ok;
            ////==========================================================================================================================================================================================================================
            //ok = Consultar_Formulario_Derecho_Autorizacion(Convert.ToInt32(((object[])Session["__JSAR__"])[0]), "ELIMINAR RESERVA", "MODIFICAR", "Ud. no tiene derecho y permisos necesarios para modificar los datos ingresados en esta pagina web.\n\nComuniquese con el administrador del sistema");
            //if (ok == false)
            //{
            //    return;
            //}
            ////==========================================================================================================================================================================================================================
            //btnActualizar.Focus();
        }
        catch (Exception)
        {
            //throw;
            __mensaje.Value = "Su sesion ha expirado, vuela a iniciar sesion";
            __pagina.Value = "CerrarSession.aspx";
        }

        __mensaje.Value = "";
        __pagina.Value = "";

      
        DateTime Hoy = DateTime.Today;
        string fecha_actual = Hoy.ToString("dd-MM-yyyy");


        //if (Valido == "SI")
        //{
            //Valido = "NO";
            Matenimiento(Convert.ToInt32(Id.Value), Voto, 
                Convert.ToInt32(Id_Socio.Value), CapitalSocios, ProporcionSocios
               , Convert.ToInt32(Id_Candidato.Value) , Convert.ToDateTime(fecha_actual), ValidoMant, "M");
        //}

        //if (Valido == "NO")
        //{
        // //Valido = "SI";
        // Matenimiento(Convert.ToInt32(Id.Value), Voto, Convert.ToInt32(Id_Socio.Value), Convert.ToInt32(Id_Candidato.Value)
        //, Convert.ToDateTime(fecha_actual), "SI", "M");
        //}

        //gvReporte.DataBind();
        //TxtNumero.Focus();

        //TxtNumero.Attributes.Add("value", TxtNumero.Text);
        //Title= Id.Value + " " + Numero; ;
    }

    protected void gvReporte_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string _estado = DataBinder.Eval(e.Row.DataItem, "CERRADA").ToString();
            if (_estado == "SI")
            {
                //e.Row.Cells[20].ForeColor = System.Drawing.Color.Blue;
                //e.Row.Cells[21].Enabled = false;
                e.Row.Cells[0].Enabled = false;
                e.Row.ToolTip = "Votacion esta cerrada, no se puede modificar";
            }
        }
    }



}