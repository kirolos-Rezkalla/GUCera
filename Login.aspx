<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GUCera.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Please Login<br />
            <br />
            ID:<br />
            <asp:TextBox ID="username" runat="server"></asp:TextBox>
            <br />
            <br />
            Password:<br />
            <asp:TextBox ID="password" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="signin" runat="server" Text="Login" OnClick="login" />
        </div>
    </form>
</body>
</html>
