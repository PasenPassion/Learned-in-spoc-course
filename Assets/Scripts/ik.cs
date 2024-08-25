using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ik : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim= GetComponent<Animator>();
    }
    //需要勾选Layer中的IK才能生效
    //视线跟踪鼠标
    private void OnAnimatorIK(int layerIndex)
    {
        //得到一条射线，从相机位置到鼠标位置
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        float enter = 0f;
        Vector3 target;
        //out是把数据存到该变量的意思
        if (plane.Raycast(ray, out enter))
        {
            //从相机出发，得到射线和平面的交点，enter的值是相机即射线起点与交点的距离
            target = ray.GetPoint(enter);
            //设置权重
            anim.SetLookAtWeight(0.5f, 0.3f, 0.8f, 1);
            anim.SetLookAtPosition(target);
        }
    }
}
