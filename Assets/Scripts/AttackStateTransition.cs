using UnityEngine;
using System.Collections;

public class AttackStateTransition : MonoBehaviour {
    
    public bool gotAttackFinished;

    void Update()
    {
        GetComponentInParent<KnightController>().gotAttackFinished = gotAttackFinished;
    }
}
