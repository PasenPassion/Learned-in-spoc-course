using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C5_Script : MonoBehaviour
{
    public float speed;
    private void Awake()
    {
        Debug.Log("Awake here");
    }
 private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyWithFadeEffect());

    }
    IEnumerator DestroyWithFadeEffect()
    {
        yield return new WaitForSeconds(5f); // �ȴ�5���������ִ��

        float fadeDuration = 2f;
        float timer = 0f;
        Color startColor = spriteRenderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            yield return null;//ֹͣЭ��
        }

        Destroy(gameObject); // ������������Ϸ����
    }


// Update is called once per frame
void Update()
    {
        //transform.position += Vector3.right * speed * Time.deltaTime;
        //if (transform.position.x > 5 || transform.position.x < -5)
        //{
        //    speed = -speed;
        //}
    }
}
