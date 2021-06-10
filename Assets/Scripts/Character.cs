using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    new protected CircleCollider2D collider2D;
    protected Rigidbody2D rigidBody2D;
    protected Animator animator;

    //Keeping it public because it's useful for debugging
    public GameObject goal; 

    //Character movement speed, it's offset by RigidBody's damping
    public float speed;

    //How close should we be to the goal before we stop moving
    public float goalTolerance;

    private SimpleTimer blinkTimer;

    protected virtual void Initialize()
    {
        animator = GetComponentInChildren<Animator>();
        collider2D = GetComponent<CircleCollider2D>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        print("Character Initialized: " + this.name + " with class: " + GetType().Name);

        blinkTimer = new SimpleTimer(1.0f);
        blinkTimer.MarkTimer();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public virtual void RunCharacterLogic()
    {       
        if (animator.GetInteger("blink") == 0)
        {
            if (blinkTimer.IsTimerComplete())
            {
                var randomValue = Random.Range(0, 100.0f);
                animator.SetInteger("blink", randomValue > 50.0f ? 1 : 0);
                blinkTimer.MarkTimer(Random.Range(1.0f, 3.0f));
            }
        }

        animator.SetFloat("xVelocity", rigidBody2D.velocity.x);
        animator.SetFloat("yVelocity", rigidBody2D.velocity.y);
        
        animator.SetBool("xMagGreater", Mathf.Abs(rigidBody2D.velocity.x) > Mathf.Abs(rigidBody2D.velocity.y));

        if (goal != null)
        {
            Vector2 towards = goal.transform.position - rigidBody2D.transform.position;
            if (towards.sqrMagnitude > goalTolerance)
            {
                Vector2 direction = towards.normalized;
                rigidBody2D.AddForceAtPosition(direction * speed, Vector2.zero, ForceMode2D.Impulse);
            }
        }
    }

    private void FixedUpdate()
    {
        RunCharacterLogic();
    }
}
