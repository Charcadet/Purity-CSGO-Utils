using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace riGO
{
    public static class Offsets
    {
        // UPDATED OFFSETS AT: 2/24/2021
        public static Int32 dwClientState = 0x58EFE4;
        public static Int32 dwLocalPlayer = 0xD8B2BC;
        public static Int32 dwGlowObjectManager = 0x52EB540;
        public static Int32 dwEntityList = 0x4DA2F44;

        public static Int32 m_iHealth = 0x100;
        public static Int32 m_iGlowIndex = 0xA438;
        public static Int32 m_bSpotted = 0x93D;
        public static Int32 m_bSpottedByMask = 0x980;
        public static Int32 m_flFlashDuration = 0xA420;
        public const Int32 m_fFlags = 0x104;
        public const Int32 dwForceJump = 0x524CEA4;
        public const Int32 m_flFlashMaxAlpha = 0xA41C;
        public const Int32 m_iTeamNum = 0xF4;
        public const Int32 m_bDormant = 0xED;
    }
}
