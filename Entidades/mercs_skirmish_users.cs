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
    
    public partial class mercs_skirmish_users
    {
        public int id { get; set; }
        public Nullable<int> id_skirmish { get; set; }
        public Nullable<int> id_user_league { get; set; }
        public string team_name { get; set; }
        public Nullable<int> mission_score { get; set; }
        public string skirmish_results { get; set; }
        public string reward { get; set; }
    }
}