using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject target;
    private Transform _playerTransform;
    private Vector3 _offset = new Vector3(5, 1, 0); // 相机相对于玩家的位置
    private Vector3 _pos;// 相机相对于世界的位置
    private float _speedRotate = 20;// 相机旋转的速度
    private float _speedMove = 20;// 相机移动的速度
    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = target.transform;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        _pos = _playerTransform.position + _offset;

        // 位移相机
        this.transform.position = Vector3.Lerp(this.transform.position, _pos, _speedMove * Time.deltaTime);

        // 得到旋转镜头的四元数
        Quaternion angel = Quaternion.LookRotation(_playerTransform.position - this.transform.position);

        // 旋转相机
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, angel, _speedRotate * Time.deltaTime);
    }
}
