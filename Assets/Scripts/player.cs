using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class player : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyColider;
    BoxCollider2D myfeet;
    AudioSource jumpAudio;

    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] AudioClip jumpClip;
    float gravityScaleAtStart;

    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyColider = GetComponent<CapsuleCollider2D>();
        myfeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
        jumpAudio = new AudioSource();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Run();
        Jump();
        FlipSprite();
        ClimbLadder();
    }

    void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float calculatedRunSpeed = 5f;

        if (CrossPlatformInputManager.GetButton("RunFast"))
        {
            calculatedRunSpeed = runSpeed * 2f;
            myAnimator.SetFloat("DoubleAnimationPlay", 1f);
        }
        else
        {
            calculatedRunSpeed = runSpeed;
        }

        Vector2 playerVelocity = new Vector2(controlThrow * calculatedRunSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
            myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    void ClimbLadder()
    {
        if (!myfeet.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return; 
        }

            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x,controlThrow * climbSpeed);
            myRigidBody.velocity = climbVelocity;
            myRigidBody.gravityScale = 0f;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasHorizontalSpeed);
    }

    void Jump()
    {
        if (!myfeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
            AudioSource.PlayClipAtPoint(jumpClip,new Vector3(myRigidBody.transform.position.x, myRigidBody.transform.position.y, myRigidBody.transform.position.z));
        }
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    public void Die()
    {
        isAlive = false;
    }
}
