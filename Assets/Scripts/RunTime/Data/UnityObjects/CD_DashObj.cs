using RunTime.Data.ValueObjects.DashObject;
using UnityEngine;

namespace RunTime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_DashObj", menuName = "SO/CD_DashObj", order = 0)]
    public class CD_DashObj : ScriptableObject
    {
        public DashObjData DashObjData;
    }
}