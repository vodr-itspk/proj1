using System;
using UnityEngine;
using Lean.Touch;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityModifier = 1.5f;
    [SerializeField] private float laneOffset;
    [SerializeField] private float laneChangeSpeed;
    private Rigidbody _rigidbody;
    private Vector3 movePosition;
    private bool isChangingLane;
    private bool onGround = true;
    private float initialPosX = 0.0f;
    public UnityAction wasHitted;
    
    

    private void Awake()
    {
        Physics.gravity = new Vector3(0,-9.8f, 0) * gravityModifier;
        _rigidbody = GetComponent<Rigidbody>();
        movePosition = transform.position;
        // INIT
        laneOffset = 1.0f;
        laneChangeSpeed = 15.0f;
        PlayerAnimController.InitAnimator(GetComponent<Animator>());
        PlayerAnimController.Run(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            onGround = true;
        else
        {
            wasHitted.Invoke();
            Destroy(other.gameObject);
        }
    }

    private void OnEnable()
    {
        LeanTouch.OnFingerSwipe += OnSwipeHandler;
        LeanTouch.OnFingerTap += OnTapHandler;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerSwipe -= OnSwipeHandler;
        LeanTouch.OnFingerTap -= OnTapHandler;
    }

    private void OnSwipeHandler(LeanFinger finger)
    {
        isChangingLane = true;
        var direction = finger.SwipeScreenDelta.x;
        if (direction < 0 && movePosition.x > -laneOffset)
            movePosition = new Vector3(movePosition.x - laneOffset, transform.position.y, transform.position.z);

        if (direction > 0 && movePosition.x < laneOffset)
            movePosition = new Vector3(movePosition.x + laneOffset, transform.position.y, transform.position.z);
    }

    private void OnTapHandler(LeanFinger finger)
    {
        if (finger.IsOverGui) return;
        if (!onGround) return;
        onGround = false;
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        PlayerAnimController.Jump();
    }

    private void Update()
    {
        if (transform.position == movePosition)
        {
            isChangingLane = false;
        }

        if (isChangingLane)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, movePosition, laneChangeSpeed * Time.deltaTime);
        }
    }
}