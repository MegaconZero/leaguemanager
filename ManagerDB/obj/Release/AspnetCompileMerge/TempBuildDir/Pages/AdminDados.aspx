﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDados.aspx.cs" Inherits="ManagerDB.Pages.AdminDadosAspx"  MasterPageFile="~/master/main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentProgram" runat="server">
    <div class="container">

        <div class="row">
            <div class="col-md-12">
                <h1>
                    <asp:ImageButton runat="server" ID="BtBack" 
                                     ImageUrl="~/images/webapp/back.png" OnClientClick="goBack();" 
                                     CssClass="image_button" Width="45px" 
                                     style="vertical-align:text-top;"/>
                    <asp:Label runat="server" ID="_LbTitle" Text="ADMINISTRAR RONDA DE JUEGO MERCS"></asp:Label>
                </h1>
                <hr />
            </div>
        </div>


        <br />
        <div class="row">
            <div class="col-md-12">
                <h3>
                    <asp:Label ID="lblLiga" runat="server" ></asp:Label>
                    -
                    <asp:Label ID="lblRonda" runat="server"></asp:Label>
                </h3>
                <br />
                <div style="text-align:right" class="div_box">
                    <asp:Button CssClass="btn" id="btnAddTurno" runat="server" 
                                            OnCommand="btnAddTurno_Command" text="Nueva ronda" 
                                            style="width:150px;"
                                            ToolTip="Pasa la liga al siguiente turno" Width="150px"></asp:Button> 
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <h3>
                    <label id="tituloUsuarios" class="titulo">PARTICIPANTES</label>            
                </h3>
                <div class="div_box">
                    <asp:Repeater runat="server" ID="RptDatosUsuarios" OnItemDataBound="RptDatosUsuarios_ItemDataBound">
                        <ItemTemplate>
                            <div class="boxDados">
                                <div style="padding-left: 30px">
                                    <asp:Image CssClass="imagenDados" runat="server" ID="ImgUser"
                                                    Width="45px"
                                                    ToolTip='<%# Eval("user_name") %>'
                                                    AlternateText='<%# Eval("user_name") %>'
                                                    ImageUrl= '<%# "../images/" + Eval("user_avatar").ToString().Trim() %>'>
                                                </asp:Image>
                                    <asp:Label id="user" Text='<%# Eval("user_name") %>' CssClass="textoDadosUsuario" runat="server"></asp:Label>
                                    <asp:Label id="creditos" Text='<%# Eval("textoCreditos") %>' CssClass="textoDadosUsuario" runat="server"></asp:Label>
                                    <asp:Label id="materiales" Text='<%# Eval("textoMateriales") %>' CssClass="textoDadosUsuario" runat="server"></asp:Label>                           
                                    <asp:Button CssClass="btn" id="btnAddDado" runat="server" 
                                        OnCommand="btnAddDado_Command" text="Añadir dado" 
                                        style="vertical-align:text-bottom; margin-right:30px; float:right"
                                        ToolTip="Ver historial de las tiradas de dados" Width="150px"></asp:Button> 
                                </div>
                                <div style="padding-left: 30px">
                                    <label class="textoDados">Recursos: </label>
                                    <asp:Repeater runat="server" ID="RptDadosUsuarios" OnItemCommand="RptDadosUsuarios_ItemCommand">
                                        <ItemTemplate>                                    
                                            <asp:ImageButton runat="server" ID="ImgDice"
                                                Width="60px"
                                                CssClass="dado"
                                                ToolTip='<%# Eval("info") %>'
                                                AlternateText='<%# Eval("info") %>'
                                                ImageUrl= '<%# "../images/t_dices/mercs/" + Eval("img_Dice").ToString().Trim() %>'
                                                CommandName="AdministrarDado"
                                                CommandArgument='<%# Eval("id") %>'>
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <br />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <br /><br />

    <!-- Popup -->
    <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" style="display:none"/>
    <ajax:ModalPopupExtender ID="PopUpDado" runat="server" PopupControlID="PnlPopUp" TargetControlID="btnShow"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="PnlPopUp" runat="server" CssClass="PopUp" style="display:none">
        <asp:Button ID="btnClose" CssClass="ClosePopUp" runat="server" Text="X" />
        <div class="box" style="width:80%; max-width:100%; padding: 10px; margin-top:25px;">            
            <div style="padding-left: 30px">
                <div class="row">                    
                    <asp:RadioButton id="optReroll" GroupName="AdminDado" Text="Reroll" runat="server"/>
                </div>
                <div class="row">
                    <asp:RadioButton id="optAumentarCoste" GroupName="AdminDado" Text="Aumentar coste" runat="server"/>
                    <asp:TextBox ID="txtAumentarCoste" runat="server" Height="23px" Width="30px"  style="margin-left:25px;"></asp:TextBox>
                </div>
                <div class="row">                    
                    <asp:RadioButton id="optEliminar" GroupName="AdminDado" Text="Eliminar dado" runat="server"/>
                </div>
                <div class="row">
                    <asp:TextBox ID="txtMensaje" runat="server" Height="100px" TextMode="MultiLine" Width="90%"></asp:TextBox>
                </div>
                <div class="row" style="text-align:center">
                    <asp:Button CssClass="btn" id="btnUsarDado" runat="server" 
                                oncommand="btnAdminDado_Command" text="Aceptar" 
                                style="vertical-align:text-bottom; margin-left:30px"
                                ToolTip="Usar dado" Width="100px"></asp:Button>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>