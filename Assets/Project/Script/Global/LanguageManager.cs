using System.Collections.Generic;

namespace Colonizer
{
    /// <summary>
    /// Use this worker to obtain the actual string you're looking for, for the property <br />
    /// </summary>
    public class LanguageManager
    {
        public Dictionary<string, PropertyLanguage> RegisterProperty { private set; get; }

        static LanguageManager _Instance;
        public static LanguageManager Instance
        {
            get
            {
                if (_Instance == null) _Instance = new LanguageManager();
                return _Instance;
            }
        }

        private LanguageManager()
        {
            RegisterProperty = new Dictionary<string, PropertyLanguage>();
        }
        /// <summary>
        /// Clean the register property string data <br />
        /// The map instance will be empty after this getting called
        /// </summary>
        public void Clean()
        {
            RegisterProperty = new Dictionary<string, PropertyLanguage>();
        }
        /// <summary>
        /// Appending the map of register <br />
        /// It will override the string if the key is the same
        /// </summary>
        /// <param name="v">Adding map source</param>
        public void Append(Dictionary<string, PropertyLanguage> v)
        {
            foreach(var i in v)
            {
                RegisterProperty[i.Key] = i.Value;
            }
        }
    }
}
