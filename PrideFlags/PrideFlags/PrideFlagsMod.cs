using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using JetBrains.Annotations;
using MSCLoader;
using PrideFlags.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PrideFlags
{
    [UsedImplicitly]
    public class PrideFlagsMod : Mod
    {
        private static bool _loadedCustom;
        private readonly PrideFlagCreator _prideFlagCreator;

        internal readonly List<GameObject> Presets = new List<GameObject>();
        internal List<CustomFlag> Custom = new List<CustomFlag>();
        internal PrideFlagsEditorData EditorData = new PrideFlagsEditorData
        {
            NewName = "",
            NewPreset = PrideFlag.PrideFlagPreset.Two,
            CurrentName = "",
            EditingColor = 0,
            EditingColorB = 1,
            EditingColorG = 1,
            EditingColorR = 1,
            Show = false,
            Window = EditorCurrentWindow.List
        };

        public Transform Player;

        public PrideFlagsMod()
        {
            _prideFlagCreator = new PrideFlagCreator(this);
        }

        public override string Name => "LGBTQIA+ Pride Flags";

        public override string ID => "lgbtprideflags";

        public override string Version => "2.1.1";

        public override string Author => "アカツキ";

        public override void ModSetup()
        {
            base.ModSetup();

            SetupFunction(Setup.OnLoad, Mod_OnLoad);
            SetupFunction(Setup.OnSave, Mod_OnSave);
            SetupFunction(Setup.OnNewGame, Mod_OnNewGame);
            SetupFunction(Setup.OnGUI, Mod_OnGUI);
            SetupFunction(Setup.OnMenuLoad, Mod_OnMenuLoad);
        }

        private static void Mod_OnNewGame()
        {
            var path = Path.Combine(Application.persistentDataPath, "PrideFlags.xml");
            if (!File.Exists(path))
                File.Delete(path);
        }

        private void Mod_OnLoad()
        {
            var assetBundle = ReadAssetBundle();

            Presets.Add(assetBundle.LoadAsset<GameObject>("2")); // 2 stripes
            Presets.Add(assetBundle.LoadAsset<GameObject>("3")); // 3 stripes
            Presets.Add(assetBundle.LoadAsset<GameObject>("3mid")); // 3 stripes with middle one smaller
            Presets.Add(assetBundle.LoadAsset<GameObject>("4")); // 4 stripes
            Presets.Add(assetBundle.LoadAsset<GameObject>("5")); // 5 stripes
            Presets.Add(assetBundle.LoadAsset<GameObject>("6")); // 6 stripes
            Presets.Add(assetBundle.LoadAsset<GameObject>("IntersexFlag")); // intersex flag (comes with color)
            Presets.Add(assetBundle.LoadAsset<GameObject>("QueerFlag")); // Queer Flag

            assetBundle.Unload(false);

            Player = GameObject.Find("PLAYER")?.transform;

            LoadFlags();
        }

        private static AssetBundle ReadAssetBundle()
        {
            byte[] bytes;
            using (var s = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("PrideFlags.Resources.prideflags.unity3d"))
            {
                bytes = new byte[s.Length];
                _ = s.Read(bytes, 0, bytes.Length);
            }

            if (bytes.Length == 0) throw new FileLoadException("Could not load prideflags.unity3d from DLL!");

            var assetBundle = AssetBundle.CreateFromMemoryImmediate(bytes);
            return assetBundle;
        }

        private void LoadFlags()
        {
            var path = Path.Combine(Application.persistentDataPath, "PrideFlags.xml");
            if (!File.Exists(path)) return;
            using (var stream = File.OpenRead(path))
            {
                var serializer = new XmlSerializer(typeof(PrideFlagsData));
                var flags = serializer.Deserialize(stream) as PrideFlagsData;

                if (flags == null) return;

                foreach (var flag in flags.Flags)
                {
                    var flag2 = Object.Instantiate(Presets[(int)flag.Preset]);

                    flag2.transform.position = new Vector3(flag.PosX, flag.PosY, flag.PosZ);
                    flag2.transform.rotation = new Quaternion(flag.RotX, flag.RotY, flag.RotZ, flag.RotW);
                    var flagT = flag2.AddComponent<PrideFlag>();

                    flagT.preset = flag.Preset;
                    if (flag.Preset == PrideFlag.PrideFlagPreset.Queer) continue;
                    flagT.SetColours(flag.Colours);
                }
            }
        }

        private void Mod_OnMenuLoad()
        {
            if (_loadedCustom) return;
            _loadedCustom = true;
            LoadCustomFlags();
        }

        private void Mod_OnSave()
        {
            SaveFlags();
            SaveCustomFlags();
        }

        private static void SaveFlags()
        {
            var path = Path.Combine(Application.persistentDataPath, "PrideFlags.xml");
            if (File.Exists(path)) File.Delete(path);

            using (var stream = File.OpenWrite(path))
            {
                var serializer = new XmlSerializer(typeof(PrideFlagsData));
                var flags = new PrideFlagsData();

                foreach (var flag in Object.FindObjectsOfType<PrideFlag>())
                {
                    var flagTransform = flag.transform;
                    var flagPos = flagTransform.position;
                    var flagRot = flagTransform.rotation;

                    flags.Flags.Add(new PrideFlagData
                    {
                        PosX = flagPos.x,
                        PosY = flagPos.y,
                        PosZ = flagPos.z,
                        RotX = flagRot.x,
                        RotZ = flagRot.z,
                        RotY = flagRot.y,
                        RotW = flagRot.w,
                        Colours = flag.colours,
                        Preset = flag.preset,
                        Size = flag.size
                    });
                }

                serializer.Serialize(stream, flags);
            }
        }

        public override void ModSettings()
        {
            base.ModSettings();

            Settings.AddHeader(this, "Spawn Flags");
            Settings.AddButton(this, "Spawn Lesbian Flag", delegate
            {
                var flag = Object.Instantiate(Presets[4]); // 5 stripes flag

                flag.transform.position = Player.transform.position;
                var flagT = flag.AddComponent<PrideFlag>();

                flagT.SetType("lesbian");
            });
            Settings.AddButton(this, "Spawn Gay Flag", delegate
            {
                var flag = Object.Instantiate(Presets[5]); // 6 stripes flag

                flag.transform.position = Player.transform.position;
                var flagT = flag.AddComponent<PrideFlag>();

                flagT.SetType("gay");
            });
            Settings.AddButton(this, "Spawn Bi Flag", delegate
            {
                var flag = Object.Instantiate(Presets[2]); // 3 stripes with middle one smaller

                flag.transform.position = Player.transform.position;
                var flagT = flag.AddComponent<PrideFlag>();

                flagT.SetType("bi");
            });
            Settings.AddButton(this, "Spawn Trans Flag", delegate // my flag :3
            {
                var flag = Object.Instantiate(Presets[4]); // 5 stripes

                flag.transform.position = Player.transform.position;
                var flagT = flag.AddComponent<PrideFlag>();

                flagT.SetType("trans");
            });
            Settings.AddButton(this, "Spawn Intersex Flag", delegate
            {
                var flag = Object.Instantiate(Presets[6]); // Intersex

                flag.transform.position = Player.transform.position;
                var flagT = flag.AddComponent<PrideFlag>();

                flagT.preset = PrideFlag.PrideFlagPreset.Intersex;
            });
            Settings.AddButton(this, "Spawn Queer Flag", delegate
            {
                var flag = Object.Instantiate(Presets[7]); // Queer

                flag.transform.position = Player.transform.position;
                var flagT = flag.AddComponent<PrideFlag>();

                flagT.preset = PrideFlag.PrideFlagPreset.Queer;
            });
            Settings.AddButton(this, "Make your own pride flag", () =>
            {
                EditorData.Show = true;
                EditorData.Window = EditorCurrentWindow.List;
            });

            Settings.AddHeader(this, "Manage Existing Flags");
            Settings.AddButton(this, "Delete all flags", () =>
            {
                foreach (var flag in Object.FindObjectsOfType<PrideFlag>()) Object.Destroy(flag.gameObject);
            });
            Settings.AddButton(this, "Delete save file", () =>
            {
                var path = Path.Combine(Application.persistentDataPath, "PrideFlags.xml");
                if (File.Exists(path))
                    File.Delete(path);
            });
            Settings.AddButton(this, "Duplicate all the flags", () =>
            {
                foreach (var flag in Object.FindObjectsOfType<PrideFlag>())
                {
                    var duplicate = Object.Instantiate(flag.gameObject);
                    duplicate.name = flag.name;
                }
            });
        }

        public void SaveCustomFlags()
        {
            var ser = new XmlSerializer(typeof(List<CustomFlag>));
            var path = Path.Combine(Application.persistentDataPath, "CustomPrideFlags.xml");
            if (File.Exists(path))
                File.Delete(path);

            using (var str = File.OpenWrite(path))
            {
                ser.Serialize(str, Custom);
            }
        }

        private void LoadCustomFlags()
        {
            var ser = new XmlSerializer(typeof(List<CustomFlag>));
            var path = Path.Combine(Application.persistentDataPath, "CustomPrideFlags.xml");
            if (!File.Exists(path)) return;

            using (var str = File.OpenRead(path))
            {
                var dat = ser.Deserialize(str) as List<CustomFlag>;
                if (dat == null) return;
                Custom = dat;
            }
        }

        private void Mod_OnGUI()
        {
            if (!EditorData.Show) return;
            _prideFlagCreator.GUIFlagCreator();
        }

        internal enum EditorCurrentWindow
        {
            ColorEdit,
            List,
            Edit,
            NameNew
        }

        internal struct PrideFlagsEditorData
        {
            internal EditorCurrentWindow Window;
            internal string CurrentName;
            internal string NewName;
            internal PrideFlag.PrideFlagPreset NewPreset;
            internal bool Show;
            internal int EditingColor;
            internal float EditingColorR;
            internal float EditingColorG;
            internal float EditingColorB;
        }
    }
}