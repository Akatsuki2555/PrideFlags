using System;
using System.Linq;
using PrideFlags.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PrideFlags
{
    public class PrideFlagCreator
    {
        private readonly PrideFlagsMod _prideFlagsMod;

        public PrideFlagCreator(PrideFlagsMod prideFlagsMod)
        {
            _prideFlagsMod = prideFlagsMod;
        }

        public void GUIFlagCreator()
        {
            var w = Screen.width;
            var h = Screen.height;
            var halfWidth = w / 2f;
            var halfHeight = h / 2f;
            switch (_prideFlagsMod.EditorData.Window)
            {
                case PrideFlagsMod.EditorCurrentWindow.List:
                    GUIFlagCreator_List(halfWidth, halfHeight);
                    break;
                case PrideFlagsMod.EditorCurrentWindow.NameNew:
                    GUIFlagCreator_NameNew(halfWidth, halfHeight);
                    break;
                case PrideFlagsMod.EditorCurrentWindow.Edit:
                    GUIFlagCreator_Edit(halfWidth, halfHeight);
                    break;
                case PrideFlagsMod.EditorCurrentWindow.ColorEdit:
                    GUIFlagCreator_ColorEdit(halfWidth, halfHeight);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void GUIFlagCreator_ColorEdit(float halfWidth, float halfHeight)
        {
            var selected = _prideFlagsMod.Custom.FirstOrDefault(x => x.Name == _prideFlagsMod.EditorData.CurrentName);
            if (selected == null)
            {
                _prideFlagsMod.EditorData.CurrentName = "";
                _prideFlagsMod.EditorData.Window = PrideFlagsMod.EditorCurrentWindow.List;
                return;
            }

            var txt = "";
            switch (_prideFlagsMod.EditorData.EditingColor)
            {
                case 1:
                    txt = "first";
                    break;
                case 2:
                    txt = "second";
                    break;
                case 3:
                    txt = "third";
                    break;
                case 4:
                    txt = "fourth";
                    break;
                case 5:
                    txt = "fifth";
                    break;
                case 6:
                    txt = "sixth";
                    break;
            }

            GUI.Box(new Rect(halfWidth - 100, halfHeight - 50, 200, 100), $"Edit {txt} colour");
            GUI.Label(new Rect(halfWidth - 95, halfHeight - 30, 50, 20), "Red");
            GUI.Label(new Rect(halfWidth - 95, halfHeight - 10, 50, 20), "Green");
            GUI.Label(new Rect(halfWidth - 95, halfHeight + 10, 50, 20), "Blue");

            _prideFlagsMod.EditorData.EditingColorR = GUI.HorizontalSlider(
                new Rect(halfWidth - 40, halfHeight - 30, 130, 20), _prideFlagsMod.EditorData.EditingColorR, 0f, 1f);
            _prideFlagsMod.EditorData.EditingColorG = GUI.HorizontalSlider(
                new Rect(halfWidth - 40, halfHeight - 10, 130, 20), _prideFlagsMod.EditorData.EditingColorG, 0f, 1f);
            _prideFlagsMod.EditorData.EditingColorB = GUI.HorizontalSlider(
                new Rect(halfWidth - 40, halfHeight + 10, 130, 20), _prideFlagsMod.EditorData.EditingColorB, 0f, 1f);

            var a = GUI.contentColor;
            GUI.contentColor = new Color(_prideFlagsMod.EditorData.EditingColorR,
                _prideFlagsMod.EditorData.EditingColorG, _prideFlagsMod.EditorData.EditingColorB);
            GUI.Label(new Rect(halfWidth - 95, halfHeight + 30, 190, 20), "This is how it looks");
            GUI.contentColor = a;

            if (!GUI.Button(new Rect(halfWidth - 95, halfHeight + 50, 190, 20), "Save")) return;
            _prideFlagsMod.EditorData.Window = PrideFlagsMod.EditorCurrentWindow.Edit;
            switch (_prideFlagsMod.EditorData.EditingColor)
            {
                case 1:
                    selected.Colours = new PrideFlag.PrideFlagColours
                    {
                        One = new Color(_prideFlagsMod.EditorData.EditingColorR,
                            _prideFlagsMod.EditorData.EditingColorG, _prideFlagsMod.EditorData.EditingColorB),
                        Two = selected.Colours.Two,
                        Three = selected.Colours.Three,
                        Four = selected.Colours.Four,
                        Five = selected.Colours.Five,
                        Six = selected.Colours.Six
                    };
                    break;
                case 2:
                    selected.Colours = new PrideFlag.PrideFlagColours
                    {
                        One = selected.Colours.One,
                        Two = new Color(_prideFlagsMod.EditorData.EditingColorR,
                            _prideFlagsMod.EditorData.EditingColorG, _prideFlagsMod.EditorData.EditingColorB),
                        Three = selected.Colours.Three,
                        Four = selected.Colours.Four,
                        Five = selected.Colours.Five,
                        Six = selected.Colours.Six
                    };
                    break;
                case 3:
                    selected.Colours = new PrideFlag.PrideFlagColours
                    {
                        One = selected.Colours.One,
                        Two = selected.Colours.Two,
                        Three = new Color(_prideFlagsMod.EditorData.EditingColorR,
                            _prideFlagsMod.EditorData.EditingColorG, _prideFlagsMod.EditorData.EditingColorB),
                        Four = selected.Colours.Four,
                        Five = selected.Colours.Five,
                        Six = selected.Colours.Six
                    };
                    break;
                case 4:
                    selected.Colours = new PrideFlag.PrideFlagColours
                    {
                        One = selected.Colours.One,
                        Two = selected.Colours.Two,
                        Three = selected.Colours.Three,
                        Four = new Color(_prideFlagsMod.EditorData.EditingColorR,
                            _prideFlagsMod.EditorData.EditingColorG, _prideFlagsMod.EditorData.EditingColorB),
                        Five = selected.Colours.Five,
                        Six = selected.Colours.Six
                    };
                    break;
                case 5:
                    selected.Colours = new PrideFlag.PrideFlagColours
                    {
                        One = selected.Colours.One,
                        Two = selected.Colours.Two,
                        Three = selected.Colours.Three,
                        Four = selected.Colours.Four,
                        Five = new Color(_prideFlagsMod.EditorData.EditingColorR,
                            _prideFlagsMod.EditorData.EditingColorG, _prideFlagsMod.EditorData.EditingColorB),
                        Six = selected.Colours.Six
                    };
                    break;
                case 6:
                    selected.Colours = new PrideFlag.PrideFlagColours
                    {
                        One = selected.Colours.One,
                        Two = selected.Colours.Two,
                        Three = selected.Colours.Three,
                        Four = selected.Colours.Four,
                        Five = selected.Colours.Five,
                        Six = new Color(_prideFlagsMod.EditorData.EditingColorR,
                            _prideFlagsMod.EditorData.EditingColorG, _prideFlagsMod.EditorData.EditingColorB)
                    };
                    break;
            }

            _prideFlagsMod.SaveCustomFlags();
        }

        private void GUIFlagCreator_Edit(float halfWidth, float halfHeight)
        {
            var selected = _prideFlagsMod.Custom.FirstOrDefault(x => x.Name == _prideFlagsMod.EditorData.CurrentName);
            if (selected == null)
            {
                _prideFlagsMod.EditorData.CurrentName = "";
                _prideFlagsMod.EditorData.Window = PrideFlagsMod.EditorCurrentWindow.List;
                return;
            }

            int howManyColours;
            switch (selected.Preset)
            {
                case PrideFlag.PrideFlagPreset.Two:
                    howManyColours = 2;
                    break;
                case PrideFlag.PrideFlagPreset.Three:
                case PrideFlag.PrideFlagPreset.ThreeSmallMiddle:
                    howManyColours = 3;
                    break;
                case PrideFlag.PrideFlagPreset.Four:
                    howManyColours = 4;
                    break;
                case PrideFlag.PrideFlagPreset.Five:
                    howManyColours = 5;
                    break;
                case PrideFlag.PrideFlagPreset.Six:
                    howManyColours = 6;
                    break;
                case PrideFlag.PrideFlagPreset.Intersex: // Intersex is not a custom flag
                case PrideFlag.PrideFlagPreset.Queer: // Queer is not a custom flag
                default:
                    throw new ArgumentOutOfRangeException();
            }

            GUI.Box(new Rect(halfWidth - 100, halfHeight - 100, 200, 200), $"Editing {selected.Name}");
            if (GUI.Button(new Rect(halfWidth - 95, halfHeight - 80, 190, 20), "Change first color"))
            {
                _prideFlagsMod.EditorData.Window = PrideFlagsMod.EditorCurrentWindow.ColorEdit;
                _prideFlagsMod.EditorData.EditingColor = 1;
                _prideFlagsMod.EditorData.EditingColorR = selected.Colours.One.r;
                _prideFlagsMod.EditorData.EditingColorG = selected.Colours.One.g;
                _prideFlagsMod.EditorData.EditingColorB = selected.Colours.One.b;
            }

            if (GUI.Button(new Rect(halfWidth - 95, halfHeight - 60, 190, 20),
                    "Change second color"))
            {
                _prideFlagsMod.EditorData.Window = PrideFlagsMod.EditorCurrentWindow.ColorEdit;
                _prideFlagsMod.EditorData.EditingColor = 2;
                _prideFlagsMod.EditorData.EditingColorR = selected.Colours.Two.r;
                _prideFlagsMod.EditorData.EditingColorG = selected.Colours.Two.g;
                _prideFlagsMod.EditorData.EditingColorB = selected.Colours.Two.b;
            }

            if (howManyColours >= 3 &&
                GUI.Button(new Rect(halfWidth - 95, halfHeight - 40, 190, 20), "Change third color"))
            {
                _prideFlagsMod.EditorData.Window = PrideFlagsMod.EditorCurrentWindow.ColorEdit;
                _prideFlagsMod.EditorData.EditingColor = 3;
                _prideFlagsMod.EditorData.EditingColorR = selected.Colours.Three.r;
                _prideFlagsMod.EditorData.EditingColorG = selected.Colours.Three.g;
                _prideFlagsMod.EditorData.EditingColorB = selected.Colours.Three.b;
            }

            if (howManyColours >= 4 && GUI.Button(new Rect(halfWidth - 95, halfHeight - 20, 190, 20),
                    "Change fourth color"))
            {
                _prideFlagsMod.EditorData.Window = PrideFlagsMod.EditorCurrentWindow.ColorEdit;
                _prideFlagsMod.EditorData.EditingColor = 4;
                _prideFlagsMod.EditorData.EditingColorR = selected.Colours.Four.r;
                _prideFlagsMod.EditorData.EditingColorG = selected.Colours.Four.g;
                _prideFlagsMod.EditorData.EditingColorB = selected.Colours.Four.b;
            }

            if (howManyColours >= 5 &&
                GUI.Button(new Rect(halfWidth - 95, halfHeight, 190, 20), "Change fifth color"))
            {
                _prideFlagsMod.EditorData.Window = PrideFlagsMod.EditorCurrentWindow.ColorEdit;
                _prideFlagsMod.EditorData.EditingColor = 5;
                _prideFlagsMod.EditorData.EditingColorR = selected.Colours.Five.r;
                _prideFlagsMod.EditorData.EditingColorG = selected.Colours.Five.g;
                _prideFlagsMod.EditorData.EditingColorB = selected.Colours.Five.b;
            }

            if (howManyColours >= 6 &&
                GUI.Button(new Rect(halfWidth - 95, halfHeight + 20, 190, 20), "Change sixth color"))
            {
                _prideFlagsMod.EditorData.Window = PrideFlagsMod.EditorCurrentWindow.ColorEdit;
                _prideFlagsMod.EditorData.EditingColor = 6;
                _prideFlagsMod.EditorData.EditingColorR = selected.Colours.Six.r;
                _prideFlagsMod.EditorData.EditingColorG = selected.Colours.Six.g;
                _prideFlagsMod.EditorData.EditingColorB = selected.Colours.Six.b;
            }

            if (GUI.Button(new Rect(halfWidth - 95, halfHeight - 80 + howManyColours * 20, 190, 20), "< Back"))
            {
                _prideFlagsMod.EditorData.Window = PrideFlagsMod.EditorCurrentWindow.List;
                _prideFlagsMod.EditorData.CurrentName = "";
            }
        }

        private void GUIFlagCreator_NameNew(float halfWidth, float halfHeight)
        {
            GUI.Box(new Rect(halfWidth - 100, halfHeight - 120, 200, 240), "New Custom Flag");
            GUI.Label(new Rect(halfWidth - 95, halfHeight - 100, 190, 20), "Name of flag");

            _prideFlagsMod.EditorData.NewName =
                GUI.TextField(new Rect(halfWidth - 95, halfHeight - 80, 190, 20), _prideFlagsMod.EditorData.NewName);
            var originalColor = GUI.contentColor;
            GUI.Label(new Rect(halfWidth - 95, halfHeight - 60, 190, 20), "Flag layout");
            if (_prideFlagsMod.EditorData.NewPreset == PrideFlag.PrideFlagPreset.Two)
                GUI.contentColor = Color.green;
            if (GUI.Button(new Rect(halfWidth - 95, halfHeight - 40, 190, 20), "2 stripes"))
                _prideFlagsMod.EditorData.NewPreset = PrideFlag.PrideFlagPreset.Two;
            GUI.contentColor = originalColor;

            if (_prideFlagsMod.EditorData.NewPreset == PrideFlag.PrideFlagPreset.Three)
                GUI.contentColor = Color.green;
            if (GUI.Button(new Rect(halfWidth - 95, halfHeight - 20, 190, 20), "3 stripes"))
                _prideFlagsMod.EditorData.NewPreset = PrideFlag.PrideFlagPreset.Three;
            GUI.contentColor = originalColor;

            if (_prideFlagsMod.EditorData.NewPreset == PrideFlag.PrideFlagPreset.ThreeSmallMiddle)
                GUI.contentColor = Color.green;
            if (GUI.Button(new Rect(halfWidth - 95, halfHeight, 190, 20), "3 stripes (middle is small)"))
                _prideFlagsMod.EditorData.NewPreset = PrideFlag.PrideFlagPreset.ThreeSmallMiddle;
            GUI.contentColor = originalColor;

            if (_prideFlagsMod.EditorData.NewPreset == PrideFlag.PrideFlagPreset.Four)
                GUI.contentColor = Color.green;
            if (GUI.Button(new Rect(halfWidth - 95, halfHeight + 20, 190, 20), "4 stripes"))
                _prideFlagsMod.EditorData.NewPreset = PrideFlag.PrideFlagPreset.Four;
            GUI.contentColor = originalColor;

            if (_prideFlagsMod.EditorData.NewPreset == PrideFlag.PrideFlagPreset.Five)
                GUI.contentColor = Color.green;
            if (GUI.Button(new Rect(halfWidth - 95, halfHeight + 40, 190, 20), "5 stripes"))
                _prideFlagsMod.EditorData.NewPreset = PrideFlag.PrideFlagPreset.Five;
            GUI.contentColor = originalColor;

            if (_prideFlagsMod.EditorData.NewPreset == PrideFlag.PrideFlagPreset.Six)
                GUI.contentColor = Color.green;
            if (GUI.Button(new Rect(halfWidth - 95, halfHeight + 60, 190, 20), "6 stripes"))
                _prideFlagsMod.EditorData.NewPreset = PrideFlag.PrideFlagPreset.Six;
            GUI.contentColor = originalColor;

            GUI.Label(new Rect(halfWidth - 90, halfHeight + 80, 190, 20), "Create flag");


            var existingFlag = _prideFlagsMod.Custom.FirstOrDefault(x => x.Name == _prideFlagsMod.EditorData.NewName);
            if (existingFlag == null && GUI.Button(new Rect(halfWidth - 95, halfHeight + 100, 190, 20), "Create"))
            {
                _prideFlagsMod.Custom.Add(new CustomFlag
                {
                    Name = _prideFlagsMod.EditorData.NewName,
                    Colours = new PrideFlag.PrideFlagColours
                    {
                        One = Color.black,
                        Two = Color.black,
                        Three = Color.black,
                        Four = Color.black,
                        Five = Color.black,
                        Six = Color.black
                    },
                    Preset = _prideFlagsMod.EditorData.NewPreset
                });
                _prideFlagsMod.SaveCustomFlags();
                _prideFlagsMod.EditorData.CurrentName = _prideFlagsMod.EditorData.NewName;
                _prideFlagsMod.EditorData.NewName = "";
                _prideFlagsMod.EditorData.Window = PrideFlagsMod.EditorCurrentWindow.List;
            }
            else if (existingFlag != null)
            {
                var a = GUI.contentColor;
                GUI.contentColor = Color.red;
                GUI.Label(new Rect(halfWidth - 95, halfHeight + 100, 190, 20), "An existing flag has been found");
                GUI.contentColor = a;
            }
        }

        private void GUIFlagCreator_List(float halfWidth, float halfHeight)
        {
            GUI.Box(new Rect(halfWidth - 200, halfHeight - 300, 400, 600), "Custom Flag List");

            if (GUI.Button(new Rect(halfWidth + 150, halfHeight - 285, 20, 20), "+"))
                _prideFlagsMod.EditorData.Window = PrideFlagsMod.EditorCurrentWindow.NameNew;
            if (GUI.Button(new Rect(halfWidth + 175, halfHeight - 285, 20, 20), "x"))
                _prideFlagsMod.EditorData.Show = false;

            var i = 0;
            CustomFlag toRemove = null;

            foreach (var customFlag in _prideFlagsMod.Custom)
            {
                GUI.Label(new Rect(halfWidth - 195, halfHeight - 260 + i, 390, 20), customFlag.Name);
                if (GUI.Button(new Rect(halfWidth - 35, halfHeight - 260 + i, 80, 20), "Spawn"))
                {
                    var flag = Object.Instantiate(_prideFlagsMod.Presets[(int)customFlag.Preset]);


                    flag.transform.position = _prideFlagsMod.Player.transform.position;
                    flag.name = $"{customFlag.Name} Flag (Clone)";
                    var flagT = flag.AddComponent<PrideFlag>();

                    flagT.SetColours(customFlag.Colours);
                }

                if (GUI.Button(new Rect(halfWidth + 35, halfHeight - 260 + i, 80, 20), "Edit"))
                {
                    _prideFlagsMod.EditorData.CurrentName = customFlag.Name;
                    _prideFlagsMod.EditorData.Window = PrideFlagsMod.EditorCurrentWindow.Edit;
                }

                if (GUI.Button(new Rect(halfWidth + 115, halfHeight - 260 + i, 80, 20), "Delete"))
                    toRemove = customFlag;
                i += 15;
            }

            if (toRemove != null) _prideFlagsMod.Custom.Remove(toRemove);
        }
    }
}