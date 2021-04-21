using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GridDirection {
    LEFT,
    RIGHT,
    UP,
    DOWN
}

public class TopDownCharacterController : GridObject
{

    public float speed;
    public GridDirection direction;
    public Rigidbody2D rb;
    private Animator animator;

    #region Singleton
    public static TopDownCharacterController Instance;
    private void Awake()
    {
     if(Instance == null)
        {
            Instance = this;
        }   
    }
    #endregion

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            dir.x = -1;
            animator.SetInteger("Direction", 3);
            direction = GridDirection.LEFT;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            dir.x = 1;
            animator.SetInteger("Direction", 2);
            direction = GridDirection.RIGHT;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            dir.y = 1;
            animator.SetInteger("Direction", 1);
            direction = GridDirection.UP;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            dir.y = -1;
            animator.SetInteger("Direction", 0);
            direction = GridDirection.DOWN;
        }

        dir.Normalize();
        animator.SetBool("IsMoving", dir.magnitude > 0);
        rb.velocity = speed * dir;
    }
}

