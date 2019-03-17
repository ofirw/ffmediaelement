using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unosquare.FFME.GDI.Sample
{
    public partial class Form1 : Form
    {
        private MediaPlayer player = new MediaPlayer();

        public Form1()
        {
            InitializeComponent();
            this.player.OnFrame += this.Player_OnFrame;
        }

        private void Player_OnFrame(Bitmap obj)
        {
            this.panel1.BackgroundImage = obj;
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            await this.player.Open(new Uri("rtsp://184.72.239.149/vod/mp4:BigBuckBunny_175k.mov"));
            await this.player.Play();
        }
    }
}
