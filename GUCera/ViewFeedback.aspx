<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewFeedback.aspx.cs" Inherits="GUCera.ViewFeedback" MasterPageFile="~/Site9.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">

        <div>

                Course Id:<br />
            <asp:TextBox ID="CourseId" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Submit" runat="server" CssClass="auto-style5" Text="Submit" OnClick="Submit_Click" />
            <br />
            <br />
            <br />
            <table class="table">
            <thead class="thead-light">
            <tr>

                        <th scope="col" class="auto-style6"> Number</th>
                        <th scope="col" class="auto-style6">Comment</th>
                        <th scope="col" class="auto-style6"> Number Of Likes</th>
                     

                    </tr>
                    </thead>
                <tbody runat="server" id="tabs">

                </tbody>
            </table>
        <%--</div>--%>

    </asp:Content>