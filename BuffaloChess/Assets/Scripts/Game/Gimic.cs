using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimic : MonoBehaviour
{
    private GameObject target;
    float time = 1f;
    float waittime = 0.1f;
    Vector3 Origin_Size;

    // Start is called before the first frame update
    void Start()
    {
        Origin_Size = this.gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        Gimic_Click_Event();
    }

    public void Reset_Event()
    {
        /*GameObject.Find("Gimic_Dog").*/GetComponent<Animator>().SetTrigger("Gimic_Stop");
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
            if (target == this.gameObject)
            {
                //타겟 오브젝트가 스크립트가 붙은 오브젝트라면
                // 여기에 실행할 코드를 적습니다.
                int RandNum = Random.Range(0,5);
                Debug.Log("Random Num : " + RandNum);
                time = 0;

                switch (RandNum)
                {
                    case 0:
                        GameObject.Find("Gimic_Dog").GetComponent<Animator>().SetTrigger("Gimic_Play");
                        Debug.Log("Gimic Works");
                        break;

                    case 1:
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        break;
                }
            }
        }
    }

    void CastRay() // 유닛 히트처리 부분.  레이를 쏴서 처리합니다. 
    {
        target = null;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        { 
            //히트되었다면 여기서 실행
            //Debug.Log (hit.collider.name);  //이 부분을 활성화 하면, 선택된 오브젝트의 이름이 찍혀 나옵니다. 
            target = hit.collider.gameObject;  //히트 된 게임 오브젝트를 타겟으로 지정
        }
    }

    void Gimic_Click_Event()
    {
        if (time <= waittime)
            this.gameObject.transform.localScale = Origin_Size * 1.1f;
        else
            this.gameObject.transform.localScale = Origin_Size;
    }
}
