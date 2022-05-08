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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Opponent"))
        {

            /*if (side.Equals(RotatingSide.Right))
            {
                other.transform.GetComponent<Rigidbody>().AddForce(Vector3.right * force, ForceMode.Force);
            }
            else
            {
                other.transform.GetComponent<Rigidbody>().AddForce(Vector3.right, ForceMode.Force);
            }
            */
            //other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //other.transform.parent = null;
    }
}

public enum RotatingSide
{
    Right,
    Left
}