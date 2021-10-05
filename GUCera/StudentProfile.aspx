<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentProfile.aspx.cs" Inherits="GUCera.StudentProfile" MasterPageFile="~/Site2.Master"%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">

        <div>
            <h2>My Profile</h2>
            <table class="table">
                <thead class="thead-light">
                    <tr>
                        <th scope="col" class="auto_style6">Id</th>
                        <th scope="col" class="auto_style6">First Name</th>
                        <th scope="col" class="auto_style6">Last Name</th>
                        <th scope="col" class="auto_style6">Gender</th>
                        <th scope="col" class="auto_style6">Email</th>
                        <th scope="col" class="auto_style6">Address</th>
                    </tr>
                </thead>
                <tbody runat="server" id="tabs"></tbody>
            </table><br/><br/>
           
        </div>
 
</asp:Content>