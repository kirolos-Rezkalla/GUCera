<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="GUCera.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Please Register as a Student or as an Instructor<br />
            <br />
            First Name:<br />
            <asp:TextBox ID="firstName" runat="server"></asp:TextBox>
            <br />
            <br />
            Last Name:<br />
            <asp:TextBox ID="lastName" runat="server"></asp:TextBox>
            <br />
            <br />
            Password:<br />
            <asp:TextBox ID="password" runat="server"></asp:TextBox>
            <br />
            <br />
            Email:<br />
            <asp:TextBox ID="email" runat="server"></asp:TextBox>
            <br />
            <br />
            Gender:<br />
            <asp:DropDownList ID="gender" runat="server">
                <asp:ListItem Value="-1">Choose your gender </asp:ListItem>
                <asp:ListItem Value="0">Male</asp:ListItem>
                <asp:ListItem Value="1">Female</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            Address:<br />
            <asp:TextBox ID="address" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="instructor" runat="server" Text="Register as Instructor" OnClick="instructor_register" />
        &nbsp;
            <asp:Button ID="student" runat="server" Text="Register as Student" OnClick="student_register" />
            <br />
            <br />
            Already have an account?<br />
            <br />
            <asp:Button ID="login" runat="server" Text="Login" OnClick="login_directly" />
            <br />
        </div>
    </form>
</body>
</html>
