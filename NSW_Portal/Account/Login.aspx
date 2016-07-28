<%@ Page Title="Mikechi.com" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="NSW.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Wizard ID="LoginWizard" runat="server" EnableViewState="true" DisplaySideBar="False" >
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>
            <asp:WizardStep ID="LoginUserWizardStep0" runat="server" Title="Account Information">
                <h2>
                   <asp:Label ID="LoginPageTitle" runat="server" Text="Log In"></asp:Label>
                </h2>
                <p>
                    <asp:Label ID="LoginPageInstructions" runat="server" Text="Please enter your username and password."></asp:Label>
                    <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Register</asp:HyperLink> <asp:Label ID="RegisterInstructions" runat="server" Text="if you don't have an account."></asp:Label>
                </p>
                <span class="failureNotification">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                        ValidationGroup="LoginUserValidationGroup"/>
                <div class="accountInfo">
                    <fieldset class="login">
                        <legend><%= NSW.Data.LabelText.Text("Login.Legend") %></legend>
                        <p>
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" CssClass="UserNameLabel" Text="x"></asp:Label>
                            <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                    CssClass="failureNotification" ErrorMessage="User Name is required."
                                    ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="PasswordLabel" Text="x"></asp:Label>
                            <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                    CssClass="failureNotification" ErrorMessage="Password is required."
                                    ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                            <p class="ForgotPasswordLink">
                                <asp:LinkButton ID="ForgotPasswordLink" runat="server" Text="x" OnClick="ForgotPasswordLink_Click"></asp:LinkButton>
                            </p>
                       </p>
                        <p>
                            <asp:CheckBox ID="RememberMe" runat="server"/>
                            <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline" Text="x"></asp:Label>
                        </p>
                    </fieldset>
                </div>
            </asp:WizardStep>
        </WizardSteps>
        <FinishNavigationTemplate>
            <p class="LoginButton">
                <asp:Button ID="LoginButton" runat="server" CommandName="MoveComplete"
                    ValidationGroup="LoginUserValidationGroup" onclick="LoginButton_Click"/>
            </p>
        </FinishNavigationTemplate>
    </asp:Wizard>
</asp:Content>
