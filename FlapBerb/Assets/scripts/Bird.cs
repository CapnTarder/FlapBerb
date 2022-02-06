using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bird : MonoBehaviour
{
    public float upForce;                    //Upward force of the "flap".
    private bool isDead = false;            //Has the player collided with a wall?

    //public int oof =2;
    public SpriteRenderer spritey;
    public PolygonCollider2D poliCol;
    private Animator anim;                    //Reference to the Animator component.
    private Rigidbody2D rb2d;                //Holds a reference to the Rigidbody2D component of the bird.
    public int oofs=2;
    void Start()
    {
       // oofs = 2;
        //Get reference to the Animator component attached to this GameObject.
        anim = GetComponent<Animator>();
        //Get and store a reference to the Rigidbody2D attached to this GameObject.
        rb2d = GetComponent<Rigidbody2D>();

        spritey = GetComponent<SpriteRenderer>();
        poliCol = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        //Don't allow control if the bird has died.
        if (isDead == false)
        {
            //Look for input to trigger a "flap".
            if (Input.GetButtonDown("Jump"))
            {
                //...tell the animator about it and then...
                anim.SetTrigger("Flap");
                //...zero out the birds current y velocity before...
                rb2d.velocity = Vector2.zero;
                //    new Vector2(rb2d.velocity.x, 0);
                //..giving the bird some upward force.
                rb2d.AddForce(new Vector2(0, upForce));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "column")
        {
            Debug.Log("oof1");
            oofs--;
            if(oofs <= 0)
            {
                Debug.Log("oof2");
                // Zero out the bird's velocity
                rb2d.velocity = Vector2.zero;
                // If the bird collides with something set it to dead...
                isDead = true;
                //...tell the Animator about it...
                anim.SetTrigger("Die");
                //...and tell the game control about it.
                GameControl.instance.BirdDied();
            } 
            else
            {
                poliCol.enabled = false;
                spritey.color = new Color(1, 0, 0, 0.5f);
                StartCoroutine(EnableBox(1.0f));
            }
        }
        else
        {
            Debug.Log("urdum");
            // Zero out the bird's velocity
            rb2d.velocity = Vector2.zero;
            // If the bird collides with something set it to dead...
            isDead = true;
            //...tell the Animator about it...
            anim.SetTrigger("Die");
            //...and tell the game control about it.
            GameControl.instance.BirdDied();
        }
       
    }
    IEnumerator EnableBox( float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        poliCol.enabled = true;
        spritey.color = new Color(1, 1, 1, 1);

    }
}