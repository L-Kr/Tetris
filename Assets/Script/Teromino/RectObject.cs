using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectObject
{
    private Vector2Int[] posList;
    private GameObject go;

    private int _posStateNow = 0;
    public int PosStateNow
    {
        get
        {
            return _posStateNow;
        }
        set
        {
            if(_posStateNow != value)
            {
                _posStateNow = value;
                int x = posList[_posStateNow].x;
                int y = posList[_posStateNow].y;
                go.transform.position = DataManager.Instance.Pos[x][y];
            }
        }
    }

    public Vector2Int PosVirtualNow
    {
        get
        {
            return posList[_posStateNow];
        }
        set
        {
            Vector2Int offset = posList[_posStateNow] - value;
            for (int i = 0; i < posList.Length; i++)
                posList[i] -= offset;
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
        int x = posList[PosStateNow].x + offsetX;
        int y = posList[PosStateNow].y + offsetY;
        bool passible = !DataManager.Instance.CollisionNow[x][y];
        return passible;
    }

    public RectObject(Vector2Int[] posList, Sprite sprite)
    {
        this.posList = posList;
        go = new GameObject();
        go.AddComponent<SpriteRenderer>().sprite = sprite;
    }

    public void ChangeShape()
    {
        if (posList == null)
            return;
        if (PosStateNow == posList.Length - 1)
            PosStateNow = 0;
        else
            PosStateNow++;
    }

    public void InitialOnQueue()
    {
        go.transform.position = new Vector2(-2, 0);
    }
}
