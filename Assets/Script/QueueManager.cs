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
        GameObject root = new GameObject("queue");
        root.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, -10, 0));
        root.SetActive(false);
        DataManager.Instance.queueTransform = root.transform;

        queueNow = new Queue<TetrominoBase>(QueueMaxCount);
        for (int i = 0; i < QueueMaxCount; i++)
            queueNow.Enqueue(RandomTetromino());
    }

    public TetrominoBase GetNextTetromino()
    {
        TetrominoBase t = queueNow.Dequeue();
        queueNow.Enqueue(RandomTetromino());
        UpdatePos();
        t.Initial();
        return t;
    }

    private TetrominoBase RandomTetromino()
    {
        TetrominoBase tb = null;
        int i = UnityEngine.Random.Range(0, DataManager.Instance.tetrominoSpriteName.Count - 1);
        foreach(var t in DataManager.Instance.tetrominoSpriteName.Keys)
        {
            if (i == 0)
            {
                tb = PoolManager.Instance.TetrominoPop(t);
                tb.Initial();
                tb.InitialOnQueue();
            }
            i--;
        }
        return tb;
    }

    private void UpdatePos()
    {
        foreach (var obj in queueNow)
            obj.InitialOnQueue();
    }
}
