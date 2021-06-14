using UnityEngine;
using System;
using System.Linq;

namespace SpritePlatformer
{

    internal sealed class GameController : MonoBehaviour
    {
        private ListControllers _listControllers = new ListControllers();        
        private PoolInstatiate _poolInstatiate = new PoolInstatiate();
        private Reference _reference = new Reference();
        private Global _global;

        private void Awake()
        {
            Time.timeScale = 1;
            GC.Collect();
            LoadDataObjects.Clear();

            _global = LoadDataObjects.GetValue<Global>("Global");
            NewLabirinthLevel();
        }

        private void NewLabirinthLevel()
        {

            var player = (GameObject)new PlayerBuild(_poolInstatiate, _listControllers, _reference).CreateGameObject(_reference.Trash).AddComponents();
            _reference.player = player;

            //new BeeBuild(_poolInstatiate, _listControllers, _reference).CreateGameObject(_reference.Trash).AddComponents();
            //new CannonBuild(_poolInstatiate, _listControllers, _reference).CreateGameObject(_reference.Trash).AddComponents();

            FindAndCreateObjects();


            Transform background = FindObjectOfType<TagBackground>().transform;
            if (background == null) Debug.LogWarning($"Dont find Background");
            _listControllers.Add(
                new ParallaxController(
                    background, player.transform, _global.coefficientParallaxBackground, _global.SizeLoopBackground, _listControllers
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

        public void FindAndCreateObjects()
        {
            var objects = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IUnitView>();
            foreach (var item in objects)
            {
                var t = item.GetTypeItem();                
                if (t.type!=TypeItem.Player)ParseType(t.type).SetNumCfg(t.cfg).SetGameObject((item as MonoBehaviour).gameObject).AddComponents();
            }
        }

        private UnitBuildBasic ParseType(TypeItem typeItem)
        {
            return typeItem switch
            {                
                TypeItem.Player => new PlayerBuild(_poolInstatiate, _listControllers, _reference),
                TypeItem.Bee => new BeeBuild(_poolInstatiate, _listControllers, _reference),
                TypeItem.Cannon => new CannonBuild(_poolInstatiate, _listControllers, _reference),
                TypeItem.Core => new CoreBuild(_poolInstatiate, _listControllers, _reference),
                TypeItem.None => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}