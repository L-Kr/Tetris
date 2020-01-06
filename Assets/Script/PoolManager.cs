using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<Type, Stack<TetrominoBase>> pool;

    public void Init()
    {
        pool = new Dictionary<Type, Stack<TetrominoBase>>();
        foreach (var t in DataManager.Instance.tetrominoTypes)
            pool.Add(t, new Stack<TetrominoBase>());
    }

    public TetrominoBase TetrominoPop(Type t)
    {
        TetrominoBase tetromino;
        if (pool[t].Count == 0)
            tetromino = Activator.CreateInstance(t) as TetrominoBase;
        else
            tetromino = pool[t].Pop();
        tetromino.Initial();
        return tetromino;
    }

    public void TetrominoPush<T>(T tetromino) where T : TetrominoBase
    {
        tetromino.Destroy();
        pool[typeof(T)].Push(tetromino as TetrominoBase);
    }
}
