<%@ Page Title="Mikechi.com" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True"
    CodeBehind="Profile.aspx.cs" Inherits="NSW.Account.Profile" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Wizard ID="ProfileWizard" runat="server" DisplaySideBar="False" 
        OnFinishButtonClick="FinishClick"> 
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>
            <asp:WizardStep ID="ProfileWizardStep0" runat="server" 
                Title="Security Information">
                    <h2>
                       <asp:Label ID="ProfileTitle" runat="server" Text="User Profile Information" />
                    </h2>
                    <p>
                       <asp:Label ID="ProfileInstructions" runat="server" Text="Use the form below to edit your account information." />
                    </p>
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="ProfileValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="ProfileValidationGroup"/>
                    <div class="accountInfo">
                        <fieldset class="register">
                            <legend><%= NSW.Data.LabelText.Text("Profile.Legend") %></legend>
                            <p>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName"></asp:Label>
                                <asp:TextBox ID="UserName" runat="server" CssClass="textEntry" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                     CssClass="failureNotification" 
                                     ValidationGroup="ProfileValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email"></asp:Label>
                                <asp:TextBox ID="Email" runat="server" CssClass="textEntry" MaxLength="50"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="IsEmail" runat="server" ControlToValidate="Email" CssClass="failureNotification" 
                                     ValidationGroup="ProfileValidationGroup" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                                     CssClass="failureNotification" 
                                     ValidationGroup="ProfileValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="PhoneLabel" runat="server" AssociatedControlID="PhoneNumber"></asp:Label>
                                <asp:TextBox ID="PhoneNumber" runat="server" CssClass="textEntry" MaxLength="15"></asp:TextBox>
                            </p>
                            <p>
                                <asp:Label ID="PostalCodeLabel" runat="server" AssociatedControlID="PostalCode"></asp:Label>
                                <asp:DropDownList ID="PostalCode" runat="server" CausesValidation="false" CssClass="textEntry">
                                    <asp:ListItem>Select One...</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="PostalCodeRequired" runat="server" ControlToValidate="PostalCode" 
                                     CssClass="failureNotification" 
                                     ValidationGroup="ProfileValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <fieldset class="LangCheck">
                                <asp:Label ID="LanguagePreferenceLabel" runat="server" /> 
                                <br />
                                <asp:RadioButton ID="EnglishPrefRadio" runat="server" CausesValidation="false" TextAlign="Right" GroupName="Language" />
                                <asp:RadioButton ID="JapanesePrefRadio" runat="server" CausesValidation="false" TextAlign="Right" GroupName="Language" />
                            </fieldset>
                        </fieldset>
                    </div>
            </asp:WizardStep>
        </WizardSteps>
        <FinishNavigationTemplate>
            <div class="ExecuteButton">
                <asp:Button ID="FinishButton" runat="server" ValidationGroup="ProfileValidationGroup" CommandName="MoveComplete" />
            </div>
        </FinishNavigationTemplate>
    </asp:Wizard>
</asp:Content>
