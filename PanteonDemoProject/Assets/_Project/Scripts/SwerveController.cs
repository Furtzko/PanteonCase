using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveController : MonoBehaviour
{
    private SwerveInput _swerveInput;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;
    [SerializeField] private float forwardSpeed = 5f;
    public float xClampValue = 3.5f;

    void Start()
    {
        _swerveInput = GetComponent<SwerveInput>();
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xClampValue, xClampValue), transform.position.y, transform.position.z);

        float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInput.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        transform.Translate(swerveAmount, 0, forwardSpeed * Time.deltaTime);
    }
}
