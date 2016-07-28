<%@ Page Title="Mikechi.com" Language="C#" AutoEventWireup="true" CodeBehind="Splash.aspx.cs" Inherits="NSW.Splash" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mikechi.com</title>
    <meta name="description" content="Classified ad website for Nagano Prefecture Japan" />
    <meta property="og:title" content="Mikechi.com, Classified Ads for Nagano." />
    <meta property="og:type" content="website"/>
    <meta property="og:image" content="http://www.mikechi.com/images/Splash.JPG" />
    <meta property="og:url" content="http://www.mikechi.com" />
    <meta property="og:description" content="Need something that you don't want to buy brand new? Want to sell something you no longer need? Try mikechi.com for your used item classified ads!" />
    <meta property="fb:admins" content="1307956076,anthony.charles.nicholls" />
    <meta name="twitter:card" content="summary" />
    <meta name="twitter:url" content="http://www.mikechi.com" />
    <meta name="twitter:title" content="Classified Ads for Nagano Pref., Japan" />
    <meta name="twitter:description" content="Need something that you don't want to buy brand new? Want to sell something you no longer need? Try mikechi.com for your used item classified ads!" />
    <meta name="twitter:image" content="http://www.mikechi.com/images/Splash.JPG" />



    <% if (!Request.Browser.IsMobileDevice)
       {%>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <% }
       else
       { %>
    <link href="~/Styles/MobileSite.css" rel="stylesheet" type="text/css" />
    <% } %>
    <meta name="viewport" content="width=device-width"/>
</head>
<body>
    <form id="NSW_SplashForm" runat="server">
    <div class="Splash">
    <br />

        <h1>
           <asp:Label ID="SplashWelcomeEnglish" runat="server" />
            <br />
           <asp:Label ID="SplashWelcomeJapanese" runat="server" />
        </h1>
    <br />
    <center>
        <div class="container">
            <img src="images/Splash.JPG" class="splashImage" alt="A photo of a flower." />
        </div>
    </center>

    <br />
           <asp:Label ID="SplashInstructionsEnglish" runat="server" />
    <br />
           <asp:Label ID="SplashInstructionsJapanese" runat="server" />
    <br />
    <p>
        <asp:Button ID="btnEnglish" runat="server" Text="English 英語" 
            CssClass="SplashButton" onclick="btnEnglish_Click" />  
        <asp:Button ID="btnJapanese" runat="server" Text="Japanese 日本語" 
            CssClass="SplashButton" onclick="btnJapanese_Click" />
    </p>
    </div>
    </form>
</body>
</html>
