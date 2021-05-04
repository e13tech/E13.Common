using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Cli
{
    /// <summary>
    /// Base implementation of CliConsole used for E13.Tech CLI commands
    /// 
    /// Also serves as a concrete example to use for reference to build your own
    /// </summary>
    public abstract class E13ConsoleBase : CliConsole
    {
        /// <summary>
        /// Simple Empty Constructor relaying ILogger to base(...)
        /// </summary>
        /// <param name="logger"></param>
        public E13ConsoleBase(ILogger logger)
            : base(logger) { }

        /// <summary>
        /// Simple Non-Interactive CLI header
        /// </summary>
        protected override void NonInteractiveHeader()
        {
            Console.Write("E13: ");
        }
    }
}
