using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class demoNPC : MonoBehaviour
{
    public GameObject targetObject;
    public Text uiText;
    public float displayDuration = 2.0f;
    private bool isTargetInContact = false;

    private Camera mainCamera;
    private Vector3 originalCameraPosition;
    private Vector3 targetCameraPosition; // Position to move the camera towards when zooming in.
    private float zoomSpeed = 2.0f;

    private void Start()
    {
        uiText.enabled = false;
        mainCamera = Camera.main;
        originalCameraPosition = mainCamera.transform.position;
    }

    private void Update()
    {
        if (isTargetInContact)
        {
            // Calculate the position to move the camera towards.
            targetCameraPosition = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, mainCamera.transform.position.z);
            
            // Move the camera smoothly toward the target position.
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetCameraPosition, Time.deltaTime * zoomSpeed);

            // Zoom in gradually.
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 2.0f, Time.deltaTime * zoomSpeed);
        }
        else
        {
            // Move the camera smoothly back to the original position.
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, originalCameraPosition, Time.deltaTime * zoomSpeed);

            // Zoom out gradually to the original size.
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 5.0f, Time.deltaTime * zoomSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == targetObject && !isTargetInContact)
        {
            StartCoroutine(DisplayTextForDuration());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == targetObject)
        {
            uiText.enabled = false;
            isTargetInContact = false;
        }
    }

    private IEnumerator DisplayTextForDuration()
    {
        isTargetInContact = true;
        uiText.enabled = true;

        yield return new WaitForSeconds(displayDuration);

        uiText.enabled = false;
        isTargetInContact = false;
    }
}
