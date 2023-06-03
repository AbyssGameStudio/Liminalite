using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 1000f;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float xRotation = 0f;

    public float defaultFOV = 75f;
    
    // Camera Zoom
    public float zoomFOV = 30f;
    public float zoomTime = 0.3f;
    private KeyCode zoomKey = KeyCode.Mouse1;
    private Coroutine zoomRoutine;

    // Sprint Camera
    public float sprintZoomFOV = 90f;
    public float sprintZoomTime = 0.2f;
    private KeyCode sprintZoomKey = KeyCode.LeftShift;
    private Coroutine sprintZoomRoutine;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mousex = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mousex);

        if(Input.GetKeyDown(zoomKey))
        {
           if(zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }
            zoomRoutine = StartCoroutine(Zoom(true));
        }
        if (Input.GetKeyUp(zoomKey))
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }
            zoomRoutine = StartCoroutine(Zoom(false));
        }
        
        if (Input.GetKeyDown(sprintZoomKey))
        {
            if (sprintZoomRoutine != null)
            {
                StopCoroutine(sprintZoomRoutine);
                sprintZoomRoutine = null;
            }
            sprintZoomRoutine = StartCoroutine(SprintZoom(true));
        }
        if (Input.GetKeyUp(sprintZoomKey))
        {
            if (sprintZoomRoutine != null)
            {
                StopCoroutine(sprintZoomRoutine);
                sprintZoomRoutine = null;
            }
            sprintZoomRoutine = StartCoroutine(SprintZoom(false));
        }
    }

    private IEnumerator Zoom(bool isEnter)
    {
        float targetFov = isEnter ? zoomFOV : defaultFOV;
        Camera playerCamera = Camera.main;
        float startingFov = playerCamera.fieldOfView;
        float timeElaped = 0f;
        while (timeElaped < zoomTime)
        {
            playerCamera.fieldOfView = Mathf.Lerp(startingFov, targetFov, timeElaped / zoomTime);
            timeElaped += Time.deltaTime;
            yield return null;
        }
        playerCamera.fieldOfView = targetFov;
        zoomRoutine = null;
    }

    public IEnumerator SprintZoom(bool isSprint)
    {
        float targetFov = isSprint ? sprintZoomFOV : defaultFOV;
        Camera playerCamera = Camera.main;
        float startingFov = playerCamera.fieldOfView;
        float timeElaped = 0f;
        while (timeElaped < sprintZoomTime)
        {
            playerCamera.fieldOfView = Mathf.Lerp(startingFov, targetFov, timeElaped / sprintZoomTime);
            timeElaped += Time.deltaTime;
            yield return null;
        }
        playerCamera.fieldOfView = targetFov;
        sprintZoomRoutine = null;
    }
}
