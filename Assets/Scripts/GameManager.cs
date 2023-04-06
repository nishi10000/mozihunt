using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // ゲームマネージャーのインスタンス

    public TextMeshProUGUI scoreText; // スコアを表示するTextMeshProUGUIコンポーネント
    public TextMeshProUGUI collectedText; // 取得したテキストを表示するTextMeshProUGUIコンポーネント
    public TextMeshProUGUI timeText; // 残り時間を表示するTextMeshProUGUIコンポーネント

    private int score; // スコア
    public string collectedString = ""; // 取得したテキストを格納する文字列

    public float timeLimit = 60f; // 制限時間
    public float timer; // 残り時間

    public GameObject endResultObject; // Endresultオブジェクトを格納する変数

    public AudioSource endAudio;
    
    private bool isGameOver = false; // ゲームオーバー判定用のフラグ
    // ゲーム開始時に呼び出される関数
    private void Awake()
    {

        isGameOver = false; // フラグをfalseにする
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject); 
        }
        else
        {
            Destroy(this.gameObject);
        }
    
        StartGame();
        
    }

    // 毎フレーム呼び出される関数
    void Update()
    {
        // 残り時間を更新する
        timer -= Time.deltaTime;

        if (!isGameOver && timer <= 0f)
        {
            isGameOver = true;
            GameOver();
        }
        if(timer >= 0f){
            // 残り時間を表示する
            timeText.text = "のこりじかん: " + Mathf.FloorToInt(timer).ToString();
    
        }
    }

    // ゲームを開始する
    public void StartGame()
    {
        timer = timeLimit; // 残り時間を初期化する
    }

    // スコアを加算する関数
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "スコア: " + score;
        StoreManager.instance.score = score;
    }

    // 取得したテキストを足す関数
    public void AddCollectedText(string text, int maxLength = 5)
    {
        // 取得したテキストを格納する文字列をmaxLengthより小さい値にする。
        if (collectedString.Length >= maxLength)
        {
            //Debug.Log("Collected text is too long!");
            return;
        }
        
        collectedString += text;
        collectedText.text = collectedString;
    }

    // 取得したテキストをクリアする関数
    public void ClearCollectedText()
    {
        collectedString = "";
        collectedText.text = collectedString;
        //Debug.Log("Collected text cleared!");
    }

    // 取得したテキストの最後の1文字を削除する関数
    public void RemoveLastCharacter()
    {
        if (collectedString.Length > 0)
        {
            collectedString = collectedString.Substring(0, collectedString.Length - 1);
            collectedText.text = collectedString;
        }
    }
    void GameOver()
    {
        endAudio.Play();
        Debug.Log("Game Over!");
        isGameOver = true; // フラグをtrueにする
        // 3秒後にリザルトシーンに遷移する
        StartCoroutine(LoadResultScene());

        // Endresultオブジェクトをアニメーションで移動させる
        endResultObject.SetActive(true); // Endresultオブジェクトを表示する
        Vector3 startPos = new Vector3(Screen.width + endResultObject.GetComponent<RectTransform>().rect.width / 2f, Screen.height / 2f, 0f); // スタート位置（画面外の右側）
        Vector3 endPos = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f); // エンド位置（画面中央）
        StartCoroutine(MoveEndresultObject(startPos, endPos, 1f)); // アニメーションで移動させる
        
    }

        // Endresultオブジェクトをアニメーションで移動させる関数
    private IEnumerator MoveEndresultObject(Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            endResultObject.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
    }

    private IEnumerator LoadResultScene()
    {
        yield return new WaitForSeconds(3f); // 3秒待つ
        SceneManager.LoadScene("Result"); // リザルトシーンに切り替える
    }
    public int GetScore()
    {
        return score;
    }
}