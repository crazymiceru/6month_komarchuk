using UnityEngine;

[CreateAssetMenu(menuName = "My/FireCfg",fileName = "FireUnit")]
public class FireCfg : ScriptableObject
{
    public float frequencyFire=5;
    public float attackPower = 5f;
    public Vector2 angleClamp=new Vector2(0,360);
}
