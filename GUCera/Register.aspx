<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="GUCera.Register" MasterPageFile="~/Site3.Master"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">

            <div class="col-md-12 mb-3">
            <h4>Please Register as a Student or as an Instructor</h4>
            </div>

            <div class="ml-3 mb-3">
            First Name:
            <asp:TextBox ID="firstName" runat="server"></asp:TextBox>
            </div>

            <div class="ml-3 mb-3">
            Last Name:
            <asp:TextBox ID="lastName" runat="server"></asp:TextBox>
            </div>

            <div class="ml-3 mb-3">
            Password:
            <asp:TextBox ID="password" runat="server"></asp:TextBox>
            </div>

            <div class="ml-3 mb-3">
            Email:
            <asp:TextBox ID="email" runat="server"></asp:TextBox>
            </div>

            <div class="ml-3 mb-3">
            Gender:
            <asp:DropDownList ID="gender" runat="server">
                <asp:ListItem Value="-1">Choose your gender </asp:ListItem>
                <asp:ListItem Value="0">Male</asp:ListItem>
                <asp:ListItem Value="1">Female</asp:ListItem>
            </asp:DropDownList>
            </div>

            <div class="ml-3 mb-3">
            Address:
            <asp:TextBox ID="address" runat="server"></asp:TextBox>
            </div>

            <div class="ml-3 mb-3">
            <asp:Button ID="instructor" runat="server" Text="Register as Instructor" OnClick="instructor_register" class="btn btn-dark"/>
            </div>

            <div class="ml-3 mb-3">
            <asp:Button ID="student" runat="server" Text="Register as Student" OnClick="student_register" class="btn btn-dark"/>
            </div>
 
</asp:Content>