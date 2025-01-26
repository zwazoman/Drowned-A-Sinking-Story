using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollisions : MonoBehaviour
{
    [SerializeField] float _volume = 0;

    bool _soundPlayed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            if (_soundPlayed) return;
            _soundPlayed = true;
            AudioManager.Instance.PlaySFXClipAtPosition(Sounds.SandHit, collision.contacts[0].point, false,_volume);
            StartCoroutine(Allez());
        }
    }

    IEnumerator Allez()
    {
        yield return new WaitForSeconds(0.3f);
        _soundPlayed = false;
    }
}
