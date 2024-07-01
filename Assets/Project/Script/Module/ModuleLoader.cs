using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public class ModuleLoader : MonoBehaviour, IModuleLoader
    {
        [Tooltip("Loaded modules info")]
        [SerializeField] [ReadOnly] Manifest[] manifest = new Manifest[0];

        /// <summary>
        /// Key: Module string <br />
        /// Value: Absolute path
        /// </summary>
        Dictionary<string, string> map = new Dictionary<string, string>();
        /// <summary>
        /// Left: The content of the module <br />
        /// Right: Is the module enable or not
        /// </summary>
        Tuple<ModuleContent, bool, Manifest>[] loadedModules = new Tuple<ModuleContent, bool, Manifest>[0];
        ModuleRecord loadedRecords = new ModuleRecord();

        public Manifest[] Manifests => manifest;
        public ModuleContent[] Contents => loadedModules.ToList().Where(x => x.Item2).Select(x => x.Item1).ToArray();
        public ModuleRecord Records => loadedRecords;
        const bool defaultmodulebehaviour = false;
        const string foldername = "mods";
        const string manifestname = "manifest.json";
        const string recordname = "record.json";
        string ModFolder => Path.Combine(Application.streamingAssetsPath, foldername);
        string ExtractFolder => Path.Combine(Application.temporaryCachePath, foldername);
        string RecordPath => Path.Combine(Application.streamingAssetsPath, "mods", recordname);

        event Action loadModule;
        event Action unloadModule;
        event Action recordUpdate;

        public void StartLoadingRecord()
        {
            if (File.Exists(RecordPath))
            {
                LoadRecord();
            }
            else
            {
                SaveRecord();
            }
        }
        public void SetEnable(Manifest target, bool value)
        {
            SaveRecord();
        }
        public void StartLoadingModuleHeader()
        {
            string[] paths = ModuleLoaderSetup();
            manifest = ModuleChecker(paths);
            UpdateRecord();
        }
        public void StartLoadingModuleContent()
        {
            loadedModules = new Tuple<ModuleContent, bool, Manifest>[0];
            if (unloadModule != null) unloadModule.Invoke();
            StartCoroutine(LoadingModuleAsync());
        }


        public void RegisterModuleAction(Action Unload, Action load)
        {
            if (load != null)
            {
                loadModule += load;
            }
            if (Unload != null)
            {
                unloadModule += Unload;
            }
        }
        public void RegisterRecordUpdate(Action recordupdate)
        {
            if (recordupdate != null)
            {
                recordUpdate += recordupdate;
            }
        }

        /// <summary>
        /// Extract all the zip to the target temp folder
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Loading all the extract module and check header file, which is manifest.json exist
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
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
        /// Save current record to json and put it to the record path
        /// </summary>
        void SaveRecord()
        {
            File.WriteAllText(RecordPath, JsonUtility.ToJson(loadedRecords, true));
        }
        /// <summary>
        /// Trying loading the record and store to <seealso cref="loadedRecords"/> <br />
        /// If this step failed, it will save the empty record to override the file
        /// </summary>
        void LoadRecord()
        {
            string json = File.ReadAllText(RecordPath);
            try
            {
                loadedRecords = JsonUtility.FromJson<ModuleRecord>(json);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                SaveRecord();
            }
            if (recordUpdate != null) recordUpdate.Invoke();
        }
        /// <summary>
        /// Adding missing record and delete not exist manifest by checking manifest list <br />
        /// The default behaviour is disable the module
        /// </summary>
        void UpdateRecord()
        {
            List<string> buffer = manifest.Select(x => x.ToString()).ToList();
            // This action will remove records for those not existing in manifest list
            List<ModuleRecordElement> elements = loadedRecords.Records.ToList();
            var ret = elements.Where(x => buffer.Contains(x.Name)).ToList();
            // This action will add records for those are extra in the manifest list
            foreach(var i in buffer)
            {
                if (!ret.Select(x => x.Name).Contains(i))
                {
                    ret.Add(new ModuleRecordElement() { Name = i, Enable = defaultmodulebehaviour });
                }
            }
            // Replace the record element and save it
            loadedRecords.Records = ret.ToArray();
            SaveRecord();
            if (recordUpdate != null) recordUpdate.Invoke();
        }
        /// <summary>
        /// This will base on current enable module load them or unload them
        /// </summary>
        /// <returns></returns>
        IEnumerator LoadingModuleAsync()
        {
            if (loadModule != null) loadModule.Invoke();
            yield return null;
        }
        /// <summary>
        /// Trying to get the module folder by giving the manifest info
        /// </summary>
        /// <param name="m">Target manifest info</param>
        /// <returns>Return absolute path, if failed, empty string</returns>
        string GetFolderPath(Manifest m)
        {
            string path;
            if (map.TryGetValue(m.ToString(), out path)) return path;
            else return string.Empty;

        }
    }
}
