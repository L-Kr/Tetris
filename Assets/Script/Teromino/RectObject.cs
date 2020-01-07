using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectObject
{
    public Vector2Int[] PosList;
    private GameObject go;
    private string spriteName;  //带路径

    private int _posStateNow = 0;
    public int PosStateNow
    {
        get
        {
            return _posStateNow;
        }
        set
        {
            _posStateNow = value;
            UpdateGoPos();
        }
    }

    public Vector2Int PosVirtualNow
    {
        get
        {
            return PosList[_posStateNow];
        }
        set
        {
            Vector2Int offset = PosList[_posStateNow] - value;
            for (int i = 0; i < PosList.Length; i++)
                PosList[i] -= offset;
            UpdateGoPos();
        }
    }
    
    /// <summary>
    /// 碰撞检验
    /// </summary>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    /// <returns></returns>
    public bool IsPassble(int offsetX = 0, int offsetY = 0)
    {
        int x = PosList[PosStateNow].x + offsetX;
        int y = PosList[PosStateNow].y + offsetY;
        if (x < 0 || y < 0 || x >= DataManager.BackWidth)
            return false;
        return !DataManager.Instance.CollisionNow[x][y];
    }

    public RectObject(string spriteName)
    {
        go = new GameObject();
        go.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spriteName);
        this.spriteName = spriteName;
    }

    public void ChangeShape()
    {
        if (PosList == null)
            return;
        if (PosStateNow == PosList.Length - 1)
            PosStateNow = 0;
        else
            PosStateNow++;
    }

    public void InitialOnQueue()
    {
        go.transform.parent = DataManager.Instance.queueTransform;
        go.transform.position = Vector2.zero;
    }

    public void Initial()
    {
        go.transform.parent = DataManager.Instance.rootTransform;
        PosStateNow = 0;
    }

    public void Destroy()
    {
        go.transform.parent = DataManager.Instance.poolTransform;
        go.transform.position = Vector2.zero;
        PosStateNow = 0;
        PosList = null;
        PoolManager.Instance.RectObjPush(spriteName, this);
    }

    private void UpdateGoPos()
    {
        int x = PosList[_posStateNow].x;
        int y = PosList[_posStateNow].y;
        go.transform.position = DataManager.Instance.Pos[x][y];
        if (y >=  DataManager.BackHight)
            go.SetActive(false);
        else if (!go.activeSelf)
            go.SetActive(true);
    }
}
