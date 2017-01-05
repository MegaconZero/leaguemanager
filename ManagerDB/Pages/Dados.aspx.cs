﻿using Entidades;
using ManagerDB.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ManagerDB.Pages
{
    public partial class Dados : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                if (this.ValidateSession() == false)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                else
                {
                    //Ponemos la ronda actual y el nombre de la liga
                    this.lblLiga.Text += this.liga.name;
                    this.lblRonda.Text += this.liga.current_round.ToString();
                    this.LblHistorial.Text += this.liga.name + ":";

                    //Ocultamos el botón del historial si estamos en la ronda 1
                    if (this.liga.current_round == null || this.liga.current_round == 1)
                    {
                        this.btnVerHistorial.Visible = false;
                    }

                    this.MostrarDados();
                    this.MostrarDadosUsuarios();
                }
            }
        }

        private void MostrarDados()
        {
            List<UserDice> dadosUsuario = (from ul in this.manager.t_user_leagues
                                           join l in this.manager.t_leagues on ul.id_league equals l.id
                                           join ud in this.manager.mercs_user_dice on ul.id equals ud.id_user_league
                                           join dt in this.manager.mercs_die_types on ud.id_die_type equals dt.id
                                    where ul.id_user == this.usuario.id
                                            && l.current_round == ud.round
                                            && l.id == this.liga.id
                                    select new UserDice
                                    {
                                        id = ud.id,
                                        id_die_type = ud.id_die_type,
                                        spent_date = ud.spent_date,
                                        resources_gained = ud.resources_gained,
                                        rolled_date = ud.rolled_date,
                                        die_type_name = dt.name,
                                        total_faces = dt.total_faces,
                                        info_die = dt.info,
                                        id_die_face = ud.id_die_face,
                                        img_Dice = ""
                                    }).ToList();

            if (dadosUsuario.Count > 0)
            {
                var caras = (from df in this.manager.mercs_die_faces select df).ToList();

                foreach (var dado in dadosUsuario)
                {                    
                    //Elegimos que info mostramos en el dado (si no se ha tirado mostramos la general y si se ha tirado la específica de la cara)
                    dado.info = (dado.rolled_date == null) ? dado.info_die : dado.info_face;

                    if (dado.id_die_face != null)
                    {
                        var cara = caras.Where(a => a.id == dado.id_die_face).FirstOrDefault();
                        dado.action = cara.action;
                        dado.info = cara.info;
                        dado.cost_credits = cara.cost_credits;
                        dado.sell_materials = cara.sell_materials;
                        dado.sell_credits = cara.sell_credits;
                        dado.die_face = cara.die_face;
                    }

                    //Calculamos que imagen debe ir en el dado
                    var imagen = CalcularImagenDado(dado.id_die_type, dado.die_face, dado.rolled_date, dado.spent_date, dado.resources_gained);
                    dado.img_Dice = imagen;
                }

                this.RptDices.DataSource = dadosUsuario.OrderBy(o => o.id);
                this.RptDices.DataBind();
            }
        }

        private void MostrarDadosUsuarios()
        {
            List<UserDice> usuarios = (from ul in this.manager.t_user_leagues
                                           join l in this.manager.t_leagues on ul.id_league equals l.id
                                           join ud in this.manager.mercs_user_dice on ul.id equals ud.id_user_league
                                           join dt in this.manager.mercs_die_types on ud.id_die_type equals dt.id
                                           join u in this.manager.t_users on ul.id_user equals u.id
                                           where ul.id_user != this.usuario.id
                                                   && l.current_round == ud.round
                                                   && l.id == this.liga.id
                                           select new UserDice
                                           {
                                               user_name = u.login,
                                               id_user_league = ul.id
                                           }).Distinct().ToList();

            if (usuarios.Count > 0)
            {
                this.RptDatosUsuarios.DataSource = usuarios;
                this.RptDatosUsuarios.DataBind();
            }
        }

        protected void RptDatosUsuarios_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var _usuarioActual = (UserDice)e.Item.DataItem;

                List<UserDice> datosUsuario = (from ul in this.manager.t_user_leagues
                                           join l in this.manager.t_leagues on ul.id_league equals l.id
                                           join ud in this.manager.mercs_user_dice on ul.id equals ud.id_user_league
                                           join dt in this.manager.mercs_die_types on ud.id_die_type equals dt.id
                                           join u in this.manager.t_users on ul.id_user equals u.id
                                           where ul.id_user != this.usuario.id
                                                   && l.current_round == ud.round
                                                   && l.id == this.liga.id
                                                   && ul.id == _usuarioActual.id_user_league
                                           select new UserDice
                                           {
                                               user_name = u.login,
                                               id = ud.id,
                                               id_die_type = ud.id_die_type,
                                               spent_date = ud.spent_date,
                                               resources_gained = ud.resources_gained,
                                               rolled_date = ud.rolled_date,
                                               die_type_name = dt.name,
                                               total_faces = dt.total_faces,
                                               info_die = dt.info,
                                               id_die_face = ud.id_die_face,
                                               img_Dice = ""
                                           }).ToList();

                if (datosUsuario.Count > 0)
                {
                    var caras = (from df in this.manager.mercs_die_faces select df).ToList();

                    foreach (var dado in datosUsuario)
                    {
                        //Elegimos que info mostramos en el dado (si no se ha tirado mostramos la general y si se ha tirado la específica de la cara)
                        dado.info = (dado.rolled_date == null) ? dado.info_die : dado.info_face;
                        
                        if (dado.id_die_face != null)
                        {
                            var cara = caras.Where(a => a.id == dado.id_die_face).FirstOrDefault();
                            dado.action = cara.action;
                            dado.info = cara.info;
                            dado.cost_credits = cara.cost_credits;
                            dado.sell_materials = cara.sell_materials;
                            dado.sell_credits = cara.sell_credits;
                            dado.die_face = cara.die_face;
                        }

                        //Calculamos que imagen debe ir en el dadodie_typedie_type
                        var imagen = CalcularImagenDado(dado.id_die_type, dado.die_face, dado.rolled_date, dado.spent_date, dado.resources_gained);
                        dado.img_Dice = imagen;
                    }
                    Repeater childRepeater = (Repeater)e.Item.FindControl("RptDadosUsuarios");
                    childRepeater.DataSource = datosUsuario.OrderByDescending(o=>o.spent_date).ThenByDescending(t=>t.rolled_date).ThenBy(t2=>t2.id_die_type).ThenBy(i=>i.id_die_face);
                    childRepeater.DataBind();
                }
            }
        }
        
        private string CalcularImagenDado(int? tipoDado, int? cara, DateTime? fechaTirada, DateTime? fechaUso, int? recursosGanados)
        {
            var imagen = string.Empty;
            //miramos que tipo de imagen corresponde (a,c,g,v)
            switch (tipoDado)
            {
                case 1:
                    imagen = "a";
                    break;
                case 2:
                    imagen = "v";
                    break;
                case 3:
                    imagen = "g";
                    break;
                case 4:
                    imagen = "c";
                    break;
            }

            if (fechaTirada != null)
            {
                //añadimos al nombre la cara de la tirada
                imagen += cara;

                if (fechaUso != null)
                {
                    if (recursosGanados != null)
                    {
                        imagen += "s";
                    }
                    else
                    {
                        imagen += "u";
                    }
                }
            }

            return imagen + ".png";
        }

        private void RollDice(int idDice, DateTime fecha)
        {
            var dado = this.manager.mercs_user_dice.Where(a => a.id == idDice).FirstOrDefault();
            if (dado != null)
            {                
                if (dado.rolled_date == null)
                {
                    //Hacemos la tirada
                    dado.rolled_date = fecha;
                    var tirada = RandomGenerator.RandomNumber(1, 6);
                    var cara = this.manager.mercs_die_faces.Where(a => a.die_face == tirada && a.id_die_type == dado.id_die_type).FirstOrDefault();
                    dado.id_die_face = cara.id;

                    GuardarMensaje("Tirada de dado", "Se tira un dado de '" + this.manager.mercs_die_types.Where(a => a.id == dado.id_die_type).FirstOrDefault().name + "' y ha salido '" + cara.info + "'", this.usuario.id, this.usuario.id);                    
                }
                else if (dado.spent_date == null)                
                {
                    //ponemos la pantalla en un modo inicial
                    optCreditos.Checked = false;
                    optMateriales.Checked = false;
                    optUsar.Checked = false;
                    txtUsar.Text = string.Empty;

                    var cara = this.manager.mercs_die_faces.Where(a => a.id == dado.id_die_face).FirstOrDefault();
                    //Controlamos la visibilidad de las opciones
                    if (cara.sell_credits > 0)
                    {
                        this.optCreditos.Text = "Obtener créditos (" + cara.sell_credits + ")";
                        this.optCreditos.Visible = true;
                    }
                    else
                    {
                        this.optCreditos.Visible = false;
                    }

                    if (cara.sell_materials > 0)
                    {
                        this.optMateriales.Text = "Obtener materiales (" + cara.sell_materials + ")";
                        this.optMateriales.Visible = true;
                    }
                    else
                    {
                        this.optMateriales.Visible = false;
                    }

                    if (cara.sell_credits == 0)
                    {
                        this.optUsar.ToolTip = cara.info;
                        this.optUsar.Visible = true;
                        this.txtUsar.Visible = true;
                    }
                    else
                    {
                        this.optUsar.Visible = false;
                        this.txtUsar.Visible = false;
                    }

                    //Guardamos el dado del usuario en los datos de sesión
                    dadoSeleccionado = dado;
                    
                    //Mostramos el modal con las opciones para gastarlo
                    this.PopUpDado.Show();
                }
            }
        }

        protected void RptDices_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "RollDice":
                    {
                        int idDice = Convert.ToInt32(e.CommandArgument);
                        //Tiramos el dado
                        RollDice(idDice, DateTime.Now);
                        //Guardamos la tirada
                        this.manager.SaveChanges();
                        //recalculamos los dados a mostrar
                        MostrarDados();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        protected void rollButton_Command(object sender, CommandEventArgs e)
        {
            var dadosPendientesTirar = (from ul in this.manager.t_user_leagues
                                           join l in this.manager.t_leagues on ul.id_league equals l.id
                                           join ud in this.manager.mercs_user_dice on ul.id equals ud.id_user_league
                                           where ul.id_user == this.usuario.id
                                                   && l.current_round == ud.round
                                                   && l.id == this.liga.id
                                                   && ud.rolled_date == null
                                           select ud.id
                                           ).ToList();
            if (dadosPendientesTirar.Count > 0)
            {
                var fecha = DateTime.Now;
                foreach (var idDado in dadosPendientesTirar)
                {
                    //Tiramos el dado
                    RollDice(idDado, fecha);
                }
            
                //Guardamos la tirada
                this.manager.SaveChanges();
                //recalculamos los dados a mostrar
                MostrarDados();
            }
        }

        protected void RptHistorialUsuarios_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var _usuarioActual = (UserDice)e.Item.DataItem;

                List<UserDice> datosUsuario = (from ul in this.manager.t_user_leagues
                                               join l in this.manager.t_leagues on ul.id_league equals l.id
                                               join ud in this.manager.mercs_user_dice on ul.id equals ud.id_user_league
                                               join dt in this.manager.mercs_die_types on ud.id_die_type equals dt.id
                                               join u in this.manager.t_users on ul.id_user equals u.id
                                               where ud.round == _usuarioActual.round
                                                       && l.id == this.liga.id
                                                       && ul.id == _usuarioActual.id_user_league
                                               select new UserDice
                                               {
                                                   user_name = u.login,
                                                   id = ud.id,
                                                   id_die_type = ud.id_die_type,
                                                   spent_date = ud.spent_date,
                                                   resources_gained = ud.resources_gained,
                                                   rolled_date = ud.rolled_date,
                                                   die_type_name = dt.name,
                                                   total_faces = dt.total_faces,
                                                   info_die = dt.info,
                                                   id_die_face = ud.id_die_face,
                                                   img_Dice = "",
                                                   round = ud.round
                                               }).ToList();

                if (datosUsuario.Count > 0)
                {
                    var caras = (from df in this.manager.mercs_die_faces select df).ToList();

                    foreach (var dado in datosUsuario)
                    {
                        //Elegimos que info mostramos en el dado (si no se ha tirado mostramos la general y si se ha tirado la específica de la cara)
                        dado.info = (dado.rolled_date == null) ? dado.info_die : dado.info_face;

                        if (dado.id_die_face != null)
                        {
                            var cara = caras.Where(a => a.id == dado.id_die_face).FirstOrDefault();
                            dado.action = cara.action;
                            dado.info = cara.info;
                            dado.cost_credits = cara.cost_credits;
                            dado.sell_materials = cara.sell_materials;
                            dado.sell_credits = cara.sell_credits;
                            dado.die_face = cara.die_face;
                        }

                        //Calculamos que imagen debe ir en el dadodie_typedie_type
                        var imagen = CalcularImagenDado(dado.id_die_type, dado.die_face, dado.rolled_date, dado.spent_date, dado.resources_gained);
                        dado.img_Dice = imagen;
                    }
                    Repeater childRepeater = (Repeater)e.Item.FindControl("RptHistorialDadosUsuarios");
                    childRepeater.DataSource = datosUsuario.OrderBy(r => r.round).OrderByDescending(o => o.spent_date).ThenByDescending(t => t.rolled_date).ThenBy(t2 => t2.id_die_type).ThenBy(i => i.id_die_face);
                    childRepeater.DataBind();
                }
            }

            PopUpHistorial.Show();
        }

        protected void btnVerHistorial_Command(object sender, CommandEventArgs e)
        {
            List<UserDice> historial = (from ul in this.manager.t_user_leagues
                                        join l in this.manager.t_leagues on ul.id_league equals l.id
                                        join ud in this.manager.mercs_user_dice on ul.id equals ud.id_user_league
                                        join dt in this.manager.mercs_die_types on ud.id_die_type equals dt.id
                                        join u in this.manager.t_users on ul.id_user equals u.id
                                        where  ud.round < l.current_round
                                                && l.id == this.liga.id
                                        select new UserDice
                                        {
                                            user_name = u.login,
                                            id_user_league = ul.id,
                                            round = ud.round
                                        }).Distinct().ToList();

            if (historial.Count > 0)
            {
                this.RptHistorialUsuarios.DataSource = historial;
                this.RptHistorialUsuarios.DataBind();
            }
        }

        protected void btnUsarDado_Command(object sender, CommandEventArgs e)
        {
            if (!optUsar.Checked && !optCreditos.Checked && !optMateriales.Checked)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "alert('Debes seleccionar una opción')", true);
                //lblError.Text = "Debes seleccionar una opción";
                //lblError.Visible = true;
            }
            else if (optUsar.Checked && string.IsNullOrEmpty(txtUsar.Text))
            {
                //Obligamos a meter texto si está marcado usar
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "alert('Si se va a usar el dado debe informarse que va a hacerse con él')", true);
            }
            else
            {
                //Guardamos la fecha de uso del dado
                var dado = this.manager.mercs_user_dice.Where(a => a.id == dadoSeleccionado.id).FirstOrDefault();
                dado.spent_date = DateTime.Now;
                this.manager.SaveChanges();
                this.MostrarDados();
            }
        }

        private int ObtenerIdMensaje()
        {
            var ultimoMsg = (from m in this.manager.t_messages
                             select m).OrderByDescending(a => a.id).FirstOrDefault();
            var idMsg = 0;
            if (ultimoMsg == null)
            {
                idMsg = 1;
            }
            else
            {
                idMsg = ultimoMsg.id + 1;
            }
            return idMsg;
        }

        private void GuardarMensaje(string asunto, string mensaje, int? from, int? to)
        {
            //Guardamos el mensaje
            var idMsg = ObtenerIdMensaje();
            t_messages msg = new t_messages();
            msg.id = idMsg;
            msg.id_league = this.liga.id;
            msg.id_user_from = from;
            msg.id_user_to = to;
            msg.message = mensaje;
            msg.round = this.liga.current_round;
            msg.sent_date = DateTime.Now;
            msg.subject = asunto;

            //añadimos el mensaje
            this.manager.t_messages.Add(msg);
            //Guardamos aquí para que no se pisen los Ids de los mensajes
            this.manager.SaveChanges();
        }
    }
}