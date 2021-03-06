using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{

    private Animator animator;
    private SwerveController playerMovement;
    private Rigidbody playerRb;

    private Vector3 startPosition;
    [SerializeField]private Transform drawingPosition;

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
        playerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Karakter su seviyesine d??erse ba?lang?? posizyonuna g?nderilir.
        if (transform.position.y < -2f)
        {
            EventManager._onScreenFadeOut();
            playerMovement.enabled = false;

            StartCoroutine(RestartLevel());
        }

        if (isAddingForce)
        {
            if (forceToRight)
            {
                playerRb.AddForce(Vector3.right * 50, ForceMode.Force);
            }
            else
            {
                playerRb.AddForce(Vector3.left * 50, ForceMode.Force);
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
            case GameState.Drawing:
                playerMovement.enabled = false;
                animator.SetBool("isRunning", false);
                animator.SetTrigger("jogging");
                transform.DODynamicLookAt(drawingPosition.position, 1f);
                transform.DOMove(drawingPosition.position, 3f).SetEase(Ease.Linear);
                break;
            case GameState.LevelEnd:
                StartCoroutine(VictoryAfterDelay());
                break;
            case GameState.LevelFail:
                playerMovement.enabled = false;
                animator.SetBool("isRunning", false);
                animator.Play("Idle");
                break;
            default:
                break;
        }
    }

    IEnumerator VictoryAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        animator.SetTrigger("victory");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            EventManager._onScreenFadeOut();
            VirtualCamController.Instance.ShakeCamera(2f, .3f);
            playerMovement.enabled = false;
            animator.SetTrigger("hitObstacle");

            StartCoroutine(RestartLevel());
        }
        else if (collision.gameObject.CompareTag("RotatorStick"))
        {
            animator.SetBool("hitStick", true);

            transform.DOJump(transform.position + Vector3.back * .5f, 1.5f, 1, .6f).SetEase(Ease.Linear).OnComplete( () =>
            {
                animator.SetBool("hitStick", false);
            });
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RotatingPlatform"))
        {
            //Karakterlerin suya d??ebilmeleri i?in clamp aral??? art?r?l?yor.
            GetComponent<SwerveController>().xClampValue = 100f;

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
        else if (other.CompareTag("DrawingPosition"))
        {
            animator.SetTrigger("drawing");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RotatingPlatform"))
        {
            GetComponent<SwerveController>().xClampValue = 3.5f;
            isAddingForce = false;
        }
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(.8f);

        EventManager._onScreenFadeIn();
        animator.SetTrigger("restartTrack");
        transform.position = startPosition;


        if (GameManager.Instance.currentState.Equals(GameState.InGame))
        {
            yield return new WaitForSeconds(.5f);

            playerMovement.enabled = true;
            animator.SetBool("isRunning", true);
        }

    }
}
