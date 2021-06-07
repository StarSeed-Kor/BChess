using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnOff : MonoBehaviour
{
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
}
