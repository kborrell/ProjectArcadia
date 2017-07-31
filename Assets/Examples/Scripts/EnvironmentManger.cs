using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManger : MonoBehaviour {

    [SerializeField]
    ParticleSystem  DragonFly;

    private void Awake()
    {
        DragonFly.Stop();
    }
    void Update () {
        ParticleSwitch();

    }

    void ParticleSwitch() {

        if (TeleportManager.Instance.enabled)
        {
            DragonFly.Play();
        }
        else
        {
            DragonFly.Stop();
        }
    }
}
