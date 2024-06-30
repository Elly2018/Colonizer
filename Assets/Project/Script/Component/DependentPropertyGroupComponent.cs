using System;
using System.Linq;
using UnityEngine;
using MoonSharp.Interpreter;

namespace Colonizer
{
    [RequireComponent(typeof(ObjectDefinition))]
    [AddComponentMenu("Colonizer/Object/Dependent Property Bank")]
    public class DependentPropertyGroupComponent : MonoBehaviour, IDependentPropertyBank
    {
        BoolPropertyDependent[] bool_values = new BoolPropertyDependent[0];
        StringPropertyDependent[] string_values = new StringPropertyDependent[0];
        IntPropertyDependent[] int_values = new IntPropertyDependent[0];
        FloatPropertyDependent[] float_values = new FloatPropertyDependent[0];
        Vector2PropertyDependent[] vector2_values = new Vector2PropertyDependent[0];
        Vector3PropertyDependent[] vector3_values = new Vector3PropertyDependent[0];

        IPropertiesBankAccess Access;
        Script env;
        LuaPropertyObject luaProperty;

        private void Awake()
        {
            Script.DefaultOptions.DebugPrint = (s) => Debug.Log(s.ToLower());
            UserData.RegisterAssembly();
            Access = GetComponent<ObjectDefinition>();
            env = new Script(CoreModules.String | CoreModules.Debug | CoreModules.Basic);
            luaProperty = new LuaPropertyObject(Access);
            env.Globals["State"] = UserData.Create(luaProperty);
        }

        public void Append(PropertyHeader header)
        {
            if (!header.IsDependent) return;
            //if (header.DataType == typeof(bool)) Append(new BoolPropertyDependent(header.Name, header.Description, GetFuncByHeader_Int(header.DependMethod)));
            //else if (header.DataType == typeof(string)) Append(new StringProperty(header.Name, header.Description, default(string)));
            if (header.DataType == PropertyType.Int) Append(new IntPropertyDependent(header.Name, header.Description, GetFuncByHeader_Int(header.DependMethod)));
            else if (header.DataType == PropertyType.Float) Append(new FloatPropertyDependent(header.Name, header.Description, GetFuncByHeader_Float(header.DependMethod)));
            //else if (header.DataType == typeof(Vector2)) Append(new Vector2Property(header.Name, header.Description, default(Vector2)));
            //else if (header.DataType == typeof(Vector3)) Append(new Vector3Property(header.Name, header.Description, default(Vector3)));
            else
            {
                Debug.LogWarning($"The property type is not define in the core module: {header.DataType}");
            }
        }
        public void Append(PropertyHeader[] header)
        {
            foreach (var i in header) Append(i);
        }
        public void Append(BoolPropertyDependent v)
        {
            var list = bool_values.ToList();
            list.Add(v);
            bool_values = list.ToArray();
        }
        public void Append(BoolPropertyDependent[] v)
        {
            var list = bool_values.ToList();
            list.AddRange(v);
            bool_values = list.ToArray();
        }
        public void Append(StringPropertyDependent v)
        {
            var list = string_values.ToList();
            list.Add(v);
            string_values = list.ToArray();
        }
        public void Append(StringPropertyDependent[] v)
        {
            var list = string_values.ToList();
            list.AddRange(v);
            string_values = list.ToArray();
        }
        public void Append(IntPropertyDependent v)
        {
            var list = int_values.ToList();
            list.Add(v);
            int_values = list.ToArray();
        }
        public void Append(IntPropertyDependent[] v)
        {
            var list = int_values.ToList();
            list.AddRange(v);
            int_values = list.ToArray();
        }
        public void Append(FloatPropertyDependent v)
        {
            var list = float_values.ToList();
            list.Add(v);
            float_values = list.ToArray();
        }
        public void Append(FloatPropertyDependent[] v)
        {
            var list = float_values.ToList();
            list.AddRange(v);
            float_values = list.ToArray();
        }
        public void Append(Vector2PropertyDependent v)
        {
            var list = vector2_values.ToList();
            list.Add(v);
            vector2_values = list.ToArray();
        }
        public void Append(Vector2PropertyDependent[] v)
        {
            var list = vector2_values.ToList();
            list.AddRange(v);
            vector2_values = list.ToArray();
        }
        public void Append(Vector3PropertyDependent v)
        {
            var list = vector3_values.ToList();
            list.Add(v);
            vector3_values = list.ToArray();
        }
        public void Append(Vector3PropertyDependent[] v)
        {
            var list = vector3_values.ToList();
            list.AddRange(v);
            vector3_values = list.ToArray();
        }

        public PropertyBase<T> Get<T>(string label)
        {
            if (typeof(T) == typeof(bool)) return GetBool(label) as PropertyBase<T>;
            else if (typeof(T) == typeof(string)) return GetString(label) as PropertyBase<T>;
            else if (typeof(T) == typeof(int)) return GetInt(label) as PropertyBase<T>;
            else if (typeof(T) == typeof(float)) return GetFloat(label) as PropertyBase<T>;
            else if (typeof(T) == typeof(Vector2)) return GetVector2(label) as PropertyBase<T>;
            else if (typeof(T) == typeof(Vector3)) return GetVector3(label) as PropertyBase<T>;
            else
            {
                Debug.LogWarning($"The property type is not define in the core module: {typeof(T).FullName}");
                return null;
            }
        }
        public PropertyBase<bool> GetBool(string label)
        {
            foreach(var i in bool_values)
            {
                if (i.Label == label) return i;
            }
            return null;
        }
        public PropertyBase<string> GetString(string label)
        {
            foreach (var i in string_values)
            {
                if (i.Label == label) return i;
            }
            return null;
        }
        public PropertyBase<int> GetInt(string label)
        {
            foreach (var i in int_values)
            {
                if (i.Label == label) return i;
            }
            return null;
        }
        public PropertyBase<float> GetFloat(string label)
        {
            foreach (var i in float_values)
            {
                if (i.Label == label) return i;
            }
            return null;
        }
        public PropertyBase<Vector2> GetVector2(string label)
        {
            foreach (var i in vector2_values)
            {
                if (i.Label == label) return i;
            }
            return null;
        }
        public PropertyBase<Vector3> GetVector3(string label)
        {
            foreach (var i in vector3_values)
            {
                if (i.Label == label) return i;
            }
            return null;
        }

        public bool LabelExist(PropertyType type, string label)
        {
            if (type == PropertyType.Boolean) return bool_values.ToList().FindIndex(x => x.Label == label) != -1;
            else if (type == PropertyType.String) return string_values.ToList().FindIndex(x => x.Label == label) != -1;
            else if (type == PropertyType.Int) return int_values.ToList().FindIndex(x => x.Label == label) != -1;
            else if (type == PropertyType.Float) return float_values.ToList().FindIndex(x => x.Label == label) != -1;
            else if (type == PropertyType.Vector2) return vector2_values.ToList().FindIndex(x => x.Label == label) != -1;
            else if (type == PropertyType.Vector3) return vector3_values.ToList().FindIndex(x => x.Label == label) != -1;
            else
            {
                Debug.LogWarning($"The property type is not define in the core module: {type}");
                return false;
            }
        }

        Func<int> GetFuncByHeader_Int(string method)
        {
            return new Func<int>(() =>
            {
                DynValue dyn = env.DoString(method);
                return (int)dyn.Number;
            });
        }
        Func<float> GetFuncByHeader_Float(string method)
        {
            return new Func<float>(() =>
            {
                DynValue dyn = env.DoString(method);
                return (float)dyn.Number;
            });
        }

        [MoonSharpUserData]
        class LuaPropertyObject
        {
            readonly IPropertiesBankAccess Access;

            public LuaPropertyObject(IPropertiesBankAccess access)
            {
                Access = access;
            }

            public int GetInt(string label)
            {
                return Access.GetData<int>(label);
            }
            public float GetFloat(string label)
            {
                return Access.GetData<float>(label);
            }
        }
    }
}
