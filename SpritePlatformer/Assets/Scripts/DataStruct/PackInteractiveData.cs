namespace SpritePlatformer
{
    public sealed class PackInteractiveData
    {
        public int attackPower=0;
        public TypeItem typeItem;
        public PackInteractiveData(int attackPower, TypeItem typeItem)
        {
            this.attackPower = attackPower;
            this.typeItem = typeItem;
        }
    }
}