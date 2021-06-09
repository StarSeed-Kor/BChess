using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    private string S_Name;

    // Start is called before the first frame update
    public void ChangeTheScene(string Name)
    {
        SceneManager.LoadScene(Name);
    }

    public void ChangeScene_3s(string Name)
    {
        S_Name = Name;//어느 씬으로 갈지
        Invoke("CallScene", 3);//3초 뒤에 씬 넘기기

        if (GetComponent<AudioSource>())//특정 오디오가 있다면 실행
        {
            GetComponent<AudioSource>().Play();
        }
    }

    void CallScene()
    {
        SceneManager.LoadScene(S_Name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
