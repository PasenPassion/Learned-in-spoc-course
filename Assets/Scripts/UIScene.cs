using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIScene : MonoBehaviour
{
    public int score=0;
    public Text scoreText;
    public Text timeText;
    float timer = 0;
    //极限时间
    int t = 60;
    //public Text HealthText;
    //public Image HealthImage;
    //float health = 100;

    private void Update()
    {

        //if (Input.GetMouseButtonDown(0))
        //{
        //    health--;
        //    HealthText.text = health.ToString() + "/100";
        //    HealthImage.GetComponent<Image>().fillAmount = health / 100;
        //}
        ////计时器
        if (t <= 0)
        {
            Debug.Log("GameEnd");
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                timer = 0;
                t--;
                timeText.text = "倒计时：" + t.ToString();
            }
        }
        ////点击屏幕加分
        //if(Input.GetMouseButtonDown(0))
        //{
        //    score += 10;
        //    scoreText.text = score.ToString();
        //}
        
    }
    //public void ClickAdd()
    //{
    //    score += 10;
    //    scoreText.text=score.ToString();
    //}
    public void StartGame()
    {
        Debug.Log("Start the game");
    }
    public void Setting()
    {
        Debug.Log("Open the setting");
    }
    public void ExitGame()
    {
        Debug.Log("Exit the game");
    }
}
