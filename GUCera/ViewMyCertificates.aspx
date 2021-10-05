<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewMyCertificates.aspx.cs" Inherits="GUCera.ViewMyCertificates" MasterPageFile="~/Site2.Master"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">
        <div>
            Course Name:
            <select name='select_course' id='select_course' runat="server" class="auto-style1" ></select>
            <br/>
            <br/>
            <asp:Button ID="B1" runat="server" Text="View My Certificates" OnClick="B1_Click" Width="184px" class="btn btn-dark"/>
            <br/>
            <br/>
            <table class="table">
            <thead class="thead-light">
                <tr>
                <th scope="col" class="auto-style2">Course ID</th>
                <th scope="col" class="auto-style2">Issue Date</th>
     
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

