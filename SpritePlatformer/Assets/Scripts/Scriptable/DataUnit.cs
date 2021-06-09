using UnityEngine;

namespace SpritePlatformer
{
    [CreateAssetMenu(menuName = "My/UnitData")]
    public sealed class DataUnit : ScriptableObject
    {
        [Header("Move")]
        public float powerMove = 500f;
        public float powerJump = 300f;

        [Header("Limits")]
        public float maxSpeed = 10;

        [Header("Live")]
        public int maxLive = 1;
        public GameObject destroyEffects;
        public float timeViewDestroyEffects = 10;

        [Header("Attack")]
        public int AttackPower = 1;
        public int addScores = 0;
    }
}
