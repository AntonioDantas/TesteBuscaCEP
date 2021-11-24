<%@ Page Title="Página de Busca" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BuscaCEP._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>Busca CEP no sistema do Correios</h2>
        <p class="lead">Digite abaixo o CEP que deseja pesquisar</p>
    </div>

    <div class="col-md-12 row">
        <div class="col-md-2">
            CEP:
        </div>
        <div class="col-md-3">
            <asp:TextBox runat="server" placeholder="00000-000" onkeyup="formataCEP(this,event);" ID="txtCEP" MaxLength="09" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <asp:LinkButton runat="server" ID="lnkBusca" CssClass="btn btn-primary" OnClick="lnkBusca_Click"> 
                <span class="glyphicon glyphicon-search"></span> Pesquisar e Registrar</asp:LinkButton>
        </div>
        <div class="col-md-4"></div>
    </div>
    <br />
    <br />
    <br />
    <div class="col-md-12 row">
        <asp:GridView ID="grdResultado" Width="100%" runat="server" AllowPaging="false" HeaderStyle-CssClass="thead-dark"
            EmptyDataText="Sem resultados a serem exibidos" CssClass="table table-hover text-uppercase" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="CEP" HeaderText="CEP" ControlStyle-CssClass="text-uppercase" />
                <asp:BoundField DataField="Logradouro" HeaderText="Logradouro" ControlStyle-CssClass="text-uppercase" />
                <asp:BoundField DataField="Bairro" HeaderText="Bairro" ControlStyle-CssClass="text-uppercase" />
                <asp:BoundField DataField="Cidade.Nome" HeaderText="Cidade" ControlStyle-CssClass="text-uppercase" />
                <asp:BoundField DataField="Cidade.Estado.Sigla" HeaderText="Estado" ControlStyle-CssClass="text-uppercase" />
            </Columns>
        </asp:GridView>
    </div>
    <p style="height: 100px !important;"></p>
</asp:Content>
