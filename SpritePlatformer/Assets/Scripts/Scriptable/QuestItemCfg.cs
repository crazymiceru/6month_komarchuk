using UnityEngine;

namespace SpritePlatformer
{

    [CreateAssetMenu(menuName = "My/QuestItemData",fileName ="QuestItem")]
    public sealed class QuestItemCfg : ScriptableObject
    {
        public Sprite image => _image;
        [SerializeField] private Sprite _image;
        public int[] completeItems => _completeItems;
        [SerializeField] private int[] _completeItems;
        public GameObject copletePrefab => _copletePrefab;
        [SerializeField] private GameObject _copletePrefab;
        public bool isMove => _isMove;
        [SerializeField] private bool _isMove=true;
    }
}
