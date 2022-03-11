<%@ Page Title="" Language="C#" MasterPageFile="~/Reportes/MaestraMenuRep.master" AutoEventWireup="true" CodeFile="RepListaGanadores.aspx.cs" Inherits="Reportes_RepListaGanadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- PARA BUSCAR EN DROPDOWN LIST --%>
    <link rel="stylesheet" href="../Otros_css_js/chosen.css" />
    <!-- ESTILOS PARA GRIDVIEW -->
    <style type="text/css">
        gridview {
            margin: 5px;
            border: none;
            background-color: #284775;
        }

        .gridview tr {
            text-align: center;
            border: none;
            border-bottom: solid;
            border-color: #FFFFFF;
            border-width: 2px;
            font-size: small;
        }

        .gridview th {
            border: none;
            background-color: #284775;
            padding: 3px;
            font-size: small;
            text-align: center;
            align-content: Center;
            /*font-size: x-small;*/
        }

        .gridview td {
            border: none;
            padding: 8px;
            margin: 5px;
        }
    </style>

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
    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <form id="frm_rep_enfundeXlote" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container">
            <div class="table-responsive">
                <table class="table table text-center">
                    <tr>
                        <td colspan="12" style="text-align: center; color: blue; background-color: white;">
                            <b>LISTA DE GANADORES</b></td>
                    </tr>

                    <tr>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="cbPeriodo" runat="server" OnCheckedChanged="cbPeriodo_CheckedChanged"
                                Text=" &nbsp;Periodo" AutoPostBack="True" /></td>
                        <td style="text-align: left;"></td>
                        <td style="text-align: left;"></td>
                        <td style="text-align: left;"></td>
                        <td style="text-align: left;"></td>
                        <td style="text-align: left;"></td>
                        <td style="text-align: left;">&nbsp;</td>
                        <td style="text-align: left;">&nbsp;</td>
                        <td style="text-align: left;">&nbsp;</td>
                        <td>
                            <asp:LinkButton ID="btnexportar" runat="server"
                                Style="font-family: Calibri; font-size: medium"
                                Text="Exportar Excel <span class='glyphicon glyphicon-th'></span>"
                                CssClass="btn btn-success" OnClick="btnexportar_Click" Visible="True" />
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: left;">
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate> 
                            <asp:DropDownList ID="DdlPeriodo" runat="server" class="chzn-select form-control input-sm"
                                Font-Size="Small" Style="width: 220px;" data-placeholder="Ingrese Periodo" Enabled="False"
                                AutoPostBack="False">
                            </asp:DropDownList>
                                 </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbPeriodo" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        </td>
                        <td>&nbsp;</td>
                        <td style="text-align: left;">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="text-align: left;"></td>
                        <td style="text-align: left;">&nbsp;</td>
                        <td style="text-align: left;">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                        <td>

                            <asp:LinkButton ID="bf" runat="server" OnClick="bf_Click"
                                Text="Buscar &nbsp;<span class='glyphicon glyphicon-search'></span>"
                                class="btn btn-info"
                                CausesValidation="False" UseSubmitBehavior="False" />
                        </td>
                    </tr>


                    <tr>
                        <td style="text-align: center" colspan="12">
                            
                                <asp:GridView ID="gvRep_EnfxColor" runat="server"
                                    HorizontalAlign="Center" Font-Names="Tahoma"
                                    CellPadding="4" ForeColor="#333333" GridLines="Both"
                                    Font-Size="8" Width="100%" ShowFooter="false"
                                    CssClass="table table-striped table-hover table-condensed small-top-margin"
                                    OnRowDataBound="gvRep_EnfxColor_RowDataBound">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <FooterStyle BackColor="AliceBlue" Font-Bold="True" ForeColor="Black" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />

                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridview th" />
                                    <EditRowStyle BackColor="#999999" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                           
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right" colspan="6">
                            <asp:HiddenField ID="__mensaje" runat="server" />
                            <asp:HiddenField ID="__pagina" runat="server" />
                        </td>
                        <td style="text-align: right" colspan="1">
                            <asp:TextBox ID="TxtFechaActual" runat="server"
                                Width="10px" AutoCompleteType="Disabled" Enabled="False"
                                Font-Size="Small"
                                Autocomplete="off" MaxLength="3" Visible="False"></asp:TextBox>
                        </td>
                        <td style="text-align: right" colspan="2">
                            <asp:TextBox ID="TxtHoraActual" runat="server"
                                Width="10px" AutoCompleteType="Disabled" Enabled="False"
                                Font-Size="Small"
                                Autocomplete="off" MaxLength="3" Visible="False"></asp:TextBox>
                        </td>
                        <td style="text-align: right" colspan="3"></td>
                    </tr>
                </table>
            </div>
        </div>
    </form>

    <%-- PARA DROPDOWN LIST --%>
    <script src="../Otros_css_js/jquery.min.js" type="text/javascript"></script>
    <script src="../Otros_css_js/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>


</asp:Content>

