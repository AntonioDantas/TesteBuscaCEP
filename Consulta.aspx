<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Consulta.aspx.cs" Inherits="BuscaCEP.Consulta" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
     <div class="panel panel-body">
        <div class="form-group row"></div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-12 text-center">
                        <b>CONSULTA DE BUSCAS</b>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="form-group row">
                    <span class="col-md-2">CEP:</span>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtCEP"  placeholder="00000-000" onkeyup="formataCEP(this,event);"  MaxLength="9" runat="server" CssClass="form-control text-uppercase" ></asp:TextBox>
                    </div>
                    <span class="col-md-2">Data:</span>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtData" runat="server" CssClass="form-control" onkeyup="formataData(this,event);"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <span class="col-md-2">Logradouro:</span>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtEndereco" runat="server" CssClass="form-control text-uppercase"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group text-center">
                    <asp:LinkButton ID="btnPesquisar" runat="server" CssClass="btn btn-primary btn-preloader" OnClick="btnPesquisar_Click">
                        <span class="glyphicon glyphicon-search"></span> PESQUISAR
                    </asp:LinkButton>
                </div>
            </div>
        </div>
        <div class="panel panel-default" id="divResultado" runat="server" visible="false">
            <div class="panel-body">
                <div class="form-group text-center">
                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                </div>
                <div class="form-group text-center">
                    <asp:GridView ID="grdResultado" Width="100%" runat="server" AllowPaging="true" PageSize="5" OnPageIndexChanging="grdResultado_PageIndexChanging" HeaderStyle-CssClass="thead-dark" 
                        EmptyDataText="Não foram encontrados dados de pesquisa" CssClass="table table-hover text-uppercase" AllowSorting="true"  AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="CEP" HeaderText="CEP" ControlStyle-CssClass="text-uppercase" />
                            <asp:BoundField DataField="DataHora"  HeaderText="DATA HORA" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"  />
                            <asp:BoundField DataField="Resultado" HeaderText="RESULTADOS" ControlStyle-CssClass="text-uppercase" />                            
                        </Columns>
                        <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>