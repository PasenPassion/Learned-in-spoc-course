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
        //����������
        //if (Input.GetButton("Fire1"))
        //    anim.SetTrigger("normal");
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        Vector3 move = v * new Vector3(0,0,0.5f) + h * Vector3.right;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            //����
            move.z *= 2f;
        }
        float turn = move.x;
        float forward = move.z;
        //�ٶȣ�����������Ϊ�ﵽĿ��ֵ�����ʱ��
        anim.SetFloat("speed", forward, 0.1f, Time.deltaTime);
        //����
        anim.SetFloat("turn", turn, 0.1f, Time.deltaTime);
    }
}
