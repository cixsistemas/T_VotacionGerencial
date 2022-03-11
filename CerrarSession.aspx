<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CerrarSession.aspx.cs" Inherits="CerrarSession" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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
        <asp:HiddenField ID="__mensaje" runat="server" />
        <asp:HiddenField ID="__pagina" runat="server" />
    </form>
</body>
</html>
