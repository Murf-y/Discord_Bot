using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Discord_Bot

{
    public static class Data
    {
        public static ulong[] PermittedRolesIds = new ulong[] {777625627569160202,
777625709887356989,
777625832042397707,
777625955606593547,
777629970436587550,
777630449589682236,
779393009661247589,
779833283881467904};

        public static readonly string funcommands = "codeblock \ngoogle \nspoon \nping \nUserInfo" ;
        public static readonly string rolescommands="GiveRole \nRemoveRole";
        public static readonly string moderationcommands = "ban \nunban \nkick";



    }
}
