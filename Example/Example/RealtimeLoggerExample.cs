using System;
using System.Windows.Forms;
using NLog;

namespace RealtimeLogger
{
    public partial class RealtimeLoggerExample : Form
    {
        /// <summary>
        /// The logger for this example.
        /// </summary>
        private static Logger logger = LogManager.GetLogger("E");

        /// <summary>
        /// The form constructor.
        /// </summary>
        public RealtimeLoggerExample()
        {
            InitializeComponent();

            // bind the AppendLog method in this class to the 
            RealtimeLogger.LogArrived += AppendLog;
        }

        /// <summary>
        /// The event handler for the "Add Log" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddLog_Click(object sender, EventArgs e)
        {
            logger.Info("Hello World!");
        }

        /// <summary>
        /// Fired when the RealtimeLogger receives a new log message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppendLog(object sender, RealtimeLoggerEventArgs e)
        {
            //threadsafe
            this.Invoke((MethodInvoker)delegate
            {
                string msg = e.DateTime + " | " + e.Message;
                txtLogs.Text += msg + Environment.NewLine;
            });
        }

        /// <summary>
        /// The event handler for the form load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RealtimeLoggerExample_Load(object sender, EventArgs e)
        {
            logger.Info("Form loaded.");
        }
    }
}
