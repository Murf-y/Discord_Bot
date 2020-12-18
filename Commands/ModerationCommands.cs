using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Linq;
using System;

namespace Discord_Bot.Commands
{
    public class ModerationCommands : BaseCommandModule
    {
        
        #region unban
        [Command("unban")]
        [RequirePermissions(DSharpPlus.Permissions.BanMembers)]
       
        [Description("Unban a user from a guild")]
        public async Task UnbanAsync(CommandContext ctx,
            [Description("the user to unban")] DiscordUser user,
            [RemainingText][Description("optional reason to unban")] string reason ="Unspecified")
        {
            //get the bans for this server
            var banlist = ctx.Guild.GetBansAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            //check if the specified user was banned
            if(banlist.Any(x => x.User.Id == user.Id))
            {
                //build the embed
                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Green,
                    Title = $"{user.Username} has been unbanned!",
                    Description = $"Reason : {reason}",
                    Timestamp = DateTime.Now

                };
                //unban the user
                await ctx.Guild.UnbanMemberAsync(user, reason).ConfigureAwait(false);
                //send the embed
                await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
            }

            else
            {
                //build the embed
                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Red,
                    Title = $"this user was not banned previously!",
                    Timestamp = DateTime.Now

                };
                //send the embed
                await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
            }
        }
        #endregion


        #region ban
        [Command("ban")]
        [Description("Bans a user from the guild")]
        [RequirePermissions(DSharpPlus.Permissions.BanMembers)]
        
        public async Task BanAsync(CommandContext ctx ,
            [Description("the user to ban")] DiscordUser user,
            [RemainingText][Description("optional reason to ban the user")] string reason = "Undefined")
        {
            //get the banlist
            var banlist = ctx.Guild.GetBansAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            // check if user is banned already
            if(banlist.Any(x=>x.User.Id== user.Id))
            {
                //build embed
                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Red,
                    Timestamp = DateTime.Now,
                    Title = $"{user.Username} is already banned"
                };
                //send embed
                await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
            }
            //ban the user
            else
            {
                //build embed
                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Green,
                    Timestamp = DateTime.Now,
                    Title = $"{user.Username} was banned",
                    Description = $"Reason : {reason}"


                };
                //ban the member
                await ctx.Guild.BanMemberAsync(user.Id, 0 ,reason).ConfigureAwait(false);
                //send embed
                await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                
            }
        }
        #endregion


        #region Kick

        [Command("Kick")]
        [RequirePermissions(DSharpPlus.Permissions.KickMembers)]
        public async Task KickAsync(CommandContext ctx ,
            [Description("the user u want to kick")] DiscordUser user ,
            [Description("optional reason to kick")] [RemainingText]string reason = "Undefined")
        {
            //if user in the guild
            if(ctx.Guild.Members.Any(x => x.Key == user.Id))
            {
                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Green,
                    Title = $"{user.Username} has been kicked",
                    Description = $"reason: {reason}",
                    Timestamp = DateTime.Now
                };
                await ctx.Guild.GetMemberAsync(user.Id).Result.RemoveAsync(reason).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
            }

            //user is not in the guild
            else
            {
                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Red,
                    Title = $"this user is not in this server!!",
                    Timestamp = DateTime.Now
                };
                await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
            }
        }

        #endregion


        #region mute
        public async Task MuteAsync(CommandContext ctx, DiscordMember m,[RemainingText] string reason = "undefined")
        {
            //to be implemented
        }
        #endregion
    }
}
