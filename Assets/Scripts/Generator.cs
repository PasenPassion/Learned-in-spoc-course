using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    // Start is called before the first frame update
    float t = 0;
    public GameObject cube;
    public GameObject sphere;
    public static int score=0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 0.3f)
        {
            t = 0;
            GameObject pre;
            if (Random.Range(0, 2) < 1)
            {
                pre = Instantiate(cube);

            }
            else
            {
                pre = Instantiate(sphere);
            }
            pre.transform.position = new Vector3(Random.Range(-5,5), 8, Random.Range(-5, 5));
        }
    }
}
