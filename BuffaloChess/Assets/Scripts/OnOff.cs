using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnOff : MonoBehaviour
{
    AudioSource Myaudio;
    private void Start()
    {
        Myaudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    public void OnClickMenu(GameObject Menu)
    {
        if (Menu.activeSelf)
        {
            Menu.SetActive(false);
        }
        else
        {
            Menu.SetActive(true);
        }
    }

    public void MusicOnOff()
    {
        if(Myaudio.isPlaying)
        {
            Myaudio.Pause();
        }

        else
        {
            Myaudio.Play();
        }
    }

}
