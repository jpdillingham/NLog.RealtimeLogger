/*
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀  ▀  ▀      ▀▀
      █
      █      ▄████████                                                                   ▄█
      █     ███    ███                                                                  ███
      █    ▄███▄▄▄▄██▀    ▄█████   ▄█████   █           ██     █     ▄▄██▄▄▄     ▄█████ ███        ██████     ▄████▄     ▄████▄     ▄█████    █████
      █   ▀▀███▀▀▀▀▀     ██   █    ██   ██ ██       ▀███████▄ ██   ▄█▀▀██▀▀█▄   ██   █  ███       ██    ██   ██    ▀    ██    ▀    ██   █    ██  ██
      █   ▀███████████  ▄██▄▄      ██   ██ ██           ██  ▀ ██▌  ██  ██  ██  ▄██▄▄    ███       ██    ██  ▄██        ▄██        ▄██▄▄     ▄██▄▄█▀
      █     ███    ███ ▀▀██▀▀    ▀████████ ██           ██    ██   ██  ██  ██ ▀▀██▀▀    ███       ██    ██ ▀▀██ ███▄  ▀▀██ ███▄  ▀▀██▀▀    ▀███████
      █     ███    ███   ██   █    ██   ██ ██▌    ▄     ██    ██   ██  ██  ██   ██   █  ███▌    ▄ ██    ██   ██    ██   ██    ██   ██   █    ██  ██
      █     ███    ███   ███████   ██   █▀ ████▄▄██    ▄██▀   █     █  ██  █    ███████ █████▄▄██  ██████    ██████▀    ██████▀    ███████   ██  ██
      █
 ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄  ▄▄ ▄▄   ▄▄▄▄ ▄▄     ▄▄     ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄ ▄
 █████████████████████████████████████████████████████████████ ███████████████ ██  ██ ██   ████ ██     ██     ████████████████ █ █
      ▄
      █  The RealtimeLogger class works in conjunction with the NLog 'MethodCall' logging target to expose log messages via an event
      █  in real time.
      █
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀ ▀ ▀▀▀     ▀▀               ▀
      █  The MIT License (MIT)
      █
      █  Copyright (c) 2016-2017 JP Dillingham (jp@dillingham.ws)
      █
      █  Permission is hereby granted, free of charge, to any person obtaining a copy
      █  of this software and associated documentation files (the "Software"), to deal
      █  in the Software without restriction, including without limitation the rights
      █  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
      █  copies of the Software, and to permit persons to whom the Software is
      █  furnished to do so, subject to the following conditions:
      █
      █  The above copyright notice and this permission notice shall be included in all
      █  copies or substantial portions of the Software.
      █
      █  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
      █  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
      █  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
      █  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
      █  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
      █  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
      █  SOFTWARE.
      █
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀     ▀▀▀
      █  Dependencies:
      █     └─ NLog (https://www.nuget.org/packages/NLog/)
      █
      ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀  ▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀██
                                                                                                   ██
                                                                                               ▀█▄ ██ ▄█▀
                                                                                                 ▀████▀
                                                                                                   ▀▀                              */

using System;
using System.Collections.Generic;

namespace NLog.RealtimeLogger
{
    /// <summary>
    ///     The <see cref="RealtimeLogger"/> class acts as a target for the NLog method logging target; it fires the
    ///     <see cref="LogAppended"/> event when new log messages are created by NLog.
    /// </summary>
    public static class RealtimeLogger
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RealtimeLogger"/> class.
        /// </summary>
        static RealtimeLogger()
        {
            LogHistory = new Queue<RealtimeLoggerEventArgs>();
        }

        #endregion Constructors

        #region Events

        /// <summary>
        ///     The Changed event is fired when new log messages are created by NLog.
        /// </summary>
        public static event EventHandler<RealtimeLoggerEventArgs> LogAppended;

        #endregion Events

        #region Properties

        /// <summary>
        ///     Gets a queue containing the newest log messages, up to the LogHistoryLimit.
        /// </summary>
        public static Queue<RealtimeLoggerEventArgs> LogHistory { get; private set; }

        /// <summary>
        ///     Gets the maximum number of log messages to store in the log history queue.
        /// </summary>
        /// <remarks>
        ///     If the value is reduced while the log is populated, the length of the LogHistory queue will be reduced to the
        ///     desired value upon the addition of the next log.
        /// </remarks>
        /// <exception cref="FormatException">
        ///     Thrown when the value specified in the LogHistoryLimit NLog configuration variable can not be parsed to an integer.
        /// </exception>
        public static int LogHistoryLimit
        {
            get
            {
                int logHistoryLimit = 300;

                // retrieve the value from the NLog configuration variable "RealtimeLogger.LogHistoryLimit", if it exists.
                if (LogManager.Configuration != default(Config.LoggingConfiguration) && LogManager.Configuration.Variables.ContainsKey("RealtimeLogger.LogHistoryLimit"))
                {
                    string value = LogManager.Configuration.Variables["RealtimeLogger.LogHistoryLimit"].Text;

                    // try to parse an integer from the specified value. throw a FormatException if the parse fails.
                    if (!int.TryParse(value, out logHistoryLimit))
                    {
                        throw new FormatException("The configured value for RealtimeLogger.LogHistoryLimit ('" + value + "') is invalid.  The value must be an integer.");
                    }
                }

                return logHistoryLimit;
            }
        }

        #endregion Properties

        #region Methods

        #region Public Methods

        #region Public Static Methods

        /// <summary>
        ///     Called by the NLog method logging target, this method fires the Changed event with the thread ID, timestamp, level,
        ///     logger and message associated with the new log message.
        /// </summary>
        /// <param name="threadID">The ID of the thread that originated the log message.</param>
        /// <param name="dateTime">The timestamp of the log message in long date format.</param>
        /// <param name="level">The level of the log message.</param>
        /// <param name="logger">The logger instance that generated the message.</param>
        /// <param name="message">The log message.</param>
        public static void AppendLog(string threadID, string dateTime, string level, string logger, string message)
        {
            RealtimeLoggerEventArgs eventArgs = new RealtimeLoggerEventArgs(threadID, dateTime, level, logger, message);

            AppendLogHistory(eventArgs);

            if (LogAppended != null)
            {
                LogAppended(default(object), eventArgs);
            }
        }

        #endregion Public Static Methods

        #endregion Public Methods

        #region Private Methods

        #region Private Static Methods

        /// <summary>
        ///     Enqueues the supplied <see cref="RealtimeLoggerEventArgs"/> instance to the LogHistory queue. If the queue exceeds
        ///     200 entries, the oldest log is first de-queued before the new log is enqueued.
        /// </summary>
        /// <param name="eventArgs">The event args instance to enqueue.</param>
        private static void AppendLogHistory(RealtimeLoggerEventArgs eventArgs)
        {
            LogHistory.Enqueue(eventArgs);

            if (LogHistory.Count > LogHistoryLimit)
            {
                PruneLogHistory();
            }
        }

        /// <summary>
        ///     Repeatedly De-queues logs from the LogHistory queue until the queue length matches LogHistoryLimit.
        /// </summary>
        private static void PruneLogHistory()
        {
            while (LogHistory.Count > LogHistoryLimit)
            {
                LogHistory.Dequeue();
            }
        }

        #endregion Private Static Methods

        #endregion Private Methods

        #endregion Methods
    }
}