<%@ Page Title="Mikechi.com" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewPost.aspx.cs" Inherits="NSW.Posts.ViewPost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="https://cdn.leafletjs.com/leaflet-0.7.3/leaflet.css" />
    <script type="text/javascript" src="https://cdn.leafletjs.com/leaflet-0.7.3/leaflet.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="item">
        <table class="itemInfo">
            <tr>
                <td>
                    <asp:Label ID="vpItemTitle" runat="server" CssClass="itemTitle" />
                </td>
    <% if (IsMobileView)
       {%>

            </tr>
            <tr>
            <% } %>
                <td class="itemPrice">
                    <asp:Label ID="vpItemPrice" runat="server" CssClass="itemPrice" />
                </td>
            </tr>
            <tr>
                <td class="picCell" <%if (!IsMobileView) { %> colspan="2" <%} %>>
                    <asp:Image ID="vpItemPic" runat="server" CssClass="itemPic" />
                </td>
            </tr>
            <tr>
                <td class="thumbCell" <% if(!IsMobileView) { %>  colspan="2"  <% } %>>
                    <asp:ImageButton ID="vpItemThumb1" runat="server" CssClass="itemThumb" 
                        onclick="vpItemThumb1_Click"/>
                    <asp:ImageButton ID="vpItemThumb2" runat="server" CssClass="itemThumb" 
                        onclick="vpItemThumb2_Click"/>
                    <asp:ImageButton ID="vpItemThumb3" runat="server" CssClass="itemThumb" 
                        onclick="vpItemThumb3_Click"/>
                    <asp:ImageButton ID="vpItemThumb4" runat="server" CssClass="itemThumb" 
                        onclick="vpItemThumb4_Click"/>
                </td>
            </tr>
            <tr>
                <td <% if (!IsMobileView) { %> colspan="2" <%} %> >
                    <p>
                    <asp:Label ID="vpDescription" runat="server" CssClass="itemDesc" />
                    </p>
                </td>
            </tr>
            <tr>
                <td class="mapCell">
                    <div id="map"></div>
                </td>
                <% if (IsMobileView ) { %>
            </tr>
            <tr>
            <%} %>
                <td class="userInfo">
                    <br />
                    <asp:Label ID="vpUserPhone" runat="server" CssClass="userContactPhone" />
                    <br />
                    <br />
                    <asp:Button ID="vpUserContact" runat="server" CssClass="userContactButton" 
                        onclick="vpUserContact_Click" />
                    <br />
                    <br />
                    <asp:Button ID="vpUserItems" runat="server" CssClass="userItemsButton" 
                        onclick="vpUserItems_Click" />
                        <br />
                        <br />
                    <asp:Button ID="vpUserEdit" runat="server" CssClass="userEditButton" 
                        onclick="vpUserEdit_Click" />
                </td>
            </tr>
        </table>
    </div>
 <%= LoadMap() %>
</asp:Content>
