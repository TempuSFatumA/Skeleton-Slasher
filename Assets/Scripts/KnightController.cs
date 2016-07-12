using UnityEngine;
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
            SetDirection(true, false, false, false);
            anim.SetFloat("movingState", 1.0f);
        } else if (yMove < 0)
        {
            SetDirection(false, false, false, true);
            anim.SetFloat("movingState", 1.0f);
        } else if (xMove > 0)
        {
            SetDirection(false, false, true, false);
            anim.SetFloat("movingState", 1.0f);
        } else if (xMove < 0)
        {
            SetDirection(false, true, false, false);
            anim.SetFloat("movingState", 1.0f);
        } else
        {
            anim.SetBool("isWalking", false);
            anim.SetFloat("movingState", 0.0f);
        }
        transform.position = new Vector3(transform.position.x + xMove * speed / 100,
                                            transform.position.y + yMove * speed / 100);
	}

    void SetDirection(bool top, bool left, bool right, bool bottom)
    {
        anim.SetBool("isFacingTop", top);
        anim.SetBool("isFacingLeft", left);
        anim.SetBool("isFacingRight", right);
        anim.SetBool("isFacingBottom", bottom);
    }

}
