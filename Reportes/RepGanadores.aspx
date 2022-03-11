<%@ Page Title="" Language="C#" MasterPageFile="~/Reportes/MaestraMenuRep.master" AutoEventWireup="true" CodeFile="RepGanadores.aspx.cs" Inherits="Reportes_RepGanadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <%-- PARA BUSCAR EN DROPDOWN LIST --%>
    <link rel="stylesheet" href="../Otros_css_js/chosen.css" />

    <%-- VALIDACIONES EN TEXTBOX --%>
    <script src="../Otros_css_js/Validaciones.js" type="text/javascript"></script>

    <!-- ESTILOS PARA GRIDVIEW -->
    <style type="text/css">
        gridview {
            margin: 5px;
            border: none;
            background-color: #284775;
        }

        .gridview tr {
            text-align: left;
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
            text-align: left;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
     <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="table-responsive">
                <table class="table table text-left">
                    <tr>
                        <td colspan="11" style="text-align: center; color: blue; background-color: white;">
                            <b>REPORTE DE VOTACIONES</b></td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="CbCargo" runat="server" OnCheckedChanged="CbCargo_CheckedChanged"
                                Text=" &nbsp;Cargo" AutoPostBack="True" /></td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="cbPeriodo" runat="server" OnCheckedChanged="cbPeriodo_CheckedChanged"
                                Text=" &nbsp;Periodo" AutoPostBack="True" /></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                       
                        <td colspan="1">
                            <asp:LinkButton ID="btnexportar" runat="server"
                                Style="font-family: Calibri; font-size: medium"
                                Text="&nbsp;Excel&nbsp; <span class='glyphicon glyphicon-th'></span>"
                                CssClass="btn btn-success" OnClick="btnexportar_Click" Visible="False" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:DropDownList ID="DdlCargo" runat="server" class="chzn-select form-control input-sm"
                                Font-Size="Small" Style="width: 220px;" data-placeholder="Ingrese Cargo..." Enabled="False"
                                AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td>

                            <asp:DropDownList ID="DdlPeriodo" runat="server" class="chzn-select form-control input-sm"
                                Font-Size="Small" Style="width: 220px;" data-placeholder="Ingrese Periodo" Enabled="False"
                                AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: left;">

                            &nbsp;</td>
                       
                        <td>
                            <asp:LinkButton ID="btnBuscar" runat="server"
                                Text="Buscar  <span class='glyphicon glyphicon-search'></span>"
                                class="btn btn-info" OnClick="btnBuscar_Click" />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="10">
                            <div style="overflow-y: scroll; height: 380px">
                                <asp:GridView ID="GvReporte" runat="server" CellPadding="3" ShowFooter="false"
                                    CaptionAlign="Top" Font-Size="8.5pt" Font-Names="Tahoma" CssClass="table-hover"
                                    Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="Solid" BorderWidth="1px"
                                    OnRowDataBound="GvReporte_RowDataBound"
                                    OnRowCommand="GvReporte_RowCommand" DataKeyNames="CANDIDATO">
                                    <AlternatingRowStyle BackColor="#f3f7f2" />
                                    <Columns>
                                        <asp:BoundField DataField="CANDIDATO" HeaderText="CANDIDATO">
                                            <HeaderStyle ForeColor="White" BackColor="#006699" BorderStyle="Inset" />
                                            <ItemStyle HorizontalAlign="Left" ForeColor="Black" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="CARGO" HeaderText="CARGO">
                                            <HeaderStyle ForeColor="White" BackColor="#006699" BorderStyle="Inset" />
                                            <ItemStyle HorizontalAlign="Left" ForeColor="Black" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="PERIODO" HeaderText="PERIODO">
                                            <HeaderStyle ForeColor="White" BackColor="#006699" BorderStyle="Inset" />
                                            <ItemStyle HorizontalAlign="Left" ForeColor="Black" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="VOTO" HeaderText="VOTO">
                                            <HeaderStyle ForeColor="White" BackColor="#006699" BorderStyle="Inset" />
                                            <ItemStyle HorizontalAlign="Left" ForeColor="Black" />
                                        </asp:BoundField>
                                    
                                        <asp:ButtonField DataTextField="TOTAL" HeaderText="TOTAL" Visible="true" CommandName="TOTAL">
                                            <HeaderStyle ForeColor="White" BackColor="#006699" BorderStyle="Inset" />
                                            <ItemStyle HorizontalAlign="Left" ForeColor="Blue" BackColor="#f2f2f2" Font-Bold="true" />
                                        </asp:ButtonField>
                                    </Columns>
                                    <FooterStyle BackColor="LightYellow" Font-Bold="True" ForeColor="Black" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="gridview th" />
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
                        <td colspan="7">
                            <asp:HiddenField ID="__mensaje" runat="server" />
                            <asp:HiddenField ID="__pagina" runat="server" />

                        </td>
                      
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

