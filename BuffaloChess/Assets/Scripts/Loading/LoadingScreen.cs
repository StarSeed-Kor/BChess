using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //나중에 touch로 바꿔야함
        if (Input.GetMouseButtonDown(0))
        {
                SceneManager.LoadScene("Main");
        }
    }
}
