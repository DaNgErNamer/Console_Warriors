using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPointsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
        float randomPos_x = Random.Range(-50f, 50f);
        float randomPos_y = Random.Range(0f, 30f);
        transform.localPosition += new Vector3(randomPos_x, randomPos_y, 25f);
    }
}
