using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SmartCamera : MonoBehaviour
{
    [Header("Ҫ���������")]
    public Transform target = null;

    [Header("��껬���ٶ�")]
    [Range(0, 1)]
    public float linearSpeed = 1;
    [Header("���������Ҿ���")]
    [Range(2, 15)]
    public float distanceFromTarget = 5;
    [Header("������ٶ�")]
    [Range(1, 50)]
    public float speed = 5;
    [Header("x��ƫ����")]
    public float xOffset = 0.5f;


    private float yMouse;
    private float xMouse;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            gameObject.layer = target.gameObject.layer;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    private void LateUpdate()
    {
        if (target != null)
        {
            xMouse += Input.GetAxis("Mouse X") * linearSpeed;
            yMouse -= Input.GetAxis("Mouse Y") * linearSpeed;
            yMouse = Mathf.Clamp(yMouse, -30, 80);//���ƴ�ֱ����ĽǶ�

            distanceFromTarget -= Input.GetAxis("Mouse ScrollWheel") * 10;//��������Զ���ﾵͷ
            distanceFromTarget = Mathf.Clamp(distanceFromTarget, 2, 15);
            //�û������������ת�������ת���л�
            Quaternion targetRotation = Quaternion.Euler(yMouse, xMouse, 0);
            //����ƶ���Ŀ��λ��
            CamCheck(out RaycastHit hit, out float dis);
            Vector3 targetPostion = target.position + targetRotation * new Vector3(xOffset, 0, -dis) + target.GetComponent<CapsuleCollider>().center * 1.75f;
            //���ٶȽ��в�ֵ��ʹ֮���г�̸�
            speed = target.GetComponent<Rigidbody>().velocity.magnitude > 0.1f ?
   Mathf.Lerp(speed, 7, 5f * Time.deltaTime) : Mathf.Lerp(speed, 25, 5f * Time.deltaTime);
            //ʹ��Lerp��ֵ��ʵ������ĸ���
            transform.position = Vector3.Lerp(transform.position, targetPostion, Time.deltaTime * speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 25f);

        }
    }
    private void CamCheck(out RaycastHit raycast, out float dis)
    {
        //���������ײ
#if UNITY_EDITOR
        Debug.DrawLine(target.position + target.GetComponent<CapsuleCollider>().center * 1.75f,
            target.position + target.GetComponent<CapsuleCollider>().center * 1.75f +
            (transform.position - target.position - target.GetComponent<CapsuleCollider>().center * 1.75f).normalized * distanceFromTarget
            , Color.blue);
#endif
        //�����ײ�����壬��ȡ��ײ����Ϣ�����¼�����룬���򷵻�Ĭ��ֵ
        if (Physics.Raycast(target.position + target.GetComponent<CapsuleCollider>().center * 1.75f,
          (transform.position - target.position - target.GetComponent<CapsuleCollider>().center * 1.75f).normalized, out raycast,
           distanceFromTarget, ~Physics.IgnoreRaycastLayer))
        {
            dis = Vector3.Distance(target.position + target.GetComponent<CapsuleCollider>().center * 1.75f + new Vector3(xOffset, 0, 0), raycast.point);
        }
        else
            dis = distanceFromTarget;
    }
    public void CursorArise()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && Cursor.visible == false)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
    public void Teleport(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;

        if (target != null)
        {

        }
    }
}

