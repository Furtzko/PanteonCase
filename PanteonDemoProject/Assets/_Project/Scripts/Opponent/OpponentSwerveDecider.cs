using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSwerveDecider : MonoBehaviour
{
    private GameObject hitObj;
    private float sphereRadius = 0.4f;
    private float maxDistance = 5f;
    [SerializeField] private LayerMask layerMask;
    private Vector3 origin;
    private Vector3 direction;

    private float curHitDistance;


    private bool isRandomNumGenerated = false;
    //private float randomNum;


    private float moveFactorX;
    public float MoveFactorX => moveFactorX;

    //TODO: raycast 

    // Update is called once per frame
    void Update()
    {
        origin = transform.position + Vector3.up * 0.3f;
        direction = transform.forward;
        RaycastHit hit;
        if(Physics.SphereCast(origin,sphereRadius,direction,out hit,maxDistance,layerMask, QueryTriggerInteraction.UseGlobal))
        {
            hitObj = hit.transform.gameObject;
            curHitDistance = hit.distance;

            if (!isRandomNumGenerated)
            {
                RandomNumGenerate(hit.transform.position.x);
            }
        }
        else
        {
            curHitDistance = maxDistance;
            hitObj = null;
            isRandomNumGenerated = false;
            moveFactorX = 0f;
        }

    }

    //TODO: kaldýrýlabilir.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * curHitDistance);
        Gizmos.DrawWireSphere(origin + direction * curHitDistance, sphereRadius);
    }

    private void RandomNumGenerate(float obsTransfromX)
    {
        moveFactorX = Random.Range(-10f, 10f);

        /*
        if(transform.position.x > obsTransfromX)
        {
            moveFactorX = Mathf.Abs(moveFactorX);
        }
        else
        {
            if(moveFactorX > 0)
            {
                moveFactorX *= -1;
            }
        }*/

        isRandomNumGenerated = true;
    }
}
