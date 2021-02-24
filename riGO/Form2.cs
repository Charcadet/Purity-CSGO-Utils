using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace riGO
{
    public partial class Form2 : Form
    {



        public const string gamename = "Counter-Strike: Global Offensive";

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr window, int index);
        IntPtr handle = FindWindowByCaption(IntPtr.Zero, gamename);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        public struct RECT
        {
            public int left, top, right, bottom;
        }

        System.Drawing.Graphics g;

        public Form2()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+?><}{][.,";
            var stringChars = new char[16];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            InitializeComponent();
            this.Text = finalString;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            RECT outrect;
            GetWindowRect(handle, out outrect);

            this.Size = new Size(outrect.right - outrect.left, outrect.bottom - outrect.top);

            this.Top = outrect.top;
            this.Left = outrect.left;

            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = System.Drawing.Color.Black;
            this.TransparencyKey = System.Drawing.Color.Black;

            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            this.TopMost = true;

            Form2.CheckForIllegalCrossThreadCalls = false;

            Thread PrePaintThread = new Thread(new ThreadStart(prepaint));
            PrePaintThread.Start();
        }

        private void prepaint()
        {
            while (true)
            {
                this.Refresh();
                System.Threading.Thread.Sleep(50);
            }
        }

        private void painttext(System.Drawing.Graphics g)
        {
            Font bigFont = new Font("Arial", 14);
            Brush mybrush = new SolidBrush(Color.White);
            g.DrawString("Rico | CS:GO Utils", bigFont, mybrush, 3, 3);
        }

        private void paintrect(System.Drawing.Graphics g)
        {
            SolidBrush blackpen = new SolidBrush(Color.DarkSlateBlue);
            Rectangle rect = new Rectangle(3, 3, 165, 22);
            g.FillRectangle(blackpen, rect);
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            paintrect(g);
            painttext(g);

        }
    }
}
