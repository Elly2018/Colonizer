using System;

namespace Colonizer
{
    [System.Serializable]
    public struct Manifest: IEquatable<Manifest>
    {
        public string Name;
        public string Docs;
        public string Link;
        public string Description;
        public string Version;
        public string Author;
        public ModuleDependent Dependents;

        public override bool Equals(object obj)
        {
            return obj is Manifest manifest &&
                   Name == manifest.Name &&
                   Version == manifest.Version &&
                   Author == manifest.Author;
        }

        public bool Equals(Manifest other)
        {
            return Name == other.Name &&
                   Version == other.Version &&
                   Author == other.Author;
        }

        public override string ToString()
        {
            return $"{Name}_{Version}_{Author}";
        }
    }
}
