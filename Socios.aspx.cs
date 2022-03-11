using System;
using System.Web.UI.WebControls;
using System.Data;


public partial class Socios : System.Web.UI.Page
{
    private string Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
    policia.clsaccesodatos servidor = new policia.clsaccesodatos();
    System.Web.UI.WebControls.TableRow tRow;
    Lista _Lista = new Lista();
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
    private void Listar_(string nombre)
    {
        __mensaje.Value = "";
        __pagina.Value = "";
        ////==========================================================================================================================================================================================================================
        ////Convert.ToInt32(((object[])Session["__JSAR__"])[0])
        //Boolean ok_seleccionar_eliminar = Consultar_Formulario_Derecho_Autorizacion(Convert.ToInt32(((object[])Session["__JSAR__"])[0]), "AREA DE TRABAJO", "ELIMINAR", "");
        ////==========================================================================================================================================================================================================================
        ////==========================================================================================================================================================================================================================
        ////Convert.ToInt32(((object[])Session["__JSAR__"])[0])
        //Boolean ok_seleccionar_modificar = Consultar_Formulario_Derecho_Autorizacion(Convert.ToInt32(((object[])Session["__JSAR__"])[0]), "AREA DE TRABAJO", "MODIFICAR", "");
        ////==========================================================================================================================================================================================================================


        for (int i = 1; i < TableAreaTrabajo.Rows.Count; i++)
        {
            TableAreaTrabajo.Rows[i].Cells.Clear();
        }

        try
        {
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true)
            {
                DataTable dt = servidor.consultar("[dbo].[Pa_Listar_Socio]", nombre).Tables[0];

                if (dt.Rows.Count == 0)
                {
                    servidor.cerrarconexion();
                    __mensaje.Value = "No hay Registros para mostrar.";
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
                                    tCell.Text = dt.Rows[i]["CAPITAL"].ToString();
                                    tCell.HorizontalAlign = HorizontalAlign.Center;
                                    tCell.Visible = true;
                                    tRow.Cells.Add(tCell);
                                    break;

                                case 2:
                                    tCell.Text = dt.Rows[i]["PROPORCION"].ToString();
                                    tCell.HorizontalAlign = HorizontalAlign.Center;
                                    tCell.Visible = true;
                                    tRow.Cells.Add(tCell);
                                    break;

                                case 3:
                                    System.Web.UI.WebControls.LinkButton b = new System.Web.UI.WebControls.LinkButton();
                                    System.Web.UI.WebControls.LinkButton bb = new System.Web.UI.WebControls.LinkButton();
                                    b.Text = "<img src='../imagenes/editar.png'/>";
                                    //b.Visible = ok_seleccionar_modificar;
                                    //b.Text = "Editar";
                                    b.ToolTip = "Seleccione registro a modificar";
                                    b.Height = 25;
                                    b.Width = 65;
                                    b.BorderStyle = BorderStyle.None;
                                    //b.BackColor = System.Drawing.Color.Lavender;
                                    b.ForeColor = System.Drawing.Color.Black;
                                    b.CausesValidation = false;
                                    b.Attributes.Add("data-toggle", "modal");
                                    b.Attributes.Add("data-target", "#myModal");
                                    b.OnClientClick = "Socios('" + dt.Rows[i]["ID"].ToString() + "','" + dt.Rows[i]["NOMBRE"].ToString() + "','" + dt.Rows[i]["CAPITAL"].ToString() + "','" + dt.Rows[i]["PROPORCION"].ToString() + "');";
                                    //b.OnClientClick = "Area_Trabajos('" + dt.Rows[i]["NOMBRE"].ToString() + "');";
                                    //Title = dt.Rows[i]["ID"].ToString();
                                    tCell.HorizontalAlign = HorizontalAlign.Center;
                                    tCell.Controls.Add(b);
                                    tRow.Cells.Add(tCell);
                                    break;

                                case 4:
                                    bb = new System.Web.UI.WebControls.LinkButton();
                                    bb.Height = 25;
                                    bb.Width = 65;
                                    bb.Text = "<img src='../imagenes/delete.png'/>";
                                    //bb.Visible = ok_seleccionar_eliminar;
                                    //bb.Text = "Eliminar";
                                    bb.ToolTip = "Seleccione item para ser eliminado.";
                                    //bb.BackColor = System.Drawing.Color.Salmon;
                                    bb.ForeColor = System.Drawing.Color.Black;
                                    bb.BorderStyle = BorderStyle.None;
                                    bb.CausesValidation = false;
                                    bb.PostBackUrl = "Socios.aspx";
                                    bb.CommandArgument = dt.Rows[i]["ID"].ToString();
                                    bb.Attributes["Onclick"] = "return confirm('Desea eliminar Registro?')";
                                    //bb.OnClientClick = "return Confirmar('¿Desea eliminar Area de Trabajo?');";
                                    bb.Click += new System.EventHandler(eliminar_empacadora);
                                    tCell.HorizontalAlign = HorizontalAlign.Center;
                                    tCell.Controls.Add(bb);
                                    tRow.Cells.Add(tCell);
                                    break;
                            }
                        }
                        TableAreaTrabajo.Rows.Add(tRow);
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
        }
        catch (Exception)
        {
            __mensaje.Value = "Error inesperado al intentar conectarnos con el servidor.";
            __pagina.Value = "CerrarSession.aspx";
        }
    }

    private void Elimnar_empacadora(int codigoenfundedaipa)
    {
        _Lista.ShowMessage(__mensaje, __pagina, "", "");
        try
        {
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true)
            {
                servidor.ejecutar("[dbo].[pa_mantenimiento_Socio]",
                                 false,
                                 codigoenfundedaipa,
                                 null,
                                 null,
                                 null,
                                 null,
                                 "E",
                                 0, "");

                if (servidor.getRespuesta() == 1)
                {
                    servidor.cerrarconexiontrans();
                    _Lista.ShowMessage(__mensaje, __pagina, servidor.getMensaje(), "Socios.aspx");
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
        Elimnar_empacadora(codigo_enfunde_daipa);
    }
    protected void visualiza_area_trabajo(object sender, EventArgs e)
    {
        int cod_fundo = Convert.ToInt32(Request.QueryString.Get("Codigo").Trim());
        Id.Value = cod_fundo.ToString();
        Consultar(cod_fundo);
        btnRegistrar.Visible = false;
        oculta(true);

    }

    private void oculta(bool ok)
    {
        //this.btnModificar.Visible = ok;
        //this.btnEliminar.Visible = ok;
        //this.btnCancelar.Visible = ok;
    }
    private void Matenimiento(int id, string nombre, string capital, string proporcion, string valido, string operacion)
    {
        try
        {
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true)
            {
                servidor.ejecutar("[dbo].[pa_mantenimiento_Socio]",
                                    false,
                                    id,
                                    nombre.Trim(),
                                    capital.Trim(),
                                    proporcion.Trim(),
                                    valido.Trim(),
                                    operacion,
                                    0, "");
                if (servidor.getRespuesta() == 1)
                {
                    servidor.cerrarconexiontrans();
                    __mensaje.Value = servidor.getMensaje();
                    __pagina.Value = "Socios.aspx";
                }
                else
                {
                    servidor.cancelarconexiontrans();
                    __mensaje.Value = servidor.getMensaje();
                    __pagina.Value = "Socios.aspx";
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
            __mensaje.Value = "Error inesperado al intentar conectarnos con el servidor.";
            __pagina.Value = "CerrarSession.aspx";
        }
    }

    private void Consultar(int Id_)
    {
        try
        {
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true)
            {
                DataTable dt = servidor.consultar("[dbo].[pr_ConsultarSocio]",
                                    Id_).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    servidor.cerrarconexion();
                    __mensaje.Value = "Registro no tiene datos.";
                    __pagina.Value = "CerrarSession.aspx";
                }
                else
                {
                    Id.Value = dt.Rows[0]["ID"].ToString();
                    TxtNombre.Text = dt.Rows[0]["NOMBRE"].ToString().Trim();
                    TxtCapital.Text = dt.Rows[0]["CAPITAL"].ToString().Trim();
                    TxtProporcion.Text = dt.Rows[0]["PROPORCION"].ToString().Trim();
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
            __pagina.Value = "CerrarSession.aspx";
        }
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
        //    __pagina.Value = "CerrarSession.aspx";
        //    return;
        //}
        ////=============================================================================================================

        if (Convert.ToInt32(Id.Value.Trim()) == 0)
        {
            btnRegistrar.OnClientClick = "return Confirmar('¿Desea Registrar?');";
        }
        else
        {
            btnRegistrar.OnClientClick = "return Confirmar('¿Desea Modificar Registro?');";
        }
        TxtNombre.Focus();
        Listar_("");
        Title = "Socios";
    }


    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        Boolean ok;
        ////==========================================================================================================================================================================================================================
        //ok = Consultar_Formulario_Derecho_Autorizacion(Convert.ToInt32(((object[])Session["__JSAR__"])[0]), "AREA DE TRABAJO", "NUEVO", "Ud. no tiene derecho y permisos necesarios para registrar los datos ingresados en esta pagina web.\n\nComuniquese con el administrador del sistema");
        //if (ok == false)
        //{
        //    return;
        //}
        ////==========================================================================================================================================================================================================================

        ok = rfvnombreareatrabajo.IsValid;
        if (ok == false)
        {
            return;
        }

        if (Convert.ToInt32(Id.Value.Trim()) == 0)
        {
            Matenimiento(Convert.ToInt32(Id.Value.Trim()),
            Convert.ToString(TxtNombre.Text.Trim()),
            Convert.ToString(TxtCapital.Text.Trim()),
            Convert.ToString(TxtProporcion.Text.Trim()),
            "SI",
            "N");
        }

        else
        {
            btnRegistrar.OnClientClick = "return Confirmar('¿Desea Modificar Registro?');";
            Matenimiento(Convert.ToInt32(Id.Value.Trim()),
             Convert.ToString(TxtNombre.Text.Trim()),
             Convert.ToString(TxtCapital.Text.Trim()),
            Convert.ToString(TxtProporcion.Text.Trim()),
            "SI",
          "M");
        }
        TxtNombre.Focus();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        TxtNombre.Text = "";
        TxtNombre.Focus();
        btnRegistrar.Visible = true;
        oculta(false);
    }

    protected void btnNuevo_Click1(object sender, EventArgs e)
    {
        _Lista.ShowMessage(__mensaje, __pagina, "", "");

        //Boolean ok;
        ////==========================================================================================================================================================================================================================
        //ok = Consultar_Formulario_Derecho_Autorizacion(Convert.ToInt32(((object[])Session["__JSAR__"])[0]), "AREA DE TRABAJO", "NUEVO", "Ud. no tiene derecho y permisos necesarios para registrar los datos ingresados en esta pagina web.\n\nComuniquese con el administrador del sistema");
        //if (ok == false)
        //{
        //    return;
        //}
        ////==========================================================================================================================================================================================================================

        TxtNombre.Focus();
        TxtNombre.Text = "";
    }
    protected void Buscar_Click(object sender, EventArgs e)
    {
        _Lista.ShowMessage(__mensaje, __pagina, "", "");

        bool ok = NombreBuscar.Text.Trim() != "";

        if (ok == false)
        {
            NombreBuscar.Text = "";
            _Lista.ShowMessage(__mensaje, __pagina, "Complete datos formulario.\n\nIngrese Nombre por favor.", ""); NombreBuscar.Focus();

            return;
        }

        Listar_(NombreBuscar.Text.Trim());
    }
    protected void Limpiar_Click(object sender, EventArgs e)
    {
        _Lista.ShowMessage(__mensaje, __pagina, "", "");
        NombreBuscar.Text = "";
        Listar_("");
    }
}