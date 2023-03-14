using PropertyHook;
using System;

namespace PropertyHookSample
{
    public class SampleHookPTDE : PHook
    {
        private PHPointer WorldChrBase;
        private PHPointer CharData1;
        private PHPointer ChrData2;
        private PHPointer AiTimer;

        public SampleHookPTDE() : base(5000, 5000, p => p.ProcessName == "DARKSOULS")
        {
            AiTimer = RegisterAbsoluteAOB(DSOffsets.AiTimerAOB, DSOffsets.AiTimerOffset1, DSOffsets.AiTimerOffset2);
        }

        public float AiTimerValue => AiTimer.ReadSingle(DSOffsets.AiTimerOffset3);
    }

    public class SampleHookDSR : PHook
    { 
        private PHPointer WorldChrBase;
        private PHPointer ChrData1;
        private PHPointer ChrData2;
        private PHPointer AiTimer;

        public const string AiTimerAOB = "48 8b 0d ? ? ? ? 48 85 c9 74 0e 48 83 c1 28";
        public const int AiTimerOffset1 = 0;
        public const int AiTimerOffset2 = 0x24;

        public SampleHookDSR() : base(5000, 5000, p => p.ProcessName == "DarkSoulsRemastered")
        {
            WorldChrBase = CreateBasePointer((IntPtr)0x141D151B0, 0);
            ChrData1 = CreateChildPointer(WorldChrBase, 0x68);
            ChrData2 = RegisterRelativeAOB("48 8B 05 ? ? ? ? 48 85 C0 ? ? F3 0F 58 80 AC 00 00 00", 3, 7, 0, 0x10);
            AiTimer = RegisterRelativeAOB(AiTimerAOB, 3, 7, AiTimerOffset1);
        }
        public float AiTimerValue => AiTimer.ReadSingle(AiTimerOffset2);

        public bool DeathCam
        {
            get => WorldChrBase.ReadBoolean(0x70);
            set => WorldChrBase.WriteBoolean(0x70, value);
        }

        public int Health
        {
            get => ChrData1.ReadInt32(0x3E8);
            set => ChrData1.WriteInt32(0x3E8, value);
        }

        public int Stamina
        {
            get => ChrData1.ReadInt32(0x3F8);
            set => ChrData1.WriteInt32(0x3F8, value);
        }

        public Stats GetStats()
        {
            Stats stats;
            stats.Vitality = ChrData2.ReadInt32(0x40);
            stats.Attunement = ChrData2.ReadInt32(0x48);
            stats.Endurance = ChrData2.ReadInt32(0x50);
            // etc
            return stats;
        }
    }

    public struct Stats
    {
        public int Vitality;
        public int Attunement;
        public int Endurance;
    }
}
