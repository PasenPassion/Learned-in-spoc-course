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
    //��Ҫ��ѡLayer�е�IK������Ч
    //���߸������
    private void OnAnimatorIK(int layerIndex)
    {
        //�õ�һ�����ߣ������λ�õ����λ��
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        float enter = 0f;
        Vector3 target;
        //out�ǰ����ݴ浽�ñ�������˼
        if (plane.Raycast(ray, out enter))
        {
            //������������õ����ߺ�ƽ��Ľ��㣬enter��ֵ���������������뽻��ľ���
            target = ray.GetPoint(enter);
            //����Ȩ��
            anim.SetLookAtWeight(0.5f, 0.3f, 0.8f, 1);
            anim.SetLookAtPosition(target);
        }
    }
}
