using UnityEngine;
using System;

namespace SpritePlatformer
{
    internal sealed class GameController : MonoBehaviour
    {
        private ListControllers _listControllers = new ListControllers();
        private LoadDataObjects _loadDataObjects = new LoadDataObjects();
        private PoolInstatiate _poolInstatiate = new PoolInstatiate();
        private Reference _reference = new Reference();

        private void Awake()
        {
            Time.timeScale = 1;
            GC.Collect();

            NewLabirinthLevel();
        }

        private void NewLabirinthLevel()
        {
            var player = (GameObject)new PlayerBuild(0, _poolInstatiate, _listControllers, _reference).CreateGameObject(_reference.Trash).AddComponents();

            new BeeBuild(0, _poolInstatiate, _listControllers, _reference).CreateGameObject(_reference.Trash).AddComponents();

            Transform background = FindObjectOfType<TagBackground>().transform;
            if (background == null) Debug.LogWarning($"Dont find Background");
            _listControllers.Add(
                new ParallaxController(
                    background, player.transform, new Vector2(0.5f, 0), new Vector2(18f, 0f), _listControllers
                    ));
            _listControllers.Add(
                new ParallaxController(
                    _reference.MainCamera.transform, player.transform, new Vector2(0f, 0f), new Vector2(0f, 0f), _listControllers
                    ));
        }

        private void Start()
        {
            _listControllers.Initialization();
        }

        private void Update()
        {
            _listControllers.Execute(Time.deltaTime);
        }

        private void LateUpdate()
        {
            _listControllers.LateExecute();
        }


    }
}