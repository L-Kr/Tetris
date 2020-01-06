using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoGreen : TetrominoBase
{
    protected override void Register()
    {
        DataManager.Instance.tetrominoTypes.Add(typeof(TetrominoGreen));
    }

    public override void Initial()
    {
        base.Initial();
        rects = new RectObject[4];
        
    }
}
