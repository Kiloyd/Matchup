using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    public float playerSpeed;
    public float fallingSpeed;
    public float JumpPower;
    public Rigidbody2D rigidbody;
    public Collider2D collider;
    public Animator animator;
    private Vector3 facingDir;

    public PlayerController(float speed, float falling, float jump,  Rigidbody2D rb, Collider2D c, Animator a)
    {
        playerSpeed = speed;
        fallingSpeed = falling;
        JumpPower = jump;
        rigidbody = rb;
        collider = c;
        animator = a;
        facingDir = new Vector3();
    }

    public void Moving(float horizontalInput, float spaceAxis, bool inAir, bool jumpRelease)
    {
        if (!inAir)
            if (spaceAxis > 0.01f && jumpRelease)
            {
                rigidbody.AddForce(Vector2.up * JumpPower * spaceAxis, ForceMode2D.Impulse);
                Debug.Log(Vector2.up * JumpPower * spaceAxis);
            }

        if (Math.Abs(horizontalInput) > 0.01f)
            rigidbody.velocity = new Vector2(horizontalInput * playerSpeed, rigidbody.velocity.y);
        else
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);

        if ((horizontalInput * playerSpeed) > 0)
            facingDir = Vector3.left;
        else if ((horizontalInput * playerSpeed) < 0)
            facingDir = Vector3.right;

        collider.transform.right = facingDir;

        animator.SetFloat("HorizontalSpeed", Math.Abs(horizontalInput * playerSpeed));
    }
}
