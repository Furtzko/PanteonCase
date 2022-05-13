using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSwerveDecider : MonoBehaviour
{
    private float sphereRadius = 0.4f;
    private float maxDistance = 8f;
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

        //Obstacle'la karþýlaþýlýrsa random bir deðer üretilip, swerve deðeri olarak OpponentSwerve'e gönderiliyor. 
        if (Physics.SphereCast(origin,sphereRadius,direction,out hit,maxDistance,layerMask, QueryTriggerInteraction.UseGlobal))
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

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("VerticalPlatform"))
        {
            StartCoroutine(EscapeRotatingPlatform());
        }
    }

    //Karakter RotatingPlatformda takýlý kaldýysa, x pozisyounun tersine kýsa süreli kuvvet uygulanarak düz platforma dönmesi saðlanýr.
    private IEnumerator EscapeRotatingPlatform()
    {
        isEscapingRotatingPlatform = true;
        moveFactorX = -transform.position.x;

        yield return new WaitForSeconds(2f);

        moveFactorX = 0;
    }

    //SphereCast visualization.
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
