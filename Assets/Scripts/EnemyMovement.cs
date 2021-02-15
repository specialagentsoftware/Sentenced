using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = .25f;
    Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            if (this.tag == "FlyingEnemy")
            {
                myRigidBody.velocity = new Vector2(moveSpeed, Random.Range(-5, 5f));
            }
            else if (this.tag == "VerticalEnemy") {
                myRigidBody.velocity = new Vector2(0, Random.Range(-10,10));
            }
            else
            {
                myRigidBody.velocity = new Vector2(moveSpeed, 0);
            }
        }
        else
        {
            if (this.tag == "FlyingEnemy")
            {
                myRigidBody.velocity = new Vector2(-moveSpeed, Random.Range(-5,5f));
            }
            else if (this.tag == "VerticalEnemy")
            {
                myRigidBody.velocity = new Vector2(0, Random.Range(-10, 10));
            }
            else
            {
                myRigidBody.velocity = new Vector2(-moveSpeed, 0);
            }
        }
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "FlyingEnemy")
        {
            return;
        }
        else
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x) * -1, 1f);
        }
    }
}
