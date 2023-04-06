using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // プレイヤーの移動速度
    public float jumpForce = 10f; // プレイヤーのジャンプ力
    public GameObject giftPrefab; // 打ち上げるGiftのプレハブ

    public GiftSpawner giftSpawner; // GiftSpawnerコンポーネント
    public CharacterCount characterCount; // CharacterCountコンポーネント
    private bool isMovingEnabled = true; // プレイヤーの移動が有効かどうか
    private bool isJumping = false; // プレイヤーがジャンプしているかどうか
    private Rigidbody2D rb; // プレイヤーのリジッドボディ

    public  GameObject playersprite; 
    private Animator playerspriteAnim;

    public AudioSource giftspawnsound;

    private bool isgiftDisabled = false; // 上キーが無効化されているかどうか
    public float giftDisableTime = 0.5f; // 上キーを無効化する時間
     private float giftDisableTimer = 0f; // 上キー無効化のタイマー

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerspriteAnim = playersprite.GetComponent<Animator>();
    }

    // 毎フレーム呼び出される関数
void Update()
{
    // プレイヤーの移動が有効な場合に移動する
    if (isMovingEnabled)
    {
        // 左方向に移動する場合
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(-1, 1, 1); // 正面を向く
            playerspriteAnim.SetBool("isWalk", true);
        }
        // 右方向に移動する場合
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(1, 1, 1); // 反転して正面を向く
            playerspriteAnim.SetBool("isWalk", true);
        }
        else
        {
            playerspriteAnim.SetBool("isWalk", false); // 移動していない場合にisWalkをfalseにする
        }

        // スペースボタンを押すとジャンプする
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playerspriteAnim.SetBool("isJump", true); // ジャンプアニメーションを再生する
        }

// 上キーを押した時に一定時間上キーを無効化する
            if (Input.GetKeyDown(KeyCode.UpArrow) && !isgiftDisabled)
            {

                if(GameManager.instance.collectedString.Length > 0){
                    isgiftDisabled = true;
                    giftDisableTimer = giftDisableTime;
                    giftSpawner.launching_gift(transform.position);
                    GameManager.instance.RemoveLastCharacter();
                    giftspawnsound.Play();
                }
            }

            // 上キー無効化中は上キーを無効化する
            if (isgiftDisabled)
            {
                giftDisableTimer -= Time.deltaTime;
                if (giftDisableTimer <= 0f)
                {
                    isgiftDisabled = false;
                }
            }

        // 画面外に出ないように制限する
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            if (screenPos.x < 0f)
            {
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0f, screenPos.y, screenPos.z));
            }
            else if (screenPos.x > Screen.width)
            {
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, screenPos.y, screenPos.z));
            }
        
    }

    

    // GameManagerクラスのタイマーが0以下になった場合に移動を停止する
    if (GameManager.instance.timer <= 0f)
    {
        isMovingEnabled = false;
         playerspriteAnim.SetBool("isWalk", false);
    }
    }

    // 地面に着地したときにジャンプ状態を解除する
private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        isJumping = false;
        playerspriteAnim.SetBool("isJump", false); // ジャンプアニメーションを停止する
    }
}
}