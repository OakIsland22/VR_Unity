using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static event Action<int> OnDamage;

    float elapsedTime = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > 1f)
        {
            TakeDamage(1);
            elapsedTime = 0f;
        }
    }

    public void TakeDamage(int amount)
    {
        OnDamage?.Invoke(amount);
    }
}
