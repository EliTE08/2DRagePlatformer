using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 300;
    public float jumpHeight = 300;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;



    private float Move;

    private Rigidbody2D rb;

    // Start is called before the game is loaded.
    private void Start(){

        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move = Input.GetAxis("Horizontal"); //Gets all inputs for horizontal movement, such as A and D, and the left and right arrow keys.


        if(KBCounter <= 0){
            if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded()){ //Jump movement
                rb.AddForce(new Vector2(rb.velocity.x, jumpHeight));
            }

            rb.velocity = new Vector2(Move * speed * Time.deltaTime, rb.velocity.y); //Left and right movement
        }
        else{
            if(KnockFromRight){
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            if(!KnockFromRight){
                rb.velocity = new Vector2(KBForce, KBForce);
            }

            KBCounter -= Time.deltaTime;
        }
    }



    public bool isGrounded(){
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer)){  //Continuously checks if they player is touching the ground
            return true;
        }
        else{
            return false;
        }
    }

    private void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize); //Lets us see the raycast below player feet
    }

}
