using UnityEngine;

public class TransformMovement : MonoBehaviour
{
    private const float vectorMinMagnitude = 0.1f;

    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float turnSmoothTime = 0.1f;

    private float horizontal;
    private float vertical;
    private float turnSmoothVelocity;
    private float targetAngle;
    private float angle;

    private Vector3 direction;
    private Vector3 moveDir;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= vectorMinMagnitude)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}