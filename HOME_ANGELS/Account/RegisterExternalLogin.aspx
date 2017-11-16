<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RegisterExternalLogin.aspx.cs" Inherits="Account_RegisterExternalLogin" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
    </section>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1>Registrar com sua conta <%: ProviderDisplayName %></h1>
        <h2><%: ProviderUserName %>.</h2>
    </hgroup>

    
    <asp:Label runat="server" ID="providerMessage" CssClass="field-validation-error" />
    

    <asp:PlaceHolder runat="server" ID="userNameForm">
        <fieldset>
            <legend>Formulário de associação</legend>
            <p>
                Você autenticou com <strong><%: ProviderDisplayName %></strong> como
                <strong><%: ProviderUserName %></strong>. Insira um nome de usuário abaixo do site atual
                e clique no botão Logon.
            </p>
            <ol>
                <li class="email">
                    <asp:Label runat="server" AssociatedControlID="userName">Nome de usuário</asp:Label>
                    <asp:TextBox runat="server" ID="userName" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="userName"
                        Display="Dynamic" ErrorMessage="O nome de usuário é obrigatório" ValidationGroup="NewUser" />
                    
                    <asp:Label runat="server" ID="userNameMessage" CssClass="field-validation-error" />
                    
                </li>
            </ol>
            <asp:Button runat="server" Text="Logon" ValidationGroup="NewUser" OnClick="logIn_Click" />
            <asp:Button runat="server" Text="Cancelar" CausesValidation="false" OnClick="cancel_Click" />
        </fieldset>
    </asp:PlaceHolder>
</asp:Content>
