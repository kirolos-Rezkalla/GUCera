<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GUCera.Login" MasterPageFile="~/Site1.Master"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">

            <div class="col-md-12">
            <h4>Please Login</h4>
            </div>

            <div class="col-md-6 mb-2">
            ID:
            <asp:TextBox ID="username" runat="server" TextMode="Number" min="1" step="1"></asp:TextBox>
            </div>

            <div class="col-md-6">
            Password:
            <asp:TextBox ID="password" runat="server"></asp:TextBox>
            </div>

            <div class="col-12 mt-3">
            <asp:Button ID="signin" runat="server" Text="Login" OnClick="login" class="btn btn-dark"/>
            </div>

</asp:Content>
