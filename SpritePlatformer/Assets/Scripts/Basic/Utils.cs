using System;
using UnityEngine;

namespace SpritePlatformer
{
    public static class Utils
    {
        public static float ClampAround(this float value, float min, float max)
        {
            if (value < min || value > max)
            {
                var l1 = Mathf.Abs(Mathf.Min(min - value, 360 - (min - value)));
                var l2 = Mathf.Abs(Mathf.Min(max - value, 360 - (max - value)));
                if (l1 < l2) value = min; else value = max;
            }
            return value;
        }
        static public float SqrDist(Vector3 v1, Vector3 v2)
        {
            return (v1 - v2).sqrMagnitude;
        }
        public static Vector3 Change(this Vector3 org, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x == null ? org.x : (float)x, y == null ? org.y : (float)y, z == null ? org.z : (float)z);
        }

        public static Vector2 Change(this Vector2 org, object x = null, object y = null)
        {
            return new Vector2(x == null ? org.x : (float)x, y == null ? org.y : (float)y);
        }

        public static void SetPositionSpriteGround(this GameObject gameObject, GameObject targetGameObject)
        {

            var position = targetGameObject.transform.position;

            if (gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer sprite)
                && targetGameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteTarget))
            {
                gameObject.transform.position = position.Change(y: position.y + sprite.bounds.size.y / 2 - spriteTarget.bounds.size.y  / 2);
            }
        }

        public static UnitBuildBasic InstantiateCntr(GameObject gameObject, PoolInstatiate _poolInstatiate, ListControllers _listControllers, Reference _reference)
        {
            if (gameObject.TryGetComponent<IUnitView>(out IUnitView unitView))
            {
                var t = unitView.GetTypeItem();
                UnitBuildBasic go = Utils.ParseType(t.type, _poolInstatiate, _listControllers, _reference)
                           .SetNumCfg(t.cfg).SetGameObject(gameObject).AddComponents();
                return go;
            }
            Debug.LogWarning($"Dont find nitView at object {gameObject.name}");
            return null;
        }

        public static UnitBuildBasic ParseType(TypeItem typeItem, PoolInstatiate _poolInstatiate, ListControllers _listControllers, Reference _reference)
        {
            //Debug.Log($"Parse:{typeItem}");
            return typeItem switch
            {
                TypeItem.Player => new PlayerBuild(_poolInstatiate, _listControllers, _reference),
                TypeItem.Bee => new BeeBuild(_poolInstatiate, _listControllers, _reference),
                TypeItem.Cannon => new CannonBuild(_poolInstatiate, _listControllers, _reference),
                TypeItem.Core => new CoreBuild(_poolInstatiate, _listControllers, _reference),
                TypeItem.Coin => new CoinBuild(_poolInstatiate, _listControllers, _reference),
                TypeItem.Flag => new FlagBuild(_poolInstatiate, _listControllers, _reference),
                TypeItem.Box => new BoxBuild(_poolInstatiate, _listControllers, _reference),
                TypeItem.Exit => new ExitBuild(_poolInstatiate, _listControllers, _reference),
                TypeItem.QuestFindItem => new QuestFindItemBuild(_poolInstatiate, _listControllers, _reference),
                TypeItem.None => new EmptyBuild(_poolInstatiate, _listControllers, _reference),
                _ => new EmptyBuild(_poolInstatiate, _listControllers, _reference),
            };
        }
    }
}