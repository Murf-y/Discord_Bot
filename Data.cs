using System;
using System.Collections.Generic;
using DSharpPlus.Entities;
using System.IO;
using System.Threading.Tasks;

namespace Discord_Bot

{
    public static class Data
    {
        public static List<ulong> PermittedRolesIds = new List<ulong>{};
        public static DiscordRole Mutedrole;
        
    }
}
