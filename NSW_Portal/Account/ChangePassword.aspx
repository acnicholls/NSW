﻿<%@ Page Title="Mikechi.com" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="NSW.Account.ChangePassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Wizard ID="ChangePasswordWizard" runat="server" DisplaySideBar="false" DisplayCancelButton="true" OnFinishButtonClick="FinishClick" >
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>
            <asp:WizardStep ID="ProfileWizardStep0" runat="server" >
                <h2>
                    <asp:Label ID="PageTitle" runat="server" />
                </h2>
                <p>
                    <asp:Label ID="Instructions" runat="server" />
                </p>
                <p>
                    <asp:Label ID="Instructions2" runat="server" />
                </p>
                <span class="failureNotification">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification" 
                     ValidationGroup="ChangeUserPasswordValidationGroup"/>
                <div class="accountInfo">
                    <fieldset class="changePassword">
                        <legend><%= NSW.Data.LabelText.Text("ChangePass.Legend") %></legend>
                        <p>
                            <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword"></asp:Label>
                            <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" 
                                 CssClass="failureNotification" 
                                 ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword"></asp:Label>
                            <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" 
                                 CssClass="failureNotification"
                                 ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword"></asp:Label>
                            <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" 
                                 CssClass="failureNotification" Display="Dynamic" 
                                 ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                                 CssClass="failureNotification" Display="Dynamic"
                                 ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                        </p>
                    </fieldset>
                </div>
            </asp:WizardStep> 
        </WizardSteps>
        <FinishNavigationTemplate>
                    <p class="LoginButton">
                        <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="MoveComplete" 
                             ValidationGroup="ChangeUserPasswordValidationGroup"/>
                    </p>
        </FinishNavigationTemplate>
    </asp:Wizard>
</asp:Content>
