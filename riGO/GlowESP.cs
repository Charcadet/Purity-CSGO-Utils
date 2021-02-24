using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace riGO
{
    class GlowESP : Module
    {
        private volatile bool running = false;
        private VAMemory memory;
        private int baseClient;
        private int baseEngine;

        public GlowESP(VAMemory memory, int baseClient, int baseEngine)
        {
            this.memory = memory;
            this.baseClient = baseClient;
            this.baseEngine = baseEngine;
        }

        public string getName()
        {
            return "GlowESP";
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

        public struct GlowStruct
        {
            public float r;
            public float g;
            public float b;
            public float a;
            public bool rwo;
            public bool rwuo;
        }

        GlowStruct EnemyTeamColor = new GlowStruct()
        {
            r = 1,
            g = 0,
            b = 0,
            a = 0.8f,
            rwo = true,
            rwuo = false
        };

        GlowStruct EnemyTeamColorSpotted = new GlowStruct()
        {
            r = 0,
            g = 1,
            b = 0,
            a = 0.8f,
            rwo = true,
            rwuo = false
        };

        private void run()
        {
            int localPlayer = 0;
            Thread.CurrentThread.IsBackground = true;
            while (running)
            {
                int LocalPlayer = memory.ReadInt32((IntPtr)baseClient + Offsets.dwLocalPlayer);
                int MyTeam = memory.ReadInt32((IntPtr)LocalPlayer + Offsets.m_iTeamNum);

                int i = 1;
                int address;
                do
                {

                    address = baseClient + Offsets.dwEntityList + (i - 1) * COff.EntityLoopDistance;
                    int EntityList = memory.ReadInt32((IntPtr)address);

                    address = EntityList + Offsets.m_iTeamNum;
                    int HisTeam = memory.ReadInt32((IntPtr)address);

                    address = EntityList + Offsets.m_bSpotted;
                    bool isSpotted = memory.ReadBoolean((IntPtr)address);

                    address = EntityList + Offsets.m_bSpottedByMask;
                    int EnemySpotted = memory.ReadInt32((IntPtr)address);


                    address = EntityList + Offsets.m_bDormant;
                    bool isDormant = memory.ReadBoolean((IntPtr)address);

                    if (!isDormant && EntityList != 0)
                    {
                        address = EntityList + Offsets.m_iGlowIndex;

                        int GlowIndex = memory.ReadInt32((IntPtr)address);

                        if (MyTeam != HisTeam)
                        {
                            if (localPlayer > 0)
                            {
                                if ((EnemySpotted & (1 << localPlayer - 1)) != 0)
                                {
                                    address = baseClient + Offsets.dwGlowObjectManager;
                                    int GlowObject = memory.ReadInt32((IntPtr)address);

                                    int calculation = GlowIndex * 0x38 + 0x4;
                                    int current = GlowObject + calculation;
                                    memory.WriteFloat((IntPtr)current, EnemyTeamColorSpotted.r);

                                    calculation = GlowIndex * 0x38 + 0x8;
                                    current = GlowObject + calculation;
                                    memory.WriteFloat((IntPtr)current, EnemyTeamColorSpotted.g);

                                    calculation = GlowIndex * 0x38 + 0xC;
                                    current = GlowObject + calculation;
                                    memory.WriteFloat((IntPtr)current, EnemyTeamColorSpotted.b);

                                    calculation = GlowIndex * 0x38 + 0x10;
                                    current = GlowObject + calculation;
                                    memory.WriteFloat((IntPtr)current, EnemyTeamColorSpotted.a);

                                    calculation = GlowIndex * 0x38 + 0x24;
                                    current = GlowObject + calculation;
                                    memory.WriteBoolean((IntPtr)current, EnemyTeamColorSpotted.rwo);

                                    calculation = GlowIndex * 0x38 + 0x25;
                                    current = GlowObject + calculation;
                                    memory.WriteBoolean((IntPtr)current, EnemyTeamColorSpotted.rwuo);
                                }
                                else
                                {
                                    address = baseClient + Offsets.dwGlowObjectManager;
                                    int GlowObject = memory.ReadInt32((IntPtr)address);

                                    int calculation = GlowIndex * 0x38 + 0x4;
                                    int current = GlowObject + calculation;
                                    memory.WriteFloat((IntPtr)current, EnemyTeamColor.r);

                                    calculation = GlowIndex * 0x38 + 0x8;
                                    current = GlowObject + calculation;
                                    memory.WriteFloat((IntPtr)current, EnemyTeamColor.g);

                                    calculation = GlowIndex * 0x38 + 0xC;
                                    current = GlowObject + calculation;
                                    memory.WriteFloat((IntPtr)current, EnemyTeamColor.b);

                                    calculation = GlowIndex * 0x38 + 0x10;
                                    current = GlowObject + calculation;
                                    memory.WriteFloat((IntPtr)current, EnemyTeamColor.a);

                                    calculation = GlowIndex * 0x38 + 0x24;
                                    current = GlowObject + calculation;
                                    memory.WriteBoolean((IntPtr)current, EnemyTeamColor.rwo);

                                    calculation = GlowIndex * 0x38 + 0x25;
                                    current = GlowObject + calculation;
                                    memory.WriteBoolean((IntPtr)current, EnemyTeamColor.rwuo);
                                }
                            }

                        }
                        else
                        {
                            if (EntityList == LocalPlayer)
                            {
                                localPlayer = i;
                            }
                        }
                    }
                    i++;
                }
                while (i < 64);


               Thread.Sleep(1);

            }
        }
    }
}
