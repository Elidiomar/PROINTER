<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <%--<div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>                 
                <h2><br />Modify this template to jump-start your ASP.NET application.</h2>
            </hgroup>            
        </div>--%>
    </section>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Sobre nós:</h3>
    <p>A <b>Home Angels</b> é uma empresa especializada em oferecer cuidado humanizado com respeito às necessidades físicas, emocionais e culturais de seus clientes preservando a rotina familiar e estimulando a independência de seus assistidos. Marco imperador, Sócio-Diretor, criou a empresa com o objetivo de promover o encontro de cuidadores e pessoas com a necessidade de um cuidador.</p>

</asp:Content>