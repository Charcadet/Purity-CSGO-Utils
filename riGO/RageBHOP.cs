using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace riGO
{
    class RageBHOP : Module
    {
        private volatile bool running = false;
        private VAMemory memory;
        private int baseClient;
        private int baseEngine;

        public RageBHOP(VAMemory memory, int baseClient, int baseEngine)
        {
            this.memory = memory;
            this.baseClient = baseClient;
            this.baseEngine = baseEngine;
        }

        public string getName()
        {
            return "RageBHOP";
        }

        public bool isRunning()
        {
            return running;
        }

        public void start()
        {
            if (!running)
            {
                running = true;
                new Thread(run).Start();
                Console.WriteLine(getName() + " activated");

            }
            else
            {
                Console.WriteLine(getName() + " is already active");
            }
        }

        public void stop()
        {
            if (running)
            {
                running = false;
                Console.WriteLine(getName() + " deactivated");
            }
            else
            {
                Console.WriteLine(getName() + " is not active");
            }
        }

        public void toggle()
        {
            if (running)
            {
                stop();
            }
            else
            {
                start();
            }
        }

        private void run()
        {
            int localPlayer = 0;
            Thread.CurrentThread.IsBackground = true;
            while (running)
            {
                int LocalPlayer = memory.ReadInt32((IntPtr)baseClient + Offsets.dwLocalPlayer);

                int i = 1;
                int address;
                address = baseClient + Offsets.dwEntityList + (i - 1) * COff.EntityLoopDistance;

                byte oFlags = memory.ReadByte((IntPtr)LocalPlayer + Offsets.m_fFlags);
                
                [DllImport("user32.dll")]
                static extern short GetAsyncKeyState(Keys vKey);

                [DllImport("user32.dll")]
                static extern void mouse_event(int a, int b, int c, int d, int swed);

                int m3DOWN = 0x0020;
                int m3UP = 0x0040;

                if (GetAsyncKeyState(Keys.Space)<0)
                {
                    
      
                }
                Thread.Sleep(10);
            }
        }
    }
}
