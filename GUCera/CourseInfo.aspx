<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseInfo.aspx.cs" Inherits="GUCera.CourseInfo" MasterPageFile="~/Site2.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Body" runat="server">
        <div>
            <h2>Available Courses</h2>
           <table class="table">
                <thead class="thead-light">
                    <tr>
                        <th scope="col" class="auto_style6">Course</th>
                        <th scope="col" class="auto_style6">Course id</th>
                        <th scope="col" class="auto_style6">Credit hours</th>
                        <th scope="col" class="auto_style6">Description</th>
                        <th scope="col" class="auto_style6">Main instructor</th>
                        <th scope="col" class="auto_style6">Price</th>
                        <th scope="col" class="auto_style6">Enroll</th>
                    </tr>
                </thead>
                <tbody runat="server" id="tabs"></tbody>
            </table><br/><br/>
   
        </div>
</asp:Content>