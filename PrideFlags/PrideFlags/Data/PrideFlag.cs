using System;

namespace PrideFlags.Data
{
    [Serializable]
    public class PrideFlagData
    {
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float RotX { get; set; }
        public float RotY { get; set; }
        public float RotZ { get; set; }
        public float RotW { get; set; }
        public PrideFlag.PrideFlagColours Colours { get; set; }
        public PrideFlag.PrideFlagPreset Preset { get; set; }
        public float Size { get; set; } = 1f;
    }
}