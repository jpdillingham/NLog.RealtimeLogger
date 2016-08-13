using System;
using NLog.Config;
using NLog.Targets;
using Xunit;

namespace NLog.RealtimeLogger.Tests
{
    /// <summary>
    ///     Tests for the <see cref="RealtimeLogger"/> class. 
    /// </summary>
    public class RealtimeLoggerTests
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RealtimeLoggerTests"/> class. 
        /// </summary>
        public RealtimeLoggerTests()
        {
            // configure the logger with a debugger target
            LoggingConfiguration config = new LoggingConfiguration();
            DebuggerTarget debug = new DebuggerTarget();
            config.AddTarget("debug", debug);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, debug));

            LogManager.Configuration = config;
        }

        /// <summary>
        ///     Tests the <see cref="RealtimeLogger.LogHistoryLimit"/> property.
        /// </summary>
        [Fact]
        public void LogHistoryLimit()
        {
            SetVariable("RealtimeLogger.LogHistoryLimit", "500");
            Assert.Equal(500, RealtimeLogger.LogHistoryLimit);
        }

        /// <summary>
        ///     Tests the <see cref="RealtimeLogger.LogHistoryLimit"/> property with a known bad value.
        /// </summary>
        [Fact]
        public void LogHistoryLimitBad()
        {
            SetVariable("RealtimeLogger.LogHistoryLimit", "one");

            int val;

            Assert.Throws(typeof(FormatException), () => val = RealtimeLogger.LogHistoryLimit);
        }

        /// <summary>
        ///     Tests <see cref="RealtimeLogger.AppendLog(string, string, string, string, string)"/>.
        /// </summary>
        /// <param name="threadID">The ThreadID of the log.</param>
        /// <param name="dateTime">The DateTime of the log.</param>
        /// <param name="logLevel">The LogLevel of the log.</param>
        /// <param name="logger">The logger for the log.</param>
        /// <param name="message">The message for the log.</param>
        [Theory]
        [InlineData("1", "8/12/2016 9:10", "Trace", "Test", "Hello World!")]
        [InlineData("test", "test", "test", "test", "test")]
        public void AppendLog(string threadID, string dateTime, string logLevel, string logger, string message)
        {
            RealtimeLogger.AppendLog(threadID, dateTime, logLevel, logger, message);
        }

        /// <summary>
        ///     Tests the constructor of <see cref="RealtimeLoggerEventArgs"/>.
        /// </summary>
        [Fact]
        public void EventArgs()
        {
            DateTime dt = DateTime.Now;
            RealtimeLoggerEventArgs e = new RealtimeLoggerEventArgs("3", dt.ToString(), "Trace", "Test", "Hello World!");

            Assert.Equal(e.ThreadID, 3);
            Assert.Equal(e.DateTime.ToString(), dt.ToString());
            Assert.Equal(e.Level, LogLevel.Trace);
            Assert.Equal(e.Logger, "Test");
            Assert.Equal(e.Message, "Hello World!");
        }

        /// <summary>
        ///     Tests the constructor of <see cref="RealtimeLoggerEventArgs"/> with "bad" data.
        /// </summary>
        [Fact]
        public void EventArgsBad()
        {
            RealtimeLoggerEventArgs e = new RealtimeLoggerEventArgs("test", "test", "test", "test", "test");

            Assert.Equal(e.ThreadID, 1);
            Assert.IsType<DateTime>(e.DateTime);
            Assert.Equal(e.Level, LogLevel.Info);
            Assert.Equal(e.Logger, "test");
            Assert.NotEmpty(e.Message);
        }

        /// <summary>
        ///     Sets the specified variable in the NLog configuration to the specified value.
        /// </summary>
        /// <param name="variable">The variable to set.</param>
        /// <param name="value">The value to which to set the variable.</param>
        private void SetVariable(string variable, string value)
        {
            if (LogManager.Configuration != default(Config.LoggingConfiguration))
            {
                if (LogManager.Configuration.Variables.ContainsKey(variable))
                {
                    LogManager.Configuration.Variables[variable] = new Layouts.SimpleLayout(value);
                }
                else
                {
                    LogManager.Configuration.Variables.Add(variable, new Layouts.SimpleLayout(value));
                }
            }
        }
    }
}
