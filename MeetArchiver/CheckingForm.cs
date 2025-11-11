using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MeetArchiver
{
    /// <summary>
    /// Simple non-modal busy form that runs on its own STA thread so the parent thread
    /// may block/wait without freezing this form.
    /// Usage:
    ///     CheckingForm.Show("Checking divers...");   // before blocking call
    ///     ... blocking work ...
    ///     CheckingForm.Close();                      // after work completes
    /// </summary>
    public class CheckingForm : Form
    {
        private readonly Label _messageLabel;
        private readonly ProgressBar _progress;
        private readonly PictureBox _icon;

        // single STA thread + form instance
        private static Thread? s_thread;
        private static CheckingForm? s_instance;
        private static readonly object s_lock = new object();

        public CheckingForm(string message)
        {
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            ControlBox = false;
            Width = 420;
            Height = 140;
            BackColor = SystemColors.Window;

            _icon = new PictureBox
            {
                Size = new Size(48, 48),
                Location = new Point(16, 20),
                SizeMode = PictureBoxSizeMode.CenterImage
            };
            // Simple built-in look: draw a spinning glyph using system icon isn't available,
            // leave PictureBox empty so layout looks balanced.
            Controls.Add(_icon);

            _messageLabel = new Label
            {
                AutoSize = false,
                Location = new Point(80, 10),
                Size = new Size(320, 50),
                Text = message ?? "Please wait...",
                Font = new Font(FontFamily.GenericSansSerif, 10f),
                TextAlign = ContentAlignment.MiddleLeft
            };
            Controls.Add(_messageLabel);

            _progress = new ProgressBar
            {
                Location = new Point(80, 70),
                Size = new Size(320, 18),
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30
            };
            Controls.Add(_progress);
        }

        /// <summary>
        /// Show the busy form on a dedicated STA thread. Safe to call from any thread.
        /// If already visible this updates the message.
        /// </summary>
        public  void Show(string? message = null)
        {
            lock (s_lock)
            {
                if (s_instance != null && !s_instance.IsDisposed)
                {
                    if (!string.IsNullOrEmpty(message))
                        s_instance.BeginInvoke((Action)(() => s_instance._messageLabel.Text = message));
                    return;
                }

                s_thread = new Thread(() =>
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    s_instance = new CheckingForm(message ?? "Checking divers against website. This may take some time...");
                    try
                    {
                        Application.Run(s_instance);
                    }
                    finally
                    {
                        // ensure static references cleared when form loop ends
                        lock (s_lock)
                        {
                            s_instance = null;
                            s_thread = null;
                        }
                    }
                });

                s_thread.SetApartmentState(ApartmentState.STA);
                s_thread.IsBackground = true;
                s_thread.Start();

                // Wait briefly for form to be created so caller can block immediately and still see it.
                int waited = 0;
                while (s_instance == null && waited < 2000)
                {
                    Thread.Sleep(25);
                    waited += 25;
                }
            }
        }

        /// <summary>
        /// Close the busy form if visible. Safe to call from any thread.
        /// </summary>
        public void Close()
        {
            
            lock (s_lock)
            {
                if (s_instance == null) return;

                try
                {
                    if (s_instance.IsHandleCreated && !s_instance.IsDisposed)
                    {
                        s_instance.BeginInvoke((Action)(() =>
                        {
                            try { s_instance.Close(); }
                            catch { /* swallow */ }
                        }));
                    }
                }
                catch
                {
                    // ignored
                }
                finally
                {
                    // Wait for thread to exit (short timeout)
                    var t = s_thread;
                    if (t != null && t.IsAlive)
                    {
                        t.Join(2000);
                    }
                    s_instance = null;
                    s_thread = null;
                }
            }
        }
    }
}