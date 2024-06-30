using System;

namespace Colonizer
{
    public interface IPropertiesBankAccess
    {
        object GetInfo(Type type, string Label);
        T GetInfo<T>(string Label);
        object GetData(Type type, string Label);
        T GetData<T>(string Label);
    }
}
