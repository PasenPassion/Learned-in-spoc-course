using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets;
using UnityStandardAssets.CrossPlatformInput;

public class TryBlendTree : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //摁下鼠标左键
        //if (Input.GetButton("Fire1"))
        //    anim.SetTrigger("normal");
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        Vector3 move = v * new Vector3(0,0,0.5f) + h * Vector3.right;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            //快跑
            move.z *= 2f;
        }
        float turn = move.x;
        float forward = move.z;
        //速度，第三个参数为达到目标值所需的时间
        anim.SetFloat("speed", forward, 0.1f, Time.deltaTime);
        //朝向
        anim.SetFloat("turn", turn, 0.1f, Time.deltaTime);
    }
}
