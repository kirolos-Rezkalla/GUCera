<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentProfile.aspx.cs" Inherits="GUCera.Student" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class ="d-flex flex-column m1-3">
        <h2>My Profile</h2>
            <asp:Label ID ="Label1" class="text-secondary" runat="server" Text="Label">First Name:</asp:Label>
            <p id="p1" runat="server"> </p>
            <asp:Label ID ="Label2" class="text-secondary" runat="server" Text="Label">Last Name:</asp:Label>
            <p id="p2" runat="server"> </p>
            <asp:Label ID ="Label3" class="text-secondary" runat="server" Text="Label">Password:</asp:Label>
            <p id="p3" runat="server"> </p>
            <asp:Label ID ="Label4" class="text-secondary" runat="server" Text="Label">Address:</asp:Label>
            <p id="p4" runat="server"> </p>
            <asp:Label ID ="Label5" class="text-secondary" runat="server" Text="Label">Gender:</asp:Label>
            <p id="p5" runat="server"> </p>
            <asp:Label ID ="Label6" class="text-secondary" runat="server" Text="Label">Email:</asp:Label>
            <p id="p6" runat="server"> </p>
            <asp:Label ID ="Label7" class="text-secondary" runat="server" Text="Label">GPA:</asp:Label>
            <p id="p7" runat="server"> </p>
            <asp:Label ID ="Label8" class="text-secondary" runat="server" Text="Label">ID:</asp:Label>
            <p id="p8" runat="server"> </p>
        </div>
    </form>
</body>
</html>
