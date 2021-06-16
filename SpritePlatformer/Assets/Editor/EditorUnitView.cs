using UnityEditor;
using UnityEngine;

namespace SpritePlatformer
{
    [CustomEditor(typeof(UnitView))]

    public class EditorUnitView : Editor
    {
        private IUnitView _unitView;
        private void OnEnable()
        {
            _unitView = target as IUnitView;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (_unitView != null)
            {
                float win = Screen.width;

                var typeItem = _unitView.GetTypeItem();
                var build = Utils.ParseType(typeItem.type, null, null, null);
                build.SetNumCfg(typeItem.cfg);
                EditorGUI.BeginDisabledGroup(true);
                foreach (var item in build.GetDataName())
                {
                    EditorGUILayout.ObjectField("Script", LoadDataObjects.GetValue<ScriptableObject>(item), typeof(ScriptableObject), false);
                }
                EditorGUI.BeginDisabledGroup(false);
            }
        }
    }
}