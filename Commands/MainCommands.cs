using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Linq;
using DSharpPlus;

namespace Discord_Bot.Commands
{
    public class MainCommands : BaseCommandModule
    {

        


        #region codeblock 
        [Command("codeblock"),Aliases("cb","code")]
        [Description("use CodeBlocks!")]
        
        public async Task CodeblockAsync(CommandContext ctx)
        {
            string cb = "**use codeblock to send code in discord**   \n" +
                        "\n" +
                        " look at this example to send c#  code:\n" +
                        " \\`\\`\\`cs \n" +
                        " code here\n" +
                        "\\`\\`\\` \n to send lenghty code use \n" +
                        " https://paste.myste.rs/ \n " +
                        "and send the pasted link in chat !!";
            await ctx.Channel.SendMessageAsync(cb).ConfigureAwait(false);
        }
        #endregion 


        #region google command
        [Command("google"),Aliases("ggl")]
        [Description("se")]
        public async Task Google(CommandContext ctx)
        {
            string google = "Ask <https://Google.com> , Most generic issues can be googled!";
            await ctx.Channel.SendMessageAsync(google).ConfigureAwait(false);
        }
        #endregion

        
        #region spoon command
        [Command("spoon")]
        [Description("")]
        public async Task spoonfead(CommandContext ctx)
        {
            string spoon = "**we are not going to spoon feed u code , but to try to help u solve/undesrtand your problem** ";
            await ctx.Channel.SendMessageAsync(spoon).ConfigureAwait(false);
        }
        #endregion


        #region Ping
        [Command("ping"),Aliases("latency")]
        [Description("get the ping of the client")]
        public async Task GetLatencyAsync(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder
            {
                Description=$"Current latency is about {ctx.Client.Ping}ms",
                Color = DiscordColor.Green
            };
            await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
        }
        #endregion



        #region UserInfo
        [Command("userinfo"),Aliases("info")]

        [Description("shows information about a user")]

        public async Task UserInfoAsync(CommandContext ctx ,
            [Description("the user u want to show info about")] DiscordUser user)
        {
            if(ctx.Guild.Members.Any(x=> x.Key == user.Id))
            {
                var embed = new DiscordEmbedBuilder
                {
                    Title = $"Information about {user.Username}{user.Discriminator}",
                    Color = ctx.Guild.GetMemberAsync(user.Id).Result.Color
                    

                };
                embed.AddField("Id:", $"**{user.Id}**")
                    .AddField("User Created:", user.CreationTimestamp.UtcDateTime.ToString())
                    .AddField("permessions:", ctx.Guild.GetMemberAsync(user.Id).Result.PermissionsIn(ctx.Guild.GetChannel(ctx.Channel.Id)).ToString())
                    
                    .WithThumbnail(user.GetAvatarUrl(DSharpPlus.ImageFormat.Png));

                await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
            }

            else
            {
                var embed = new DiscordEmbedBuilder
                {
                    Title = "This user is not in this server!",
                    Color = DiscordColor.Red
                   

                };
                await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
            }

        }
        #endregion


        #region avatar
        [Command("avatar"),Aliases("pfp"),Description("shows the avatar of a user")]
        public async Task AvatarAsync(CommandContext ctx ,[RemainingText, Description("User whose avatar to display ")]  DiscordUser user =null)
        {
            user ??= ctx.User;
            var avatar = user.AvatarHash!= null
                ?user.GetAvatarUrl(user.AvatarHash[0..2].SequenceEqual("a_") ? ImageFormat.Gif : ImageFormat.Png , 1024)
                :user.DefaultAvatarUrl;
            var embed = new DiscordEmbedBuilder().WithAuthor(name: $"{user.Username}#{user.Discriminator}", url: null, iconUrl:avatar).WithImageUrl(avatar).Build();

            await ctx.RespondAsync(embed: embed).ConfigureAwait(false);
        }
        #endregion

       
        #region invite
        [Command("invite"),Description("this bots invite link")]
        public async Task InviteAsync(CommandContext ctx)
        {
            await ctx.RespondAsync($"This is my invite link :\n<https://discord.com/oauth2/authorize?client_id=779291794475188234&scope=bot>");
        }
        #endregion
    }
}
