<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefineAssignment.aspx.cs" Inherits="GUCera.DefineAssignment" MasterPageFile="~/Site9.Master"%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">

        Please enter Assignment details here<p>
             <br />

            Course Id:<br />
            <asp:TextBox ID="CourseId" runat="server" TextMode="Number" min="1" step="1"></asp:TextBox>
            <br />
            <br />
            Number:<br />
            <asp:TextBox ID="Number" runat="server" TextMode="Number" min="1" step="1"></asp:TextBox>
            <br />
            <br />
            type:<br />
            <asp:DropDownList ID="Type" runat="server">
                <asp:ListItem Value="quiz"></asp:ListItem>
                <asp:ListItem Value="project"></asp:ListItem>
                <asp:ListItem Value="exam"></asp:ListItem>
             </asp:DropDownList>

               <br />
            <br />
             Full Grade:<br />
            <asp:TextBox ID="FullGrade" runat="server" TextMode="Number"></asp:TextBox>
            <br />
             Weight:<br />
            <asp:TextBox ID="Weight" runat="server" TextMode="Number" step="0.1"></asp:TextBox>
            <br />
            <div class="input-group">
             <p>
        <span class="input-group-text">Deadline</span>
    
    <asp:TextBox ID="Deadline" runat="server" TextMode="Date"></asp:TextBox>

            </p>
         </div>
            <br />
           
             Content:<br />
            <asp:TextBox ID="Content" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="SubmitAssignment" runat="server" Text="SubmitAssignment" OnClick="Submit_Click" />
            <br />
             
            <br />


        </p>

</asp:Content>