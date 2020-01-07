using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public const int BackWidth = 10;
    public const int BackHight = 20;
    public const float TerominoWidth = 0.8f;
    public Dictionary<Type, string> tetrominoSpriteName;
    public List<RectObject> dropedObj;

    public float dropTime = 1f;

    public Vector2[][] Pos;

    public bool[][] CollisionNow;

    public TetrominoBase ObjectNow;

    public Transform poolTransform;
    public Transform rootTransform;
    public Transform queueTransform;
   
    public void Init()
    {
        Pos = new Vector2[BackWidth][];
        for (int i = 0; i < BackWidth; i++)
            Pos[i] = new Vector2[BackHight + 5];

        CollisionNow = new bool[BackWidth][];
        for (int i = 0; i < BackWidth; i++)
            CollisionNow[i] = new bool[BackHight + 5];

        dropedObj = new List<RectObject>();
        TetrominoSpriteInit();
    }

    public void TetrominoSpriteInit()
    {
        tetrominoSpriteName = new Dictionary<Type, string>();
        tetrominoSpriteName.Add(typeof(TetrominoGreen), TetrominoGreen.SpriteName);
    }

    public void CheckKill()
    {
        for (int i = 0; i < BackHight; i++)
        {
            int collisionCount = 0;
            for(int j = 0; j < BackWidth; j++)
            {
                if (CollisionNow[j][i])
                    collisionCount++;
            }
            if(collisionCount >= BackWidth)
            {
                for(int k = 0;k< dropedObj.Count;k++)
                {
                    var rect = dropedObj[k];
                    if(rect.PosVirtualNow.y == i)
                    {
                        rect.Destroy();
                        dropedObj.RemoveAt(k);
                        k--;
                    }
                    else if(rect.PosVirtualNow.y > i)
                    {
                        rect.PosVirtualNow = new Vector2Int(rect.PosVirtualNow.x, rect.PosVirtualNow.y - 1);
                    }
                }
                UpdateCollision();
            }
            else if(collisionCount == 0)
            {
                break;
            }
        }
    }

    public void UpdateCollision()
    {
        for (int i = 0; i < BackWidth; i++)
        {
            for (int j = 0; j < BackHight; j++)
            {
                CollisionNow[i][j] = false;
            }
        }
        foreach (var rect in dropedObj)
            CollisionNow[rect.PosVirtualNow.x][rect.PosVirtualNow.y] = true;
    }
}
