using UnityEngine;

namespace SpritePlatformer
{

    [CreateAssetMenu(menuName = "My/AnimationData",fileName ="UnitAnimation")]
    public sealed class AnimationCfg : ScriptableObject
    {
        public float speed=10f;
        public AnimationData[] animationData => _animationData;
        [SerializeField] private AnimationData[] _animationData;
    }
}
