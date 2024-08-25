using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject target;
    private Transform _playerTransform;
    private Vector3 _offset = new Vector3(5, 1, 0); // ����������ҵ�λ��
    private Vector3 _pos;// �������������λ��
    private float _speedRotate = 20;// �����ת���ٶ�
    private float _speedMove = 20;// ����ƶ����ٶ�
    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = target.transform;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        _pos = _playerTransform.position + _offset;

        // λ�����
        this.transform.position = Vector3.Lerp(this.transform.position, _pos, _speedMove * Time.deltaTime);

        // �õ���ת��ͷ����Ԫ��
        Quaternion angel = Quaternion.LookRotation(_playerTransform.position - this.transform.position);

        // ��ת���
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, angel, _speedRotate * Time.deltaTime);
    }
}
