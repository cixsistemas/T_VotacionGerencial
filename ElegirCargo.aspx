<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ElegirCargo.aspx.cs" Inherits="VotacionCargo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" type="image/ico" href="Imagenes/Inicio.ico" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Elegir Cargo</title>
    <!-- Bootstrap -->
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
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
</head>
<body onload="MostrarMensaje()">
    <form id="form1" runat="server">
        <%-- <div class="container">  
        <h3>Bootstrap Image Responsive Embeds</h3>  
        <h4>Responsive Embeds aspect ratio 16:9</h4>  
        <div class="embed-responsive embed-responsive-16by9">--%>  
        <div class="col-md-6 col-md-offset-3">
            <div class="table-responsive">
                <table class="table table-bordered table-hover table text-center">
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: center; color: #FFFFFF; background-color: indianred;">
                            <asp:Label ID="Label1" runat="server" Text="CARGO A VOTAR" Font-Bold="True" Font-Names="Tahoma"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="DdlCargo" runat="server" class="form-control input-sm"
                                data-placeholder="Seleccione Cargo" Enabled="True">
                            </asp:DropDownList>
                            <asp:RangeValidator ID="RvDdlCargo" runat="server" BackColor="Yellow"
                                ControlToValidate="DdlCargo" ErrorMessage="Busque y Seleccione Cargo" ForeColor="Red" MaximumValue="99999"
                                MinimumValue="1" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                            <asp:RequiredFieldValidator ID="RFVDdlCargo" runat="server" BackColor="Yellow"
                                ControlToValidate="DdlCargo" ErrorMessage="*..." ForeColor="Red"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; color: #FFFFFF; background-color: indianred;">
                            <asp:Label ID="Label2" runat="server" Text="PERIODO" Font-Bold="True" Font-Names="Tahoma"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="DdlPeriodo" runat="server" class="form-control input-sm"
                                data-placeholder="Seleccione Cargo" Enabled="True">
                            </asp:DropDownList>
                            <asp:RangeValidator ID="RvDdlPeriodo" runat="server" BackColor="Yellow"
                                ControlToValidate="DdlPeriodo" ErrorMessage="Busque y Seleccione Periodo" ForeColor="Red" MaximumValue="99999"
                                MinimumValue="1" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                            <asp:RequiredFieldValidator ID="RfvDdlPeriodo" runat="server" BackColor="Yellow"
                                ControlToValidate="DdlPeriodo" ErrorMessage="*..." ForeColor="Red"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="MensajeCandidatos" runat="server" Text="Label" Visible="False"
                                Font-Bold="True" Font-Names="Tahoma" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BtnEmpezar" runat="server" Text="Empezar" OnClick="BtnEmpezar_Click"
                                class="btn btn-success" OnClientClick="return Confirmar('¿Desea empezar votacion?');" />

                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="BtnAperturar" runat="server" Text="Aperturar" class="btn btn-info" OnClick="BtnAperturar_Click"
                            OnClientClick="return Confirmar('¿Seguro que desea aperturar votacion?');" />

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="__mensaje" runat="server" />
                            <asp:HiddenField ID="__pagina" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
