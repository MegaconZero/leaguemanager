//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class t_user_leagues
    {
        public int id { get; set; }
        public Nullable<int> id_user { get; set; }
        public Nullable<int> id_league { get; set; }
        public Nullable<int> id_faction { get; set; }
        public string team_name { get; set; }
        public string team_avatar_url { get; set; }
        public Nullable<int> wins { get; set; }
        public Nullable<int> losses { get; set; }
        public Nullable<int> draws { get; set; }
        public Nullable<int> total_score { get; set; }
        public Nullable<int> security_level { get; set; }
        public string active { get; set; }
        public Nullable<int> total_kills { get; set; }
        public Nullable<int> total_vp { get; set; }
    }
}
