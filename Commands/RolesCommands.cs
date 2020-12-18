using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Linq;

namespace Discord_Bot.Commands
{
    public class RolesCommands : BaseCommandModule
    {

        #region GiveRole command
        [Command("giverole")]
        [Description("Add a specific role")]
        public async Task GiveRoleAsync(CommandContext ctx,
            [Description("the role that u want to add")][RemainingText] DiscordRole role)
        {
           
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
                    Title = ($"U have been successfully Granted {role.Name} role!")

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
        [Command("removerole")]
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


        #region AddPermittedRole
        [Command("AddPermittedRole")]
        [RequireOwner]
        [Description("Adds a specific role to Permitted roles list ")]
        
        public async Task AddRole(CommandContext ctx,[RemainingText, Description("roles u want to add to Permitted roles list ")] DiscordRole role)
        {

            if (Data.PermittedRolesIds.Contains(role.Id))
            {
                var embed = new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Red)
                    .WithTitle("this role already existes in the list");
            }

            else
            {
                Data.PermittedRolesIds.Add(role.Id);
                var embed = new DiscordEmbedBuilder()
                .WithTitle($"Sucssesufuly added {role.Name} role to permittedRoles.")
                .WithColor(DiscordColor.Green);

                await ctx.RespondAsync(embed: embed).ConfigureAwait(false);
            }

        }
        #endregion


        #region RemovePermittedRole
        [Command("RemovePermittedRole")]
        [RequireOwner]
        [Description("Removes a specific role from Permitted roles list ")]
        public async Task RemovePermittedRole(CommandContext ctx,[RemainingText][Description("The role u want to remove from permittedRoles list")] DiscordRole role)
        {
            if (Data.PermittedRolesIds.Contains(role.Id))
            {
                Data.PermittedRolesIds.Remove(role.Id);
                var embed = new DiscordEmbedBuilder()
                    .WithTitle($"Sucssesufuly removed {role.Name} from permitted roles list.")
                    .WithColor(DiscordColor.Green);
                await ctx.RespondAsync(embed: embed).ConfigureAwait(false);
            }
            else
            {
                var embed = new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Red)
                    .WithTitle("this role does not exist in permitted roles list.");
                await ctx.RespondAsync(embed: embed).ConfigureAwait(false);
            }
        }
        #endregion


        #region PermittedRolesList
        [Command("PermittedRoles"), Aliases("roles")]
        
        public async Task ShowPermittedRoles(CommandContext ctx)
        {
            if (Data.PermittedRolesIds.Count!=0)
            {
                var embed = new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Green)
                    .WithTitle("Permitted Roles:");
                foreach (var id in Data.PermittedRolesIds)
                {
                    var _role = ctx.Guild.GetRole(id);
                    embed.AddField(_role.Name, "------------------------------------");

                }
                await ctx.RespondAsync(embed: embed).ConfigureAwait(false);
            }
            else
            {
                var embed = new DiscordEmbedBuilder()
                    .WithTitle("Roles list is empty, make sure to add roles to it throught <AddPermittedRole> command ")
                    .WithColor(DiscordColor.Red);
                await ctx.RespondAsync(embed: embed).ConfigureAwait(false);
            }
            
        }

        #endregion


    }
}
