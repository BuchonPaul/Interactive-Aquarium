using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishs : MonoBehaviour
{
    public string description;
    public string fishName;
    public Vector3 tankCenterGoal;
    public float swimSpeedMin = 0.2f;
    public float swimSpeedMax = 0.6f;
    public float maxTurnRateY = 15f;
    public float maxWanderAngle = 90f;
    public float wanderPeriodDuration = 0.8f;
    public float wanderProbability = 0.15f;
    public float detectionDistance = 4f;

    public delegate void FishSelectedEventHandler(Fishs fish);
    public static event FishSelectedEventHandler FishSelectedEvent;
    public int fishId;

    public Sprite clearSprite;
    public Sprite hideSprite;
    public Sprite siz;
    public Sprite wei;
    public Sprite spe;
    public Sprite col;

    [HideInInspector]
    public float swimSpeed;
    private bool obstacleDetected = false;
    private float wanderPeriodStartTime;
    private Quaternion goalLookRotation;
    private float randomOffset;
    private Vector3 hitPoint;
    private Vector3 goalPoint;

    void Start()
    {
        randomOffset = Random.value;
        goalLookRotation = transform.rotation;
    }

    void Update()
    {
        if (!obstacleDetected)
        {
            Wander();
        }
        AvoidObstacles();
        UpdatePosition();
    }

    void Wander()
    {
        float speedPercent = Mathf.PerlinNoise(Time.time * 0.5f + randomOffset, randomOffset);
        swimSpeed = Mathf.Lerp(swimSpeedMin, swimSpeedMax, Mathf.Pow(speedPercent, 3));

        if (Time.time > wanderPeriodStartTime + wanderPeriodDuration)
        {
            wanderPeriodStartTime = Time.time;
            float randomAngle = Random.Range(-maxWanderAngle, maxWanderAngle);
            goalLookRotation = Quaternion.AngleAxis(randomAngle, Vector3.up) * transform.rotation;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, goalLookRotation, Time.deltaTime / 2f);
    }

    void AvoidObstacles()
    {
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
        transform.position += transform.forward * swimSpeed * Time.deltaTime;
    }

    public void TaskOnClick()
    {
        FishSelectedEvent?.Invoke(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = obstacleDetected ? Color.red : Color.cyan;
        Gizmos.DrawRay(transform.position, transform.forward * (swimSpeedMax * detectionDistance));
        if (obstacleDetected)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(hitPoint, goalPoint);
        }
    }
}
