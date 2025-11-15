using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MeetArchiver
{
    /// <summary>
    /// Simple non-modal busy form that runs on its own STA thread so the parent thread
    /// may block/wait or perform heavy work without freezing this form.
    /// Usage:
    ///     WorkingForm.Show("This may take some time...");
    ///     ... blocking work on the main thread ...
    ///     WorkingForm.Close();
    /// </summary>
    public class WorkingForm : Form
    {
        private readonly Label _messageLabel;
        private readonly ProgressBar _progress;

        // single STA thread + form instance
        private static Thread? s_thread;
        private static WorkingForm? s_instance;
        private static readonly object s_lock = new object();

        // Private ctor - use static API
        private WorkingForm(string message)
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
            TopMost = true;

            _messageLabel = new Label
            {
                AutoSize = false,
                Location = new Point(16 + 48 + 8, 12),
                Size = new Size(360, 48),
                Text = message ?? "This may take some time...",
                Font = new Font(FontFamily.GenericSansSerif, 10f),
                TextAlign = ContentAlignment.MiddleLeft
            };
            Controls.Add(_messageLabel);

            _progress = new ProgressBar
            {
                Location = new Point(16 + 48 + 8, 72),
                Size = new Size(300, 18),
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30
            };
            Controls.Add(_progress);

            // Optional icon placeholder to balance layout
            var icon = new PictureBox
            {
                Size = new Size(48, 60),
                Location = new Point(16, 20),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Properties.Resources.DiverCheck // fallback to existing resource
            };
            Controls.Add(icon);
        }

        /// <summary>
        /// Show the busy form on a dedicated STA thread. Safe to call from any thread.
        /// If already visible this updates the message.
        /// </summary>
        public static void Show(string? message = null)
        {
            lock (s_lock)
            {
                if (s_instance != null && !s_instance.IsDisposed)
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        try
                        {
                            if (s_instance.IsHandleCreated && !s_instance.IsDisposed)
                                s_instance.BeginInvoke((Action)(() => s_instance._messageLabel.Text = message));
                            else
                                s_instance._messageLabel.Text = message;
                        }
                        catch
                        {
                            // ignore update failure
                        }
                    }
                    return;
                }

                s_thread = new Thread(() =>
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    s_instance = new WorkingForm(message ?? "This may take some time...");
                    try
                    {
                        Application.Run(s_instance);
                    }
                    finally
                    {
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
        public static void Close()
        {
            lock (s_lock)
            {
                if (s_instance == null) return;

                var form = s_instance;
                try
                {
                    if (form.IsHandleCreated && !form.IsDisposed)
                    {
                        // Invoke the real Form.Close on the UI thread.
                        form.BeginInvoke((Action)(() =>
                        {
                            try
                            {
                                ((Form)form).Close(); // ensure we call Form.Close, not this static helper
                            }
                            catch
                            {
                                // swallow
                            }
                        }));
                    }
                    else
                    {
                        try { form.Dispose(); }
                        catch { /* swallow */ }
                    }
                }
                catch
                {
                    // ignored
                }
                finally
                {
                    var t = s_thread;
                    if (t != null && t.IsAlive)
                    {
                        t.Join(200);
                    }
                    s_instance = null;
                    s_thread = null;
                }
            }
        }
    }
}