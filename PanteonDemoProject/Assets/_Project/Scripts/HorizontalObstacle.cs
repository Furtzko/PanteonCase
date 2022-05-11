using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HorizontalObstacle : MonoBehaviour
{
    [SerializeField] private MovingBehaviour behaviour;

    void Start()
    {
        if (behaviour.Equals(MovingBehaviour.Left2Right))
        {
            transform.DOMoveX(transform.position.x + 2f, 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        else if (behaviour.Equals(MovingBehaviour.Right2Left))
        {
            transform.DOMoveX(transform.position.x - 2f, 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }

}

public enum MovingBehaviour
{
    Right2Left,
    Left2Right
}