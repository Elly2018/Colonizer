using System;

namespace Colonizer
{
    public interface IModuleLoader
    {
        /// <summary>
        /// Will return all the manifest including the disable one
        /// </summary>
        Manifest[] Manifests { get; }
        /// <summary>
        /// Will return all the enable modules
        /// </summary>
        ModuleContent[] Contents { get; }
        /// <summary>
        /// Will return current record
        /// </summary>
        ModuleRecord Records { get; }


        void SetEnable(Manifest target, bool value);
        void StartLoadingRecord();
        void StartLoadingModuleHeader();
        void StartLoadingModuleContent();


        void RegisterModuleAction(Action Unload, Action load);
        void RegisterRecordUpdate(Action recordupdate);
    }
}
