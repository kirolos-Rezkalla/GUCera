<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignmentsContent.aspx.cs" Inherits="GUCera.AssignmentsContent" MasterPageFile="~/Site2.Master"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">
        <div>
            Course Name:
            <select name="select_course" id="select_course" runat="server" class="auto-style1"></select>
            <br />
            <br />
            <asp:Button ID="view_assign" runat="server" Text="View My Assignments" OnClick="view_assign_Click" Width="205px" class="btn btn-dark"/>
            <br />
            <br />
            <table class="table">
            <thead class="thead-light">
            <tr>

                        <th scope="col" class="auto-style6">Assignment Number</th>
                        <th scope="col" class="auto-style6">Assignment Type</th>
                        <th scope="col" class="auto-style6">Assignment Content</th>

                    </tr>
                    </thead>
                <tbody runat="server" id="tabs">

                </tbody>
            </table>
        </div>
 </asp:Content>


<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 100px;
        }
    </style>
</asp:Content>



