using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    //视界范围
    public float fieldOfView = 120f;
    public bool PlayerInSight;
    //玩家最后一次被看到的位置
    public Vector3 playerLastSeenPosition;
    //public static Vector3 resetPos = Vector3.back;
    BoxCollider col;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, direction);
            if (angle < fieldOfView / 2f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction, out hit, col.size.z))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        //Debug.Log("Player seen");
                        PlayerInSight = true;
                        playerLastSeenPosition = player.transform.position;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInSight = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player==null)
        {
            PlayerInSight = false;
        }
    }
}
