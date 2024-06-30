using System;
using UnityEngine;

namespace Colonizer
{
    [RequireComponent(typeof(DependentPropertyGroupComponent))]
    [RequireComponent(typeof(PropertyGroupComponent))]
    [AddComponentMenu("Colonizer/Object/Object Definition")]
    public class ObjectDefinition : MonoBehaviour, IPropertiesBankAccess, IUnitSpawnerHelper
    {
        [SerializeField] GameObjectType _Type;

        public GameObjectType Type { get => _Type; set => _Type = value; }

        IDependentPropertyBank DPGC = null;
        IPropertyBank PGC = null;

        void Awake()
        {
            DPGC = GetComponent<DependentPropertyGroupComponent>();
            PGC = GetComponent<PropertyGroupComponent>();
        }

        /// <summary>
        /// This will create the property bank for this object
        /// </summary>
        /// <param name="Headers"></param>
        public void CreateProperties(PropertyHeader[] Headers)
        {
            DPGC.Append(Headers);
            PGC.Append(Headers);
        }

        public object GetInfo(Type type, string Label)
        {
            throw new NotImplementedException();
        }
        public T GetInfo<T>(string Label)
        {   
            return default(T);
        }
        public object GetData(Type type, string Label)
        {
            throw new NotImplementedException();
        }
        public T GetData<T>(string Label)
        {
            return default(T);
        }
    }
}
