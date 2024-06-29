using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.IO;
using Unity.SharpZipLib.Utils;
using UnityEngine;

namespace Colonizer
{
    /// <summary>
    /// 
    /// This worker will trying to load the modules <br />
    /// Extract the information from it. to setup the dataset in this component <br />
    /// Other component can gather module related header information by using this component
    /// 
    /// </summary>
    [AddComponentMenu("Colonizer/Module/Module Loader")]
    public class ModuleLoader: MonoBehaviour
    {
        [Tooltip("Loaded modules info")]
        [SerializeField] [ReadOnly] Manifest[] manifest = new Manifest[0];

        /// <summary>
        /// Key: Module string <br />
        /// Value: Absolute path
        /// </summary>
        Dictionary<string, string> map = new Dictionary<string, string>();

        const string foldername = "mods";
        const string manifestname = "manifest.json";
        string ModFolder => Path.Combine(Application.streamingAssetsPath, foldername);
        string ExtractFolder => Path.Combine(Application.temporaryCachePath, foldername);
        public static ModuleLoader Instance;

        private void Awake()
        {
            Instance = this;
            StartLoadingModuleHeader();
            StartLoadingModuleContent();
        }

        void StartLoadingModuleHeader()
        {
            string[] paths = ModuleLoaderSetup();
            manifest = ModuleChecker(paths);
        }
        void StartLoadingModuleContent()
        {

        }

        string[] ModuleLoaderSetup()
        {
            if (!Directory.Exists(ExtractFolder))
            {
                Directory.CreateDirectory(ExtractFolder);
            }
            else
            {
                Directory.Delete(ExtractFolder, true);
                Directory.CreateDirectory(ExtractFolder);
            }
            if (!Directory.Exists(ModFolder))
            {
                Directory.CreateDirectory(ModFolder);
                return new string[0];
            }
            string[] files = Directory.GetFiles(ModFolder, "*.zip");
            Debug.Log("[ModuleLoader] Start loading modules");
            List<string> paths = new List<string>();
            foreach (var file in files)
            {
                string n_t = Path.GetFileName(file);
                string n = Path.GetFileNameWithoutExtension(file);
                string fromm = Path.Combine(ModFolder, n_t);
                string tom = Path.Combine(ExtractFolder, n);
                ZipUtility.UncompressFromZip(fromm, null, tom);
                Debug.Log($"[ModuleLoader] Start loading modules, {fromm} => {tom}");
                paths.Add(tom);
            }
            return paths.ToArray();
        }
        Manifest[] ModuleChecker(string[] paths)
        {
            List<Manifest> fest = new List<Manifest>();
            foreach(var file in paths)
            {
                string tom = Path.Combine(file, manifestname);
                if (!File.Exists(tom)) continue;
                string j = File.ReadAllText(tom);
                try
                {
                    Manifest buffer = JsonUtility.FromJson<Manifest>(j);
                    fest.Add(buffer);
                    map.Add(buffer.ToString(), file);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
            return fest.ToArray();
        }



        /// <summary>
        /// Trying to get the module folder by giving the manifest info
        /// </summary>
        /// <param name="m">Target manifest info</param>
        /// <returns>Return absolute path, if failed, empty string</returns>
        public string GetFolderPath(Manifest m)
        {
            string path;
            if (map.TryGetValue(m.ToString(), out path)) return path;
            else return string.Empty;

        }
    }
}
