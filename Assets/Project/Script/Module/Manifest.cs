namespace Colonizer
{
    [System.Serializable]
    public struct Manifest
    {
        public string Name;
        public string Docs;
        public string Link;
        public string Description;
        public string Version;
        public string Author;
        public ModuleDependent Dependents;

        public override string ToString()
        {
            return $"{Name}_{Version}_{Author}";
        }
    }
}
