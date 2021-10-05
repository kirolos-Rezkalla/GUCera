<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="acceptcourse.aspx.cs" Inherits="GUCera.acceptcourse" MasterPageFile="~/Site4.Master"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">
        <div>
             <br />
            All Non Accepted Courses:<br />
            <asp:DropDownList ID="DropDownList1" runat="server" style="margin-top: 10px;">
            </asp:DropDownList>
          
          </div>
          <asp:Button ID="Done" runat="server" OnClick="AcceptCourse" Text="Accept Course" style="margin-left: 5px; margin-top: 30px;" class="btn btn-dark"/>
 </asp:Content>
