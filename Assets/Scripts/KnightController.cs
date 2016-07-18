using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KnightController : MonoBehaviour
{
    public float baseSpeed;
    public GameObject Representation;
    public LayerMask whatIsEnemy;
    public Text score;
    public bool gotAttackFinished;

    Animator anim;
    bool gotAttackChecked;
    int successfullAttacksCount;
    Vector3 macePosition;
    float xMove;
    float zMove;
     
    void Start()
    {
        gotAttackChecked = false;
        successfullAttacksCount = 0;
        score.text = "Skulls: " + successfullAttacksCount.ToString();
        anim = Representation.GetComponent<Animator>();
        SetFacingDirection(Constants.Bottom);
        SetActionType(Constants.Idle);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            SetActionType(Constants.Attack);
            if (gotAttackFinished) {
                if (!gotAttackChecked) {
                    Collider[] hitColliders = Physics.OverlapSphere(macePosition, 0.25f, whatIsEnemy);
                    foreach (Collider hitCollider in hitColliders) {
                        if (!hitCollider.isTrigger) {
                            successfullAttacksCount++;
                            score.text = "Skulls: " + successfullAttacksCount.ToString();
                        }
                    }
                    gotAttackChecked = true;
                }
            } else {
                gotAttackChecked = false;
            }
        } else {
            if (Input.GetKey(KeyCode.UpArrow)) {
                if (Input.GetKey(KeyCode.LeftArrow)) {
                    zMove = 1.0f;
                    xMove = -1.0f;
                } else if (Input.GetKey(KeyCode.RightArrow)) {
                    zMove = 1.0f;
                    xMove = 1.0f;
                } else if (Input.GetKey(KeyCode.DownArrow)) {
                    zMove = 0.0f;
                    xMove = 0.0f;
                } else {
                    zMove = 1.0f;
                    xMove = 0.0f;
                }
            } else if (Input.GetKey(KeyCode.LeftArrow)) {
                if (Input.GetKey(KeyCode.RightArrow)) {
                    zMove = 0.0f;
                    xMove = 0.0f;
                } else if (Input.GetKey(KeyCode.DownArrow)) {
                    zMove = -1.0f;
                    xMove = -1.0f;
                } else {
                    zMove = 0.0f;
                    xMove = -1.0f;
                }
            } else if (Input.GetKey(KeyCode.RightArrow)) {
                if (Input.GetKey(KeyCode.DownArrow)) {
                    zMove = -1.0f;
                    xMove = 1.0f;
                } else {
                    zMove = 0.0f;
                    xMove = 1.0f;
                }
            } else if (Input.GetKey(KeyCode.DownArrow)) {
                zMove = -1.0f;
                xMove = 0.0f;
            } else {
                zMove = 0.0f;
                xMove = 0.0f;
            }

            if (zMove > 0) {
                SetFacingDirection(Constants.Top);
                SetActionType(Constants.Walk);
            } else if (zMove < 0) {
                SetFacingDirection(Constants.Bottom);
                SetActionType(Constants.Walk);
            } else if (xMove > 0) {
                SetFacingDirection(Constants.Right);
                SetActionType(Constants.Walk);
            } else if (xMove < 0) {
                SetFacingDirection(Constants.Left);
                SetActionType(Constants.Walk);
            } else {
                SetActionType(Constants.Idle);
            }
            if (zMove != 0 && xMove != 0) {
                transform.position = new Vector3(transform.position.x + xMove * baseSpeed * Mathf.Sqrt(0.5f) / 100,
                                                transform.position.y,
                                                    transform.position.z + zMove * baseSpeed * Mathf.Sqrt(0.5f) / 100);
            } else {
                transform.position = new Vector3(transform.position.x + xMove * baseSpeed / 100,
                                                    transform.position.y,
                                                        transform.position.z + zMove * baseSpeed / 100);
            }
        }
    }

    void SetActionType(float _State)
    {
        anim.SetFloat("actionType", _State);
    }
    void SetFacingDirection(float _Direction)
    {
        anim.SetFloat("facingDirection", _Direction);
        if (_Direction == Constants.Top) {
            macePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.15f);
        } else if (_Direction == Constants.Left) {
            macePosition = new Vector3(transform.position.x - 1.15f, transform.position.y, transform.position.z);
        } else if (_Direction == Constants.Right) {
            macePosition = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
        } else if (_Direction == Constants.Bottom) {
            macePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.15f);
        } else {
            macePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
    float GetFacingDirection() { return anim.GetFloat("facingDirection"); }
}