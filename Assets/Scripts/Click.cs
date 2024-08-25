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
            Debug.Log("点错了！分数减一！");
            Debug.Log("当前分数：" + Generator.score);
        }
        if (num == 1)
        {
            Generator.score++;
            Debug.Log("点对了！分数加一！");
            Debug.Log("当前分数：" + Generator.score);
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
