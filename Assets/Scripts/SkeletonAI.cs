using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SkeletonAI : MonoBehaviour
{
    int SizeMultiplier;
    public float ViewRadius;
    public LayerMask whatIsObstacle;
    public GameObject Representation;

    public Text score1;
    public Text s0;



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

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ViewRadius/Mathf.Sqrt(2f), whatIsObstacle);
        foreach (Collider collider in hitColliders) 
        {
            int x = (int)(collider.gameObject.transform.position.x - transform.position.x), z = (int)(collider.gameObject.transform.position.z - transform.position.z);
            if ((Mathf.Abs(x) > ViewRadius) ||(Mathf.Abs(z) > ViewRadius)) field [x, z] = 1;
        }

        int TargetX = (int)(UnitX - transform.position.x), TargetZ = (int)(UnitZ - transform.position.z);
        






        
            int d;
            int[,] wave = new int[FieldSize, FieldSize];
            for (int x = 0; x < FieldSize; x++)
            for (int y = 0; y < FieldSize; y++)
                if (field[x, y] == 0) wave[x, y] = -1; else wave[x, y] = 100;

            
        wave[Center + TargetX, Center + TargetZ] = 0;

        d = 0;
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
                            if (y < FieldSize - 1) if (wave[x, y + 1] == -1)
                            {
                                wave[x, y + 1] = d + 1;
                            }

                        }
                    }
                if (f == 0) break;
                d++;

            }
            while (wave[Center, Center] == -1);
            
            //d--;
            //Debug.Log(d);
        score1.text = d.ToString();
        //score1.text = wave[7, 0].ToString() + "\t" + wave[7, 1].ToString() + "\t" + wave[7, 2].ToString() + "\t" + wave[7, 3].ToString() + "\t" + wave[7, 4].ToString() + "\t" + wave[7, 5].ToString() + "\t" + wave[7, 6].ToString() + "\t" + wave[7, 7].ToString() + "\t" + wave[7, 8].ToString() + "\t" + wave[7, 9].ToString() + "\t" + wave[7, 10].ToString() + "\t" + wave[7, 11].ToString() + "\t" + wave[7, 12].ToString();

        /* s0.text = wave[0, 0].ToString() + "\t" + wave[0, 1].ToString() + "\t" + wave[0, 2].ToString() + "\t" + wave[0, 3].ToString() + "\t" + wave[0, 4].ToString() + "\t" + wave[0, 5].ToString() + "\t" + wave[0, 6].ToString() + "\t" + wave[0, 7].ToString() + "\t" + wave[0, 8].ToString() + "\t" + wave[0, 9].ToString() + "\t" + wave[0, 10].ToString() + "\t" + wave[0, 11].ToString() + "\t" + wave[0, 12].ToString() + "\n" +
        wave[1, 0].ToString() + "\t" + wave[1, 1].ToString() + "\t" + wave[1, 2].ToString() + "\t" + wave[1, 3].ToString() + "\t" + wave[1, 4].ToString() + "\t" + wave[1, 5].ToString() + "\t" + wave[1, 6].ToString() + "\t" + wave[1, 7].ToString() + "\t" + wave[1, 8].ToString() + "\t" + wave[1, 9].ToString() + "\t" + wave[1, 10].ToString() + "\t" + wave[1, 11].ToString() + "\t" + wave[1, 12].ToString() + "\n" +
        wave[2, 0].ToString() + "\t" + wave[2, 1].ToString() + "\t" + wave[2, 2].ToString() + "\t" + wave[2, 3].ToString() + "\t" + wave[2, 4].ToString() + "\t" + wave[2, 5].ToString() + "\t" + wave[2, 6].ToString() + "\t" + wave[2, 7].ToString() + "\t" + wave[2, 8].ToString() + "\t" + wave[2, 9].ToString() + "\t" + wave[2, 10].ToString() + "\t" + wave[2, 11].ToString() + "\t" + wave[2, 12].ToString() + "\n" +
        wave[3, 0].ToString() + "\t" + wave[3, 1].ToString() + "\t" + wave[3, 2].ToString() + "\t" + wave[3, 3].ToString() + "\t" + wave[3, 4].ToString() + "\t" + wave[3, 5].ToString() + "\t" + wave[3, 6].ToString() + "\t" + wave[3, 7].ToString() + "\t" + wave[3, 8].ToString() + "\t" + wave[3, 9].ToString() + "\t" + wave[3, 10].ToString() + "\t" + wave[3, 11].ToString() + "\t" + wave[3, 12].ToString() + "\n" +
        wave[4, 0].ToString() + "\t" + wave[4, 1].ToString() + "\t" + wave[4, 2].ToString() + "\t" + wave[4, 3].ToString() + "\t" + wave[4, 4].ToString() + "\t" + wave[4, 5].ToString() + "\t" + wave[4, 6].ToString() + "\t" + wave[4, 7].ToString() + "\t" + wave[4, 8].ToString() + "\t" + wave[4, 9].ToString() + "\t" + wave[4, 10].ToString() + "\t" + wave[4, 11].ToString() + "\t" + wave[4, 12].ToString() + "\n" +
        wave[5, 0].ToString() + "\t" + wave[5, 1].ToString() + "\t" + wave[5, 2].ToString() + "\t" + wave[5, 3].ToString() + "\t" + wave[5, 4].ToString() + "\t" + wave[5, 5].ToString() + "\t" + wave[5, 6].ToString() + "\t" + wave[5, 7].ToString() + "\t" + wave[5, 8].ToString() + "\t" + wave[5, 9].ToString() + "\t" + wave[5, 10].ToString() + "\t" + wave[5, 11].ToString() + "\t" + wave[5, 12].ToString() + "\n" +
        wave[6, 0].ToString() + "\t" + wave[6, 1].ToString() + "\t" + wave[6, 2].ToString() + "\t" + wave[6, 3].ToString() + "\t" + wave[6, 4].ToString() + "\t" + wave[6, 5].ToString() + "\t" + wave[6, 6].ToString() + "\t" + wave[6, 7].ToString() + "\t" + wave[6, 8].ToString() + "\t" + wave[6, 9].ToString() + "\t" + wave[6, 10].ToString() + "\t" + wave[6, 11].ToString() + "\t" + wave[6, 12].ToString() + "\n" +
        wave[7, 0].ToString() + "\t" + wave[7, 1].ToString() + "\t" + wave[7, 2].ToString() + "\t" + wave[7, 3].ToString() + "\t" + wave[7, 4].ToString() + "\t" + wave[7, 5].ToString() + "\t" + wave[7, 6].ToString() + "\t" + wave[7, 7].ToString() + "\t" + wave[7, 8].ToString() + "\t" + wave[7, 9].ToString() + "\t" + wave[7, 10].ToString() + "\t" + wave[7, 11].ToString() + "\t" + wave[7, 12].ToString() + "\n" +
        wave[8, 0].ToString() + "\t" + wave[8, 1].ToString() + "\t" + wave[8, 2].ToString() + "\t" + wave[8, 3].ToString() + "\t" + wave[8, 4].ToString() + "\t" + wave[8, 5].ToString() + "\t" + wave[8, 6].ToString() + "\t" + wave[8, 7].ToString() + "\t" + wave[8, 8].ToString() + "\t" + wave[8, 9].ToString() + "\t" + wave[8, 10].ToString() + "\t" + wave[8, 11].ToString() + "\t" + wave[8, 12].ToString() + "\n" +
        wave[9, 0].ToString() + "\t" + wave[9, 1].ToString() + "\t" + wave[9, 2].ToString() + "\t" + wave[9, 3].ToString() + "\t" + wave[9, 4].ToString() + "\t" + wave[9, 5].ToString() + "\t" + wave[9, 6].ToString() + "\t" + wave[9, 7].ToString() + "\t" + wave[9, 8].ToString() + "\t" + wave[9, 9].ToString() + "\t" + wave[9, 10].ToString() + "\t" + wave[9, 11].ToString() + "\t" + wave[9, 12].ToString() + "\n" +
        wave[10, 0].ToString() + "\t" + wave[10, 1].ToString() + "\t" + wave[10, 2].ToString() + "\t" + wave[10, 3].ToString() + "\t" + wave[10, 4].ToString() + "\t" + wave[10, 5].ToString() + "\t" + wave[10, 6].ToString() + "\t" + wave[10, 10].ToString() + "\t" + wave[10, 8].ToString() + "\t" + wave[10, 9].ToString() + "\t" + wave[10, 10].ToString() + "\t" + wave[10, 11].ToString() + "\t" + wave[10, 12].ToString() + "\n" +
        wave[11, 0].ToString() + "\t" + wave[11, 1].ToString() + "\t" + wave[11, 2].ToString() + "\t" + wave[11, 3].ToString() + "\t" + wave[11, 4].ToString() + "\t" + wave[11, 5].ToString() + "\t" + wave[11, 6].ToString() + "\t" + wave[11, 11].ToString() + "\t" + wave[11, 8].ToString() + "\t" + wave[11, 9].ToString() + "\t" + wave[11, 10].ToString() + "\t" + wave[11, 11].ToString() + "\t" + wave[11, 12].ToString() + "\n" +
        wave[12, 0].ToString() + "\t" + wave[12, 1].ToString() + "\t" + wave[12, 2].ToString() + "\t" + wave[12, 3].ToString() + "\t" + wave[12, 4].ToString() + "\t" + wave[12, 5].ToString() + "\t" + wave[12, 6].ToString() + "\t" + wave[12, 12].ToString() + "\t" + wave[12, 8].ToString() + "\t" + wave[12, 9].ToString() + "\t" + wave[12, 10].ToString() + "\t" + wave[12, 11].ToString() + "\t" + wave[12, 12].ToString();*/
        if (wave[Center - 1, Center] == d - 1) transform.position = new Vector3(transform.position.x - 0.08f, transform.position.y, transform.position.z); 
            else if (wave[Center + 1, Center] == d - 1) transform.position = new Vector3(transform.position.x + 0.08f, transform.position.y, transform.position.z); 
            else if (wave[Center, Center - 1] == d - 1) transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.08f); 
            else if (wave[Center, Center + 1] == d - 1) transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.08f);
        //}

    }
}
