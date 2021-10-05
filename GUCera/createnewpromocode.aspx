<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="createnewpromocode.aspx.cs" Inherits="GUCera.createnewpromocode" MasterPageFile="~/Site4.Master"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">
        <div>
            <br />code<br />
            <asp:TextBox ID="code" runat="server"></asp:TextBox>
               <br />issuedate<br /><asp:Calendar id="calendar1" runat="server">

           <OtherMonthDayStyle ForeColor="LightGray">
           </OtherMonthDayStyle>

           <TitleStyle BackColor="Blue"
                       ForeColor="White">
           </TitleStyle>

           <DayStyle BackColor="gray">
           </DayStyle>

           <SelectedDayStyle BackColor="LightGray"
                             Font-Bold="True">
           </SelectedDayStyle>

      </asp:Calendar>
            <br />expiry date<br />
          <asp:Calendar id="calendar2" runat="server">

           <OtherMonthDayStyle ForeColor="LightGray">
           </OtherMonthDayStyle>

           <TitleStyle BackColor="Blue"
                       ForeColor="White">
           </TitleStyle>

           <DayStyle BackColor="gray">
           </DayStyle>

           <SelectedDayStyle BackColor="LightGray"
                             Font-Bold="True">
           </SelectedDayStyle>

      </asp:Calendar>
         <br />discount<br />
            <asp:TextBox ID="discount" runat="server" Width="181px"></asp:TextBox>
         
        </div>
        <p>
            <asp:Button ID="done" runat="server" style="margin-left: 0px; margin-top: 5px" Text="Done" Width="215px" OnClick="createpromo" class="btn btn-dark"/>
        </p>

</asp:Content>

