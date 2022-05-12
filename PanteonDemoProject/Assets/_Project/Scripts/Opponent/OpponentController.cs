using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class OpponentController : MonoBehaviour
{
    [SerializeField] Transform targetPosition;
    private Vector3 startPos;

    private Animator animator;
    private OpponentSwerve opponentMovement;
    private Rigidbody opponentRb;

    private bool isAddingForce = false;
    private bool forceToRight = false;
    private float rotateCounterForce;


    private void Awake()
    {
        EventManager.OnStateChanged += GameStateChanged;
    }

    private void OnDestroy()
    {
        EventManager.OnStateChanged -= GameStateChanged;
    }

    void Start()
    {
        startPos = transform.position;
        animator = GetComponent<Animator>();
        opponentMovement = GetComponent<OpponentSwerve>();
        opponentRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y < -2f)
        {
            StartCoroutine(RestartLevel());
        }

        //TODO: RotatingPlatform'a taþýnabilir
        if (isAddingForce)
        {
            if (forceToRight)
            {
                opponentRb.AddForce(Vector3.right * (50 - rotateCounterForce), ForceMode.Force);
            }
            else
            {
                opponentRb.AddForce(Vector3.left * (50 - rotateCounterForce), ForceMode.Force);
            }

        }
    }

    private void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.InGame:
                opponentMovement.enabled = true;
                animator.SetBool("isRunning", true);
                break;
            case GameState.Drawing:
                opponentMovement.enabled = false;
                animator.SetBool("isRunning", false);
                animator.Play("Idle");
                break;
            case GameState.LevelFail:
                opponentMovement.enabled = false;
                animator.SetBool("isRunning", false);
                animator.Play("Idle");
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            opponentMovement.enabled = false;
            animator.SetTrigger("hitObstacle");

            StartCoroutine(RestartLevel());
        }
        else if (collision.gameObject.CompareTag("RotatorStick"))
        {
            animator.SetBool("hitStick", true);

            transform.DOJump(transform.position + Vector3.back * .5f, 1.5f, 1, .6f).SetEase(Ease.Linear).OnComplete(() =>
            {
                animator.SetBool("hitStick", false);
            });

        }
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(.8f);

        animator.SetTrigger("restartTrack");
        transform.position = startPos;

        if (GameManager.Instance.currentState.Equals(GameState.InGame))
        {
            yield return new WaitForSeconds(.5f);

            opponentMovement.enabled = true;
            animator.SetBool("isRunning", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RotatingPlatform"))
        {
            //TODO: swerveCont singleton yap, instancedan düzenle.
            GetComponent<OpponentSwerve>().xClampValue = 100f;

            isAddingForce = true;
            if (other.GetComponent<RotatingPlatform>().side.Equals(RotatingSide.Right))
            {
                forceToRight = true;
            }
            else
            {
                forceToRight = false;
            }

            rotateCounterForce = Random.Range(100.0f, 0.0f);
            Debug.Log(rotateCounterForce);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RotatingPlatform"))
        {
            GetComponent<OpponentSwerve>().xClampValue = 3.5f;
            isAddingForce = false;
            rotateCounterForce = 0f;
        }
    }


}
