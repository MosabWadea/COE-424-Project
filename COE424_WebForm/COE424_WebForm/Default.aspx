
<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="COE424_WebForm._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Content/StyleSheet1.css" rel="stylesheet" />
    <div class="jumbotron" style="align-content:inherit">
        <center>
        <h1 style="align-content:inherit">Bus Schedule</h1>
        </center>
<%--        <p><a href="http://www.asp.net" class="btn btn-primary btn-large">Learn more &raquo;</a></p>--%>
        
    </div>

    <div class="row">
        <asp:UpdatePanel runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:GridView ID="ScheduleGrid" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2">
                    <Columns>
                        <asp:BoundField DataField="BusId" HeaderText="BusId" SortExpression="BusId"></asp:BoundField>
                        <asp:BoundField DataField="CurrentStation" HeaderText="CurrentStation" SortExpression="CurrentStation"></asp:BoundField>
                        <asp:BoundField DataField="NextStation" HeaderText="NextStation" SortExpression="NextStation"></asp:BoundField>
                        <asp:BoundField DataField="ETA" HeaderText="ETA" SortExpression="ETA"></asp:BoundField>
                        <asp:BoundField DataField="CheckIn" HeaderText="CheckIn" SortExpression="CheckIn"></asp:BoundField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings:COE424ConnectionString2 %>' SelectCommand="SELECT * FROM [Schedule]"></asp:SqlDataSource>
                <asp:Timer ID="tableCounter" runat="server" OnTick="tableCounter_Tick" Interval="1000"></asp:Timer>
<%--        <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:COE424ConnectionString %>' SelectCommand="SELECT * FROM [Schedule]"></asp:SqlDataSource>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
        


        <%--<div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>--%>
    </div>

</asp:Content>
