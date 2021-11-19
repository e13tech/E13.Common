using CommandLine;
using System;

namespace E13.Common.Cli
{
    /// <summary>
    /// Base Options Class for implementing the E13 standard CLI options such as logging output control
    /// </summary>
    public class BaseOptions
    {
        /// <summary>
        /// Logging group const to ensure that only one logging option is provided
        /// </summary>
        public const string LoggingGroup = "Logging";

        /// <summary>
        /// Verbose output: default LogLevel to Info
        /// </summary>
        [Option('v', "verbose", Group = LoggingGroup, HelpText = "Verbose Log Output")]
        public bool LoggingVerbose { get; set; }

        /// <summary>
        /// Quiet output: default LogLevel to Error
        /// </summary>
        [Option('q', "quiet", Group = LoggingGroup, HelpText = "Quiet Log Output")]
        public bool LoggingQuiet { get; set; }
    }
}
