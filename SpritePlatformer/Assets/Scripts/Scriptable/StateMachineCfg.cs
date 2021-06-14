using UnityEngine;

namespace SpritePlatformer
{
    [CreateAssetMenu(menuName = "My/StateMachine", fileName = "StateMachineUnit")]
    public sealed class StateMachineCfg : ScriptableObject
    {
        public StateData[] stateData;
    }
}
