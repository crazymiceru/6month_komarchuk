using UnityEngine;

namespace SpritePlatformer
{
    public sealed class PackInteractiveData
    {
        public int attackPower=0;
        public TypeItem typeItem;
        public GameObject gameObject;
        public PackInteractiveData(int attackPower, TypeItem typeItem, GameObject gameObject)
        {
            this.attackPower = attackPower;
            this.typeItem = typeItem;
            this.gameObject = gameObject;
        }
    }
}