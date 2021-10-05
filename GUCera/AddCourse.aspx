<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="GUCera.AddCourse" MasterPageFile="~/Site9.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">

        <div>
            <br />
             Credit Hours:<br />
            <asp:TextBox ID="CreditHours" runat="server" TextMode="Number" min="1"></asp:TextBox>
            <br />
            <br />
            Course Name:<br />
            <asp:TextBox ID="CourseName" runat="server"></asp:TextBox>
            <br />
            <br />
            Pre-requisite Course:<br />
            <asp:TextBox ID="prerequisite" runat="server" TextMode="Number" ></asp:TextBox>
            <br />
            <br />
            Price:<br />
            <asp:TextBox ID="Price" runat="server" TextMode="Number"  step="0.01"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Submit" runat="server" CssClass="auto-style5" Text="Submit" OnClick="Submit_Click" />

        </div>

</asp:Content>