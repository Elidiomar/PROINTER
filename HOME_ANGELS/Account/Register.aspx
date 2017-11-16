<%@ Page Title="Registre-se" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
    </section>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
     <div class="div-center">          
                <h1><%: Title %></h1>
          
     </div>         

    <asp:CreateUserWizard runat="server" ID="RegisterUser" ViewStateMode="Disabled" OnCreatedUser="RegisterUser_CreatedUser">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="wizardStepPlaceholder" />
            <asp:PlaceHolder runat="server" ID="navigationPlaceholder" />
        </LayoutTemplate>

        <WizardSteps>
            <asp:CreateUserWizardStep runat="server" ID="RegisterUserWizardStep">
                <ContentTemplate>


                    <p class="message-info div-center">
                        As novas senhas devem ter, no mínimo, <%: Membership.MinRequiredPasswordLength %> caracteres.
                    </p>

                               <p>
                                                <br />
                                <asp:Label runat="server">Tipo de registro:</asp:Label>
                                <asp:DropDownList ID="ListRoles" runat="server" >
                                </asp:DropDownList>
                            </p>

                    <p class="validation-summary-errors">
                        <asp:Literal runat="server" ID="ErrorMessage" />
                    </p>

                    <fieldset>
                        <legend>Formulário de registro</legend>
                        <ol>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="UserName">Nome de usuário</asp:Label>                                
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                    CssClass="field-validation-error" ErrorMessage="O campo nome de usuário é obrigatório." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="CPF">CPF</asp:Label>
                                <asp:TextBox runat="server" ID="CPF" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="CPF"
                                    CssClass="field-validation-error" ErrorMessage="O campo CPF é obrigatório." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="Email">Endereço de email</asp:Label>
                                <asp:TextBox runat="server" ID="Email" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                    CssClass="field-validation-error" ErrorMessage="O campo endereço de email é obrigatório." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="Phone">Telefone</asp:Label>
                                <asp:TextBox runat="server" AutoCompleteType="HomePhone"  ID="Phone"/>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Phone"
                                    CssClass="field-validation-error" ErrorMessage="O campo telefone de email é obrigatório." />
                            </li>

                            <li>
                                <asp:Label runat="server" AssociatedControlID="City">Cidade</asp:Label>
                                <asp:TextBox runat="server" ID="City" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="City"
                                    CssClass="field-validation-error" ErrorMessage="O campo cidade é obrigatório." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="State">Estado</asp:Label>
                                <asp:TextBox runat="server" ID="State" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="State"
                                    CssClass="field-validation-error" ErrorMessage="O campo estado é obrigatório." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="Address">Endereço</asp:Label>
                                <asp:TextBox runat="server" ID="Address" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Address"
                                    CssClass="field-validation-error" ErrorMessage="O endereço estado é obrigatório." />
                            </li>


                            <li>
                                <asp:Label runat="server" AssociatedControlID="Password">Senha</asp:Label>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                                    CssClass="field-validation-error" ErrorMessage="O campo de senha é obrigatório." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="ConfirmPassword">Confirmar senha</asp:Label>
                                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="O campo para confirmar senha é obrigatório." />
                                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="A senha e a senha de confirmação não coincidem." />
                            </li>
                        </ol>
                        <asp:Button runat="server" CommandName="MoveNext" Text="Criar Registro" />
                    </fieldset>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
<asp:CompleteWizardStep runat="server"></asp:CompleteWizardStep>
        </WizardSteps>
        <StartNavigationTemplate>
            <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="Próxima" />
        </StartNavigationTemplate>
    </asp:CreateUserWizard>
</asp:Content>