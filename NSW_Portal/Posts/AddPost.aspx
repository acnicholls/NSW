﻿<%@ Page Title="Mikechi.com" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddPost.aspx.cs" Inherits="NSW.Posts.AddPost" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Wizard ID="AddPostWizard" runat="server" DisplaySideBar="false"
        OnFinishButtonClick="FinishClick">
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>    
            <asp:WizardStep ID="AddPostWizardStep0" runat="server">
                <h2>
                    <asp:Label ID="AddPostPageTitle" runat="server" />
                </h2>
                <p>
                    <asp:Label ID="AddPostInstructions" runat="server" />
                </p>
                <span class="failureNotification">
                    <asp:Literal ID="AddPostErrorMessage" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="AddPostValidationSummary" runat="server" CssClass="failureNotification" 
                        ValidationGroup="PostValidationGroup"/>
                <div class="labelInfo">
                    <fieldset class="labelText">
                        <legend><%= NSW.Data.LabelText.Text("AddPost.Legend") %></legend>
                        <p>
                            <asp:Label ID="PostCategoryPickerLabel" runat="server" AssociatedControlID="PostCategoryPicker" />
                            <br />
                            <asp:DropDownList ID="PostCategoryPicker" runat="server" TabIndex="0" />
                            <asp:RequiredFieldValidator ID="PostCategoryRequired" runat="server" ControlToValidate="PostCategoryPicker" 
                                    CssClass="failureNotification" 
                                    ValidationGroup="PostValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="PostTitleLabel" runat="server" AssociatedControlID="PostTitle"></asp:Label> 
                            <br />
                            <asp:TextBox ID="PostTitle" runat="server" MaxLength="50" CssClass="lgTextEntry" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PostTitleRequired" runat="server" ControlToValidate="PostTitle" 
                                    CssClass="failureNotification" 
                                    ValidationGroup="PostValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>                 
                            <asp:Label ID="PostDescriptionLabel" runat="server" AssociatedControlID="PostDescription"></asp:Label>
                            <br />
                            <asp:TextBox ID="PostDescription" runat="server" TabIndex="2" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PostDescriptionRequired" runat="server" ControlToValidate="PostDescription" 
                                    CssClass="failureNotification" 
                                    ValidationGroup="PostValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="PostPriceLabel" runat="server" AssociatedControlID="PostPrice"></asp:Label>
                            <br />
                            <asp:TextBox ID="PostPrice" runat="server" MaxLength="10" CssClass="TextEntry" TabIndex="3"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PostPriceRequired" runat="server" ControlToValidate="PostPrice" 
                                    CssClass="failureNotification" 
                                    ValidationGroup="PostValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <h4>
                            <asp:Label ID="PostPhotoTitle" runat="server" />
                        </h4>
                        <p>
                            <asp:FileUpload ID="PostPhoto1" runat="server" CssClass="photoEntry" TabIndex="4" />
                            <asp:RegularExpressionValidator ID="PostPhoto1FileContent" runat="server" ControlToValidate="PostPhoto1"
                            ValidationExpression="(.*\.([Gg][Ii][Ff])|.*\.([Jj][Pp][Gg])|.*\.([Bb][Mm][Pp])|.*\.([pP][nN][gG])|.*\.([tT][iI][fF][fF])$)"
                            ValidationGroup="PostValidationGroup" CssClass="failureNotification">
                            </asp:RegularExpressionValidator>
                                 <br />
                            <asp:FileUpload ID="PostPhoto2" runat="server" CssClass="photoEntry" TabIndex="5" />
                            <asp:RegularExpressionValidator ID="PostPhoto2FileContent" runat="server" ControlToValidate="PostPhoto2"
                            ValidationExpression="(.*\.([Gg][Ii][Ff])|.*\.([Jj][Pp][Gg])|.*\.([Bb][Mm][Pp])|.*\.([pP][nN][gG])|.*\.([tT][iI][fF][fF])$)"
                            ValidationGroup="PostValidationGroup" CssClass="failureNotification">
                            </asp:RegularExpressionValidator>
                                 <br />
                            <asp:FileUpload ID="PostPhoto3" runat="server" CssClass="photoEntry" TabIndex="6" />
                            <asp:RegularExpressionValidator ID="PostPhoto3FileContent" runat="server" ControlToValidate="PostPhoto3"
                            ValidationExpression="(.*\.([Gg][Ii][Ff])|.*\.([Jj][Pp][Gg])|.*\.([Bb][Mm][Pp])|.*\.([pP][nN][gG])|.*\.([tT][iI][fF][fF])$)"
                            ValidationGroup="PostValidationGroup" CssClass="failureNotification">
                            </asp:RegularExpressionValidator>
                                 <br />
                            <asp:FileUpload ID="PostPhoto4" runat="server" CssClass="photoEntry" TabIndex="7" />
                            <asp:RegularExpressionValidator ID="PostPhoto4FileContent" runat="server" ControlToValidate="PostPhoto4"
                            ValidationExpression="(.*\.([Gg][Ii][Ff])|.*\.([Jj][Pp][Gg])|.*\.([Bb][Mm][Pp])|.*\.([pP][nN][gG])|.*\.([tT][iI][fF][fF])$)"
                            ValidationGroup="PostValidationGroup" CssClass="failureNotification">
                            </asp:RegularExpressionValidator>
                        </p>
                        <p>
                    <cc1:CaptchaControl ID="Captcha1" runat="server" 
                    CaptchaBackgroundNoise="Low" CaptchaLength="5" 
                    CaptchaHeight="60" CaptchaWidth="200" 
                    CaptchaLineNoise="Low" CaptchaMinTimeout="5"
                    CaptchaMaxTimeout="240" FontColor = "#529E00" />
                            <asp:TextBox ID="txtCaptcha" runat="server" MaxLength="10" CssClass="TextEntry" TabIndex="8"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ContactCaptchaRequired" runat="server" ControlToValidate="txtCaptcha" 
                                    CssClass="failureNotification" 
                                    ValidationGroup="PostValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                    </fieldset>
                </div>
            </asp:WizardStep>
        </WizardSteps>
        <FinishNavigationTemplate>
            <div class="submitButton">
                <asp:Button ID="FinishButton" runat="server" ValidationGroup="PostValidationGroup" CommandName="MoveComplete" TabIndex="9" />
            </div>
        </FinishNavigationTemplate>
    </asp:Wizard>
</asp:Content>
