using UnityEngine;

namespace SpritePlatformer
{
    public sealed class Maze
    {
        static int x;
        static int y;

        public static void GenerateRandom(int[,] maze, int chance,int chancePlatform)
        {
            var maxX = maze.GetLength(0);
            var maxY = maze.GetLength(1);
            for (y = 0; y < maxY; y++)
            {
                for (x = 0; x < maxX; x++)
                {
                    if (x == 0 || y == 0 || x == maxX - 1 || y == maxY - 1 || Random.Range(0, 100) < chance)
                    {
                        maze[x, y] = 15;
                        int xAdd=0;
                        while (Random.Range(0, 100) < chancePlatform && x - xAdd > 0)
                        {
                            xAdd++;
                            maze[x - xAdd, y] = 15;
                        }
                        while (Random.Range(0, 100) < chancePlatform && x + xAdd < maxX - 1)
                        {
                            xAdd++;
                            maze[x + xAdd, y] = 15;
                        }


                    }
                }
            }
        }

        private static int BusyCells(int[,] maze, int x, int y)
        {
            return GetCells(maze, x - 1, y - 1) +
                    GetCells(maze, x, y - 1) +
                    GetCells(maze, x + 1, y - 1) +
                    GetCells(maze, x - 1, y) +
                    GetCells(maze, x, y) +
                    GetCells(maze, x + 1, y) +
                    GetCells(maze, x - 1, y + 1) +
                    GetCells(maze, x, y + 1) +
                    GetCells(maze, x + 1, y + 1);
        }

        private static int GetCells(int[,] maze, int x, int y)
        {
            var maxX = maze.GetLength(0);
            var maxY = maze.GetLength(1);
            if (x < 0 || x > maxX - 1 || y < 0 || y > maxY - 1) return 1;
            return maze[x, y] > 0 ? 1 : 0;
        }

        public static void MarchingSquares(int[,] maze)
        {
            var maxX = maze.GetLength(0);
            var maxY = maze.GetLength(1);

            for (y = 0; y < maxY; y++)
            {
                for (x = 0; x < maxX; x++)
                {
                    var a = GetCells(maze, x, y + 1);
                    var b = GetCells(maze, x + 1, y + 1);
                    var c = GetCells(maze, x, y);
                    var d = GetCells(maze, x + 1, y);

                    //if (a == 0 && b == 0 && c == 0 && d == 0) maze[x, y] = 0;
                    if (a == 0 && b == 0 && c == 0 && d > 0) maze[x, y] = 1;
                    else if (a == 0 && b == 0 && c > 0 && d == 0) maze[x, y] = 2;
                    else if (a == 0 && b == 0 && c > 0 && d > 0) maze[x, y] = 3;
                    else if (a > 0 && b == 0 && c == 0 && d == 0) maze[x, y] = 4;
                    else if (a > 0 && b == 0 && c == 0 && d > 0) maze[x, y] = 5;
                    else if (a > 0 && b == 0 && c > 0 && d == 0) maze[x, y] = 6;
                    else if (a > 0 && b == 0 && c > 0 && d > 0) maze[x, y] = 7;
                    else if (a == 0 && b > 0 && c == 0 && d == 0) maze[x, y] = 8;
                    else if (a == 0 && b > 0 && c == 0 && d > 0) maze[x, y] = 9;
                    else if (a == 0 && b > 0 && c > 0 && d == 0) maze[x, y] = 10;
                    else if (a == 0 && b > 0 && c > 0 && d > 0) maze[x, y] = 11;
                    else if (a > 0 && b > 0 && c == 0 && d == 0) maze[x, y] = 12;
                    else if (a > 0 && b > 0 && c == 0 && d > 0) maze[x, y] = 13;
                    else if (a > 0 && b > 0 && c > 0 && d == 0) maze[x, y] = 14;
                    else if (a > 0 && b > 0 && c > 0 && d > 0) maze[x, y] = 15;
                }
            }




        }

        public static void Smoth(int[,] maze, int count, Vector2Int cutOff)
        {
            var maxX = maze.GetLength(0);
            var maxY = maze.GetLength(1);
            for (int i = 0; i < count; i++)
            {
                for (y = 0; y < maxY; y++)
                {
                    for (x = 0; x < maxX; x++)
                    {
                        var number = BusyCells(maze, x, y);
                        if (number < cutOff.x) maze[x, y] = 0;
                        if (number > cutOff.y) maze[x, y] = 15;
                    }
                }
            }
        }


    }
}
