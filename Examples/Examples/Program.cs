using System;
using System.Windows.Forms;

namespace NLog.RealtimeLogger
{
    /// <summary>
    ///     Represents the Example application.
    /// </summary>
    public static class Program
    {
        #region Public Methods

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RealtimeLoggerExample());
        }

        #endregion Public Methods
    }
}