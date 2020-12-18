using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using System.Collections.Generic;


namespace Discord_Bot.Commands
{
    public class HelpFormatter : BaseHelpFormatter
    {
        private DefaultHelpFormatter defaultHelpFormatter;

        public HelpFormatter(CommandContext ctx) : base(ctx)
        {
            this.defaultHelpFormatter = new DefaultHelpFormatter(ctx);
        }
        public override BaseHelpFormatter WithCommand(Command command)
        {
            return this.defaultHelpFormatter.WithCommand(command);
        }
        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            return this.defaultHelpFormatter.WithSubcommands(subcommands);
        }
        public override CommandHelpMessage Build()
        {
            var message = this.defaultHelpFormatter.Build();

            var embed = new DiscordEmbedBuilder(message.Embed).WithColor(DiscordColor.Green);

            return new CommandHelpMessage(embed: embed);
        }
    }
}

