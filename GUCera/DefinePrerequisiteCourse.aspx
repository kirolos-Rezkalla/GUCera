<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefinePrerequisiteCourse.aspx.cs" Inherits="GUCera.DefinePrerequisiteCourse"  MasterPageFile="~/Site2.Master"  %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">

            <div>
            Course Name
                  <asp:Label ID="course_Name" runat="server" Text="Label"></asp:Label><br />
                <asp:Label ID="course_id" runat="server" Text="Label"></asp:Label><br />
                <asp:Button ID="Button1" runat="server" Text="Define Pre-requisite" OnClick="define_pre_requisite" />
                <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Done" />


             </div>

</asp:Content>
