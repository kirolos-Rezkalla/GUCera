<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCreditCard.aspx.cs" Inherits="GUCera.AddCreditCard" MasterPageFile="~/Site2.Master"%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">

   <div class="col-md-12">
            <h2>Add Credit Card</h2>
            </div><br />
            
            <div >Credit Card Number<br />
                <asp:TextBox ID="creditCard_number" runat="server" TextMode="Number" ></asp:TextBox><br />
                <asp:Label ID="creditCard_number_error" runat="server" style="color: red" ></asp:Label>
            </div><br />


            <div>Name On Card<br />
                <asp:TextBox ID="name" runat="server"  ></asp:TextBox><br />
                <asp:Label ID="name_error" runat="server" style="color: red" ></asp:Label>
            </div><br />

            <div>Expiry Date<br />
                <asp:TextBox ID="ExpiryDate" runat="server" TextMode="Date"></asp:TextBox><br /><br />
                <asp:Label ID="ExpiryDate_error" runat="server" style="color: red" ></asp:Label>
            </div><br />

            <div>CVV<br />
                <asp:TextBox ID="Cvv" runat="server" TextMode="Number" MaxLength="3"  ></asp:TextBox><br />
                <asp:Label ID="Cvv_error" runat="server" style="color: red" ></asp:Label>
            </div><br />



            <div>
                <asp:Button ID="Button1" runat="server" Text="Add Credit Card" OnClick="Button1_Click" />
            </div><br /><br />

    <p>------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</p>
        <div runat="server" id="credit_card_div">
            <h2>Your Credit Cards</h2>
            <table class="table">
                <thead class="thead-light">
                    <tr>
                        <th scope="col" class="auto_style6">Card number</th>
                        <th scope="col" class="auto_style6">Card holder name</th>
                        <th scope="col" class="auto_style6">Expiry date</th>
                        <th scope="col" class="auto_style6">Pay By</th>
                        <th scope="col" class="auto_style6">Remove</th>
                    </tr>
                </thead>
                <tbody runat="server" id="tabs"></tbody>
            </table><br/><br/>
            <%--<asp:Button ID="addCard" runat="server" Text="Add another Credit Card" OnClick="addCard_Click" />--%>
        </div><br /><br />
            
    
</asp:Content>