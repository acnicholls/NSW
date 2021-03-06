﻿<%@ Page Title="Mikechi.com" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="NSW.Admin.Categories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Wizard ID="CategoryWizard" runat="server" DisplaySideBar="false"
        OnFinishButtonClick="FinishClick">
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>    
            <asp:WizardStep ID="CategoryWizardStep0" runat="server">
                <h2>
                    <asp:Label ID="CategoryPageTitle" runat="server" />
                </h2>
                <p>
                    <asp:Label ID="CategoryInstructions" runat="server" />
                </p>
                <p>
                    <asp:Label ID="CategoryInstructions2" runat="server" />
                </p>
                <span class="failureNotification">
                    <asp:Literal ID="CategoryErrorMessage" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="CategoryValidationSummary" runat="server" CssClass="failureNotification" 
                        ValidationGroup="CategoryValidationGroup"/>
                <div class="labelInfo">
                    <fieldset class="labelText">
                        <legend><%= NSW.Data.LabelText.Text("Category.Title") %></legend>
                        <p>
                            <asp:Label ID="CategoryPickerLabel" runat="server" AssociatedControlID="CategoryPicker" />
                            <br />
                            <asp:DropDownList ID="CategoryPicker" runat="server" OnSelectedIndexChanged="CategoryPicker_SelectedIndexChanged" AutoPostBack="true" TabIndex="0" />
                        </p>
                        <h4>
                            <asp:Label ID="CategoryTitle" runat="server" />                           
                        </h4>
                        <p>
                            <asp:Label ID="CategoryEnglishLabel" runat="server" AssociatedControlID="CategoryEnglish"></asp:Label>
                            <br />
                            <asp:TextBox ID="CategoryEnglish" runat="server" MaxLength="50" CssClass="textEntry" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CategoryEnglishRequired" runat="server" ControlToValidate="CategoryEnglish" 
                                    CssClass="failureNotification" 
                                    ValidationGroup="CategoryValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="CategoryJapaneseLabel" runat="server" AssociatedControlID="CategoryJapanese"></asp:Label>
                            <br />
                            <asp:TextBox ID="CategoryJapanese" runat="server" MaxLength="50" TabIndex="2" CssClass="textEntry"></asp:TextBox>
                        </p>
                        <h4>
                            <asp:Label ID="CategoryDescription" runat="server" />
                        </h4>
                        <p>                 
                            <asp:Label ID="CategoryEnglishDescriptionLabel" runat="server" AssociatedControlID="CategoryEnglishDescription"></asp:Label>
                            <br />
                            <asp:TextBox ID="CategoryEnglishDescription" runat="server" TabIndex="3" TextMode="MultiLine"></asp:TextBox>
                        </p>
                        <p>
                            <asp:Label ID="CategoryJapaneseDescriptionLabel" runat="server" 
                                AssociatedControlID="CategoryJapaneseDescription"></asp:Label>
                                <br />
                            <asp:TextBox ID="CategoryJapaneseDescription" runat="server" 
                                TabIndex="4" TextMode="MultiLine"></asp:TextBox>
                        </p>
                    </fieldset>
                </div>
            </asp:WizardStep>
        </WizardSteps>
        <FinishNavigationTemplate>
            <div class="submitButton">
                <asp:Button ID="FinishButton" runat="server" ValidationGroup="CategoryValidationGroup" CommandName="MoveComplete" TabIndex="5" />
            </div>
        </FinishNavigationTemplate>
    </asp:Wizard>
</asp:Content>
