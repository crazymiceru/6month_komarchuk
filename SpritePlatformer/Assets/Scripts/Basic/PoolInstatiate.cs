using System.Collections.Generic;
using UnityEngine;

namespace SpritePlatformer
{
    public sealed class PoolInstatiate
    {
        private Dictionary<int, PoolPrefabObjectsDataArray> poolObjects = new Dictionary<int, PoolPrefabObjectsDataArray>();
        private Dictionary<GameObject, int> getIdPrefab = new Dictionary<GameObject, int>();

        public void DestroyGameObject(GameObject gameObject)
        {
            if (getIdPrefab.TryGetValue(gameObject, out int idPrefab))
            {
                getIdPrefab.Remove(gameObject);
                if (poolObjects.TryGetValue(idPrefab, out PoolPrefabObjectsDataArray poolObjectsDataArray))
                {
                    if (poolObjectsDataArray.objects.Count == 1)
                    {
                        poolObjectsDataArray.objects[0].SetActive(false);
                        //Debug.Log($"PoolInstatiate Deactivate last object idPrefab:{idPrefab}");
                    }
                    else
                    {
                        //Debug.Log($"PoolInstatiate Destroy object idPrefab:{idPrefab}");
                        poolObjectsDataArray.objects.Remove(gameObject);
                        GameObject.Destroy(gameObject);
                    }
                }
                else Debug.LogWarning( $"PoolInstatiate Dont Have key idPrefab for destroy");
            }
            else
            {
                GameObject.Destroy(gameObject);
            }
        }

        public GameObject Instantiate(GameObject prefabObject)
        {
            GameObject resultGameObject;

            var id = prefabObject.GetInstanceID();
            if (poolObjects.TryGetValue(id, out PoolPrefabObjectsDataArray poolObjectsDataArray))
            {
                if (poolObjectsDataArray.objects[0].activeSelf)
                {
                    resultGameObject = GameObject.Instantiate(poolObjectsDataArray.objects[0]);
                    poolObjectsDataArray.objects.Add(resultGameObject);
                }
                else
                {
                    resultGameObject = poolObjectsDataArray.objects[0];
                    resultGameObject.SetActive(true);
                }
            }
            else
            {
                poolObjects.Add(id, new PoolPrefabObjectsDataArray());
                poolObjects[id].objects = new List<GameObject>();

                resultGameObject = GameObject.Instantiate(prefabObject);
                poolObjects[id].objects.Add(resultGameObject);
                poolObjects[id].positions = prefabObject.transform.localPosition;
                poolObjects[id].rotation = prefabObject.transform.localRotation;
                poolObjects[id].scale = prefabObject.transform.localScale;
                poolObjects[id].name = prefabObject.name;


            }

            if (!getIdPrefab.ContainsKey(resultGameObject)) getIdPrefab.Add(resultGameObject, id);
            resultGameObject.transform.localPosition = poolObjects[id].positions;
            resultGameObject.transform.localRotation = poolObjects[id].rotation;
            resultGameObject.transform.localScale = poolObjects[id].scale;
            resultGameObject.name = poolObjects[id].name;

            if (resultGameObject.TryGetComponent(out IPool pool))
            {
                pool.SetPoolDestroy(this);
                pool.ClearEvt();
            }
            else Debug.LogWarning($"Dont find component IPool in object: {resultGameObject.name}");

            return resultGameObject;
        }

    }
}
