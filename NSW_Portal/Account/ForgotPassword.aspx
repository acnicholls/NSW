﻿<%@ Page Title="Mikechi.com" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="NSW.Account.ForgotPassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Wizard ID="ForgotPasswordWizard" runat="server" DisplaySideBar="false" DisplayCancelButton="true" OnFinishButtonClick="FinishClick" >
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>
            <asp:WizardStep ID="ForgotPassWizardStep0" runat="server" >
                <h2>
                    <asp:Label ID="PageTitle" runat="server" />
                </h2>
                <p>
                    <asp:Label ID="Instructions" runat="server" />
                </p>
                <span class="failureNotification">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="ForgotPasswordValidationSummary" runat="server" CssClass="failureNotification" 
                     ValidationGroup="ForgotPasswordValidationGroup"/>
                <div class="accountInfo">
                    <fieldset class="ForgotPassword">
                        <legend><%= NSW.Data.LabelText.Text("ForgotPass.Legend") %></legend>
                        <p>
                            <asp:Label ID="ForgotPasswordLabel" runat="server" AssociatedControlID="ForgotPasswordUser"></asp:Label>
                            <asp:TextBox ID="ForgotPasswordUser" runat="server" CssClass="dataEntry"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ForgotPasswordRequired" runat="server" ControlToValidate="ForgotPasswordUser" 
                                 CssClass="failureNotification" 
                                 ValidationGroup="ForgotPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                                            </fieldset>
                </div>
            </asp:WizardStep> 
        </WizardSteps>
        <FinishNavigationTemplate>
                    <p class="LoginButton">
                        <asp:Button ID="ForgotPasswordPushButton" runat="server" CommandName="MoveComplete" 
                             ValidationGroup="ForgotPasswordValidationGroup"/>
                    </p>
        </FinishNavigationTemplate>
    </asp:Wizard>
</asp:Content>

