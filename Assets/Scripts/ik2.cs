using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ik2 : MonoBehaviour
{
    private Animator anim;
    public Transform target;
    //关节对齐位置
    public Transform hint;
    public bool IsHand=true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //手脚为末端部位
    private void OnAnimatorIK(int layerIndex)
    {
        AvatarIKGoal g = IsHand ? AvatarIKGoal.RightHand : AvatarIKGoal.RightFoot;
        AvatarIKHint h = IsHand ? AvatarIKHint.RightElbow : AvatarIKHint.RightKnee;
        anim.SetIKPositionWeight(g, 1f);
        anim.SetIKPosition(g,target.position);
        anim.SetIKRotationWeight(g, 1f);
        anim.SetIKRotation(g,target.rotation);

        anim.SetIKHintPositionWeight(h,1f);
        anim.SetIKHintPosition(h,hint.position);

    }
}
