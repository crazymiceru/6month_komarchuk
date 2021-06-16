using System.Collections.Generic;
using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class FlagsCanvasController : IController
    {
        private ControlLeak _controlLeak = new ControlLeak("FlagsCanvasController");
        private GameM _gameM;
        private Transform posFlags;
        private List<GameObject> flagsIco=new List<GameObject>();
        private GameObject prefabFlagIco;
        private float width = 25f;

        internal FlagsCanvasController(GameM gameM)
        {
            _gameM = gameM;
            _gameM.evtFlags += UpdateFlags;
            posFlags = GameObject.FindObjectOfType<TagPosFlags>().transform;
            prefabFlagIco = LoadDataObjects.GetValue<GameObject>("Canvas/FlagIco");
        }

        private void UpdateFlags()
        {
            while (flagsIco.Count> _gameM.countFlags)
            {
                GameObject.Destroy(flagsIco[flagsIco.Count - 1]);
                flagsIco.RemoveAt(flagsIco.Count - 1);
            }

            for (int i = 0; i < _gameM.countFlags; i++)
            {
                if (flagsIco.Count < i + 1)
                {
                    var go = GameObject.Instantiate(prefabFlagIco, posFlags);
                    go.transform.localPosition = new Vector3(-i * width, 0, 0);
                    flagsIco.Add(go);
                }
            }
        }
    }
}