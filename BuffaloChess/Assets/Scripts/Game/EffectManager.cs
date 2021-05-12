using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public float Timer1;
    public GameObject Event1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Timer1 -= Time.deltaTime;

        if (Timer1 < 0 && this.name == Event1.name)
        {
            Event1.SetActive(false);
        }
    }

    public void Effect_Destroy()
    {
        Destroy(this.gameObject);
    }
}
