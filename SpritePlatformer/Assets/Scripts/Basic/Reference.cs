using UnityEngine;

namespace SpritePlatformer
{
    public sealed class Reference
    {
        private ControlLeak _controlLeak = new ControlLeak("Reference");
        private Camera _mainCamera;
        private Transform _trash;

        internal Camera MainCamera
        {
            get => _mainCamera != null ? _mainCamera : _mainCamera = Camera.main;
        }

        internal GameObject player;
        internal Transform Trash => _trash != null ? _trash : _trash = GameObject.FindObjectOfType<TagFolderActiveElements>().transform;
        internal GameM gameM => _gameM != null ? _gameM : _gameM = new GameM();
        private GameM _gameM;

        internal UnitM playerM => _playerM != null ? _playerM : _playerM = new UnitM();
        private UnitM _playerM;

        private Transform _canvas;
        internal Transform Canvas => _canvas != null ? _canvas : _canvas = GameObject.FindObjectOfType<TagCanvas>().transform;

    }
}