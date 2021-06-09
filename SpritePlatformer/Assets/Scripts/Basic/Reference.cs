using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class Reference
    {
        private ControlLeak _controlLeak = new ControlLeak("Reference");
        private Camera _mainCamera;
        private Transform _trash;

        internal Camera MainCamera
        {
            get => _mainCamera != null ? _mainCamera : _mainCamera = Camera.main;
        }

        internal Transform Trash => _trash != null ? _trash : _trash = GameObject.FindObjectOfType<TagFolderActiveElements>().transform;
    }
}