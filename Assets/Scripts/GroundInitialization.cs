using UnityEngine;
using System.Collections;

public class GroundInitialization : MonoBehaviour
{
    public Object grassTilePrefab;
    public float widthHalf;
    public float heightHalf;

    void Start()
    {
        for (float x = -widthHalf; x <= widthHalf; x++)
            for (float z = -heightHalf; z <= heightHalf; z++)
                Instantiate(grassTilePrefab, new Vector3(x, 0, z*2), Quaternion.Euler(90.0f, 0.0f, 0.0f));
    }
}
