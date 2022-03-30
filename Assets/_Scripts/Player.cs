using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;

    public float get_healthAsPercentage
    {
        get { return currentHealth / maxHealth; } /// returns a value below 1.
    }
}
