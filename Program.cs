using System;
using System.Diagnostics;

namespace Discord_Bot
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            Bot bot = new Bot();

            
            bot.RunAsync().GetAwaiter().GetResult();
            
           
        }
    }
}
