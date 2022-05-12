using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSwerveDecider : MonoBehaviour
{
    private float sphereRadius = 0.4f;
    private float maxDistance = 4f;
    [SerializeField] private LayerMask layerMask;
    private Vector3 origin;
    private Vector3 direction;

    private float curHitDistance;

    private bool isRandomNumGenerated = false;

    private bool isEscapingRotatingPlatform = false;

    private float moveFactorX;
    public float MoveFactorX => moveFactorX;

    void Update()
    {
        origin = transform.position + Vector3.up * 0.3f;
        direction = transform.forward;
        RaycastHit hit;

        if(Physics.SphereCast(origin,sphereRadius,direction,out hit,maxDistance,layerMask, QueryTriggerInteraction.UseGlobal))
        {
            curHitDistance = hit.distance;

            if (!isRandomNumGenerated)
            {
                RandomNumGenerate();
            }
        }
        else if(!isEscapingRotatingPlatform)
        {
            curHitDistance = maxDistance;
            isRandomNumGenerated = false;
            moveFactorX = 0f;
        }

        //if (Physics.SphereCast(origin, sphereRadius, direction, out hit, 0.5f))
        //{
        //    if (hit.transform.CompareTag("VerticalPlatform"))
        //    {
        //        StartCoroutine(EscapeRotatingPlatform());
        //    }
        //}

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("VerticalPlatform"))
        {
            StartCoroutine(EscapeRotatingPlatform());
        }
    }

    private IEnumerator EscapeRotatingPlatform()
    {
        isEscapingRotatingPlatform = true;

        moveFactorX = -transform.position.x;
        Debug.Log("moveX: " + moveFactorX);

        yield return new WaitForSeconds(1f);

        moveFactorX = 0;

        Debug.Log("moveX: 0000");
    }

    

    //TODO: kaldýrýlabilir.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * curHitDistance);
        Gizmos.DrawWireSphere(origin + direction * curHitDistance, sphereRadius);
    }

    private void RandomNumGenerate()
    {
        moveFactorX = Random.Range(-10f, 10f);

        isRandomNumGenerated = true;
    }
}
