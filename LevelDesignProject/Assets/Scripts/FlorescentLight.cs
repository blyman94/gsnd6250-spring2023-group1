using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorescentLight : MonoBehaviour
{
    [SerializeField] Light[] _pointLights;
    [SerializeField] GameObject _glowingLight;
    [SerializeField] GameObject _notGlowingLight;

    public void TurnOffLights()
    {
        foreach (Light light in _pointLights)
        {
            light.enabled = false;
            _glowingLight.SetActive(false);
            _notGlowingLight.SetActive(true);
        }
    }

}
