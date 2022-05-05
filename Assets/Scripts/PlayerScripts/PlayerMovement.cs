using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationManager), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 8;
    [SerializeField] private float swerveSpeed;

    private Vector2 startPos;
    private Vector2 deltaPos;
    private bool isStarted;

    private AnimationManager animManager;
    private Rigidbody rb;

    private void Start()
    {
        animManager = GetComponent<AnimationManager>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        CheckForStart();

        if (!isStarted) return;

        MoveForward();
        SwerveSystem();
        Clamp();
    }

    private void CheckForStart()
    {
        if (isStarted) return;

        if (Input.GetMouseButtonDown(0))
        {
            isStarted = true;
            animManager.SetRunAnimation(true);
        }
    }

    private void MoveForward()
    {
        rb.transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    private void SwerveSystem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            deltaPos = (Vector2)Input.mousePosition - startPos;

            rb.transform.position = new Vector3(
                Mathf.Lerp(rb.transform.position.x, rb.transform.position.x + (deltaPos.x / Screen.width) * swerveSpeed, Time.deltaTime * swerveSpeed),
                rb.transform.position.y, rb.transform.position.z);

            startPos = Input.mousePosition;
        }
    }
    private void Clamp()
    {
        Vector3 clampPos = transform.position;
        clampPos.x = Mathf.Clamp(clampPos.x, -5, 5);
        transform.position = clampPos;
    }
}
