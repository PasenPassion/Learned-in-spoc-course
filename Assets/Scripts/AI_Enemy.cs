using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class AI_Enemy : MonoBehaviour
{
    public float patrolSpeed;
    //վ׮ʱ��
    public float patrolTime;
    //վ׮��ʱ��
    private float patrolTimer;
    //Ŀ���
    public Transform targets;
    //��ǰĿ�������
    private int waypointIndex;
    private NavMeshAgent agent;
    //���ת���ٶ�
    public float ShootRotationSpeed;
    //������
    public float ShootInterval;
    private float shootTimer;
    private EnemySight enemySight;
    public Rigidbody bulletPrefab;
    private Transform player;
    //�Ƿ�׷�����
    private bool chase;
    private bool lost;
    public float chaseSpeed;
    //׷����ҹ۲�ʱ��
    public float chaseWaitTime;
    private float chaseTimer;
    public float ObserveRotateSpeed;
    //�۲�ʱ����ת�ĽǶ�
    float totalAngle = 60f;
    float currentAngle = 0f;
    bool isForward = true;
    //����׷�پ���
    public float SqrChaseDistance;
    private TextMeshPro textMesh;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemySight = transform.Find("View").GetComponent<EnemySight>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        textMesh = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        //����״̬��
        if (enemySight.PlayerInSight)
        {
            lost = false;
            chase = true;
            if (player != null)
                shoot();
        }
        //��Ұ��ʧ��׷�����
        else if (chase)
        {
            if (player != null)
                Chasing();
        }
        else
        {
            Patrol();
        }

        if (player == null)
        {
            chase = false;
        }
    }
    void Patrol()
    {
        agent.isStopped = false;
        Debug.Log("Patrolling");
        textMesh.text = "Patrolling";
        agent.speed = patrolSpeed;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped = true;
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolTime)
            {
                if (waypointIndex == targets.childCount - 1)
                {
                    waypointIndex = 0;
                }
                else
                {
                    waypointIndex++;
                }
                agent.SetDestination(targets.GetChild(waypointIndex).position);
                patrolTimer = 0;
            }
        }
        else
        {
            patrolTimer = 0;
        }
    }
    void shoot()
    {
        Debug.Log("Player in sight");
        textMesh.text = "Player in sight";
        Vector3 LookPos = player.position;
        //����y��
        LookPos.y = transform.position.y;
        Vector3 Dir = LookPos - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(Dir), Mathf.Min(1, Time.deltaTime * ShootRotationSpeed));
        agent.isStopped = true;
        //�������ʱ�����
        if (Vector3.Angle(transform.forward, Dir) < 2)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= ShootInterval)
            {
                Debug.Log("shoot");
                textMesh.text = "shoot";
                shootTimer = 0;
                Rigidbody bullet = Instantiate(bulletPrefab, transform.position,
                    Quaternion.LookRotation(player.position - transform.position));
            }
        }
        else
        {
            shootTimer = 0;
        }
    }
    void Chasing()
    {
        agent.isStopped = false;
        agent.speed = chaseSpeed;
        Vector3 Dist = player.position - transform.position;

        if (Dist.sqrMagnitude < SqrChaseDistance)
        {
            Debug.Log("ChasePlayer");
            textMesh.text = "ChasePlayer";
            lost = false;
            agent.SetDestination(player.position);
        }
        else
        {
            Debug.Log("ChasePlayerLastSeenPosition");
            textMesh.text = "ChasePlayerLastSeenPosition";
            lost = true;
            agent.SetDestination(enemySight.playerLastSeenPosition);
        }
        if (agent.remainingDistance <= agent.stoppingDistance && lost)
        {
            Debug.Log("observe");
            textMesh.text = "observe";
            //�۲�
            chaseTimer += Time.deltaTime;


            if (chaseTimer >= chaseWaitTime)
            {
                //����Ŀ��
                Debug.Log("lost");
                textMesh.text = "lost";
                chaseTimer = 0;
                chase = false;
            }
            //����תһȦ
            else
            {
                //ÿ����ת�ĽǶ�
                float anglePerSecond = totalAngle * 2 / chaseWaitTime;
                if (isForward)
                {
                    if (currentAngle < totalAngle)
                    {
                        currentAngle += anglePerSecond * Time.deltaTime;
                    }
                    else
                    {
                        isForward = false;
                        currentAngle = totalAngle;
                    }
                }
                else
                {
                    if (currentAngle > 0)
                    {
                        currentAngle -= anglePerSecond * Time.deltaTime;
                    }
                    else
                    {
                        isForward = true;
                        currentAngle = 0;
                    }
                }
                transform.localEulerAngles = new Vector3(0, currentAngle, 0);
            }
        }
        else
        {
            chaseTimer = 0;
        }
        //    if (sightingDeltaPos.sqrMagnitude > SqrChaseDistance)
        //    {
        //        agent.SetDestination(sightingDeltaPos);
        //    }

    }
}
