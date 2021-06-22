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

            new FlagsCanvasController(_reference.gameM);
            new LivesCanvasController(_reference.playerM);
            _listControllers.Add(new ScoresCanvasController(_reference.playerM)); 
            FindAndCreateObjects();
            



            Transform background = FindObjectOfType<TagBackground>().transform;
            if (background == null) Debug.LogWarning($"Dont find Background");
            _listControllers.Add(
                new ParallaxController(
                    background, player.transform, _global.coefficientParallaxBackground, _global.SizeLoopBackground, _listControllers
                    ));
            Transform backgroundFly = FindObjectOfType<TagBackgroundFly>().transform;
            if (background == null) Debug.LogWarning($"Dont find BackgroundFly");
            _listControllers.Add(
                new ParallaxController(
                    backgroundFly, player.transform, _global.coefficientParallaxBackgroundFly, new Vector2(0f, 0f), _listControllers
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
            _reference.gameM.countFlags = 0;
            foreach (var item in objects)
            {
                var t = item.GetTypeItem();
                //Debug.Log($"GameObject:{(item as MonoBehaviour).name}");
                if (t.type!=TypeItem.Player)Utils.ParseType(t.type,_poolInstatiate,_listControllers,_reference)
                        .SetNumCfg(t.cfg).SetGameObject((item as MonoBehaviour).gameObject).AddComponents();                
            }
        }
    }
}