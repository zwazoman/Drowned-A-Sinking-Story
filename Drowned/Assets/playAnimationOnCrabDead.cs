using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAnimationOnCrabDead : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<Animation>().Stop();
        BossHealth.OnBossDead += ()=> GetComponent<Animation>().Play();
    }


}
