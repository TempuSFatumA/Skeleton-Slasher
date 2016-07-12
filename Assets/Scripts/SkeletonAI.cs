using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkeletonAI : MonoBehaviour {
	
	Animator anim;
	//public Text xCor;
	//public Text yCor;
	SpriteRenderer sr;
	Collider2D[] colliders2D;
	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();

	}

	void OnTriggerStay2D(Collider2D sight)
	{
		if (sight.gameObject.tag == "Player") 
		{		
			float xDif = transform.position.x - sight.gameObject.transform.position.x;
			float yDif = transform.position.y - sight.gameObject.transform.position.y;
			//xCor.text=string.Format("{0:f}", xDif);
			//yCor.text=string.Format("{0:f}", yDif);
			colliders2D = gameObject.GetComponents<CircleCollider2D>();
			foreach (Collider2D collider2D in colliders2D) {
				if (collider2D.isTrigger) {
					if (collider2D.transform.position.y + transform.position.y >
					   sight.transform.position.y + sight.gameObject.transform.position.y) {
						sr.sortingOrder = -1;
					} else {
						sr.sortingOrder = 1;
					}
				}
			}
			if (Mathf.Abs (xDif) > Mathf.Abs (yDif)) {
				if (xDif > 0) {
					SetFacingDirection (false, true, false, false);
				} else {	
					SetFacingDirection (false, false, true, false);
				} 
		}
			else
			{
				if (yDif>0)
				{
					SetFacingDirection (false, false, false, true);
				}
				else
				{
					SetFacingDirection (true, false, false, false);
				}
			}

			anim.SetBool ("gotPlayerSpotted", true);

		}
	}
	void Update()
	{
		
	}

		void SetFacingDirection(bool top, bool left, bool right, bool bottom)
		{
			anim.SetBool("spottedPlayer", false);
			anim.SetBool("isFacingTop", top);
			anim.SetBool("isFacingLeft", left);
			anim.SetBool("isFacingRight", right);
			anim.SetBool("isFacingBottom", bottom);
		}
	// Update is called once per frame

}
