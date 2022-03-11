<%@ Page Language="C#" EnableEventValidation="true" Debug="true" AutoEventWireup="true" CodeFile="VotacionCandidato.aspx.cs" Inherits="Votacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="Imagenes/Inicio.ico"/>
<title>Votacion</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta charset="utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->


<!-- Bootstrap -->
<link href="bootstrap/css/bootstrap.min.css" rel="stylesheet"/>

<%-- PARA BUSCAR EN DROPDOWN LIST --%>
<link rel="stylesheet" href="Otros_css_js/chosen.css" />


<%
    string uAg = Request.ServerVariables["HTTP_USER_AGENT"];
    Regex regEx = new Regex(@"android|iphone|ipad|ipod|blackberry|symbianos", RegexOptions.IgnoreCase);
    bool isMobile = regEx.IsMatch(uAg);
    if (isMobile)
    {
        Response.Write("Solo puede Registrar Asistencia desde Computadora. ");
        Response.Write("Esta navegando desde un dispositivo móvil");
        //DdlCargo.Enabled = false;
        DdlSocio.Enabled = false;
        btnRegistrar.Enabled = false;
    }
%>


 <script type="text/javascript">
        function MostrarMensaje() {
            var mensaje = document.getElementById("__mensaje").value;
            if (mensaje != "") {
                alert(mensaje);
                if (document.getElementById("__pagina").value != "")
                    location.href = document.getElementById("__pagina").value;
            }
        }

        function window_load() {
            MostrarMensaje()
        }

        function Confirmar(men) {
            if (!confirm(men))
                return false;
        }

    </script>

    <script type="text/javascript">
        function myselection(rbtnid) {
            var rbtn = document.getElementById(rbtnid);
            var rbtnlist = document.getElementsByTagName("input");
            for (i = 0; i < rbtnlist.length; i++) {
                if (rbtnlist[i].text == "radio" && rbtnlist[i].id != rbtn.id) {
                    rbtnlist[i].checked = false;

                }
            }
        }
    </script>
</head>


<body onload="MostrarMensaje();">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="col-md-6 col-md-offset-3">
            <div class="table-responsive">
            <table class="table table-condensed">
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="3"
                        style="text-align: center; color: #FFFFFF; background-color: indianred;">
                        <asp:Label ID="Label3" runat="server" Text="VOTACION" Font-Bold="True" Font-Names="Tahoma">
                        </asp:Label>
                    </td>
                </tr>
              
                <tr>
                    <td
                        style="text-align: center; color: #FFFFFF; background-color: indianred;">
                        <asp:Label ID="Label1" runat="server" Text="SOCIO" Font-Bold="True" Font-Names="Tahoma">
                        </asp:Label>
                        &nbsp;&nbsp;
                    </td>

                    <td style="text-align: Left;">
                        <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>--%>
                        <asp:DropDownList ID="DdlSocio" runat="server" class="chzn-select form-control input-sm"
                            data-placeholder="Seleccione Socio" AutoPostBack="True" OnSelectedIndexChanged="DdlSocio_SelectedIndexChanged">
                        </asp:DropDownList>
                        <%--</ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnRegistrar" EventName="Click" />
                                   
                                </Triggers>
                            </asp:UpdatePanel>--%>
                    </td>

                </tr>

                <tr>
                    <td colspan="3" style="text-align: center;">
                       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                                <asp:GridView ID="GrVoto" runat="server" Font-Size="8pt" Font-Names="Tahoma"
                                    CellPadding="4" ForeColor="#333333" HorizontalAlign="Center"
                                    AutoGenerateColumns="False" DataKeyNames="ID"
                                    CssClass="table table-striped table-hover table-condensed small-top-margin" 
                                    OnSelectedIndexChanging="GrVoto_SelectedIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Seleccionar" HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <input name="RadioButton1" type="radio" value='<%# Eval("ID") %>' 
                                                    onclick="javascript.myselection(this.id)" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false">
                                            <HeaderStyle Width="1%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NOMBRE" HeaderText="NOMBRE" Visible="true">
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CARGO" HeaderText="CARGO" Visible="true">
                                            <HeaderStyle HorizontalAlign="Left" Width="1%" />
                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PERIODO" HeaderText="PERIODO" Visible="True">
                                            <HeaderStyle HorizontalAlign="Left" Width="1%" />
                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                        </asp:BoundField>

                                    </Columns>

                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="DarkSeaGreen" Font-Bold="True" ForeColor="Black" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" BorderColor="Black"
                                        CssClass="GridviewScrollHeader" HorizontalAlign="Left" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    <RowStyle CssClass="GridviewScrollItem" BackColor="#ffffff" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="GhostWhite" ForeColor="#333333" Font-Names="Tahoma" />
                                </asp:GridView>
                       
                            <%--</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DdlSocio" EventName="SelectedIndexChanged" />
                              
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center;">
                         <asp:Label ID="MensajeCandidatos" runat="server" Text="Label" Visible="False"
                            Font-Bold="True" Font-Names="Tahoma" ForeColor="Red"></asp:Label>
                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                        <asp:Label ID="MensajeSociosVotaron" runat="server" Text="Label" Visible="False"
                            Font-Bold="True" Font-Names="Tahoma" ForeColor="Red"></asp:Label>
                                <%--</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DdlSocio" EventName="SelectedIndexChanged" />                           
                            </Triggers>
                        </asp:UpdatePanel>--%>
                        </td> </tr> 
                <tr>
                    <td colspan="3" style="text-align: center;">
                                <asp:Button ID="btnRegistrar" runat="server" class="btn btn-success" OnClick="btnRegistrar_Click" 
                                    OnClientClick="return Confirmar('¿Desea Registrar Votacion?');" 
                                    Style="font-family: Calibri; font-size: medium" Text="&nbsp;&nbsp;&nbsp;VOTAR &nbsp;&nbsp;&nbsp;" Font-Bold="True" Visible="False" />
                                </td>
                </tr>
                <tr><td colspan="3" style="text-align: left;">
                        <asp:GridView ID="GvListado" runat="server" Font-Size="8" Font-Names="Tahoma"
                            CellPadding="4" ForeColor="#333333" GridLines="Both" HorizontalAlign="center"
                            AutoGenerateColumns="true" ShowFooter="false"
                            CssClass="table table-striped table-hover table-condensed small-top-margin" OnRowDataBound="GvListado_RowDataBound">
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="DarkSeaGreen" Font-Bold="True" ForeColor="Black" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" BorderColor="Black"
                                CssClass="GridviewScrollHeader" HorizontalAlign="Left" />
                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                            <RowStyle CssClass="GridviewScrollItem" BackColor="#ffffff" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <%--<AlternatingRowStyle BackColor="GhostWhite" ForeColor="#333333" Font-Names="Tahoma" />--%>
                        </asp:GridView>
                    </td></tr>
                 <tr><td colspan="3" style="text-align: center;">
                     <asp:Button ID="BtnCerrarVotacion" runat="server" Text="Cerrar Votacion" OnClick="BtnCerrarVotacion_Click"
                         CssClass="btn btn-danger" onclientclick="return Confirmar('¿Esta seguro que desea cerrar votacion?');"
                         Style="font-family: Calibri; font-size: medium" Font-Bold="True" Visible="False"/>
                     </td> </tr> 

                <tr>
                    <td style="text-align: right" colspan="2">
                        <asp:HiddenField ID="__mensaje" runat="server" />
                        <asp:HiddenField ID="__pagina" runat="server" />
                        <asp:HiddenField ID="Id_" runat="server" Value="0"/>
                        <asp:HiddenField ID="Id_CargoVoto" runat="server" Value="0" Visible="False" />
                        <asp:HiddenField ID="Id_Periodo" runat="server" Value="0" Visible="False" />
                        <asp:HiddenField ID="Id_Candidato" runat="server" Value="0"/>
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Visible="False" />
                        <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
                        <asp:TextBox ID="FechaActual" runat="server" Visible="False"></asp:TextBox>
                    </td>
                </tr>
            </table>
 </div>
        </div>
    </form>

    <%-- PARA DROPDOWN LIST --%>
    <script src="Otros_css_js/jquery.min.js" type="text/javascript"></script>
    <script src="Otros_css_js/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>


    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="bootstrap/js/jquery-1.12.4.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="bootstrap/js/bootstrap.min.js"></script>



</body>
</html>
