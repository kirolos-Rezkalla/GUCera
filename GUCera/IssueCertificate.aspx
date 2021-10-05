<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IssueCertificate.aspx.cs" Inherits="GUCera.IssueCertificate" MasterPageFile ="~/Site9.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">

        <div class="auto-style1">
            <br />
            Issue Certificate
            <br />
            <br />

          Course Id:<br />
            <asp:TextBox ID="CourseId" runat="server" TextMode="Number" min="1" step="1"></asp:TextBox>
            <br />
            <br />
        Student Id:<br />
            <asp:TextBox ID="StudentId" runat="server" TextMode="Number" min="1" step="1"></asp:TextBox>
            <br />
            <br />

                        <div class="input-group">
             <p>
        <span class="input-group-text">issuedate: </span>
    
    <asp:TextBox ID="IssueDate" runat="server" TextMode="Date"></asp:TextBox>

            </p>
         </div>
            <br />
      <asp:Button ID="Submit" runat="server" CssClass="auto-style5" Text="Submit" OnClick="Submit_Click" />


    </asp:Content>