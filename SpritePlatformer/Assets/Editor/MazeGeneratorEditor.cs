using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SpritePlatformer
{
    [CustomEditor(typeof(MazeGenerator))]
    public class MazeGeneratorEditor:Editor
    { 
        private bool isShow;

        private void OnEnable()
        {
            
        }

        public override void OnInspectorGUI()
        {
            var _mazeGenerator = target as MazeGenerator;
            float win = Screen.width;
            var win2 = win / 2;
            //GUILayout.Label("Редактор", EditorStyles.boldLabel);
            //GUILayout.Space(10);
            _mazeGenerator.tilemap = (Tilemap)EditorGUILayout.ObjectField("TileMap:", _mazeGenerator.tilemap, typeof(Tilemap), true);
            _mazeGenerator.tile = (Tile)EditorGUILayout.ObjectField("Tile:", _mazeGenerator.tile, typeof(Tile), true);
            _mazeGenerator.astarPath = (AstarPath)EditorGUILayout.ObjectField("Tile:", _mazeGenerator.astarPath, typeof(AstarPath), true);
            _mazeGenerator.mazeSize = EditorGUILayout.Vector2IntField("Size Maze:", _mazeGenerator.mazeSize);
            _mazeGenerator.smothCutOff = EditorGUILayout.Vector2IntField("Smoth Cut Off:", _mazeGenerator.smothCutOff);
            _mazeGenerator.smothCount = EditorGUILayout.IntField("Smoth Count:", _mazeGenerator.smothCount);
            _mazeGenerator.chance = EditorGUILayout.IntSlider("Chance:",_mazeGenerator.chance,1,99);
            _mazeGenerator.chanceWidthPlatform = EditorGUILayout.IntSlider("Width:",_mazeGenerator.chanceWidthPlatform, 1,99);
            isShow = EditorGUILayout.BeginFoldoutHeaderGroup(isShow, "Configure Tiles");
            if (isShow)
            {
                for (int i = 0; i < _mazeGenerator.tileMarchingSquares.Length; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField($"{i,2}:", GUILayout.Width(20));
                    _mazeGenerator.tileMarchingSquares[i] = (Tile)EditorGUILayout.ObjectField(_mazeGenerator.tileMarchingSquares[i],typeof(Tile), allowSceneObjects: true);
                    
                    
                    EditorGUI.BeginDisabledGroup(true);
                    if (_mazeGenerator.tileMarchingSquares[i]!=null)
                        EditorGUILayout.ObjectField(_mazeGenerator.tileMarchingSquares[i].sprite, typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50));
                    else EditorGUILayout.ObjectField(null, typeof(Sprite), true, GUILayout.Width(50), GUILayout.Height(50));
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Make Maze", EditorStyles.miniButtonLeft, GUILayout.Width(win2)))
            {
                _mazeGenerator.Generate();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
