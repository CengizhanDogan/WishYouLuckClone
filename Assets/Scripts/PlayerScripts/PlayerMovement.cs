using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AnimationManager), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 8;
    public float swerveSpeed = 60;

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
            SetSpeed(true);
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

    public void SetSpeed(bool give)
    {
        if (give)
        {
            animManager.SetRunAnimation(true);

            DOTween.To(() => forwardSpeed, x => forwardSpeed = x, 8, 2f).SetEase(Ease.Linear);

            swerveSpeed = 60;
        }
        else
        {
            DOTween.To(() => forwardSpeed, x => forwardSpeed = x, 0, 2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                animManager.SetRunAnimation(false);
            });

            swerveSpeed = 0;
        }
    }
}
