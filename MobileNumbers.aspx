<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobileNumbers.aspx.cs" Inherits="GUCera.MobileNumbers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Please insert your mobile number(s)<br />
            <br />
            ID:<br />
            <asp:TextBox ID="mobileNumberID" runat="server"></asp:TextBox>
            <br />
            <br />
            Mobile Number:<br />
            <asp:TextBox ID="mobileNumber" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="insert" runat="server" Text="Insert" OnClick="insert_Click" />
        &nbsp;
            <asp:Button ID="done" runat="server" Text="Done" OnClick="done_Click" />
        </div>
    </form>
</body>
</html>
