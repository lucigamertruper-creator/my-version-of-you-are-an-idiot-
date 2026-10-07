using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace ModernMockApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form mainForm = new Form();
            mainForm.Text = "YOU ARE AN IDIOT";
            mainForm.Size = new Size(500, 450);
            mainForm.StartPosition = FormStartPosition.CenterScreen;
            mainForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            mainForm.MaximizeBox = false;

            string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fixed-pcm-audio.wav");
            string gifPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "you-are-an-idiot-long.gif");

            PictureBox pb = new PictureBox();
            pb.Dock = DockStyle.Fill;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.BackColor = Color.White;

            if (File.Exists(gifPath)) {
                pb.Image = Image.FromFile(gifPath);
            } else {
                Label errorLabel = new Label();
                errorLabel.Text = "you are an idiot\n\n☺ ☺ ☺";
                errorLabel.Font = new Font("Arial", 28, FontStyle.Bold);
                errorLabel.TextAlign = ContentAlignment.MiddleCenter;
                errorLabel.Dock = DockStyle.Fill;
                mainForm.Controls.Add(errorLabel);
            }
            mainForm.Controls.Add(pb);

            SoundPlayer player = null;
            if (File.Exists(audioPath)) {
                player = new SoundPlayer(audioPath);
                mainForm.Load += (sender, e) => {
                    try {
                        player.PlayLooping();
                    } catch { }
                };
            }

            mainForm.FormClosing += (sender, e) => {
                if (player != null) {
                    player.Stop();
                    player.Dispose();
                }
            };

            Application.Run(mainForm);
        }
    }
}
