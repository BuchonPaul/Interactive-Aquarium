using UnityEngine;
using UnityEngine.EventSystems;

public class FishBehavior : MonoBehaviour
{
    public FishData fishData; // Données du poisson

    // Variables de configuration pour le comportement du poisson
    public Vector3 tankCenterGoal;
    public float swimSpeedMin = 0.2f;
    public float swimSpeedMax = 0.6f;
    public float maxTurnRateY = 15f;
    public float maxWanderAngle = 90f;
    public float wanderPeriodDuration = 0.8f;
    public float wanderProbability = 0.15f;
    public float detectionDistance = 4f;
    public int fishQuantity = 2;
    public Vector3 spawnPos;
    public delegate void FishSelectedEventHandler(FishBehavior fish, bool found);
    public static event FishSelectedEventHandler FishSelectedEvent;
    public int fishId;

    public float swimSpeed;
    private bool obstacleDetected = false;
    private float wanderPeriodStartTime;
    private Quaternion goalLookRotation;
    private float randomOffset;
    private Vector3 hitPoint;
    private Vector3 goalPoint;
    public bool isfish = true;

    public Animator animator;
    public float animationSpeed = 1.0f;
    public float animationRotationSpeed = 0.01f;
    public float maxAnimationSpeed = 0.5f;
    private Vector3 previousPosition;
    private Quaternion previousRotation;
    private float movementSpeed;
    private float rotationSpeed;

    void Start()
    {
        randomOffset = Random.Range(0f, 100f);
        wanderPeriodDuration = wanderPeriodDuration + Random.Range(-0.2f, 0.2f);
        goalLookRotation = transform.rotation; // Initialisation de la rotation cible
        previousPosition = transform.position;
        previousRotation = transform.rotation;
        Wander();
    }

    void Update()
    {
        if (isfish)
        {
            AvoidObstacles(); // Éviter les obstacles
            UpdatePosition(); // Mettre à jour la position
            if (!obstacleDetected)
            {
                Wander(); // Errer si aucun obstacle détecté
            }
        }
        Vector3 movement = transform.position - previousPosition;
        movementSpeed = movement.magnitude / Time.deltaTime;
        previousPosition = transform.position;

        Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);
        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
        rotationSpeed = angle / Time.deltaTime;
        previousRotation = transform.rotation;

        if (animator)
        {
            animator.speed = Mathf.Clamp((movementSpeed * animationSpeed) + (rotationSpeed * animationRotationSpeed), 0, maxAnimationSpeed);
        }
    }

    void Wander()
    {
        // Calcul de la vitesse de nage en utilisant le bruit de Perlin
        float speedPercent = Mathf.PerlinNoise(Time.time * 0.5f + randomOffset, randomOffset);
        swimSpeed = Mathf.Lerp(swimSpeedMin, swimSpeedMax, Mathf.Pow(speedPercent, 3));

        // Changer la direction à intervalles réguliers
        if (Time.time > wanderPeriodStartTime + wanderPeriodDuration)
        {
            wanderPeriodStartTime = Time.time;
            float randomAngle = Random.Range(-maxWanderAngle, maxWanderAngle);
            goalLookRotation = Quaternion.AngleAxis(randomAngle, Vector3.up) * transform.rotation;
        }

        // Interpoler vers la nouvelle rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, goalLookRotation, Time.deltaTime / 2f);
    }

    void AvoidObstacles()
    {
        // Détecter les obstacles devant le poisson
        float obstacleSensingDistance = swimSpeedMax * detectionDistance;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, obstacleSensingDistance))
        {
            obstacleDetected = true;
            hitPoint = hit.point;
            Vector3 reflectionVector = Vector3.Reflect(transform.forward, hit.normal);
            goalPoint = (hit.point + reflectionVector + tankCenterGoal) / 3f;
            goalLookRotation = Quaternion.LookRotation(goalPoint - transform.position);
            float turnRate = maxTurnRateY * Mathf.Pow(1 - (hit.distance / obstacleSensingDistance), 4f);
            transform.rotation = Quaternion.Slerp(transform.rotation, goalLookRotation, Time.deltaTime * turnRate);
        }
        else
        {
            obstacleDetected = false;
        }
    }

    void UpdatePosition()
    {
        // Mettre à jour la position du poisson
        transform.position += transform.forward * swimSpeed * Time.deltaTime;
    }

    public void TaskOnClick()
    {
        // Gérer le clic sur le poisson
        Debug.Log("fishName");
        FishSelectedEvent?.Invoke(this, EventSystem.current.currentSelectedGameObject.GetComponent<fishUIButton>().founded);
    }

    private void OnDrawGizmos()
    {
        // Dessiner des gizmos pour visualiser la détection d'obstacles
        Gizmos.color = obstacleDetected ? Color.red : Color.cyan;
        Gizmos.DrawRay(transform.position, transform.forward * (swimSpeedMax * detectionDistance));
        if (obstacleDetected)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(hitPoint, goalPoint);
        }
    }
}
    