using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Slider slider;

    private void Awake()
    {
        GameManager.OnDamage += UpdateHealthBar;
    }

    private void OnDestroy()
    {
        GameManager.OnDamage -= UpdateHealthBar;
    }

    void UpdateHealthBar(int amount)
    {
        if (slider != null)
        {
            slider.value -= amount;
        }
        print("Valor: "+ amount); 
    }
}
