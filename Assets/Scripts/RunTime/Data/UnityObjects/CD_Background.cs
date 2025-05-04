using RunTime.Data.ValueObjects;
using UnityEngine;

namespace RunTime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Background", menuName = "SO/CD_Background", order = 0)]
    public class CD_Background : ScriptableObject
    {
        public BGSpeedDatas BGSpeedDatas;
    }
}