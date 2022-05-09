using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotatingPlatform : MonoBehaviour
{
    public RotatingSide side;

    private void Start()
    {
        if (side.Equals(RotatingSide.Right))
        {
            transform.DORotate(new Vector3(0f, 0f, -360f), 4f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }
        else
        {
            transform.DORotate(new Vector3(0f, 0f, 360f), 4f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }
        
    }

}

public enum RotatingSide
{
    Right,
    Left
}