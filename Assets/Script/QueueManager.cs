using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : Singleton<QueueManager>
{
    public const int QueueMaxCount = 4;

    public Queue<TetrominoBase> queueNow;

    public void Init()
    {
        queueNow = new Queue<TetrominoBase>(QueueMaxCount);
        for (int i = 0; i < QueueMaxCount; i++)
            queueNow.Enqueue(RandomTetromino());
    }

    public TetrominoBase GetNextTetromino()
    {
        TetrominoBase t = queueNow.Dequeue();
        queueNow.Enqueue(RandomTetromino());
        UpdatePos();
        t.OnShow();
        return t;
    }

    private TetrominoBase RandomTetromino()
    {
        int i = UnityEngine.Random.Range(0, DataManager.Instance.tetrominoTypes.Count - 1);
        Type t = DataManager.Instance.tetrominoTypes[i];
        return PoolManager.Instance.TetrominoPop(t);
    }

    private void UpdatePos()
    {
        foreach (var obj in queueNow)
            obj.InitialOnQueue();
    }
}
