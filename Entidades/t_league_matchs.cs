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
    
    public partial class t_league_matchs
    {
        public int id { get; set; }
        public Nullable<int> id_league { get; set; }
        public Nullable<int> round { get; set; }
        public Nullable<int> p1_id_user { get; set; }
        public Nullable<int> p1_score { get; set; }
        public Nullable<int> p1_kills { get; set; }
        public string p1_notes { get; set; }
        public Nullable<int> p2_id_user { get; set; }
        public Nullable<int> p2_score { get; set; }
        public Nullable<int> p2_kills { get; set; }
        public string p2_notes { get; set; }
        public Nullable<System.DateTime> match_date { get; set; }
        public Nullable<int> winner_id_user { get; set; }
        public string isdraw { get; set; }
        public string match_notes { get; set; }
    }
}
