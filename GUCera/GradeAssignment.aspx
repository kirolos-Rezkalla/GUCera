<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GradeAssignment.aspx.cs" Inherits="GUCera.GradeAssignment" MasterPageFile="~/Site9.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">

        <div>
            Grade Assignment
        </div>
        <br />
       
        <div>

        </div>
         Student ID:<br />
            <asp:TextBox ID="StudentId" runat="server" TextMode="Number" min="1" step="1"></asp:TextBox>
            <br />
            <br />
        Course ID:<br />
            <asp:TextBox ID="CourseId" runat="server" TextMode="Number" min="1" step="1"></asp:TextBox>
            <br />
            <br />
        Assignment Number:<br />
            <asp:TextBox ID="AssignmnetNumber" runat="server" TextMode="Number" step="1"></asp:TextBox>
            <br />
            <br />
            Type:<br />
            <asp:DropDownList ID="Type" runat="server">
                <asp:ListItem Value="quiz"></asp:ListItem>
                <asp:ListItem Value="project"></asp:ListItem>
                <asp:ListItem Value="exam"></asp:ListItem>
             </asp:DropDownList>

               <br />
            <br />
         Grade:<br />
            <asp:TextBox ID="Grade" runat="server" TextMode="Number" step="0.01"></asp:TextBox>
            <br />
            <br />
        <asp:Button ID="Submit" runat="server" CssClass="auto-style5" Text="Submit" OnClick="Submit_Click" />

    </div>
    </asp:Content>