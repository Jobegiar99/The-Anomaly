using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGenerator
{
    public int[,] matrix;
    public int size;
    private Point start;
    private Point end;
    private System.Random rand;
    

    public SkeletonGenerator()
    {

        
        rand = new System.Random();
        size = rand.Next(3,7);
        matrix = GenerateMatrix();
        matrix[start.row, start.column] = 1;
        matrix[end.row, end.column] = 1;
        matrix = ResizeMatrix(matrix, size);

    }


    public int[,] GenerateMatrix()
    {

        int[,] matrix = FillMatrix();

        System.Random rand = new System.Random();
        Point start = new Point(14, rand.Next(100));
        Point end = new Point(0, rand.Next(100));

        matrix = GenerateLevel(matrix, start, end);

        return matrix;

    }

    public int[,] ResizeMatrix(int[,] matrix, int size)
    {

        int[,] newMatrix = new int[15 * size, 100 * size];
        for (int row = 0; row < 15; row++)
        {
            for (int column = 0; column < 100; column++)
            {
                for (int nRow = row * size; nRow < (row * size) + size; nRow++)
                {
                    for (int nColumn = column * size; nColumn < (column * size) + size; nColumn++)
                    {

                        newMatrix[nRow, nColumn] = matrix[row, column];


                    }
                }
            }
        }
        return newMatrix;
    }

    private int[,] FillMatrix()
    {

        int[,] matrix = new int[15, 100];

        for (int row = 0; row < 15; row++)
        {
            for (int column = 0; column < 100; column++)
            {

                matrix[row,column] = 1;

            }
        }
        return matrix;
    }



    private int[,] GenerateLevel(int[,] matrix, Point wander, Point seeker)
    {

        this.start = wander;
        this.end = seeker;

        List<Point> createdPath = new List<Point>();

        while (!createdPath.Contains(seeker))
        {
            matrix[wander.row,wander.column] = 0;
            matrix[seeker.row,seeker.column] = 0;

            createdPath.Add(wander);
            createdPath.Add(seeker);

            wander = GetWandererNextMove(matrix, createdPath, wander);
            seeker = GetSeekerNextMove(matrix, createdPath, wander, seeker);

        }
        return matrix;

    }

    private static bool Between(int lowlimit, int highlimit, int value)
    {
        return (lowlimit <= value && value <= highlimit);
    }

    private bool CheckIfValidOption(int low, int high, int value, List<Point> createdPath, Point point)
    {
        return (Between(low, high, value) && !createdPath.Contains(point));
    }

    private void CheckWandererPossiblePathOptions(ref List<Point> walkablePath,
                                                         ref Queue<Point> pathExploration,
                                                         int[,] matrix,
                                                         Point currentPoint)
    {

        Point up = new Point(currentPoint.row - 1, currentPoint.column);
        Point down = new Point(currentPoint.row + 1, currentPoint.column);
        Point left = new Point(currentPoint.row, currentPoint.column - 1);
        Point right = new Point(currentPoint.row, currentPoint.column + 1);
        List<Point> movementInfo = new List<Point>() { up, down, left, right };

        for (int i = 0; i < 4; i++)
        {
            int value = (i < 2) ? movementInfo[i].row : movementInfo[i].column;
            int size = (i < 2) ? 13 : 98;
            int row = movementInfo[i].row;
            int col = movementInfo[i].column;

            if (CheckIfValidOption(1, size, value, walkablePath, movementInfo[i]) && matrix[row,col] == 0)
            {
                pathExploration.Enqueue(movementInfo[i]);
                walkablePath.Add(movementInfo[i]);
            }
        }
    }

    private Point CheckWandererPossibleMoves(int[,] matrix, Point wanderer, List<Point> createdPath)
    {

        Queue<Point> pathExploration = new Queue<Point>();
        List<Point> walkablePath = new List<Point>();

        pathExploration.Enqueue(wanderer);
        while (pathExploration.Count > 0)
        {
            Point point = pathExploration.Dequeue();
            CheckWandererPossiblePathOptions(ref walkablePath, ref pathExploration, matrix, point);
        }

        return walkablePath[rand.Next(walkablePath.Count)];

    }

    private Point CreateWandererNextMove(List<Point> moves, int[,] matrix, List<Point> createdPath, Point wanderer)
    {

        List<Point> possibleMoves = new List<Point>();

        for (int i = 0; i < 4; i++)
        {
            int value = (i < 2) ? moves[i].row : moves[i].column;
            int size = (i < 2) ? 13 : 98; //row amount and column amount - 2
            if (CheckIfValidOption(1, size, value, createdPath, moves[i]))
                possibleMoves.Add(moves[i]);
        }


        if (possibleMoves.Count > 0)
            return possibleMoves[rand.Next(possibleMoves.Count)];


        return CheckWandererPossibleMoves(matrix, wanderer, createdPath);

    }


    private Point GetWandererNextMove(int[,] matrix, List<Point> createdPath, Point pointA)
    {

        Point up = new Point(pointA.row - 1, pointA.column);
        Point down = new Point(pointA.row + 1, pointA.column);
        Point left = new Point(pointA.row, pointA.column - 1);
        Point right = new Point(pointA.row, pointA.column + 1);

        List<Point> moves = new List<Point> { up, down, left, right };

        return CreateWandererNextMove(moves, matrix, createdPath, pointA);


    }


    private int ManhattanDistance(Point first, Point second)
    {
        int rowResult = (int)Math.Abs(first.row - second.row);
        int columnResult = (int)Math.Abs(first.column - second.column);
        return rowResult + columnResult;
    }


    private  Point CreateSeekerNextMove(List<int> distances, List<Point> moves, int[,] matrix, List<Point> createdPath)
    {
        /*
            each index corresponds to: 
            distances = original, up, down, left , right
            moves = up, down, left, right
        */
        List<Point> possibleMoves = new List<Point>();

        for (int i = 0; i < 4; i++)
        {
            int value = (i < 2) ? moves[i].row : moves[i].column;
            int size = (i < 2) ?  13 : 98;// row and column amount - 2
            if (CheckIfValidOption(0, size, value, createdPath, moves[i]) && distances[0] >= distances[i + 1])
                possibleMoves.Add(moves[i]);
        }

        if (possibleMoves.Count > 0)
            return possibleMoves[this.rand.Next(possibleMoves.Count)];

        return new Point(this.rand.Next(1, 13), this.rand.Next(1, 98));

    }


    private  Point GetSeekerNextMove(int[,] matrix,
                                 List<Point> createdPath,
                                 Point pointA,
                                 Point pointB)
    {

        Point up = new Point(pointB.row - 1, pointB.column);
        Point down = new Point(pointB.row + 1, pointB.column);
        Point left = new Point(pointB.row, pointB.column - 1);
        Point right = new Point(pointB.row, pointB.column + 1);

        int originalDistance = ManhattanDistance(pointA, pointB);
        int upDistance = ManhattanDistance(pointA, up);
        int downDistance = ManhattanDistance(pointA, down);
        int leftDistance = ManhattanDistance(pointA, left);
        int rightDistance = ManhattanDistance(pointA, right);

        List<int> distances = new List<int> { originalDistance, upDistance, downDistance, leftDistance, rightDistance };
        List<Point> moves = new List<Point> { up, down, left, right };
        return CreateSeekerNextMove(distances, moves, matrix, createdPath);

    }
}
