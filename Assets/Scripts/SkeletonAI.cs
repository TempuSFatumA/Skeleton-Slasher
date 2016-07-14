using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkeletonAI : MonoBehaviour
{

    public GameObject Representation;

    Animator anim;

    void Start()
    {
        anim = Representation.GetComponent<Animator>();
        //SetFacingDirection(Constants.Bottom);
    }

    void OnTriggerStay(Collider intruder)
    {
        if (intruder.gameObject.tag == "Player") {
            float xDif = transform.position.x - intruder.gameObject.transform.position.x;
            float zDif = transform.position.z - intruder.gameObject.transform.position.z;
            if (Mathf.Abs(xDif) > Mathf.Abs(zDif)) {
                if (xDif > 0) {
                    SetFacingDirection(Constants.Left);
                } else {
                    SetFacingDirection(Constants.Right);
                }
            } else {
                if (zDif > 0) {
                    SetFacingDirection(Constants.Bottom);
                } else {
                    SetFacingDirection(Constants.Top);
                }
            }
            anim.SetBool("gotPlayerSpotted", true);
        } else {
            anim.SetBool("gotPlayerSpotted", false);
        }
    }

    void SetActionType(float _State) { anim.SetFloat("actionType", _State); }
    void SetFacingDirection(float _Direction) { anim.SetFloat("facingDirection", _Direction); }
}
