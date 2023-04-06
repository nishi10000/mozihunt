using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Main");
    }
    public void OnClickRankingButton()
    {
        SceneManager.LoadScene("Result");
    }
    public void OnClickHowtoButton()
    {
        SceneManager.LoadScene("HowTo");
    }
}