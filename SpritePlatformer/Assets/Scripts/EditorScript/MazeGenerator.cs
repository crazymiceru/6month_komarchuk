using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

namespace SpritePlatformer
{
    [ExecuteInEditMode]
    public class MazeGenerator : MonoBehaviour
    {
        public Vector2Int mazeSize = new Vector2Int(10, 10);
        public Vector2Int smothCutOff = new Vector2Int(2, 4);
        public int smothCount = 10;
        public Tilemap tilemap;
        public Tile tile;
        public AstarPath astarPath;
        public int chance = 50;
        public int chanceWidthPlatform = 60;
        public Tile[] tileMarchingSquares = new Tile[16];
        private int[,] arrayMaze;

        private void MakeMazeTile(int[,] maze)
        {
            var maxX = maze.GetLength(0);
            var maxY = maze.GetLength(1);

            tilemap.ClearAllTiles();

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    if (maze[x, y] > 0) tilemap.SetTile(new Vector3Int(x - maxX / 2, y - maxY / 2, 0), tileMarchingSquares[maze[x, y]]);
                }
            }
        }

        private void GetMazeTile(int[,] maze)
        {            
            var maxX = maze.GetLength(0);
            var maxY = maze.GetLength(1);

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    if (tilemap.GetTile(new Vector3Int(x - maxX / 2, y - maxY / 2, 0))!=null)
                    {
                        maze[x, y] = 1;
                    }
                    else maze[x, y] = 0;
                }
            }
        }


        public void Generate()
        {
            arrayMaze = new int[mazeSize.x, mazeSize.y];
            Maze.GenerateRandom(arrayMaze, chance, chanceWidthPlatform);
            Maze.Smoth(arrayMaze, smothCount, smothCutOff);
            Maze.MarchingSquares(arrayMaze);
            MakeMazeTile(arrayMaze);

            SetAstarParam();
        }

        private void SetAstarParam()
        {
            GridGraph gridGraphs = astarPath.data.gridGraph;
            gridGraphs.SetDimensions(mazeSize.x * 2, mazeSize.y * 2, 0.5f);
        }
    }
}
