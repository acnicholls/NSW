<%@ Page Title="Mikechi.com" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostList.aspx.cs" Inherits="NSW.Posts.PostList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="PostListOptions">
        <table class="optionsTable">
            <tr>
                <td class="categoryCell">
                    <h2>
                        <asp:Label ID="CategoryName" runat="server" />
                    </h2>
                </td>
                <td class="optionsCell">
                <% if (!IsMobileView)
                   { %>
                    <asp:Label ID="ItemsPerPageLabel" runat="server" /><br />
                    <asp:DropDownList ID="ddlItemsPerPage" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlItemsPerPage_SelectedIndexChanged"></asp:DropDownList>
                <% } %>
                </td>
                <td class="sortOptionsCell">
                    <asp:Label ID="SortOptionsLabel" runat="server" /><br />
                    <asp:DropDownList ID="ddlSortOptions" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlSortOptions_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div class="PostList">
        <asp:Repeater ID="rptPostList" runat="server">
            <ItemTemplate>
                <table class="PostListItemTable">
                    <tr>
                        <td rowspan="3">
                            <asp:HyperLink ID="ImageLink" runat="server">
                            <asp:Image ID="PostImage" runat="server" CssClass="PostListImage" />
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="PostListTitleButton">
                            <asp:HyperLink ID="PostTitleButton" runat="server" CssClass="PostTitleButton" />
                        </td>
                        <td class="PostListPrice">
                            <asp:Label ID="PostPrice" runat="server" CssClass="PostListPrice" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="PostListDescription">
                            <asp:Label ID="PostDescription" runat="server" CssClass="PostListDescription" />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Label ID="emptyListLabel" runat="server" Visible="false" />
    </div>
    <div class="navPostList">
        <asp:Button ID="btnPrev" runat="server" Text="Previous Page" 
            onclick="btnPrev_Click" />
        <asp:Button ID="btnNext" runat="server" Text="Next Page" 
            onclick="btnNext_Click" />
    </div>
</asp:Content>
