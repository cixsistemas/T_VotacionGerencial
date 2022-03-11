using System;
using System.Web.UI.WebControls;
using System.Data;

public partial class Candidato : System.Web.UI.Page
{
    private String Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
    policia.clsaccesodatos servidor = new policia.clsaccesodatos();
    System.Web.UI.WebControls.TableRow tRow;
    Lista _Lista = new Lista();

    //public bool Consultar_Formulario_Derecho_Autorizacion(int Usuario, String Formulario, String Derecho, String msg)
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
    //            __pagina.Value = "../CerrarSession.aspx";
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        __mensaje.Value = "Error inesperado al intentar conectarnos con el servidor.";
    //        __pagina.Value = "../CerrarSession.aspx";
    //    }
    //    return _ok;
    //}
    private void Listar_(string criterio, string  periodo ,string Opcion)
    {
        __mensaje.Value = "";
        __pagina.Value = "";
        ////==========================================================================================================================================================================================================================
        ////Convert.ToInt32(((object[])Session["__JSAR__"])[0])
        //Boolean ok_seleccionar_eliminar = Consultar_Formulario_Derecho_Autorizacion(Convert.ToInt32(((object[])Session["__JSAR__"])[0]), "SUB AREA DE TRABAJO", "ELIMINAR", "");
        ////==========================================================================================================================================================================================================================
        ////==========================================================================================================================================================================================================================
        ////Convert.ToInt32(((object[])Session["__JSAR__"])[0])
        //Boolean ok_seleccionar_modificar = Consultar_Formulario_Derecho_Autorizacion(Convert.ToInt32(((object[])Session["__JSAR__"])[0]), "SUB AREA DE TRABAJO", "MODIFICAR", "");
        ////==========================================================================================================================================================================================================================


        for (int i = 1; i < TableSubArea.Rows.Count; i++)
        {
            TableSubArea.Rows[i].Cells.Clear();
        }

        //try
        //{
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true)
            {
                DataTable dt = servidor.consultar("[dbo].[Pa_Listar_Candidato]", criterio, periodo, Opcion).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    servidor.cerrarconexion();
                    __mensaje.Value = "No hay Datos para mostrar.";
                    __pagina.Value = "";
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tRow = new TableRow();
                        tRow.BorderColor = System.Drawing.Color.Black;
                        tRow.Attributes.Add("onmouseover", "this.style.backgroundColor='#edf4f7'");
                        tRow.Attributes.Add("onmouseout", "this.style.backgroundColor='white'");
                        for (int j = 0; j < 5; j++)//Cabecera de la tabla
                        {
                            TableCell tCell = new TableCell();

                            switch (j)
                            {
                                case 0:
                                    tCell.Text = dt.Rows[i]["NOMBRE"].ToString();
                                    tCell.HorizontalAlign = HorizontalAlign.Center;
                                    tCell.Visible = true;
                                    tRow.Cells.Add(tCell);
                                    break;

                                case 1:
                                    tCell.Text = dt.Rows[i]["CARGO"].ToString();
                                    tCell.HorizontalAlign = HorizontalAlign.Center;
                                    tCell.Visible = true;
                                    tRow.Cells.Add(tCell);
                                    break;

                                case 2:
                                    tCell.Text = dt.Rows[i]["PERIODO"].ToString();
                                    tCell.HorizontalAlign = HorizontalAlign.Center;
                                    tCell.Visible = true;
                                    tRow.Cells.Add(tCell);
                                    break;

                                case 3:
                                    System.Web.UI.WebControls.LinkButton b = new System.Web.UI.WebControls.LinkButton();
                                    System.Web.UI.WebControls.LinkButton bb = new System.Web.UI.WebControls.LinkButton();
                                    b.Text = "<img src='imagenes/editar.png'/>";
                                    b.ToolTip = "Seleccione Registro para modificarlo o eliminarlo.";
                                    //b.Visible = ok_seleccionar_modificar;
                                    //b.BackColor = System.Drawing.Color.SpringGreen;
                                    b.BorderStyle = BorderStyle.None;
                                    b.CausesValidation = false;
                                    //b.UseSubmitBehavior = true;
                                    b.Attributes.Add("data-toggle", "modal");
                                    b.Attributes.Add("data-target", "#myModal");
                                    b.OnClientClick = "ModalCandidato('" + dt.Rows[i]["ID"].ToString() + "','" + dt.Rows[i]["NOMBRE"].ToString() 
                                        + "','" + dt.Rows[i]["CARGO"].ToString() + "','" + dt.Rows[i]["PERIODO"].ToString() + "');";
                                    //b.PostBackUrl = "Sub_Area.aspx?Codigo=" + dt.Rows[i]["ID"].ToString();
                                    b.Click += new System.EventHandler(visualiza_cosecha);
                                    tCell.HorizontalAlign = HorizontalAlign.Center;
                                    tCell.Controls.Add(b);
                                    tRow.Cells.Add(tCell);
                                    break;

                                case 4:
                                    bb = new System.Web.UI.WebControls.LinkButton();
                                    bb.Height = 25;
                                    bb.Width = 65;
                                    bb.Text = "<img src='imagenes/delete.png'/>";
                                    //bb.Visible = ok_seleccionar_eliminar;
                                    //bb.Text = "Eliminar";
                                    bb.ToolTip = "Seleccione item para ser eliminado.";
                                    //bb.BackColor = System.Drawing.Color.Salmon;
                                    bb.ForeColor = System.Drawing.Color.Black;
                                    bb.BorderStyle = BorderStyle.None;
                                    bb.CausesValidation = false;
                                    bb.PostBackUrl = "Candidato.aspx";
                                    bb.CommandArgument = dt.Rows[i]["ID"].ToString();
                                    bb.Attributes["Onclick"] = "return confirm('Desea eliminar Registro?')";
                                    //bb.OnClientClick = "return Confirmar('¿Desea Eliminar Registro?');";
                                    bb.Click += new System.EventHandler(eliminar_empacadora);
                                    tCell.HorizontalAlign = HorizontalAlign.Center;
                                    tCell.Controls.Add(bb);
                                    tRow.Cells.Add(tCell);
                                    break;
                            }
                        }

                        TableSubArea.Rows.Add(tRow);
                    }
                    servidor.cerrarconexion();
                }
            }
            else
            {
                servidor.cerrarconexion();
                __mensaje.Value = servidor.getMensageError();
                __pagina.Value = "CerrarSession.aspx";
            }

        //}
        //catch (Exception)
        //{
        //    __mensaje.Value = "Error inesperado al intentar conectarnos con el servidor.";
        //    __pagina.Value = "CerrarSession.aspx";
        //}
    }

    private void Eliminar_(int Codigo)
    {
        _Lista.ShowMessage(__mensaje, __pagina, "", "");
        try
        {
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true)
            {

                servidor.ejecutar("[dbo].[pa_mantenimiento_Candidato]",
                                 false,
                                 Codigo,
                                 null,
                                 null,
                                 null,
                                 null,
                                 null,
                                 "E",
                                 0, "");

                if (servidor.getRespuesta() == 1)
                {
                    servidor.cerrarconexiontrans();
                    _Lista.ShowMessage(__mensaje, __pagina, servidor.getMensaje(), "Candidato.aspx");
                }
                else
                {
                    servidor.cancelarconexiontrans();
                    _Lista.ShowMessage(__mensaje, __pagina, servidor.getMensaje(), "");
                }

            }
            else
            {
                servidor.cerrarconexion();
                _Lista.ShowMessage(__mensaje, __pagina, servidor.getMensaje(), "CerrarSession.aspx");
            }

        }
        catch (Exception)
        {
            _Lista.ShowMessage(__mensaje, __pagina, "Error inesperado al intentar conectarnos con el servidor.", "CerrarSession.aspx");
        }
    }

    protected void eliminar_empacadora(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.LinkButton b = (System.Web.UI.WebControls.LinkButton)sender;
        //this.Title = b.CommandArgument;
        int codigo_enfunde_daipa = Convert.ToInt32(b.CommandArgument);
        Eliminar_(codigo_enfunde_daipa);
    }
    protected void visualiza_profesion(object sender, EventArgs e)
    {
        int cod_fundo = Convert.ToInt32(Request.QueryString.Get("Codigo").Trim());
        Id.Value = cod_fundo.ToString();
        Consultar(cod_fundo);
        btnRegistrar.Visible = false;
        //oculta(true);
    }


    private void Matenimiento(int IdSubArea, string nombreprofesion, int IdPeriodo, int IdCargo, string Valido, 
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
                                    IdSubArea,
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
                    __pagina.Value = "Candidato.aspx";
                }
                else
                {
                    servidor.cancelarconexiontrans();
                    __mensaje.Value = servidor.getMensaje();
                    __pagina.Value = "Candidato.aspx";
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

    private void Consultar(int ID_)
    {
        try
        {
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true)
            {
                DataTable dt = servidor.consultar("[dbo].[pr_ConsultarCandidato]",
                                    ID_).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    servidor.cerrarconexion();
                    __mensaje.Value = "Registro no tiene datos.";
                    __pagina.Value = "CerrarSession.aspx";
                }
                else
                {
                    this.Id.Value = dt.Rows[0]["ID"].ToString();
                    TxtNombre.Text = dt.Rows[0]["NOMBRE"].ToString().Trim();
                    //_Lista.Busca(CatOcupacional, "CATEGORIA_OCUPACIONAL"); //TIPO DE MOVIMIENTO.
                    _Lista.Search_DropDownList(dt, DdlCargo, "CARGO");
                    _Lista.Search_DropDownList(dt, DdlPeriodo, "PERIODO");
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
            __mensaje.Value = "Error inesperado al intentar conectarnos con el servidor.";
            //this.__pagina.Value = "CerrarSession.aspx";
        }
    }
    protected void visualiza_cosecha(object sender, EventArgs e)
    {
        int cod_lote = Convert.ToInt32(Request.QueryString.Get("Codigo").Trim());
        Id.Value = cod_lote.ToString();
        Consultar(cod_lote);
        btnRegistrar.Visible = false;
        //oculta(true);

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_init(object sender, EventArgs e)
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
        ////=============================================================================================================


        TxtNombre.Focus();
        Listar_("", "", "1");
        Obtener_Cargo("1");
        Obtener_Periodo("2");
        Title = "Candidatos";
    }


    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        Boolean ok;
        ////==========================================================================================================================================================================================================================
        //ok = Consultar_Formulario_Derecho_Autorizacion(Convert.ToInt32(((object[])Session["__JSAR__"])[0]), "SUB AREA DE TRABAJO", "NUEVO", "Ud. no tiene derecho y permisos necesarios para registrar los datos ingresados en esta pagina web.\n\nComuniquese con el administrador del sistema");
        //if (ok == false)
        //{
        //    return;
        //}
        ////==========================================================================================================================================================================================================================

        ok = rfvTxtNombre.IsValid;
        if (ok == false)
        {
            return;
        }

        if (Convert.ToInt32(Id.Value.Trim()) == 0)
        {
            Matenimiento(Convert.ToInt32(Id.Value.Trim()),
            Convert.ToString(TxtNombre.Text.Trim()),
             Convert.ToInt32(DdlCargo.SelectedValue),
             Convert.ToInt32(DdlPeriodo.SelectedValue),
             "SI", //VALIDO
             "NO", // CERRADA
            "N");
        }

        else
        {
            //btnRegistrar.OnClientClick = "return Confirmar('¿Desea Modificar Tipo de Caja?');";
            btnRegistrar.OnClientClick = "return Confirmar('¿Desea Modificar Registro?');";
            Matenimiento(Convert.ToInt32(Id.Value.Trim()),
            Convert.ToString(TxtNombre.Text.Trim()),
             Convert.ToInt32(DdlCargo.SelectedValue),
             Convert.ToInt32(DdlPeriodo.SelectedValue),
              "SI", //VALIDO
             "NO", // CERRADA
          "M");
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        TxtNombre.Text = "";
        TxtNombre.Focus();
        btnRegistrar.Visible = true;
        DdlCargo.SelectedIndex = 0;
        DdlPeriodo.SelectedIndex = 0;
        //oculta(false);
    }

    protected void btnNuevo_Click1(object sender, EventArgs e)
    {
        _Lista.ShowMessage(__mensaje, __pagina, "", "");
        TxtNombre.Focus();
        TxtNombre.Text = "";
    }

    private void Obtener_Cargo( string Opcion)
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
                _Lista.ShowMessage(__mensaje, __pagina, servidor.getMensageError(), "CerrarSession.aspx");
            }

        }
        catch (Exception)
        {
            servidor.cerrarconexion();
            _Lista.ShowMessage(__mensaje, __pagina, "Error inesperado al intentar conectarnos con el servidor...", "CerrarSession.aspx");
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
    protected void Limpiar_Click(object sender, EventArgs e)
    {
        _Lista.ShowMessage(__mensaje, __pagina, "", "");
        NombreBuscar.Text = "";
    }
    protected void Buscar_Click(object sender, EventArgs e)
    {
        bool ok = NombreBuscar.Text.Trim() != "";
        if (ok == false)
        {
            NombreBuscar.Text = "";
            _Lista.ShowMessage(__mensaje, __pagina, "Complete datos formulario.\n\nIngrese Nombre por favor.", "");
            NombreBuscar.Focus();
            return;
        }
        Listar_(NombreBuscar.Text.Trim(),"", "1");
    }
}