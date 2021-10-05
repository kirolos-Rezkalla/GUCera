<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubmitAssignment2.aspx.cs" Inherits="GUCera.SubmitAssignment2" MasterPageFile="~/Site2.Master"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">

            <div>
                Course Name:
                <select name="select_course" id="select_course" runat="server" class="auto-style1"></select>
            </div>
            <br />
            <div>
                Assignment Type:
                <asp:DropDownList ID="assignment_Type" runat="server">
                    <asp:ListItem Value="quiz">Quiz </asp:ListItem>
                    <asp:ListItem Value="project">Project</asp:ListItem>
                    <asp:ListItem Value="exam">Exam</asp:ListItem>
                </asp:DropDownList>
            </div>
            <br />
            <div>
                Assignment Number:
                <asp:TextBox ID="assign_number" runat="server" TextMode="Number" min="1" step="1"></asp:TextBox>
            </div>
            <br />
            <div>
            <asp:Button ID="submit" runat="server" Text="Submit Assignment" OnClick="Submit" class="btn btn-dark"/>
            </div>

      </asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 100px;
        }
    </style>
</asp:Content>
