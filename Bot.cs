using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Entities;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Threading;
using Discord_Bot.Commands;
using System;

namespace Discord_Bot
{
    public class Bot
    {
        
        
        public DiscordClient Client { get; private set; }


        






        public CommandsNextExtension Commands { get; private set; }
        public async Task RunAsync()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);


            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            DiscordConfiguration config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                Intents = DiscordIntents.All,
                LogTimestampFormat = DateTime.UtcNow.ToLongDateString(),
                

            };

            Client = new DiscordClient(config);


            

            Client.Ready += OnClientReady;


            Client.ClientErrored += OnClientErrored;
            


            var commandconfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = false,
                UseDefaultCommandHandler = true,
                
                


            };

            Commands = Client.UseCommandsNext(commandconfig);
            
            Commands.CommandErrored += OnCommandErrored;

            Commands.RegisterCommands<FunCommands>();

            Commands.RegisterCommands<RolesCommands>();

            Commands.RegisterCommands<ModerationCommands>();

            Commands.SetHelpFormatter<HelpFormatter>();

            await Client.ConnectAsync();

            await Task.Delay(Timeout.Infinite);
        }

        
        
        private async  Task OnClientReady(object sender, ReadyEventArgs e)
        {
            DiscordEmoji EyesEmogi = DiscordEmoji.FromName(Client, ":eyes:");
            await Client.UpdateStatusAsync(new DiscordActivity
            {
                
                Name = $" {EyesEmogi} Watching All",
                ActivityType = ActivityType.Playing,
                

            });
            
        }
        private async Task OnCommandErrored(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            DiscordEmbedBuilder embed = null;

            var ex = e.Exception;
            while(ex is AggregateException)
            {
                ex = ex.InnerException;
            }
            if(ex is ArgumentException)
            {
                embed = new DiscordEmbedBuilder
                {
                    Title = $"ArgumentException{ex.Message}",
                    Color = DiscordColor.Red,
                };
            }
            else
            {
                embed = new DiscordEmbedBuilder
                {
                    Title = $"A problem occured while excuting the command",
                    Description =ex.Message,
                    Color = DiscordColor.Red,
                };
            }
            if(embed != null)
            {
                await e.Context.RespondAsync("", embed: embed.Build());
            }
        }
        private async Task OnClientErrored(DiscordClient sender, ClientErrorEventArgs e)
        {
            Console.WriteLine($"Exception : {e.Exception}");
            await Task.CompletedTask;
        }   

    }
}
