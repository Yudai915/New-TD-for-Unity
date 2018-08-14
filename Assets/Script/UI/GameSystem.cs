using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{

    //　スタートボタンを押したら実行する
    public void GameStart()
    {
        SceneManager.LoadScene("Battle");
    }

    //　ゲーム終了ボタンを押したら実行する
    public void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
		Application.OpenURL("http://www.yahoo.co.jp/");
#else
		Application.Quit();
#endif
    }

}
