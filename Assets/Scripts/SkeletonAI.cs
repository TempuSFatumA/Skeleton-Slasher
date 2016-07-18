using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkeletonAI : MonoBehaviour
{
    int SizeMultiplier;
    public float ViewRadius;
    public LayerMask whatIsObstacle;
    public GameObject Representation;

    public Text score1;

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

            Wavemove(intruder.gameObject.transform.position.x, intruder.gameObject.transform.position.z);

        } else {
            anim.SetBool("gotPlayerSpotted", false);
        }
    }

    void SetActionType(float _State) { anim.SetFloat("actionType", _State); }
    void SetFacingDirection(float _Direction) { anim.SetFloat("facingDirection", _Direction); }

    void Wavemove(float UnitX, float UnitZ)
    {
        SizeMultiplier = 4;
        int FieldSize = (int)(10 * ViewRadius / SizeMultiplier + 1);
        if ((FieldSize % 2) == 0) FieldSize++;
        int[,] field = new int [FieldSize, FieldSize];

        int Center = (FieldSize + 1)/ 2;

        for (int i = 0; i < FieldSize; i++)
            for (int j = 0; j < FieldSize; j++) field[i,j] = 0;
       // field[Center, Center] = 5;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ViewRadius/Mathf.Sqrt(2f), whatIsObstacle);
        foreach (Collider collider in hitColliders) 
        {
            int x = (int)(collider.gameObject.transform.position.x - transform.position.x), z = (int)(collider.gameObject.transform.position.z - transform.position.z);
            if ((Mathf.Abs(x) > ViewRadius) ||(Mathf.Abs(z) > ViewRadius)) field [x, z] = 1;
        }

        int TargetX = (int)(UnitX - transform.position.x), TargetZ = (int)(UnitZ - transform.position.z);
        //field[TargetX, TargetZ] = 9;






        //if ((TargetX >= -ViewRadius) && (TargetX <= ViewRadius) && (TargetZ >= ViewRadius) && (TargetZ <= -ViewRadius))

        //{
            score1.text = TargetX.ToString() + TargetZ.ToString();
            int d;
            int[,] wave = new int[FieldSize, FieldSize];
            for (int x = 0; x < FieldSize; x++)
            for (int y = 0; y < FieldSize; y++)
                if (field[x, y] == 0) wave[x, y] = -1; else wave[x, y] = 100;

            d = 0;
            wave[Center+TargetX, Center+TargetZ] = 0;

            do
            {
                int f = 0;
                for (int x = 0; x < FieldSize; x++)
                    for (int y = 0; y < FieldSize; y++)
                    {
                        if (wave[x, y] == d)
                        {
                            f++;
                            if (x > 1) if (wave[x - 1, y] == -1)
                            {
                                wave[x - 1, y] = d + 1;
                            }
                            if (x < FieldSize - 1) if (wave[x + 1, y] == -1)
                            {
                                wave[x + 1, y] = d + 1;
                            }
                            if (y > 1) if (wave[x, y - 1] == -1)
                            {
                                wave[x, y - 1] = d + 1;
                            }
                            if (y > FieldSize - 1) if (wave[x, y + 1] == -1)
                            {
                                wave[x, y + 1] = d + 1;
                            }

                        }
                    }
                if (f == 0) break;
                d++;

            }
            while (wave[Center, Center] == -1);

            d--;

            if (wave[Center - 1, Center] == d - 1) transform.position = new Vector3(transform.position.x - 0.07f, transform.position.y, transform.position.z);
            else if (wave[Center + 1, Center] == d - 1) transform.position = new Vector3(transform.position.x + 0.07f, transform.position.y, transform.position.z); 
            else if (wave[Center, Center - 1] == d - 1) transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.07f); 
            else if (wave[Center, Center + 1] == d - 1) transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.07f);
        //}

    }
}
