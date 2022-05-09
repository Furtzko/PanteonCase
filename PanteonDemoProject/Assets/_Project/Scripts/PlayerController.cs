using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{

    private Animator animator;
    private SwerveController playerMovement;

    private Vector3 startPosition;

    private bool isAddingForce = false;
    private bool forceToRight = false;

    private void Awake()
    {
        EventManager.OnStateChanged += GameStateChanged;
    }

    private void OnDestroy()
    {
        EventManager.OnStateChanged -= GameStateChanged;
    }

    private void Start()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<SwerveController>();
    }

    private void Update()
    {

        if (isAddingForce)
        {
            if (forceToRight)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.right * 50, ForceMode.Force);
            }
            else
            {
                GetComponent<Rigidbody>().AddForce(Vector3.left * 50, ForceMode.Force);
            }
            
        }

    }

    private void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.InGame:
                playerMovement.enabled = true;
                animator.SetBool("isRunning", true);
                break;
            case GameState.SwipeToDraw:
                playerMovement.enabled = false;
                //animator.SetTrigger("Dance");
                break;
            default:
                break;
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RotatingPlatform"))
        {
            isAddingForce = true;
            if (other.GetComponent<RotatingPlatform>().side.Equals(RotatingSide.Right))
            {
                forceToRight = true;
            }
            else
            {
                forceToRight = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RotatingPlatform"))
        {
            isAddingForce = false;
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
