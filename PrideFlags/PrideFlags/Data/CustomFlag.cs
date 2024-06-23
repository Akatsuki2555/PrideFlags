using System;

namespace PrideFlags.Data
{
    [Serializable]
    public class CustomFlag
    {
        public string Name { get; set; }
        public PrideFlag.PrideFlagColours Colours { get; set; }
        public PrideFlag.PrideFlagPreset Preset { get; set; }
    }
}