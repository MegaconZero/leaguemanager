﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagerDB.Pages
{
    public class JugadorLiga
    {
        public int league_id { get; set; }
        public int? user_id { get; set; }
        public string user_name { get; set; }
        public string user_avatar { get; set; }
        public string team_name { get; set; }
        public string team_avatar_url { get; set; }
        public int? wins { get; set; }
        public int? losses { get; set; }
        public int? draws { get; set; }
        public int? score { get; set; }
        public string league_name { get; set; }
        public string league_avatar_url { get; set; }
        public string game_name { get; set; }
        public string game_avatar_url { get; set; }
        public string faction_name { get; set; }
        public string faction_avatar_url { get; set; }
    }
}