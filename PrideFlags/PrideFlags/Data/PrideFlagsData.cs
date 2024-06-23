using System;
using System.Collections.Generic;

namespace PrideFlags.Data
{
    [Serializable]
    public class PrideFlagsData
    {
        public List<PrideFlagData> Flags { get; set; } = new List<PrideFlagData>();
    }
}