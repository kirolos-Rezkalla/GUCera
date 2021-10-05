<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InstructorViewAssignment.aspx.cs" Inherits="GUCera.ViewAssignment" MasterPageFile="~/Site9.Master"%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">

        <div>
  
                Course Id:<br />
            <asp:TextBox ID="CourseId" runat="server" TextMode="Number" min="1" step="1"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Submit" runat="server" CssClass="auto-style5" Text="Submit" OnClick="Submit_Click" />
            <br />
            <br />
            <br />
            <table class="table">
            <thead class="thead-light">
            <tr>

                        <th scope="col" class="auto-style6">Student ID</th>
                        <th scope="col" class="auto-style6">Course Id</th>
                        <th scope="col" class="auto-style6">Assignment Number</th>
                        <th scope="col" class="auto-style6">Assignment Type</th>
                        <th scope="col" class="auto-style6">Grade</th>

                    </tr>
                    </thead>
                <tbody runat="server" id="tabs">

                </tbody>
            </table>
        </div>
 
    </asp:Content>