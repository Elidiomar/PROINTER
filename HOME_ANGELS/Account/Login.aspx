<%@ Page Title="Logon" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Account_Login" %>
<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
    </section>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
    </hgroup>
    
    <section id="loginForm">
        <h2>Use uma conta local para fazer o logon.</h2>
        <asp:Login runat="server" ViewStateMode="Disabled" RenderOuterTable="false">
            <LayoutTemplate>
                <p class="validation-summary-errors">
                    <asp:Literal runat="server" ID="FailureText" />
                </p>
                <fieldset>
                    <legend>Formulário de logon</legend>
                    <ol>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="UserName">Nome de usuário</asp:Label>
                            <asp:TextBox runat="server" ID="UserName" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="O campo nome de usuário é obrigatório." />
                        </li>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="Password">Senha</asp:Label>
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="O campo de senha é obrigatório." />
                        </li>
                        <li>
                            <asp:CheckBox runat="server" ID="RememberMe" />
                            <asp:Label runat="server" AssociatedControlID="RememberMe" CssClass="checkbox">Lembrar-me?</asp:Label>
                        </li>
                    </ol>
                    <%-- ENVIA DADOS E RETORNA PARA PAGINA DE REQUISICAO --%>
                    <asp:Button runat="server" CommandName="Login" Text="Logon" />
                </fieldset>
            </LayoutTemplate>
        </asp:Login>
        <p>
            <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Registre-se</asp:HyperLink>
            caso não possua uma conta.
        </p>
    </section>

    <section id="socialLoginForm">
        <h2>Seja bem vindo a <b>Home Angels</b>!</h2>
        <div style="display:none">
            <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin"  />
        </div>
    </section>
</asp:Content>