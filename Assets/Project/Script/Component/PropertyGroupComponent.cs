using System;
using System.Linq;
using UnityEngine;

namespace Colonizer
{
    [RequireComponent(typeof(ObjectDefinition))]
    [AddComponentMenu("Colonizer/Object/Property Bank")]
    public class PropertyGroupComponent : MonoBehaviour, IPropertyBank
    {
        BoolProperty[] bool_values = new BoolProperty[0];
        StringProperty[] string_values = new StringProperty[0];
        IntProperty[] int_values = new IntProperty[0];
        FloatProperty[] float_values = new FloatProperty[0];
        Vector2Property[] vector2_values = new Vector2Property[0];
        Vector3Property[] vector3_values = new Vector3Property[0];

        public void Append(PropertyHeader header)
        {
            if (header.IsDependent) return;
            if(header.DataType == PropertyType.Boolean) Append(new BoolProperty(header.Name, header.Description, default(bool)));
            else if(header.DataType == PropertyType.String) Append(new StringProperty(header.Name, header.Description, default(string)));
            else if(header.DataType == PropertyType.Int) Append(new IntProperty(header.Name, header.Description, default(int)));
            else if(header.DataType == PropertyType.Float) Append(new FloatProperty(header.Name, header.Description, default(float)));
            else if(header.DataType == PropertyType.Vector2) Append(new Vector2Property(header.Name, header.Description, default(Vector2)));
            else if(header.DataType == PropertyType.Vector3) Append(new Vector3Property(header.Name, header.Description, default(Vector3)));
            else
            {
                Debug.LogWarning($"The property type is not define in the core module: {header.DataType}");
            }
        }
        public void Append(PropertyHeader[] header)
        {
            foreach (var i in header) Append(i);
        }
        public void Append(BoolProperty v)
        {
            var list = bool_values.ToList();
            list.Add(v);
            bool_values = list.ToArray();
        }
        public void Append(BoolProperty[] v)
        {
            var list = bool_values.ToList();
            list.AddRange(v);
            bool_values = list.ToArray();
        }
        public void Append(StringProperty v)
        {
            var list = string_values.ToList();
            list.Add(v);
            string_values = list.ToArray();
        }
        public void Append(StringProperty[] v)
        {
            var list = string_values.ToList();
            list.AddRange(v);
            string_values = list.ToArray();
        }
        public void Append(IntProperty v)
        {
            var list = int_values.ToList();
            list.Add(v);
            int_values = list.ToArray();
        }
        public void Append(IntProperty[] v)
        {
            var list = int_values.ToList();
            list.AddRange(v);
            int_values = list.ToArray();
        }
        public void Append(FloatProperty v)
        {
            var list = float_values.ToList();
            list.Add(v);
            float_values = list.ToArray();
        }
        public void Append(FloatProperty[] v)
        {
            var list = float_values.ToList();
            list.AddRange(v);
            float_values = list.ToArray();
        }
        public void Append(Vector2Property v)
        {
            var list = vector2_values.ToList();
            list.Add(v);
            vector2_values = list.ToArray();
        }
        public void Append(Vector2Property[] v)
        {
            var list = vector2_values.ToList();
            list.AddRange(v);
            vector2_values = list.ToArray();
        }
        public void Append(Vector3Property v)
        {
            var list = vector3_values.ToList();
            list.Add(v);
            vector3_values = list.ToArray();
        }
        public void Append(Vector3Property[] v)
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
            foreach (var i in bool_values)
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
    }
}
