using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoGreen : TetrominoBase
{
    public const string SpriteName = "Texture/1green";

    public override void Initial()
    {
        base.Initial();
        rects = new RectObject[4];

        Vector2Int[][] posRect = new Vector2Int[4][];
        for (int i = 0; i < 4; i++)
            posRect[i] = new Vector2Int[2];

        posRect[0][0] = new Vector2Int(2 + posStartX, 1 + posStartY);
        posRect[0][1] = new Vector2Int(0 + posStartX, 2 + posStartY);
        posRect[1][0] = new Vector2Int(1 + posStartX, 1 + posStartY);
        posRect[1][1] = new Vector2Int(0 + posStartX, 1 + posStartY);
        posRect[2][0] = new Vector2Int(1 + posStartX, 0 + posStartY);
        posRect[2][1] = new Vector2Int(1 + posStartX, 1 + posStartY);
        posRect[3][0] = new Vector2Int(0 + posStartX, 0 + posStartY);
        posRect[3][1] = new Vector2Int(1 + posStartX, 0 + posStartY);

        for (int i = 0; i < 4; i++)
        {
            rects[i] = PoolManager.Instance.RectObjPop(SpriteName);
            rects[i].PosList = posRect[i];
            rects[i].Initial();
        }
    }

    public override void Destroy()
    {
        base.Destroy();
        PoolManager.Instance.TetrominoPush(this);
    }
}
