using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Cli
{
    /// <summary>
    /// Default implementation of CliConsole used for E13.Tech CLI commands
    /// 
    /// Also serves as a concrete example to use for reference to build your own
    /// </summary>
    public class E13FancyConsole : E13ConsoleBase
    {
        private static readonly ConsoleColor Logo_T = ConsoleColor.DarkGray;
        private static readonly ConsoleColor Logo_E = ConsoleColor.DarkCyan;
        private static readonly ConsoleColor Logo_1 = ConsoleColor.Gray;
        private static readonly ConsoleColor Logo_3 = ConsoleColor.DarkBlue;

        /// <summary>
        /// Simple Empty Constructor relaying ILogger to base(...)
        /// </summary>
        /// <param name="logger"></param>
        public E13FancyConsole(ILogger logger)
            : base(logger) { }

        /// <summary>
        /// Interactive CLI header with a colored ASCII version of the E13 Logo
        /// </summary>
        protected override void InteractiveHeader()
        {
            BlankLine();

            TopSegments();
            TopSegments();
            TopSegments();

            BlankLine();

            LongSegments();
            LongSegments();
            LongSegments();

            ShortSegments();
            ShortSegments();

            LongSegments();
            LongSegments();
            LongSegments();

            ShortSegments();
            ShortSegments();

            LongSegments();
            LongSegments();
            LongSegments();

            BlankLine();
        }

        private void BlankLine()
        {
            Margin();

            Console.WriteLine("                                                          ");

            Margin();
            Console.WriteLine();
        }

        private void Margin()
        {
            Console.Write("    ");
        }
        private void TopSegments()
        {
            Margin();

            Console.ForegroundColor = Logo_T;
            Console.Write("TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");

            Margin();
            Console.WriteLine();
        }

        private void LongSegments()
        {
            Margin();

            Console.ForegroundColor = Logo_E;
            Console.Write("EEEEEEEEEEEEEEEEEEEEEE");
            Console.ForegroundColor = Logo_1;
            Console.Write("   111111");
            Console.ForegroundColor = Logo_3;
            Console.Write("   333333333333333333333333");

            Margin();
            Console.WriteLine();
        }

        private void ShortSegments()
        {
            Margin();

            Console.ForegroundColor = Logo_E;
            Console.Write("EEEEEE");
            Console.ForegroundColor = Logo_1;
            Console.Write("                   111111");
            Console.ForegroundColor = Logo_3;
            Console.Write("                     333333");

            Margin();
            Console.WriteLine();
        }
    }
}
