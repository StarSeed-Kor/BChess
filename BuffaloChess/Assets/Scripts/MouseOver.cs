using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour
{
    public Sprite OffImage;
    public Sprite OnImage;
    public Image MImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PointUP()
    {
        MImage.sprite = OnImage;
    }

    public void PointOut()
    {
        MImage.sprite = OffImage;
    }
}
