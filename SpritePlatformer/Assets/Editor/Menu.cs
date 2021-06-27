
using UnityEditor;

namespace SpritePlatformer
{
    public class Menu
    {
        [MenuItem("My/Maze Generator")]
        private static void MenuOption()
        {
            EditorWindow.GetWindow(typeof(MazeGenerator), false, "Maze Generator");
        }

    }
}