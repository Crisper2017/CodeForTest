<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CodeAssesment._Default" Async="true" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <p>This is the data from your file</p>
        <p>
            <asp:DataGrid ID="myDataGrid" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" />
                    <asp:BoundColumn DataField="Name" HeaderText="Name" />
                    <asp:BoundColumn DataField="Size" HeaderText="Size (in)" />
                    <asp:BoundColumn DataField="Type" HeaderText="Type" />
                    <asp:BoundColumn DataField="Desc" HeaderText="Description" />
                </Columns>
            </asp:DataGrid>
        </p>
        <p>
            These are the creatures broken out by type
            <asp:PlaceHolder ID="plTypes" runat="server"></asp:PlaceHolder>
        </p>
    </div>

</asp:Content>
