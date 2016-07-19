using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkeletonAI : MonoBehaviour
{

    public GameObject Representation;

    AnimatorStatesController asc;

    void Start()
    {
        asc = new AnimatorStatesController(Representation.GetComponent<Animator>());
        asc.SetFacingDirection(Constants.Bottom);
    }

    void OnTriggerStay(Collider intruder)
    {
        if (intruder.gameObject.tag == "Player") {
            float xDif = transform.position.x - intruder.gameObject.transform.position.x;
            float zDif = transform.position.z - intruder.gameObject.transform.position.z;
            if (Mathf.Abs(xDif) > Mathf.Abs(zDif)) {
                if (xDif > 0) {
                    asc.SetFacingDirection(Constants.Left);
                } else {
                    asc.SetFacingDirection(Constants.Right);
                }
            } else {
                if (zDif > 0) {
                    asc.SetFacingDirection(Constants.Bottom);
                } else {
                    asc.SetFacingDirection(Constants.Top);
                }
            }
            asc.animator.SetBool("gotPlayerSpotted", true);
        } else {
            asc.animator.SetBool("gotPlayerSpotted", false);
        }
    }
}