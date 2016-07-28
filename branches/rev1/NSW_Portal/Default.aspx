<%@ Page Title="Mikechi.com" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="NSW._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<br />
<asp:Label ID="Instructions" runat="server" />
<br />
<br />
<br />
    <div class="leftSide">
        <asp:Repeater ID="CategoryListLeft" runat="server" 
            onitemcommand="CategoryList_ItemCommand">
            <ItemTemplate>
                <asp:HiddenField ID="CategoryID" runat="server" />
                 <table class="CategoryListItemTable">
                    <tr>
                        <td>
                            <table class="CategoryListItemHeader">
                                <tr class="">
                                    <td class="">
                                        <asp:Button ID="CategoryButton" runat="server" CommandName="Launch" CssClass="CategoryButton" />
                                    </td>
                                    <td class="">
                                        <asp:Label ID="CategoryPosts" runat="server" CssClass="CategoryPosts" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="CategoryListItemRow">
                        <td class="CategoryListItemCell">
                            <asp:Label ID="CategoryDescription" runat="server" CssClass="CategoryListItemDesc"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="rightSide">
        <asp:Repeater ID="CategoryListRight" runat="server" 
            onitemcommand="CategoryList_ItemCommand">
            <ItemTemplate>
                <asp:HiddenField ID="CategoryID" runat="server" />
                 <table class="CategoryListItemTable">
                    <tr>
                        <td>
                            <table class="CategoryListItemHeader">
                                <tr>
                                    <td>
                                        <asp:Button ID="CategoryButton" runat="server" CommandName="Launch" CssClass="CategoryButton" />
                                    </td>
                                    <td>
                                        <asp:Label ID="CategoryPosts" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="CategoryListItemRow">
                        <td class="CategoryListItemCell">
                            <asp:Label ID="CategoryDescription" runat="server" CssClass="CategoryListItemDesc"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
