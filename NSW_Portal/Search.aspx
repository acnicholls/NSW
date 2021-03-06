﻿<%@ Page Title="Mikechi.com" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Search.aspx.cs" Inherits="NSW.Search" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Wizard ID="SearchWizard" runat="server" DisplaySideBar="false"
        OnFinishButtonClick="FinishClick">
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>    
            <asp:WizardStep ID="SearchWizardStep0" runat="server">
                <h2>
                    <asp:Label ID="SearchPageTitle" runat="server" />
                </h2>
                <p>
                    <asp:Label ID="SearchInstructions" runat="server" />
                </p>
                <span class="failureNotification">
                    <asp:Literal ID="SearchErrorMessage" runat="server"></asp:Literal>
                </span>
                <div class="labelInfo">
                    <fieldset class="labelText">
                        <legend><%= NSW.Data.LabelText.Text("Search.Legend") %></legend>
                        <p>
                            <asp:Label ID="SearchCategoryPickerLabel" runat="server" AssociatedControlID="SearchCategoryPicker" />
                            <br />
                            <asp:Label ID="SearchCategoryPickerLabel1" runat="server" AssociatedControlID="SearchCategoryPicker" />
                            <br />
                            <asp:ListBox ID="SearchCategoryPicker" runat="server" SelectionMode="Multiple" AutoPostBack="false" TabIndex="0" CssClass="dataEntry" ></asp:ListBox>
                        </p>
                        <p>
                            <asp:Label ID="SearchTermLabel" runat="server" AssociatedControlID="SearchTerm"></asp:Label> 
                            <br />
                            <asp:TextBox ID="SearchTerm" runat="server" MaxLength="50" CssClass="lgTextEntry" TabIndex="1"></asp:TextBox>
                        </p>
                    </fieldset>
                </div>
            </asp:WizardStep>
        </WizardSteps>
        <FinishNavigationTemplate>
            <div class="submitButton">
                <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" TabIndex="3" />
            </div>
        </FinishNavigationTemplate>
    </asp:Wizard>
</asp:Content>
