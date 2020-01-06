using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        ShowBackGround();
        DataManager.Instance.Init();
        QueueManager.Instance.Init();
        PoolManager.Instance.Init();
    }

    /// <summary>
    /// 绘制背景
    /// </summary>
    private void ShowBackGround()
    {
        GameObject root = new GameObject("backGround");
        root.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        var sprite = Resources.Load<Sprite>("Texture/2background");
        for (int i = 0; i < DataManager.BackWidth; i++)
        {
            for (int j = 0; j < DataManager.BackWidth; j++)
            {
                GameObject go = new GameObject();
                go.AddComponent<SpriteRenderer>().sprite = sprite;
                go.transform.parent = root.transform;
                go.transform.position = new Vector2(DataManager.TerominoWidth * i + root.transform.position.x, DataManager.TerominoWidth * j + root.transform.position.y);
                DataManager.Instance.Pos[i][j] = go.transform.position;
            }
        }
    }
}
