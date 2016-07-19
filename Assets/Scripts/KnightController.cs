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

    public Text text1;
    public Text text2;

    AnimatorStatesController asc;
    bool gotAttackChecked;
    int successfullAttacksCount;
    float xMove;
    float zMove;

    void Start()
    {
        gotAttackChecked = false;
        successfullAttacksCount = 0;
        score.text = "Skulls: " + successfullAttacksCount.ToString();
        asc = new AnimatorStatesController(Representation.GetComponent<Animator>());
        asc.SetFacingDirection(Constants.Bottom);
        asc.SetActionType(Constants.Idle);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            asc.SetActionType(Constants.Attack);
            if (gotAttackFinished) {
                if (!gotAttackChecked) {
                    Collider[] hitColliders = Physics.OverlapSphere(GetMacePositionByDirection(asc.GetFacingDirection()),
                                                                        0.25f, whatIsEnemy);
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
                asc.SetFacingDirection(Constants.Top);
                asc.SetActionType(Constants.Walk);
            } else if (zMove < 0) {
                asc.SetFacingDirection(Constants.Bottom);
                asc.SetActionType(Constants.Walk);
            } else if (xMove > 0) {
                asc.SetFacingDirection(Constants.Right);
                asc.SetActionType(Constants.Walk);
            } else if (xMove < 0) {
                asc.SetFacingDirection(Constants.Left);
                asc.SetActionType(Constants.Walk);
            } else {
                asc.SetActionType(Constants.Idle);
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

    Vector3 GetMacePositionByDirection(float _Direction)
    {
        if (_Direction == Constants.Top) {
            return new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.15f);
        } else if (_Direction == Constants.Left) {
            return new Vector3(transform.position.x - 1.15f, transform.position.y, transform.position.z);
        } else if (_Direction == Constants.Right) {
            return new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
        } else if (_Direction == Constants.Bottom) {
            return new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.15f);
        } else {
            return new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
