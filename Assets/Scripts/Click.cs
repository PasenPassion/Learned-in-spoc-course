using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour
{
    public int num;
  
    private void OnMouseDown()
    {
        if (num == 0)
        {
            Generator.score--;
            Debug.Log("����ˣ�������һ��");
            Debug.Log("��ǰ������" + Generator.score);
        }
        if (num == 1)
        {
            Generator.score++;
            Debug.Log("����ˣ�������һ��");
            Debug.Log("��ǰ������" + Generator.score);
        }
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(gameObject.transform.position.y<-6)
        {
            Destroy(this.gameObject);
        }
    }

}
