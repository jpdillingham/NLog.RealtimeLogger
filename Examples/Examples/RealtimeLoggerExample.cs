using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace NLog.RealtimeLogger
{
    /// <summary>
    ///     Represents the application form.
    /// </summary>
    public partial class RealtimeLoggerExample : Form
    {
        #region Private Fields

        /// <summary>
        ///     The logger for this example.
        /// </summary>
        private static Logger logger = LogManager.GetLogger("E");

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RealtimeLoggerExample"/> class.
        /// </summary>
        public RealtimeLoggerExample()
        {
            InitializeComponent();

            // bind the AppendLog method in this class to the
            RealtimeLogger.LogAppended += AppendLog;
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        ///     Fired when the <see cref="RealtimeLogger"/> receives a new log message.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void AppendLog(object sender, RealtimeLoggerEventArgs e)
        {
            // threadsafe
            this.Invoke((MethodInvoker)delegate
            {
                string msg = e.DateTime + " | " + e.Message;
                txtLogs.Text += msg + Environment.NewLine;
            });
        }

        /// <summary>
        ///     The event handler for the "Add Log" button.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private void btnAddLog_Click(object sender, EventArgs e)
        {
            logger.Info("Hello World!");
        }

        /// <summary>
        ///     The event handler for the form load event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void RealtimeLoggerExample_Load(object sender, EventArgs e)
        {
            logger.Info("Form loaded.");
        }

        #endregion Private Methods
    }
}