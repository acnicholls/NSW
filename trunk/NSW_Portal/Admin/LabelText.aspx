<%@ Page Title="Mikechi.com" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LabelText.aspx.cs" Inherits="NSW.Admin.LabelText" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Wizard ID="LabelTextWizard" runat="server" DisplaySideBar="false"
        OnFinishButtonClick="FinishClick">
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>    
            <asp:WizardStep ID="LabelTextStep" runat="server" Title="Label Language Text Entry">
                    <h2>
                       <asp:Label ID="LabelTextTitle" runat="server" Text="Label Text Information" />
                    </h2>
                    <p>
                       <asp:Label ID="LabelTextInstructions" runat="server" Text="Use the form below to edit label text." />
                    </p>
                    <span class="failureNotification">
                        <asp:Literal ID="LabelTextErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="LabelTextValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="LabelTextValidationGroup"/>

                    <div class="labelInfo">
                        <fieldset class="labelText">
                            <legend><%= NSW.Data.LabelText.Text("LabelText.Legend") %></legend>
                            <p>
                                <asp:Label ID="lbl_ddlLabelID" runat="server" AssociatedControlID="ddlLabelID" CssClass="lbl_ddlLabelID"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlLabelID" runat="server" 
                                    CssClass="ddlLabelID" Height="16px" Width="203px" OnSelectedIndexChanged="ddlLabelID_SelectedIndexChanged" 
                                    ValidationGroup="LabelTextValidationGroup" AutoPostBack="True" TabIndex="0"></asp:DropDownList>
                            </p>
                            <p>                 
                                <asp:Label ID="lbl_txtEnglish" runat="server" AssociatedControlID="txtEnglish" CssClass="lbl_txtEnglish"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtEnglish" runat="server" TabIndex="1" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EnglishRequired" runat="server" ControlToValidate="txtEnglish" 
                                     CssClass="failureNotification" ErrorMessage="English text is required."
                                     ValidationGroup="LabelTextValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="lbl_txtJapanese" runat="server" 
                                    CssClass="lbl_txtJapanese" AssociatedControlID="txtJapanese"></asp:Label>
                                    <br />
                                <asp:TextBox ID="txtJapanese" runat="server" 
                                    TabIndex="2" TextMode="MultiLine"></asp:TextBox>
                            </p>

                    </fieldset>
                </div>
            </asp:WizardStep>
        </WizardSteps>
        <FinishNavigationTemplate>
            <div class="submitButton">
                <asp:Button ID="FinishButton" runat="server" ValidationGroup="LabelTextValidationGroup" CommandName="MoveComplete" TabIndex="3" />
            </div>
        </FinishNavigationTemplate>
    </asp:Wizard>
</asp:Content>
