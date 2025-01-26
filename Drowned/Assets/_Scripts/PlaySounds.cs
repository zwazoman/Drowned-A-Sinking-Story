using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    [SerializeField] float _prepareAttackVolume = 1;
    [SerializeField] float _attackVolume = 1;

    public void PlayPrepareAttck()
    {
        AudioManager.Instance.PlaySFXClip(Sounds.CrabPrepareAttack,_prepareAttackVolume);
    }

    public void PlayAttack()
    {
        AudioManager.Instance.PlaySFXClip(Sounds.CrabAttack, _attackVolume);
    }
}
