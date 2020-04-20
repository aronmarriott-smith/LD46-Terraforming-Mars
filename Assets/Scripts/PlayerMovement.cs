using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayermask;

    private Rigidbody2D rigidBody2d;
    private BoxCollider2D boxCollider2d;

    public float moveVelocity = 40.0f;
    public float jumpVelocity = 100.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 startPosition;


    private void Awake()
    {
        startPosition = transform.position;
        rigidBody2d = GetComponent<Rigidbody2D>();
        if (rigidBody2d == null)
            rigidBody2d = gameObject.AddComponent<Rigidbody2D>();

        boxCollider2d = GetComponent<BoxCollider2D>();
        if (boxCollider2d == null)
            boxCollider2d = gameObject.AddComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (IsGrounded() && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)))
        {
            rigidBody2d.velocity = Vector2.up * jumpVelocity;
        }
    }

    private void FixedUpdate()
    {
        rigidBody2d.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rigidBody2d.velocity = new Vector2(-moveVelocity, rigidBody2d.velocity.y);
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                rigidBody2d.velocity = new Vector2(+moveVelocity, rigidBody2d.velocity.y);
            }
            else
            {
                rigidBody2d.velocity = new Vector2(0, rigidBody2d.velocity.y);
                rigidBody2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = .1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeight, platformsLayermask);

        Color raycastColor;
        if (raycastHit.collider != null)
        {
            raycastColor = Color.green;
        }
        else
        {
            raycastColor = Color.red;
        }
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.center.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeight), raycastColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.center.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeight), raycastColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.center.x, boxCollider2d.bounds.center.y + extraHeight), Vector2.right * (boxCollider2d.bounds.extents.x), raycastColor);

        return raycastHit.collider != null;
    }
}
