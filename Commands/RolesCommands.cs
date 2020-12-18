using System;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Linq;
using System.Collections.Generic;

namespace Discord_Bot.Commands
{
    public class RolesCommands : BaseCommandModule
    {
        

        #region GiveRole command
        [Command("giverole"), Aliases("addrole", "role")]
        [Description("Add a specific role")]
        public async Task GiveRoleAsync(CommandContext ctx,
            [Description("the role that u want to add")][RemainingText] DiscordRole role)
        {
            if(role == null)
            {
                throw new ArgumentNullException();
            }
            if (ctx.Member.Roles.Any(x => x.Id == role.Id))
            {
                //build embed
                DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Red,
                    Title = "U already have that role"

                };
                //send embed
                await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
            }

            //if the role u requested is permitted 
            else if (Data.PermittedRolesIds.Contains(role.Id))
            {
                // give him the role
                await ctx.Member.GrantRoleAsync(role).ConfigureAwait(false);
                //build the embed
                DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Green,
                    Title = ($"U have been successfully Granted {role.Name}")

                };
                //send the embed
                await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);

            }
            //if the user can't get that role
            else if (!Data.PermittedRolesIds.Contains(role.Id))
            {
                //build the embed
                DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Red,
                    Title = "U can't have that role!"

                };
                //send the embed
                await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);

            }
            



        }
        #endregion


        #region RemoveRole command
        [Command("removerole"), Aliases("remove", "delete")]
        [Description("Removes a specified role")]
        public async Task RemoveRoleAsync(CommandContext ctx,
            [Description("the role that u want to remove")][RemainingText] DiscordRole role)
        {
            
            //if the user have that role
            if (ctx.Member.Roles.Any(x => x.Id == role.Id))
            {
                //remove the role 
                await ctx.Member.RevokeRoleAsync(role).ConfigureAwait(false);
                //build the embed
                DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Green,
                    Title = $"{role.Name} was sucssesufuly removed!"

                };
                //send the embed
                await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);

            }
            //if u dont have that role u cant remove it
            else
            {
                //build embed
                DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Red,
                    Title = "U dont have this role!"

                };
                //send embed
                await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
            }
        }
        #endregion


    }
}
