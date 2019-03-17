using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class CharacterController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float maxVelocityChange;
    public Rigidbody rb;
    public float TurnSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        maxVelocityChange = 200.0f;
        walkSpeed = 5.0F;
        runSpeed = 7.0F;
        TurnSpeed = 2F;
    }

    void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity *= (Input.GetButton("Sprint") && targetVelocity.z > 0) ? runSpeed : walkSpeed;
        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocity = rb.velocity;
        Vector3 velocityChange;
 
        velocityChange.x = (targetVelocity.x - velocity.x);

        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = (targetVelocity.z - velocity.z);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        float turn = Input.GetAxis("Mouse X") * TurnSpeed;
        transform.Rotate(0, turn, 0);

    }
}
