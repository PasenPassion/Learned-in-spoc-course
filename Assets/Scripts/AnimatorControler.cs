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
            //获取当前状态名称
            stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (Input.GetButton("Fire1"))
                anim.SetTrigger("Jump");
            //通过名字判断当前状态
            if(stateInfo.IsName("JUMP00"))
            {
                anim.SetTrigger("Jump");
                //MatchTargetWeightMask：目标点的position和rotation所占的权重
                //其中 Vector3.one 其实是 （1，1，1），即XYZ匹配的权重都是1

                //matchStart 是在动画播放到什么时候开始匹配(0~1)

                //matchEnd 是在动画播放到什么时候匹配完成(0~1)
                anim.MatchTarget(target.position, target.rotation, AvatarTarget.RightFoot,
                    new MatchTargetWeightMask(Vector3.one, 1), matchStart, matchEnd);
            }
        }
    }
    //AnimationEvents(播放到某个点做某件事)
    public void walkFinish()
    {
        Debug.Log("walkFinish");
    }
}
