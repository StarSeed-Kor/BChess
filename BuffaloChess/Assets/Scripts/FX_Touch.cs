using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class FX_Touch : MonoBehaviour
{
    //public Collider targetCollider;
	public GameObject spawnParticle;
    public float FXsize;
    

    // Start is called before the first frame update
    void Start()
    {
        if (FXsize == 0)
        {
            FXsize = 0.25f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && spawnParticle != null)
        {
            Vector2 MousePos = Input.mousePosition;//마우스 클릭 받기
            MousePos = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(MousePos);//받은 클릭 카메라에 맞춰서 위치 조정

            GameObject particles = (GameObject)Instantiate(spawnParticle); //특정 이펙트의 클론 제작
            particles.transform.localScale *= FXsize;//크기조절
            particles.transform.position = new Vector3(MousePos.x, MousePos.y - 0.1f, -3);//제작한 클론의 위치
        }
    }

    public void Particle_Destroy()
    {
        Destroy(this.gameObject);
    }
}
