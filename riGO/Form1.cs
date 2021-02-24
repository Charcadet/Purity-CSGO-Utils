using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Media;

namespace riGO
{
    public partial class RicoCSGO : Form
    {

        public static string process = "csgo";
        public static int baseClient;
        public static int baseEngine;

        private Module glowESP;
        private Module noFlash;
        private Module rageBHop;

        VAMemory memory = new VAMemory(process);

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        

        public RicoCSGO()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+?><}{][.,";
            var stringChars = new char[16];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            if (GetModuleAddress())
            {

                glowESP = new GlowESP(memory, baseClient, baseEngine);
                noFlash = new NoFlash(memory, baseClient, baseEngine);
                rageBHop = new RageBHOP(memory, baseClient, baseEngine);
                InitializeComponent();
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                nameLabel.Text = "Hey there, " + userName;
                this.Text = finalString;

            }
            else
            {
                this.Text = finalString;
                MessageBox.Show("CS:GO is not open.");
                Environment.Exit(0);
            }




        }


        static bool GetModuleAddress()
        {
            try
            {
                Process[] p = Process.GetProcessesByName(process);

                if (p.Length > 0)
                {
                    foreach (ProcessModule m in p[0].Modules)
                    {

                        if (m.ModuleName == "engine.dll")
                        {
                            baseEngine = (int)m.BaseAddress;
                        }

                        if (m.ModuleName == "client.dll")
                        {
                            baseClient = (int)m.BaseAddress;
                            return true;
                        }

                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        private void ESPCheck_OnChange(object sender, EventArgs e)
        {
            glowESP.toggle();
            SoundPlayer audio = new SoundPlayer(riGO.Properties.Resources.click); // here WindowsFormsApplication1 is the namespace and Connect is the audio file name
            audio.Play();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            SoundPlayer audio = new SoundPlayer(riGO.Properties.Resources.click); // here WindowsFormsApplication1 is the namespace and Connect is the audio file name
            audio.Play();
        }

        private void Titlebar_Click(object sender, EventArgs e)
        {

        }

        private void NoFlashCheck_OnChange(object sender, EventArgs e)
        {
            noFlash.toggle();
            int LocalPlayer = memory.ReadInt32((IntPtr)baseClient + Offsets.dwLocalPlayer);
            memory.WriteFloat((IntPtr)LocalPlayer + Offsets.m_flFlashMaxAlpha, 255.0f);
            SoundPlayer audio = new SoundPlayer(riGO.Properties.Resources.click); // here WindowsFormsApplication1 is the namespace and Connect is the audio file name
            audio.Play();
        }
        private void RageBHOPCheck_OnChange_1(object sender, EventArgs e)
        {
            rageBHop.toggle();
        }

        private void RageHOPCheck_OnChange(object sender, EventArgs e)
        {
            rageBHop.toggle();
        }

        private void bunifuCheckbox2_OnChange(object sender, EventArgs e)
        {
            SoundPlayer audio = new SoundPlayer(riGO.Properties.Resources.click); // here WindowsFormsApplication1 is the namespace and Connect is the audio file name
            audio.Play();
            if (bunifuCheckbox2.Checked)
            {
                Form2 f2 = new Form2();
                f2.Visible = true;
            }
            else
            {
                Application.OpenForms["Form2"].Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Ricozyx");
            SoundPlayer audio = new SoundPlayer(riGO.Properties.Resources.poggers); // here WindowsFormsApplication1 is the namespace and Connect is the audio file name
            audio.Play();
        }
    }

}
