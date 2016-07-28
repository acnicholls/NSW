<%@ Page Title="" Language="C#" MasterPageFile="Test.Master" AutoEventWireup="true" CodeBehind="AdRotatorExample.aspx.cs" Inherits="NSW.Admin.Test.AdRotatorExample" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:AdRotator id="arMainAd1" runat="server" AdvertisementFile="ads/ads1.xml" Target="_self"> </asp:AdRotator>
    <br />
</asp:Content>
