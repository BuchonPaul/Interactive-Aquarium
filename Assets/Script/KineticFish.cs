using UnityEngine;

public class FishMoveKinetic : MonoBehaviour
{
    public float maxForcePropulsion = 0.1f;
    public float speed = 2f;
    public float forcePropulsion = 2f;
    public float forceRotation = 0.1f;

    private float isStartedFrom = 0f;
    private float animationDuration = 0f;
    private Rigidbody rb;
    private bool impulseApplied = false;
    private bool rotateImpulseApplied = false;

    public float maxTime = 2f;
    private float frameTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ChooseAction();
    }

    void Update()
    {
        isStartedFrom += Time.deltaTime;
        if (isStartedFrom > animationDuration)
        {
            ChooseAction();
        }

    }

    void ChooseAction()
    {
        float choice = Random.Range(0f, 1f) * 100;
        isStartedFrom = 0f;
        animationDuration = Random.Range(0.25f, maxTime);
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;

        if (choice <= 50f)
        {
            rb.AddForce(transform.forward * forcePropulsion, ForceMode.Impulse);
            impulseApplied = true;
        }
        else if (choice <= 80f)
        {
        }
        else if (choice <= 100f)
        {
            if (rb != null)
            {
                rotateImpulseApplied = true;
                if (Random.Range(0f, 1f) * 100 >= 50)
                {
                    rb.AddTorque(transform.up * forceRotation, ForceMode.Impulse);
                }
                else
                {
                    rb.AddTorque(-transform.up * forceRotation, ForceMode.Impulse);
                }
            }
        }
        frameTime = 1 - (animationDuration / 60);

    }
    void FixedUpdate()
    {
        if (impulseApplied)
        {
            rb.velocity *= frameTime;
        }
        if(rotateImpulseApplied)
        {
            rb.angularVelocity *= frameTime;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (rb != null)
        {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
        }
    }
}
