using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class PlayerHealthBar : MonoBehaviour
{
    RawImage healthBarRawImage;
    Player playerScript;

    void Start()
    {
        playerScript = FindObjectOfType<Player>();
        healthBarRawImage = GetComponent<RawImage>();
    }

    void Update()
    {
        float xValue = -(playerScript.get_healthAsPercentage / 2f) - 0.5f;
        healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }
}
