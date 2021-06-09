using UnityEngine;

namespace SpritePlatformer
{
    public interface IUnitView
    {
        (TypeItem type, int cfg) GetTypeItem();
        void SetTypeItem(TypeItem type = TypeItem.None, int cfg = -1);

        public Transform objectTransform { get; }
        public Rigidbody2D objectRigidbody2D { get; }
        public SpriteRenderer objectSpriteRednderer { get; }

    }
}