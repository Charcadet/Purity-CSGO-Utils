using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace riGO
{
    class NoFlash : Module
    {
        private volatile bool running = false;
        private VAMemory memory;
        private int baseClient;
        private int baseEngine;

        public NoFlash(VAMemory memory, int baseClient, int baseEngine)
        {
            this.memory = memory;
            this.baseClient = baseClient;
            this.baseEngine = baseEngine;
        }

        public string getName()
        {
            return "NoFlash";
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

                    float flashAlpha = memory.ReadFloat((IntPtr)LocalPlayer + Offsets.m_flFlashMaxAlpha);
                    if (flashAlpha > 0.0f)
                    {
                        memory.WriteFloat((IntPtr)LocalPlayer + Offsets.m_flFlashMaxAlpha, 100.0f);
                    }
                    else if (flashAlpha == 0.0f)
                    {
                        memory.WriteFloat((IntPtr)LocalPlayer + Offsets.m_flFlashMaxAlpha, 100.0f);
                    }



            }
        }
    }
}
