using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoBase
{
    public RectObject[] rects;

    public void OnShow()
    {
        foreach (var rect in rects)
            rect.PosStateNow = 0;
    }

    public void InitialOnQueue()
    {
        foreach (var rect in rects)
            rect.InitialOnQueue();
    }

    public virtual void Initial()
    {
        Register();
    }

    public virtual void Destroy()
    {

    }

    protected virtual void Register()
    {

    }

    /// <summary>
    /// 自动下落
    /// </summary>
    public void UpdatePos()
    {
        foreach(var rect in rects)
        {
            if(!rect.IsPassble(0, -1))
            {
                TetrominoBase next = QueueManager.Instance.GetNextTetromino();
                DataManager.Instance.ObjectNow = next;
                foreach (var r in rects)
                    DataManager.Instance.CollisionNow[rect.PosVirtualNow.x][rect.PosVirtualNow.y] = true;
                return;
            }
        }
        foreach (var rect in rects)
            rect.PosVirtualNow = new Vector2Int(rect.PosVirtualNow.x, rect.PosVirtualNow.y - 1);
    }

    /// <summary>
    /// 玩家移动
    /// </summary>
    /// <param name="offset"></param>
    public void UpdatePos(int offsetX, int offsetY)
    {
        foreach (var rect in rects)
        {
            if (!rect.IsPassble(0, -1))
            {
                return;
            }
        }
        foreach (var rect in rects)
            rect.PosVirtualNow = new Vector2Int(rect.PosVirtualNow.x + offsetX , rect.PosVirtualNow.y + offsetY);
    }

    /// <summary>
    /// 当前碰撞
    /// </summary>
    public bool IsPassible(int offsetX = 0, int offsetY = 0)
    {
        foreach (var rect in rects)
            if (!rect.IsPassble(offsetX, offsetY))
                return false;
        return true;
    }

    /// <summary>
    /// 旋转
    /// </summary>
    public void ChangeShape()
    {
        //空指针保护
        foreach (var rect in rects)
            rect.ChangeShape();
        while(!IsPassible())
        {
            foreach (var rect in rects)
            {
                rect.PosVirtualNow = new Vector2Int(rect.PosVirtualNow.x, rect.PosVirtualNow.y + 1);
                if (rect.PosVirtualNow.y > DataManager.BackHight)
                    return;
            }
        }
    }
}
