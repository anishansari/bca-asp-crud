<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="crud._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   <asp:TextBox ID="txtName" runat="server" Placeholder="Name"></asp:TextBox><br />

<asp:TextBox ID="txtAddress" runat="server" Placeholder="Address"></asp:TextBox><br />
<asp:Button ID="btnAdd" runat="server" Text="Add User" OnClick="btnAdd_Click" /><br /><br />

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
    OnRowEditing="GridView1_RowEditing"
    OnRowCancelingEdit="GridView1_RowCancelingEdit"
    OnRowUpdating="GridView1_RowUpdating"
    OnRowDeleting="GridView1_RowDeleting">
    
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Address" HeaderText="Address" />

        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
    </Columns>
</asp:GridView>

</asp:Content>
