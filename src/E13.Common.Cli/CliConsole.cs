using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace E13.Cli
{
    /// <summary>
    /// Base class for a CliConsole that has a simple progress bar animation and options for interactive and non-interactive
    /// headers so locally you get a more enjoyable experience but in a DevOps pipeline you avoid cluttering up the console log
    /// </summary>
    public abstract class CliConsole : IDisposable
    {
        private const int Delay = 50;
        private const char ProgressChar = '■';
        private const int ProgressMaxLength = 10;
        private const int AnimationLength = 59;
        private const int TemplatePadding = 3;
        private const string Template = "                                                                 ";

        private readonly bool InteractiveCli = false;
        private readonly Thread SpinnerThread;

        private int Counter = 1;
        private bool ThreadActive = true;

        /// <summary>
        /// Constructor that Starts the Console itself printing appropriate headers and begins the
        /// spinner if this ia an InteractiveCli
        /// </summary>
        /// <param name="logger"></param>
        public CliConsole(ILogger logger)
        {
            try
            {
                Console.Clear();
                InteractiveCli = true;
            }
            catch { /* Console.Clear is used to determine if console is interactive */ }

            if (InteractiveCli)
            {
                InteractiveHeader();
            }
            else
            {
                NonInteractiveHeader();
            }

            SpinnerThread = new Thread(() =>
            {
                if(InteractiveCli)
                    Console.ForegroundColor = ConsoleColor.Green;

                while (ThreadActive)
                {
                    if (InteractiveCli)
                    {
                        Console.Write(NextSequence());
                        Console.SetCursorPosition(0, Console.CursorTop);
                    }
                    Thread.Sleep(Delay);
                }
            });

            SpinnerThread.Start();
        }

        /// <summary>
        /// Called to print a CLI header for an interactive window
        /// </summary>
        protected abstract void InteractiveHeader();

        /// <summary>
        /// Called to print a CLI header for a non-interactive window
        /// </summary>
        protected abstract void NonInteractiveHeader();

        private string NextSequence()
        {
            var offset = Counter;

            var next = string.Empty;
            var progress = string.Empty;
            if (offset < AnimationLength)
            {
                if (offset < ProgressMaxLength)
                {
                    progress = progress.PadRight(offset, ProgressChar);
                    offset -= progress.Length;
                }
                else
                {
                    progress = progress.PadRight(ProgressMaxLength, ProgressChar);
                    offset -= ProgressMaxLength;
                }

                offset += TemplatePadding + 1;

                next = $"{Template[0..offset]}{progress}{Template[(offset + progress.Length - 1)..]}";
            }
            else
            {
                progress = progress.PadRight(AnimationLength - (Counter - ProgressMaxLength), ProgressChar);

                offset += TemplatePadding;

                next = $"{Template[0..(AnimationLength + TemplatePadding - progress.Length)]}{progress}{Template[(AnimationLength + TemplatePadding - 1)..]}";
            }

            if (Counter >= AnimationLength + ProgressMaxLength)
                Counter = 1;
            else
                Counter++;

            return next;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Dispose pattern
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ThreadActive = false;
                    if (InteractiveCli)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.WriteLine("                            Complete".PadRight(Template.Length));
                        Console.WriteLine();
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(" Complete");
                    }
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        /// <summary>
        /// Dispose Pattern
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
