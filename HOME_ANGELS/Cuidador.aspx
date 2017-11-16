<%@ Page Title="Cuidador" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Cuidador.aspx.cs" Inherits="Cuidador" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">

    <section class="featured">
    </section>
</asp:Content>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
     <div class="div-center">          
                <h1><%: Title %>es</h1>          
                
        </div>    
    
    <div class="div-center">          
    <fieldset>
                        <legend>Formulário de pesquisa</legend>
        <div class="menu">
                        <ol>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="UserName">Nome de Cuidador</asp:Label>                                
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>                             
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="Cidade">Cidade</asp:Label>                                
                                <asp:TextBox ID="Cidade" runat="server"></asp:TextBox>                                
                            </li>
                            </ol>                        
            </div> 
        <div class="menu">
                        <ol>                            
                            <li>
                                <asp:Label runat="server" AssociatedControlID="NumeroRegistro">Numero do Registro</asp:Label>
                                <asp:TextBox runat="server" ID="NumeroRegistro" />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="Estado">Estado</asp:Label>
                                <asp:TextBox runat="server" ID="Estado" />
                            </li>
                        </ol>                        
            </fieldset>    
        </div> 
        
        <div class="div-center">
        <br />
                        <asp:Button Text="Pesquisar" runat="server" OnClick="Search" />
                </div> 
                    


<hr />
    <div class="div-center">   
        
<asp:GridView ID="GridViewCuidador" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnRowDataBound="OnRowDataBound" OnPageIndexChanging="OnPageIndexChanging">
<Columns>
    <asp:BoundField HeaderStyle-Width="100px" DataField="Id" HeaderText="Registro" />
    <asp:BoundField HeaderStyle-Width="250px" DataField="Nome" HeaderText="Nome do Cuidador"
        ItemStyle-CssClass="Nome" />
    <asp:BoundField HeaderStyle-Width="150px" DataField="Telefone" HeaderText="Telefone" />
    <asp:BoundField HeaderStyle-Width="100px" DataField="Estado" HeaderText="Estado" />
    <asp:BoundField HeaderStyle-Width="200px" DataField="Cidade" HeaderText="Cidade" />
</Columns>
</asp:GridView>
</div>
          
</asp:Content>
