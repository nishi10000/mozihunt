using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleText : MonoBehaviour
{
    public float speed = 5f;
    public float stopY = 2f;
    public float delayTime = 1f; // 落下を開始するまでの待ち時間

    private Rigidbody2D rb2d;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0f; // 落下を無効にする
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= delayTime)
        {
            rb2d.gravityScale = 1f; // 落下を有効にする
            rb2d.velocity = new Vector2(0f, speed);
        }

        if (transform.position.y <= stopY)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.gravityScale = 0f;
        }
    }
}