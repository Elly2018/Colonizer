using Siccity.GLTFUtility;
using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

namespace Colonizer
{
    /// <summary>
    /// 
    /// This worker handle the unit spawn on the map <br />
    /// Because it's dynamic loading the model, it will trying to cache it in the background <br />
    /// To save another loading time
    /// 
    /// </summary>
    public class UnitSpawner : IDisposable
    {
        readonly UnitBlueprint unitType;
        readonly Vector3 position;
        readonly PropertyContent[] contents;
        readonly bool cacheOnly;
        readonly Action<GameObject> onFinish;

        static List<UnitSpawner> spawners = new List<UnitSpawner>();
        static Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();

        public static void CreateUnit(UnitBlueprint unitType, Vector3 position, PropertyContent[] contents, Action < GameObject> onFinish = null, bool cacheOnly = false)
        {
            new UnitSpawner(unitType, position, contents, onFinish, cacheOnly);
        }

        UnitSpawner(UnitBlueprint _unitType, Vector3 _position, PropertyContent[] _contents, Action<GameObject> _onFinish = null, bool _cacheOnly = false)
        {
            this.unitType = _unitType;
            this.position = _position;
            this.contents = _contents;
            this.onFinish = _onFinish;
            this.cacheOnly = _cacheOnly;
            spawners.Add(this);

            if (cache.ContainsKey(unitType.ModelPath))
            {
                Debug.Log($"[Unit Spawner] Cache hit: {unitType.ModelPath}");
                GameObject cache_obj = cache[unitType.ModelPath];
                if (!cacheOnly)
                {
                    GameObject ret = GameObject.Instantiate(cache_obj);
                    ret.transform.position = _position;
                    if (onFinish != null) onFinish.Invoke(ret);
                }
                else
                {
                    if (onFinish != null) onFinish.Invoke(cache_obj);
                }
                
                Dispose();
            }
            else
            {
                ImportSettings iss = new ImportSettings();
                Importer.LoadFromFileAsync(
                    Path.Combine(Application.streamingAssetsPath, unitType.ModelPath),
                    iss,
                    OnFinish,
                    onProgress: OnProgress);
            }
        }

        public void Dispose()
        {
            spawners.Remove(this);
        }

        void OnFinish(GameObject go, AnimationClip[] clips)
        {
            Animation anim = go.AddComponent<Animation>();
            foreach (var i in clips)
            {
                i.legacy = true;
                i.wrapMode = WrapMode.Loop;
                anim.AddClip(i, i.name);
            }
            anim.clip = clips[0];

            IUnitSpawnerHelper od = go.AddComponent<ObjectDefinition>();
            IDependentPropertyBank dpgc = go.GetComponent<DependentPropertyGroupComponent>();
            IPropertyBank pgc = go.GetComponent<PropertyGroupComponent>();
            od.Type = GameObjectType.Unit;
            if (dpgc == null) Debug.LogWarning("[Unit Spawner] Cannot get DependentPropertyGroupComponent");
            if (pgc == null) Debug.LogWarning("[Unit Spawner] Cannot get PropertyGroupComponent");
            
            if(cache.TryAdd(unitType.ModelPath, go))
            {
                Debug.Log($"[Unit Spawner] Cache create: {unitType.ModelPath}");
            }
            else
            {
                Debug.LogWarning($"[Unit Spawner] Cache already exist: {unitType.ModelPath}");
            }

            go.SetActive(false);

            if (!cacheOnly)
            {
                GameObject goCopy = GameObject.Instantiate(go);
                anim.Play();
                goCopy.transform.position = position;
                if (onFinish != null) onFinish.Invoke(goCopy);
            }
            else
            {
                if (onFinish != null) onFinish.Invoke(go);
            }
            Dispose();
        }

        void OnProgress(float c)
        {
            Debug.Log($"[Unit Spawner] GLTF loading progress: {c}");
        }
    }
}
