using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public bool isSprinting;
    public bool canSprint;
    public float sprintMultiplier = 1.3f;
    bool isCrouched;
    public bool canCrouch;
    public float crouchMultiplier = 0.6f;
    public float gravity = -9.81f*2f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public Transform ceilingCheck;
    public float groundDistance = 0.4f;
    public float ceilingDistance = 0.4f;

    Vector3 velocity;
    bool isGrounded;
    bool canGetUp;

    private Vector3 move;
    private float x;
    private float z;

    public Animator animator;

    [SerializeField] private float defaultCamHeight = 1.24f;
    [SerializeField] private float minCamHeight = 1f;
    [SerializeField] private float maxCamHeight = 1.4f;
    [SerializeField] private float headAnimationSpeed = 0.4f;
    private bool isHeadDown = false;
    private Coroutine movementRoutine = null;
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance);
        canGetUp = !Physics.CheckSphere(ceilingCheck.position, ceilingDistance);
        HandleCrouchandSprint();

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = (transform.right * x + transform.forward * z);
        //while (move != Vector3.zero && movementRoutine == null)
        //{
        //    isHeadDown = true;
        //    StartCoroutine(HeadBob(isHeadDown));
        //} 
        //if (move == Vector3.zero && movementRoutine != null)
        //{
        //    StopHeadBob();
        //}

        if ((move != Vector3.zero && movementRoutine == null))
        {
            if (movementRoutine != null)
            {
                StopCoroutine(movementRoutine);
                movementRoutine = null;
            }
            movementRoutine = StartCoroutine(HeadBob(isHeadDown));
        }
        if (move == Vector3.zero && movementRoutine != null)
        {
            if (movementRoutine != null)
            {
                StopCoroutine(movementRoutine);
                movementRoutine = null;
            }
            movementRoutine = StartCoroutine(ResetHeadSmoothly());
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(!isCrouched && canSprint) isSprinting = Input.GetKey(KeyCode.LeftShift);
            Sprint();
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (!isSprinting && canCrouch) isCrouched = Input.GetKey(KeyCode.LeftControl);
            Crouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (!isSprinting && canCrouch) isCrouched = Input.GetKey(KeyCode.LeftControl);
            Stand();
        }

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleCrouchandSprint()
    {
        if(!isGrounded)
        {
            canCrouch = true;
            canSprint = false;
        }
    }

    void Crouch()
    {
        move = (transform.right * x + transform.forward * z) * crouchMultiplier;
        /*animator.Play("Crouch");*/
        controller.height = 1.8f;
        headAnimationSpeed = 0.7f;
    }
    void Stand()
    {
        move = (transform.right * x + transform.forward * z);
        animator.Play("Stand");
        controller.height = 3.6f;
        headAnimationSpeed = 0.4f;
    }

    void Sprint()
    {
        move = (transform.right * x + transform.forward * z) * (isSprinting ? sprintMultiplier : 1f);
        headAnimationSpeed = 0.2f;
    }

    private IEnumerator HeadBob (bool isDown) {
        float targetHeight = isDown ? minCamHeight : maxCamHeight;
        Camera playerCamera = Camera.main;
        float startingHeight = playerCamera.transform.localPosition.y;
        float timeElaped = 0f;

        while (timeElaped < headAnimationSpeed)
        {
            playerCamera.transform.localPosition = new Vector3(0.05f, Mathf.Lerp(startingHeight, targetHeight, timeElaped / headAnimationSpeed), 0.743f);
            timeElaped += Time.deltaTime;
            yield return null;
        }
        playerCamera.transform.localPosition = new Vector3(0.05f, targetHeight, 0.743f);
        ChangeHeadDirection();
    }
    private void ChangeHeadDirection() {
        movementRoutine = null;
        isHeadDown = !isHeadDown;
        movementRoutine = StartCoroutine(HeadBob(isHeadDown));
    }
    private void StopHeadBob()
    {
        StopCoroutine(movementRoutine);
        movementRoutine = null;
        Camera.main.transform.localPosition = new Vector3(0.05f, defaultCamHeight, 0.743f);
    }
    private IEnumerator ResetHeadSmoothly()
    {
        Camera playerCamera = Camera.main;
        float startingHeight = playerCamera.transform.localPosition.y;
        float timeElaped = 0f;

        while (timeElaped < headAnimationSpeed)
        {
            playerCamera.transform.localPosition = new Vector3(0.05f, Mathf.Lerp(startingHeight, defaultCamHeight, timeElaped / (headAnimationSpeed/2)), 0.743f);
            timeElaped += Time.deltaTime;
            yield return null;
        }
        playerCamera.transform.localPosition = new Vector3(0.05f, defaultCamHeight, 0.743f);
        movementRoutine = null;
    }
}
