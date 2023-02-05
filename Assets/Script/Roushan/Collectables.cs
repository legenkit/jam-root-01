using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public enum Types
    {
        orb,
        poision
    }

    public Types type;

    public void Absorb()
    {
        AudioManager.instance.PlayAudioClip(AudioManager.AudioType.orb);
        if(type == Types.orb)
            PathManager.instance.AddOrb(1);
        else
            PathManager.instance.AddOrb(-1);
        Destroy(this.gameObject);
    }
}
