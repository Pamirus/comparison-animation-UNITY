using System.Collections;
using UnityEngine;

public class CameraSwitchFOV : MonoBehaviour
{
    public GameObject parentObject;
    public float transitionTime = 1.0f;
    public float delayTime = 3.0f;

    private Transform[] objects;
    private int currentIndex = 0;
    private Camera mainCamera;

    void Start()
    {
        if (parentObject != null)
        {
            // Get all child objects from the parent
            objects = new Transform[parentObject.transform.childCount];
            for (int i = 0; i < parentObject.transform.childCount; i++)
            {
                objects[i] = parentObject.transform.GetChild(i);
            }

            // Initialize the camera
            mainCamera = Camera.main;
            StartCoroutine(SwitchObjects());
        }
        else
        {
            Debug.LogError("Please assign the parent object in the inspector!");
        }
    }

    IEnumerator SwitchObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);

            // Transition to the next object smoothly
            StartCoroutine(SmoothTransitionTo(objects[currentIndex]));
            yield return new WaitForSeconds(transitionTime);

            // Move to the next object or loop back to the first one
            currentIndex = (currentIndex + 1) % objects.Length;
        }
    }

    IEnumerator SmoothTransitionTo(Transform target)
    {
        float elapsedTime = 0f;
        Vector3 initialCameraPosition = mainCamera.transform.position;
        Vector3 targetCameraPosition = new Vector3(target.position.x, target.position.y, initialCameraPosition.z);
        float targetFOV = CalculateFOV(target);

        while (elapsedTime < transitionTime)
        {
            mainCamera.transform.position = Vector3.Lerp(initialCameraPosition, targetCameraPosition, (elapsedTime / transitionTime));
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, (elapsedTime / transitionTime));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetCameraPosition;
        mainCamera.fieldOfView = targetFOV;
    }

    float CalculateFOV(Transform target)
    {
        float objectSize = Mathf.Max(target.localScale.x, target.localScale.y, target.localScale.z);
        float distanceToTarget = target.position.z - Camera.main.transform.position.z;

        float FOV = 2 * Mathf.Atan(objectSize / distanceToTarget) * Mathf.Rad2Deg;
        return FOV;
    }
}
