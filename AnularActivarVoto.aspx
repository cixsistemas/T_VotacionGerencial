<%@ Page Title="" Language="C#" MasterPageFile="~/MaestraMenu.master" AutoEventWireup="true" CodeFile="AnularActivarVoto.aspx.cs" Inherits="AnularValidarVoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- VENTANA MODAL --%>
    <link href="Otros_css_js/VentanaModal.css" rel="stylesheet" />

    <script type="text/javascript">
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

        function Confirmar(men) {
            if (!confirm(men))
                return false;
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


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container">
            <div class="table-responsive">
                <table class="table table text-left">
                    <tr>
                        <td colspan="11" style="text-align: center; color: blue; background-color: white;" class="auto-style1">
                            <b>ANULAR o ACTIVAR VOTO</b></td>
                    </tr>
                    <tr>
                        <td>

                            <asp:Label ID="LbDni" runat="server" Text="Socio" Visible="True"></asp:Label>

                        </td>
                        <td>
                            <asp:Label ID="LbSerie" runat="server" Text="Activo"></asp:Label>
                        </td>
                        <td id="Serie">&nbsp;</td>
                        <td>&nbsp;</td>


                        <td>&nbsp;</td>
                        <td></td>

                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>

                        <td colspan="1">&nbsp;</td>
                    </tr>

                    <tr>
                        <td>
                            <%--<asp:ListItem Text="Consulta &nbsp;" Value="RbConsulta" Enabled="false">
                                </asp:ListItem>--%>
                            <asp:TextBox ID="TxtSocio" runat="server" Autocomplete="off" placeholder="Socio"
                                CssClass="form-control input-sm" MaxLength="200" Width="200px"
                                Visible="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlEstado" runat="server" CssClass="form-control input-sm"
                                AppendDataBoundItems="True" AutoPostBack="False" Width="100px">
                                <asp:ListItem>SI</asp:ListItem>
                                <asp:ListItem>NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                        <td></td>

                        <td>&nbsp;</td>
                        <td style="text-align: left;">&nbsp;</td>


                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;
                            <asp:LinkButton ID="btnBuscar" runat="server"
                                Text="Buscar  <span class='glyphicon glyphicon-search'></span>"
                                class="btn btn-info" OnClick="btnBuscar_Click" />
                        </td>


                        <td>
                            <asp:LinkButton ID="LinkLimpiar" runat="server" OnClick="LinkLimpiar_Click"
                                Text="&nbsp;Limpiar&nbsp; <span class='glyphicon glyphicon-trash'></span>"
                                class="btn btn-success"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="10">
                            <div style="overflow-y: scroll; height: 200px">

                                <asp:GridView ID="gvReporte" runat="server" CellPadding="3"
                                    CaptionAlign="Top" Font-Size="8" Width="100%" AutoGenerateColumns="False"
                                    OnSelectedIndexChanging="gvReporte_SelectedIndexChanging"
                                    DataKeyNames="ID"
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                    CssClass="table table-striped table-hover table-condensed small-top-margin" OnRowDataBound="gvReporte_RowDataBound">
                                    <Columns>
                                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" HeaderText="" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" Width="1%" />
                                            <ItemStyle HorizontalAlign="Left" Width="1%" ForeColor="#CC0000" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false">
                                            <HeaderStyle HorizontalAlign="Left" Width="1%" />
                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SOCIO" HeaderText="SOCIO" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CANDIDATO" HeaderText="CANDIDATO" Visible="True">
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CARGO" HeaderText="CARGO" Visible="True">
                                            <HeaderStyle HorizontalAlign="Left" Width="1%" />
                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PERIODO" HeaderText="PERIODO" Visible="True">
                                            <HeaderStyle HorizontalAlign="Left" Width="1%" />
                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VALIDO" HeaderText="VALIDO" Visible="true">
                                            <HeaderStyle Width="1%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ID_SOCIO" HeaderText="ID_SOCIO" Visible="false">
                                            <HeaderStyle Width="1%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ID_CANDIDATO" HeaderText="ID_CANDIDATO" Visible="false">
                                            <HeaderStyle Width="1%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CERRADA" HeaderText="CERRADA" Visible="true">
                                            <HeaderStyle Width="1%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                        </asp:BoundField>

                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                </asp:GridView>



                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: center;" colspan="10">
                            <asp:LinkButton ID="btnEliminar" runat="server"
                                Style="font-family: Calibri; font-size: medium"
                                Text="&nbsp;&nbsp;Anular &nbsp;&nbsp;<span class='glyphicon glyphicon-remove'></span>"
                                OnClick="btnEliminar_Click" CssClass="btn btn-danger" Visible="False"
                                OnClientClick="return Confirmar('¿Desea modificar el registro?');" />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="7">
                            <asp:HiddenField ID="__mensaje" runat="server" />
                            <asp:HiddenField ID="__pagina" runat="server" />
                            <asp:HiddenField ID="Id" runat="server" Value="0" />
                            <asp:HiddenField ID="Id_Socio" runat="server" Value="0" />
                            <asp:HiddenField ID="Id_Candidato" runat="server" Value="0" />
                        </td>
                        <td colspan="1"></td>
                        <td colspan="2"></td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</asp:Content>

