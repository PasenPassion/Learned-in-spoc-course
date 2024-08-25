using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControler : MonoBehaviour
{
    private Animator anim;
    public Transform target;
    AnimatorStateInfo stateInfo;
    public float matchStart;
    public float matchEnd;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(anim!=null)
        {
            //if(Input.GetKeyDown(KeyCode.Space))
            //{
            //    anim.SetTrigger("next");
            //}
            //��ȡ��ǰ״̬����
            stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (Input.GetButton("Fire1"))
                anim.SetTrigger("Jump");
            //ͨ�������жϵ�ǰ״̬
            if(stateInfo.IsName("JUMP00"))
            {
                anim.SetTrigger("Jump");
                //MatchTargetWeightMask��Ŀ����position��rotation��ռ��Ȩ��
                //���� Vector3.one ��ʵ�� ��1��1��1������XYZƥ���Ȩ�ض���1

                //matchStart ���ڶ������ŵ�ʲôʱ��ʼƥ��(0~1)

                //matchEnd ���ڶ������ŵ�ʲôʱ��ƥ�����(0~1)
                anim.MatchTarget(target.position, target.rotation, AvatarTarget.RightFoot,
                    new MatchTargetWeightMask(Vector3.one, 1), matchStart, matchEnd);
            }
        }
    }
    //AnimationEvents(���ŵ�ĳ������ĳ����)
    public void walkFinish()
    {
        Debug.Log("walkFinish");
    }
}
