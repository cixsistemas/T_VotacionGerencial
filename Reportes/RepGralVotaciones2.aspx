<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepGralVotaciones2.aspx.cs" Inherits="Reportes_RepGanadores2" %>

<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="icon" type="image/ico" href="../imagenes/Sistema_.ico" />
<title>Detalle</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta charset="utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
<meta name="viewport" content="width=device-width, initial-scale=1"/>

<!-- Bootstrap -->
<link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
 <!-- ESTILOS PARA GRIDVIEW -->
    <style type="text/css">
    gridview
{
    margin:5px;
    border:none;
    background-color:#284775;
}

.gridview tr {
    text-align:center;
    border:none;
    border-bottom: solid;
    border-color:#FFFFFF;
    border-width:2px;
    font-size: small;   
}
    
.gridview th{
    border:none;
    background-color:#284775;
    padding: 3px;
    font-size: small; 
    text-align:center;
    align-content:Center;
    /*font-size: x-small;*/   
}

.gridview td{
    border:none;
    padding: 8px;
    margin: 5px;
}
  </style>
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
<body>
    <form id="form1" runat="server">
      <div class ="container-fluid">
  <div class="table-responsive" >        
     <table class="table table text-center">
         <tr>            
              
                             
             <td style="text-align: right;" colspan="3" >                       
                  <asp:LinkButton ID="btnexportar" runat="server" 
                         style="font-family: Calibri;  font-size: medium" 
                    Text="Exportar a Excel <span class='glyphicon glyphicon-th'></span>" 
                       CssClass="btn btn-success" OnClick="btnexportar_Click" Visible="True"  />

                    </td> 
             
             
               
         </tr>
        <tr>
            <td colspan="3">
                <div style="overflow-y: scroll; height: 550px">
                    <asp:GridView ID="GvReporte2" runat="server"
                        HorizontalAlign="Center" Font-Names="Tahoma"
                        CellPadding="4" ForeColor="#333333" GridLines="Both"
                        Font-Size="8" Width="100%" ShowFooter="true"
                        CssClass="table table-striped table-hover table-condensed small-top-margin" OnRowDataBound="GvReporte2_RowDataBound">
                        <RowStyle BackColor="#ffffff" ForeColor="#333333" />
                        <FooterStyle BackColor="#f3f7f2" Font-Bold="True" ForeColor="Black" />
                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridview th" />
                        <EditRowStyle BackColor="#999999" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                    <asp:HiddenField ID="__mensaje" runat="server" />
                    </td>
            <td>
                    <asp:HiddenField ID="__pagina" runat="server" />
                    </td>
            <td>
               </td>
        </tr>
       
    </table>
    </div>
       </div>
    </form>
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
<script src="../bootstrap/js/jquery-1.12.4.min.js"></script>
<!-- Include all compiled plugins (below), or include individual files as needed -->
<script src="../bootstrap/js/bootstrap.min.js"></script>
</body>
</html>