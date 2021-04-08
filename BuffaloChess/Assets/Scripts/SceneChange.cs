using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeTheScene(string Name)
    {
        SceneManager.LoadScene(Name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
