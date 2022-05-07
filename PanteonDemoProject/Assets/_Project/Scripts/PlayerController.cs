using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{

    private Animator animator;
    private SwerveController playerMovement;

    private Vector3 startPosition;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<SwerveController>();
    }
    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        animator.SetBool("isRunning", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            EventManager._onHitObstacle();
            VirtualCamController.Instance.ShakeCamera(2f, .3f);
            playerMovement.enabled = false;
            animator.SetTrigger("hitObstacle");

            StartCoroutine(RestartLevel());

            //transform.position = new Vector3(0f,0f, -0.588f);
            
        }
        else if (collision.gameObject.CompareTag("RotatorStick"))
        {
            animator.SetBool("hitStick", true);

            transform.DOJump(transform.position + Vector3.back * 3, 2f, 1, .8f).SetEase(Ease.Linear).OnComplete( () =>
            {
                animator.SetBool("hitStick", false);
            });
        }
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(.8f);

        EventManager._onLevelRestart();
        animator.SetTrigger("restartTrack");
        transform.position = startPosition;

        yield return new WaitForSeconds(.5f);

        playerMovement.enabled = true;
        animator.SetBool("isRunning", true);

    }
}
