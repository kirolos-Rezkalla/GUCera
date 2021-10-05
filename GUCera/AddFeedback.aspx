<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddFeedback.aspx.cs" Inherits="GUCera.AddFeedback" MasterPageFile="~/Site2.Master"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">

            <div>
            Course Name:
            <select name='select_course' id='select_course' runat="server" class="auto-style1" ></select>
               <br />
               <br />
                <asp:Label  ID="Label1" runat="server" Text="Add Feedback: "></asp:Label>
                <br />
                <asp:TextBox  ID="your_feedback" runat="server" Height="100px" Width="300px"></asp:TextBox>
               <br />
               <br />
                <asp:Button ID="Button1" class="btn btn-dark" runat="server" Text="Add Feedback" onclick="Add" Width="141px"/>    
             </div>

</asp:Content>

<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 100px;
        }
    </style>
</asp:Content>


