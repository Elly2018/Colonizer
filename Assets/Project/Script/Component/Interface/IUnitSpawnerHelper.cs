namespace Colonizer
{
    public interface IUnitSpawnerHelper
    {
        GameObjectType Type { set; get; }

        void CreateProperties(PropertyHeader[] Headers);
    }
}
