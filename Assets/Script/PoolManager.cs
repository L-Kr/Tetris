using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<Type, Stack<TetrominoBase>> poolTetromino;
    private Dictionary<string, Stack<RectObject>> poolRectObject;

    public void Init()
    {
        GameObject root = new GameObject("pool");
        root.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(-10, 0, 0));
        root.SetActive(false);
        DataManager.Instance.poolTransform = root.transform;

        poolTetromino = new Dictionary<Type, Stack<TetrominoBase>>();
        foreach (var t in DataManager.Instance.tetrominoSpriteName.Keys)
            poolTetromino.Add(t, new Stack<TetrominoBase>());

        poolRectObject = new Dictionary<string, Stack<RectObject>>();
        foreach (var s in DataManager.Instance.tetrominoSpriteName.Values)
            poolRectObject.Add(s, new Stack<RectObject>());
    }

    public TetrominoBase TetrominoPop(Type t)
    {
        TetrominoBase tetromino;
        if (poolTetromino[t].Count == 0)
            tetromino = Activator.CreateInstance(t) as TetrominoBase;
        else
            tetromino = poolTetromino[t].Pop();
        return tetromino;
    }

    public void TetrominoPush<T>(T tetromino) where T : TetrominoBase
    {
        if(!poolTetromino.ContainsKey(typeof(T)))
        {
            Debug.LogError(typeof(T));
            Debug.Break();
        }
        poolTetromino[typeof(T)].Push(tetromino as TetrominoBase);
    }

    public RectObject RectObjPop(string name)
    {
        RectObject obj;
        if(poolRectObject[name].Count == 0)
            obj = new RectObject(name);
        else
            obj = poolRectObject[name].Pop();
        return obj;
    }

    public void RectObjPush(string name, RectObject obj)
    {
        poolRectObject[name].Push(obj);
    }
}
