/*using UnityEngine;
using System.Collections;

public class KnightController : MonoBehaviour {

    Animator anim;
    public float speed;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
    
	void FixedUpdate ()
    {
        float yMove = Input.GetAxis("Vertical");
        float xMove = Input.GetAxis("Horizontal");

        if (yMove > 0) 
        {
            SetMovingDirection(true, false, false, false);
        } else if (yMove < 0)
        {
            SetMovingDirection(false, false, false, true);
        } else if (xMove > 0)
        {
            SetMovingDirection(false, false, true, false);
        } else if (xMove < 0)
        {
            SetMovingDirection(false, true, false, false);
        } else
        {
            anim.SetBool("isWalking", false);
        }
        if (anim.GetBool("isWalking"))
        {
            transform.position = new Vector3(transform.position.x + xMove * speed / 100,
                                                transform.position.y + yMove * speed / 100);
        }
	}

    void SetMovingDirection(bool top, bool left, bool right, bool bottom)
    {
        anim.SetBool("isWalking", true);
        anim.SetBool("isFacingTop", top);
        anim.SetBool("isFacingLeft", left);
        anim.SetBool("isFacingRight", right);
        anim.SetBool("isFacingBottom", bottom);
    }
}
*/
