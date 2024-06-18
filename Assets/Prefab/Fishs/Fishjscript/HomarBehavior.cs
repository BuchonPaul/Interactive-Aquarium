using System.Collections;
using UnityEngine;

public class HomarBehavior : MonoBehaviour
{
    public FishData fishData;

    public float minMoveDistance = 1.5f;
    public float maxMoveDistance = 2f;
    public float moveDuration = 6f;
    public float idleDuration = 4f;
    public float minRotationAngle = -30;
    public float maxRotationAngle = 30;

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        StartCoroutine(HomarRoutine());
    }

    IEnumerator HomarRoutine()
    {
        while (true)
        {
            // Move forward
            float moveDistance = Random.Range(minMoveDistance, maxMoveDistance);
            Vector3 moveDirection = transform.forward * moveDistance;
            Vector3 targetPosition = transform.position + moveDirection;
            float elapsedTime = 0f;

            while (elapsedTime < idleDuration)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure final position is correct
            transform.position = targetPosition;

            // Idle
            yield return new WaitForSeconds(idleDuration);

            // Return to start position
            elapsedTime = 0f;
            while (elapsedTime < idleDuration)
            {
                transform.position = Vector3.Lerp(transform.position, startPosition, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure final position is correct
            transform.position = startPosition;

            // Rotate to new angle
            float randomAngle = Random.Range(minRotationAngle, maxRotationAngle);
            Quaternion targetRotation = startRotation * Quaternion.Euler(0, randomAngle, 0);
            transform.rotation = targetRotation;

            // Wait a frame before restarting the loop
            yield return null;
        }
    }
}
