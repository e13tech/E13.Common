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
    public class E13ConsoleSimple : E13ConsoleBase
    {
        /// <summary>
        /// Simple Empty Constructor relaying ILogger to base(...)
        /// </summary>
        /// <param name="logger"></param>
        public E13ConsoleSimple(ILogger logger)
            : base(logger) { }

        /// <summary>
        /// Interactive CLI header with a single color ASCII version of the E13 Logo
        /// </summary>
        protected override void InteractiveHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                                                  ");
            Console.WriteLine("    TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT    ");
            Console.WriteLine("    TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT    ");
            Console.WriteLine("    TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT    ");
            Console.WriteLine("                                                                  ");
            Console.WriteLine("    EEEEEEEEEEEEEEEEEEEEEE   111111   333333333333333333333333    ");
            Console.WriteLine("    EEEEEEEEEEEEEEEEEEEEEE   111111   333333333333333333333333    ");
            Console.WriteLine("    EEEEEEEEEEEEEEEEEEEEEE   111111   333333333333333333333333    ");
            Console.WriteLine("    EEEEEE                   111111                     333333    ");
            Console.WriteLine("    EEEEEE                   111111                     333333    ");
            Console.WriteLine("    EEEEEEEEEEEEEEEEEEEE     111111     3333333333333333333333    ");
            Console.WriteLine("    EEEEEEEEEEEEEEEEEEEE     111111     3333333333333333333333    ");
            Console.WriteLine("    EEEEEEEEEEEEEEEEEEEE     111111     3333333333333333333333    ");
            Console.WriteLine("    EEEEEE                   111111                     333333    ");
            Console.WriteLine("    EEEEEE                   111111                     333333    ");
            Console.WriteLine("    EEEEEEEEEEEEEEEEEEEEEE   111111   333333333333333333333333    ");
            Console.WriteLine("    EEEEEEEEEEEEEEEEEEEEEE   111111   333333333333333333333333    ");
            Console.WriteLine("    EEEEEEEEEEEEEEEEEEEEEE   111111   333333333333333333333333    ");
            Console.WriteLine("                                                                  ");
        }
    }
}
