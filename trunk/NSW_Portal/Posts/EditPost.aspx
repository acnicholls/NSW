<%@ Page Title="Mikechi.com" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditPost.aspx.cs" Inherits="NSW.Posts.EditPost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Wizard ID="EditPostWizard" runat="server" DisplaySideBar="false" OnFinishButtonClick="FinishClick">
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>    
            <asp:WizardStep ID="EditPostWizardStep0" runat="server">
                <h2>
                    <asp:Label ID="EditPostPageTitle" runat="server" />
                </h2>
                <p>
                    <asp:Label ID="EditPostInstructions" runat="server" />
                </p>
                <span class="failureNotification">
                    <asp:Literal ID="EditPostErrorMessage" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="EditPostValidationSummary" runat="server" CssClass="failureNotification" 
                        ValidationGroup="PostValidationGroup"/>
                <div class="labelInfo">
                    <fieldset class="labelText">
                        <legend><%= NSW.Data.LabelText.Text("EditPost.Legend") %></legend>
                        <p>
                                    <asp:Label ID="PostStatusLabel" runat="server" AssociatedControlID="PostStatus" />
                                    <br />
                                    <asp:DropDownList ID="PostStatus" runat="server" TabIndex="0" />
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
                    </fieldset>
                </div>
            </asp:WizardStep>
        </WizardSteps>
        <FinishNavigationTemplate>
            <div class="submitButton">
                <asp:Button ID="FinishButton" runat="server" ValidationGroup="PostValidationGroup" CommandName="MoveComplete" TabIndex="4" />
            </div>
        </FinishNavigationTemplate>
    </asp:Wizard>
</asp:Content>
