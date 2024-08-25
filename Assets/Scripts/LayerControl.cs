using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerControl : MonoBehaviour
{
    Animator anim;
    public float speed=1f;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //设置不同动画层的权重
        if(Input.GetMouseButton(0))
        {
            float w = anim.GetLayerWeight(1) > 1 ? 1 : anim.GetLayerWeight(1) + Time.deltaTime * speed;
            anim.SetLayerWeight(1, w);
        }
        else
        {
            float w = anim.GetLayerWeight(1) < 0 ? 0 : anim.GetLayerWeight(1) - Time.deltaTime * speed;
            anim.SetLayerWeight(1, w);
        }
    }

}
