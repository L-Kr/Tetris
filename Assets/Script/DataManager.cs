using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public const int BackWidth = 10;
    public const int BackHight = 20;
    public const float TerominoWidth = 0.8f;
    public List<Type> tetrominoTypes;

    public float dropTime = 1f;

    public Vector2[][] Pos;

    public bool[][] CollisionNow;

    public TetrominoBase ObjectNow;
   
    public void Init()
    {
        Pos = new Vector2[BackWidth][];
        for (int i = 0; i < BackWidth; i++)
            Pos[i] = new Vector2[BackHight];

        CollisionNow = new bool[BackWidth][];
        for (int i = 0; i < BackWidth; i++)
            CollisionNow[i] = new bool[BackHight];

        tetrominoTypes = new List<Type>();
    }
}
