<%@ Page Title="Requisição" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Requisicao.aspx.cs" Inherits="Requisicao" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured"></section>
</asp:Content>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

        <div class="div-center"><h1>Nova requisição </h1></div>

    <%if (Cuidador)
        { %>

        <div class="div-center"><p>Cuidador</p></div>

            <asp:Label runat="server" AssociatedControlID="DropDownUserCuidador">Nome do Cuidador</asp:Label>
            <asp:DropDownList ID="DropDownUserCuidador" runat="server"></asp:DropDownList>

    <%} %>
    <%else
        { %>
    <div class="div-center"><p>Cliente</p></div>

            <div class="div-center">
            <fieldset>
                <legend>Formulário de requisição cliente</legend>
                <div class="menu">
                    <ol>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="UserName">Nome de Cuidador</asp:Label>
                            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="Telefone">Telefone</asp:Label>
                            <asp:TextBox ID="Telefone" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="Email">Email</asp:Label>
                            <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                        </li>
                    </ol>
                </div>
                <div class="menu">
                    <ol>                        
                        <li>
                            <asp:Label runat="server" AssociatedControlID="Estado">Estado</asp:Label>
                            <asp:TextBox runat="server" ID="Estado" />
                        </li>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="Cidade">Cidade</asp:Label>
                            <asp:TextBox runat="server" ID="Cidade" />
                        </li>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="DataInicial">Data Inicial</asp:Label>                            
                            <asp:Calendar ID="DataInicial" runat="server"></asp:Calendar>
                        </li>
                    </ol>
                </div>
                
                <div class="menu">
                    <ol>                        
                          <li>
                            <asp:Label runat="server" AssociatedControlID="DataFinal">Data Final</asp:Label>
                              <asp:Calendar ID="DataFinal" runat="server"></asp:Calendar>                            
                        </li>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="HoraInicial">Hora Inicial</asp:Label>
                            <asp:TextBox runat="server" ID="HoraInicial" />

                        </li>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="HoraFinal">HoraFinal</asp:Label>
                            <asp:TextBox runat="server" ID="HoraFinal" />
                        </li>
                    </ol>
                </div>
                <div class="menu">
                    <ol>                        
                          <li>
                            <asp:Label runat="server" AssociatedControlID="Status">Status</asp:Label>
                            <asp:TextBox runat="server" ID="Status" />
                        </li>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="DropDownUsers">Nome do Cuidador</asp:Label>
                            <asp:DropDownList ID="DropDownUsers" runat="server" OnSelectedIndexChanged ="DropDownUsers_SelectedIndexChanged"></asp:DropDownList>
                        </li>                                                      

                        <li>
                            <asp:Label runat="server" AssociatedControlID="ContatoCuidador">Contato do Cuidador</asp:Label>
                            <asp:TextBox runat="server" ID="ContatoCuidador" />
                        </li>
                    </ol>
                </div>
            </fieldset>
        </div>

        <div class="div-center">
            <br />
            <asp:Button Text="Salvar" runat="server" OnClick="Salvar" />
        </div>

        <hr />
        <div class="div-center">

            <asp:GridView ID="GridViewCuidador" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnRowDataBound="OnRowDataBound" OnPageIndexChanging="OnPageIndexChanging">
                <Columns>                    
                    <asp:BoundField HeaderStyle-Width="250px" DataField="Nome" HeaderText="Nome do Cuidador"
                        ItemStyle-CssClass="Nome" />
                    <asp:BoundField HeaderStyle-Width="150px" DataField="Status" HeaderText="Status" />
                    <asp:BoundField HeaderStyle-Width="200px" DataField="DataInicial" HeaderText="DataInicial" />
                    <asp:BoundField HeaderStyle-Width="200px" DataField="DataFinal" HeaderText="DataFinal" />                     

                </Columns>
            </asp:GridView>
        </div>

    <%} %>
</asp:Content>
