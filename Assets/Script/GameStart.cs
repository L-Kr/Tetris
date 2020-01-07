using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private float dropTimeNow = 0f;
    private bool gameStart = false;

    void Start()
    {
        DataManager.Instance.Init();
        PoolManager.Instance.Init();
        QueueManager.Instance.Init();
        ShowBackGround();
        Debug.Log(typeof(TetrominoGreen));
    }

    private void Update()
    {
        if (!gameStart)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                gameStart = true;
                DataManager.Instance.ObjectNow = QueueManager.Instance.GetNextTetromino();
                Debug.Log("GameStart");
            }
            return;
        }

        dropTimeNow += Time.deltaTime;
        if(dropTimeNow >= DataManager.Instance.dropTime)
        {
            dropTimeNow = 0;
            DataManager.Instance.ObjectNow.UpdatePos();
        }

        if (Input.GetKeyDown(KeyCode.Space))
            DataManager.Instance.ObjectNow.ChangeShape();
        if (Input.GetKeyDown(KeyCode.A))
            DataManager.Instance.ObjectNow.UpdatePos(-1, 0);
        if (Input.GetKeyDown(KeyCode.D))
            DataManager.Instance.ObjectNow.UpdatePos(1, 0);
        if (Input.GetKeyDown(KeyCode.W))
            DataManager.Instance.dropTime += 0.1f;
        if (Input.GetKeyDown(KeyCode.S))
            DataManager.Instance.dropTime -= 0.1f;
    }

    /// <summary>
    /// 绘制背景
    /// </summary>
    private void ShowBackGround()
    {
        GameObject root = new GameObject("Droping");
        root.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        DataManager.Instance.rootTransform = root.transform;
        root = new GameObject("backGround");
        root.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        var sprite = Resources.Load<Sprite>("Texture/2background");
        for (int i = 0; i < DataManager.BackWidth; i++)
        {
            for (int j = 0; j < DataManager.BackHight; j++)
            {
                GameObject go = new GameObject();
                go.AddComponent<SpriteRenderer>().sprite = sprite;
                go.transform.parent = root.transform;
                go.transform.position = new Vector3(DataManager.TerominoWidth * i + root.transform.position.x, DataManager.TerominoWidth * j + root.transform.position.y, 1f);
                DataManager.Instance.Pos[i][j] = go.transform.position;
            }
        }
    }
}
