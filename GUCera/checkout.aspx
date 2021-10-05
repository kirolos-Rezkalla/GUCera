<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="GUCera.Checkout" MasterPageFile="~/Site2.Master"%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">

     <style>
      .bd-placeholder-img {
        font-size: 1.125rem;
        text-anchor: middle;
        -webkit-user-select: none;
        -moz-user-select: none;
        user-select: none;
      }
      .buttongreen
{
    color:green;    
}

      @media (min-width: 768px) {
        .bd-placeholder-img-lg {
          font-size: 3.5rem;
        }
      }
    </style>

    <h1>Checkout</h1>
        <div>
            <p>------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</p>
            <h2>Promocodes</h2>
            <table class="table">
                <thead class="thead-light">
                    <tr>
                        <th scope="col" class="auto_style6">PromoCode</th>
                        <th scope="col" class="auto_style6">Issue Date</th>
                        <th scope="col" class="auto_style6">Expiry Date</th>
                        <th scope="col" class="auto_style6">Discount</th>
                        <th scope="col" class="auto_style6">Redeem</th>
                    </tr>
                </thead>
                <tbody runat="server" id="tabs"></tbody>
            </table><br/><br/>
        </div>
    <b><asp:Label ID="promocode_success" runat="server" Text="Promocode Added successfully" Visible="false" style="color: red" ></asp:Label></b><br />
   <b><asp:Label ID="redeem_error" runat="server" Text="Choose Course to redeem promocode" Visible="false" style="color: red" ></asp:Label></b>

    
    <p>------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</p>

          
    
      <div class="col-md-5 col-lg-4 order-md-last">
        <h4 class="d-flex justify-content-between align-items-center mb-3">
          <span class="text-muted">Your cart</span>
        </h4>
        <ul class="list-group mb-3">
          <li class="list-group-item d-flex justify-content-between lh-sm">
            <div>
              <h6  class="my-0"></h6>
                <asp:Label ID="course_name" runat="server" ></asp:Label>
             </div>
              <div>
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label ID="course_price"  runat="server" ></asp:Label>            
            </div>

          </li>
        
          
          
          <li class="list-group-item d-flex justify-content-between bg-light">
            <div class="text-success">
              <h6 class="my-0">Promo code</h6>
              
                <asp:Label ID="promocode" runat="server" ></asp:Label>
                </div>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="amount_deducted" class="text-success" runat="server" ></asp:Label>
            
          </li>
          <li class="list-group-item d-flex justify-content-between">
            <strong>Total (EGP)</strong>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="total" runat="server" ></asp:Label>
          </li>
            
        </ul>

        
          <div class="input-group">
            
              <asp:Label ID="Pay_Button_id" runat="server" ></asp:Label>
              &nbsp;
              <asp:Label ID="ClearCart_Button_ID" runat="server" ></asp:Label>
              
          </div>
        
      </div><br /><br />

</asp:Content>
