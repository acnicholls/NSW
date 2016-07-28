<%@ Page Title="Mikechi.com" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="NSW.Account.Register" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Wizard ID="RegisterUser" runat="server" DisplaySideBar="False" 
        OnNextButtonClick="NextClick" OnPreviousButtonClick="PreviousClick" 
        OnFinishButtonClick="FinishClick"> 
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>
            <asp:WizardStep ID="RegisterUserWizardStep0" runat="server" 
                Title="Security Information">
                    <h2>
                        <asp:Label ID="PageTitle" runat="server"  />
                    </h2>
                    <p>
                        <asp:Label ID="PageInstructions1" runat="server"  />
                    </p>
                    <p>
                       <asp:Label ID="PageInstructions2" runat="server"  />
                    </p>
                    <p>
                       <asp:Label ID="PageInstructions3" runat="server"  />
                    </p>                    <span class="failureNotification">
                        <asp:Literal ID="Step0ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="Step0RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="RegisterUserValidationGroup"/>
                    <div class="accountInfo">
                        <fieldset class="register">
                            <legend><%= NSW.Data.LabelText.Text("Register.Step0Legend") %></legend>
                            <p>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName"></asp:Label>
                                <asp:TextBox ID="UserName" runat="server" CssClass="textEntry" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                     CssClass="failureNotification" 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email"></asp:Label>
                                <asp:TextBox ID="Email" runat="server" CssClass="textEntry" MaxLength="50"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="IsEmail" runat="server" ControlToValidate="Email" CssClass="failureNotification" 
                                     ValidationGroup="RegisterUserValidationGroup" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                                     CssClass="failureNotification" 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password"></asp:Label>
                                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                     CssClass="failureNotification"  
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword"></asp:Label>
                                <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic" 
                                      ID="ConfirmPasswordRequired" runat="server" 
                                      ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                     CssClass="failureNotification" Display="Dynamic" 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                            </p>
                        </fieldset>
                    </div>
            </asp:WizardStep>
            <asp:WizardStep ID="RegisterUserWizardStep1" runat="server" 
                Title="User Information">
                    <h2>
                        <asp:Label ID="RegisterStep1Instructions" runat="server" Text="Please provide your phone number and postal code." />
                    </h2>
                    <span class="failureNotification">
                        <asp:Literal ID="Step1ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="Step1RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="RegisterUserValidationGroup"/>
                    <div class="accountInfo">
                        <fieldset class="register">
                            <legend><%= NSW.Data.LabelText.Text("Register.Step1Legend") %></legend>
                            <p>
                                <asp:Label ID="PhoneLabel" runat="server" AssociatedControlID="PhoneNumber"></asp:Label>
                                <asp:TextBox ID="PhoneNumber" runat="server" CssClass="textEntry" MaxLength="15"></asp:TextBox>
                            </p>
                            <p>
                                <asp:Label ID="PostalCodeLabel" runat="server" AssociatedControlID="PostalCode"></asp:Label>
                                <asp:DropDownList ID="PostalCode" runat="server" CausesValidation="false" CssClass="textEntry">
                                    <asp:ListItem>Select One...</asp:ListItem>
                                </asp:DropDownList>
                            </p>
                            <fieldset class="LangCheck">
                                <asp:Label ID="LanguagePreferenceLabel" runat="server" /> 
                                <br />
                                <asp:RadioButton ID="EnglishPrefRadio" runat="server" CausesValidation="false" TextAlign="Right" GroupName="Language" ValidationGroup="RegisterUserValidationGroup" />
                                <asp:RadioButton ID="JapanesePrefRadio" runat="server" CausesValidation="false" TextAlign="Right" GroupName="Language" ValidationGroup="RegisterUserValidationGroup" />
                            </fieldset>
                            <p>
                                <cc1:CaptchaControl ID="Captcha1" runat="server" 
                                CaptchaBackgroundNoise="Low" CaptchaLength="5" 
                                CaptchaHeight="60" CaptchaWidth="200" 
                                CaptchaLineNoise="Low" CaptchaMinTimeout="5"
                                CaptchaMaxTimeout="240" FontColor = "#529E00" />
                                <asp:TextBox ID="txtCaptcha" runat="server" MaxLength="10" CssClass="TextEntry" TabIndex="8"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ContactCaptchaRequired" runat="server" ControlToValidate="txtCaptcha" 
                                        CssClass="failureNotification" 
                                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>

                        </fieldset>
                    </div>
            </asp:WizardStep>
        </WizardSteps>
        <StartNavigationTemplate>
            <asp:Button ID="StartNextButton" runat="server" ValidationGroup="RegisterUserValidationGroup" CommandName="MoveNext" />
        </StartNavigationTemplate>
        <StepNavigationTemplate>
            <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious" Text="Previous" />
            <asp:Button ID="StepNextButton" runat="server" ValidationGroup="RegisterUserValidationGroup" CommandName="MoveNext" Text="Next" />
        </StepNavigationTemplate>
        <FinishNavigationTemplate>
            <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious" />
            <asp:Button ID="FinishButton" runat="server" ValidationGroup="RegisterUserValidationGroup" CommandName="MoveComplete" />
        </FinishNavigationTemplate>
    </asp:Wizard>
</asp:Content>
