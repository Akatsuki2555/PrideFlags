using System;
using MSCLoader;
using UnityEngine;

namespace PrideFlags
{
    public class PrideFlag : MonoBehaviour
    {
        [Serializable]
        public enum PrideFlagPreset
        {
            Two = 0,
            Three = 1,
            ThreeSmallMiddle = 2,
            Four = 3,
            Five = 4,
            Six = 5,
            Intersex = 6,
            Queer = 7
        }

        [SerializeField] internal PrideFlagColours colours = new PrideFlagColours
        {
            One = Color.white,
            Two = Color.white,
            Three = Color.white,
            Four = Color.white,
            Five = Color.white,
            Six = Color.white
        };

        [SerializeField] internal float size = 1f;
        [SerializeField] internal PrideFlagPreset preset = PrideFlagPreset.Two;

        private void Start()
        {
            gameObject.AddComponent<Rigidbody>();
            gameObject.MakePickable();
        }

        internal void SetType(string type)
        {
            switch (type)
            {
                case "gay":
                    colours = new PrideFlagColours
                    {
                        One = new Color(254 / 255f, 0 / 255f, 0 / 255f),
                        Two = new Color(255 / 255f, 144 / 255f, 3 / 255f),
                        Three = new Color(245 / 255f, 243 / 255f, 11 / 255f),
                        Four = new Color(2 / 255f, 130 / 255f, 20 / 255f),
                        Five = new Color(1 / 255f, 76 / 255f, 255 / 255f),
                        Six = new Color(136 / 255f, 2 / 255f, 138 / 255f)
                    };
                    preset = PrideFlagPreset.Six;
                    gameObject.name = "Gay Pride Flag (Clone)";
                    break;
                case "lesbian":
                    colours = new PrideFlagColours
                    {
                        One = new Color(211 / 255f, 42 / 255f, 0 / 255f),
                        Two = new Color(254 / 255f, 153 / 255f, 85 / 255f),
                        Three = new Color(255 / 255f, 255 / 255f, 255 / 255f),
                        Four = new Color(203 / 255f, 92 / 255f, 107 / 255f),
                        Five = new Color(164 / 255f, 0 / 255f, 98 / 255f)
                    };
                    preset = PrideFlagPreset.Five;
                    gameObject.name = "Lesbian Pride Flag (Clone)";
                    break;
                case "trans":
                    colours = new PrideFlagColours
                    {
                        One = new Color(91 / 255f, 206 / 255f, 255 / 255f),
                        Two = new Color(245 / 255f, 169 / 255f, 184 / 255f),
                        Three = Color.white,
                        Four = new Color(245 / 255f, 169 / 255f, 184 / 255f),
                        Five = new Color(91 / 255f, 206 / 255f, 255 / 255f)
                    };
                    preset = PrideFlagPreset.Five;
                    gameObject.name = "Trans Pride Flag (Clone)";
                    break;
                case "bi":
                    colours = new PrideFlagColours
                    {
                        One = new Color(206f / 255f, 1 / 255f, 108f / 255f),
                        Two = new Color(149f / 255f, 75f / 255f, 143f / 255f),
                        Three = new Color(0 / 255f, 52 / 255f, 161 / 255f)
                    };
                    preset = PrideFlagPreset.ThreeSmallMiddle;
                    gameObject.name = "Bi Pride Flag (Clone)";
                    break;
            }

            SetColours(colours);
        }

        internal void SetColours(PrideFlagColours paramColours)
        {
            colours = paramColours;

            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                var c = Color.white;
                switch (i)
                {
                    case 0:
                        c = paramColours.One;
                        break;
                    case 1:
                        c = paramColours.Two;
                        break;
                    case 2:
                        c = paramColours.Three;
                        break;
                    case 3:
                        c = paramColours.Four;
                        break;
                    case 4:
                        c = paramColours.Five;
                        break;
                    case 5:
                        c = paramColours.Six;
                        break;
                    default:
                        ModConsole.LogWarning($"Tried to set color at index {i} but there's no support for it");
                        break;
                }

                child.GetComponent<Renderer>().material.color = c;
            }
        }

        [Serializable]
        public struct PrideFlagColours
        {
            public Color One { get; set; }
            public Color Two { get; set; }
            public Color Three { get; set; }
            public Color Four { get; set; }
            public Color Five { get; set; }
            public Color Six { get; set; }
        }
    }
}