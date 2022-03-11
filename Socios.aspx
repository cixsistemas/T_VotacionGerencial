<%@ Page Language="C#" MasterPageFile="~/MaestraMenu.master" AutoEventWireup="true" CodeFile="Socios.aspx.cs" Inherits="Socios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- VALIDACIONES EN TEXTBOX --%> <%--NO FUNCIONA EN VENTANA MODAL--%>
    <script src="../Otros_css_js/Validaciones.js" type="text/javascript"></script>

    <%-- VENTANA MODAL --%>
    <link href="../Otros_css_js/VentanaModal.css" rel="stylesheet" />

    <script type="text/javascript">
        function Socios(id, Nombre, TxtCapital, TxtProporcion) {
            document.getElementById('<%=Id.ClientID  %>').value = id;
            document.getElementById('<%=TxtNombre.ClientID  %>').value = Nombre;
            document.getElementById('<%=TxtCapital.ClientID  %>').value = TxtCapital;
            document.getElementById('<%=TxtProporcion.ClientID  %>').value = TxtProporcion;
        }

        function MostrarMensaje() {
             var mensaje = document.getElementById('<%=__mensaje.ClientID %>').value;
            if (mensaje != "") {
                alert(mensaje);
                 if (document.getElementById('<%=__pagina.ClientID%>').value != "")
                    location.href = document.getElementById('<%=__pagina.ClientID%>').value;
            }
        }

        function window_load() {
            MostrarMensaje()
        }

        //CONVERTIR MINUSCULAS A MAYUSCULAS
        function handleInput(e) {
            var ss = e.target.selectionStart;
            var se = e.target.selectionEnd;
            e.target.value = e.target.value.toUpperCase();
            e.target.selectionStart = ss;
            e.target.selectionEnd = se;
        }

        function soloLetras(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = " áéíóúabcdefghijklmnñopqrstuvwxyz-";
            especiales = [8, 37, 39, 46];

            tecla_especial = false
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }

            if (letras.indexOf(tecla) == -1 && !tecla_especial)
                return false;
        }

        //ACEPTA SOLO LETRAS Y NUMEROS (MENOS Ñ Y TILDES)
        //FUNCIONA EN FIREFOX Y CHROME
        //AGREGADO EL 31-05-2018
        function SoloDecimales(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = " 1234567890.";
            especiales = [8, 9, 37, 39, 46];
            tecla_especial = false
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }
            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }


    </script>   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <form id="FormSocio" runat="server">
        <div class="col-md-6 col-md-offset-3">
            <div class="table-responsive">
                <table class="table table-bordered table text-center">
                    <thead>
                        <tr>
                            <td colspan="4"
                                style="text-align: center; color: #FFFFFF; background-color: #000000;">INGRESE DATOS DE SOCIO</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="NombreBuscar" runat="server"
                                    CssClass="form-control input-sm" Font-Size="Small"
                                    Autocomplete="off" placeholder="Ingresar Nombre a buscar"
                                    onkeypress="BloquearTeclaEnter()" ToolTip="Ingresar Nombre a buscar"
                                    onchange="CambiaLetraMayuscula('NombreBuscar')"></asp:TextBox>
                            </td>
                            <td>

                                <asp:LinkButton ID="Buscar" runat="server"
                                    class="btn btn-info"
                                    OnClick="Buscar_Click"
                                    Text="Buscar  <span class='glyphicon glyphicon-search'></span>"
                                    CausesValidation="False" UseSubmitBehavior="False" />

                            </td>

                            <td style="text-align: left">
                                <asp:LinkButton ID="Limpiar" runat="server" OnClick="Limpiar_Click"
                                    Text="Limpiar <span class='glyphicon glyphicon-trash'></span>"
                                    CausesValidation="False" UseSubmitBehavior="False"
                                    class="btn btn-success" PostBackUrl="~/Socios.aspx" />
                            </td>
                            <td
                                style="text-align: left; color: blue; background-color: white;">
                                <asp:LinkButton ID="btnNuevo" runat="server" data-toggle="modal" data-target="#myModal"
                                    Style="font-family: Calibri; font-size: medium"
                                    Text="&nbsp; Nuevo &nbsp;  <span class='glyphicon glyphicon-file'></span>"
                                    CssClass="btn btn-primary" OnClick="btnNuevo_Click1"></asp:LinkButton>
                            </td>
                        </tr>
                    </thead>

                    <tbody>
                        <tr>
                            <td colspan="4">
                                <asp:Table ID="TableAreaTrabajo" runat="server" BackColor="White"
                                    BorderColor="White" CellPadding="2" CellSpacing="0" Font-Size="Small"
                                    GridLines="Both" Style="text-align: left" Width="100%">
                                    <asp:TableRow ID="TableRow1" runat="server">
                                        <asp:TableCell ID="TableCell1" runat="server" BackColor="Black" BorderColor="Black"
                                            ForeColor="White" HorizontalAlign="Center">NOMBRE</asp:TableCell>
                                        <asp:TableCell ID="TableCell3" runat="server" BackColor="Black" BorderColor="Black"
                                            ForeColor="White" HorizontalAlign="Center">CAPITAL</asp:TableCell>
                                        <asp:TableCell ID="TableCell4" runat="server" BackColor="Black" BorderColor="Black"
                                            ForeColor="White" HorizontalAlign="Center">PROPORCION</asp:TableCell>
                                        <asp:TableCell ID="TableCell2" runat="server" BackColor="Black" BorderColor="Black"
                                            ForeColor="White" HorizontalAlign="Center" Width="10%"></asp:TableCell>
                                        <asp:TableCell ID="TableCell12" runat="server" BackColor="Black" BorderColor="Black"
                                            ForeColor="red" HorizontalAlign="Center" Width="10%"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </td>
                        </tr>
                    </tbody>

                    <tr>
                        <td style="text-align: right" colspan="4">
                            <asp:HiddenField ID="__mensaje" runat="server" />
                            <asp:HiddenField ID="__pagina" runat="server" />
                            <asp:HiddenField ID="Id" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>

                <!-- Modal -->
                <div class="modal fade" id="myModal" role="dialog" data-keyboard="false" data-backdrop="static">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                                <h4 class="modal-title">Registro</h4>
                            </div>
                            <div class="modal-body">
                                <label class="control-label">Nombre</label>
                                <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control input-sm"
                                    Autocomplete="off" placeholder="Ingresar Nombre de Socio" MaxLength="250"
                                    Style="text-align: left;"
                                    onkeypress="return soloLetras(event);" AutoCompleteType="Disabled"
                                    oninput="handleInput(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvnombreareatrabajo" runat="server" BackColor="Yellow"
                                    ControlToValidate="TxtNombre" ErrorMessage="*" ForeColor="Red"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                            <div class="modal-body">
                                <label class="control-label">Capital</label>
                                <asp:TextBox ID="TxtCapital" runat="server" CssClass="form-control input-sm"
                                    Autocomplete="off" placeholder="" MaxLength="250"
                                    Style="text-align: left;"
                                    onkeypress="return SoloDecimales(event);" AutoCompleteType="Disabled"
                                    oninput="handleInput(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" BackColor="Yellow"
                                    ControlToValidate="TxtCapital" ErrorMessage="*" ForeColor="Red"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                            <div class="modal-body">
                                <label class="control-label">Proporcion</label>
                                <asp:TextBox ID="TxtProporcion" runat="server" CssClass="form-control input-sm"
                                    Autocomplete="off" placeholder="" MaxLength="250"
                                    Style="text-align: left;"
                                    onkeypress="return SoloDecimales(event);" AutoCompleteType="Disabled"
                                    oninput="handleInput(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" BackColor="Yellow"
                                    ControlToValidate="TxtProporcion" ErrorMessage="*" ForeColor="Red"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>


                            <div class="modal-footer">
                                <div class="col-md-6 col-md-offset-3">
                                    <asp:LinkButton ID="btnRegistrar" runat="server" class="btn btn-primary"
                                        Text="Aceptar <span class='glyphicon glyphicon-ok'></span>"
                                        Style="font-family: Calibri; font-size: medium"
                                        OnClick="btnRegistrar_Click" />
                                    &nbsp;&nbsp;                     
                                    <asp:LinkButton ID="btnCancelar" runat="server" class="btn btn-danger"
                                        Style="font-family: Calibri; font-size: medium" UseSubmitBehavior="False"
                                        CausesValidation="False"
                                        Text="Cancelar <span class='glyphicon glyphicon-remove'></span>" Visible="true"
                                        OnClick="btnCancelar_Click" PostBackUrl="~/Socios.aspx?Codigo=0" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
   
</asp:Content>
