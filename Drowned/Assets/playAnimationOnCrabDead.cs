using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAnimationOnCrabDead : MonoBehaviour
{
    
    void Start()
    {
        BossHealth.OnBossDead += ()=> GetComponent<Animation>().Play();
    }


}
