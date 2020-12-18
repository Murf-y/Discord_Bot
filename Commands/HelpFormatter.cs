using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discord_Bot.Commands
{
    public class HelpFormatter : BaseHelpFormatter
    {

        private DefaultHelpFormatter _d;

        public HelpFormatter(CommandContext ctx) : base(ctx)
        {
            this._d = new DefaultHelpFormatter(ctx);
        }
        public override BaseHelpFormatter WithCommand(Command command)
        {
            return this._d.WithCommand(command);
        }
        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            return this._d.WithSubcommands(subcommands);
        }
        public override CommandHelpMessage Build()
        {
            var message = this._d.Build();
            var embed = new DiscordEmbedBuilder(message.Embed)
            {
                Color = DiscordColor.Green,
            };
            return new CommandHelpMessage(embed: embed);
        }
    }
}

