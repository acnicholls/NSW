﻿<%@ Page Title="Mikechi.com" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactPostUser.aspx.cs" Inherits="NSW.Posts.ContactPostUser" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <asp:Wizard ID="ContactUserWizard" runat="server" DisplaySideBar="false"
        OnFinishButtonClick="FinishClick" TabIndex="99">
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>    
            <asp:WizardStep ID="ContactUserWizardStep0" runat="server">
                <h2>
                    <asp:Label ID="ContactUserTitle" runat="server" />
                </h2>
                <p>
                    <asp:Label ID="ContactUserInstructions" runat="server" />
                </p>
                <span class="failureNotification">
                    <asp:Literal ID="ContactUserErrorMessage" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="ContactValidationSummary" runat="server" CssClass="failureNotification" 
                        ValidationGroup="ContactValidationGroup"/>
                <div class="labelInfo">
                    <fieldset class="labelText">
                        <legend><%= NSW.Data.LabelText.Text("ContactUser.Legend") %></legend>
                        <p>
                            <asp:Label ID="ContactUserEmailLabel" runat="server" AssociatedControlID="ContactEmail"></asp:Label> 
                            <br />
                            <asp:TextBox ID="ContactEmail" runat="server" MaxLength="50" CssClass="lgTextEntry" TabIndex="1"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="IsEmail" runat="server" ControlToValidate="ContactEmail" CssClass="failureNotification" 
                                     ValidationGroup="ContactValidationGroup" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                               <asp:RequiredFieldValidator ID="ContactEmailRequired" runat="server" ControlToValidate="ContactEmail" 
                                    CssClass="failureNotification" 
                                    ValidationGroup="ContactValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>                 
                            <asp:Label ID="ContactBodyLabel" runat="server" AssociatedControlID="ContactBody"></asp:Label>
                            <br />
                            <asp:TextBox ID="ContactBody" runat="server" TabIndex="2" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ContactBodyRequired" runat="server" ControlToValidate="ContactBody" 
                                    CssClass="failureNotification" 
                                    ValidationGroup="ContactValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                    <cc1:CaptchaControl ID="Captcha1" runat="server" 
                    CaptchaBackgroundNoise="Low" CaptchaLength="5" 
                    CaptchaHeight="60" CaptchaWidth="200" 
                    CaptchaLineNoise="Low" CaptchaMinTimeout="5"
                    CaptchaMaxTimeout="240" FontColor = "#529E00" />
                            <asp:TextBox ID="ContactCaptcha" runat="server" MaxLength="10" CssClass="TextEntry" TabIndex="3"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ContactCaptchaRequired" runat="server" ControlToValidate="ContactCaptcha" 
                                    CssClass="failureNotification" 
                                    ValidationGroup="ContactValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                    </fieldset>
                </div>
            </asp:WizardStep>
        </WizardSteps>
        <FinishNavigationTemplate>
            <div class="submitButton">
                <asp:Button ID="FinishButton" runat="server" ValidationGroup="ContactValidationGroup" CommandName="MoveComplete" TabIndex="4" />
            </div>
        </FinishNavigationTemplate>
    </asp:Wizard>

</asp:Content>
