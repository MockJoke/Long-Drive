using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetHealth(int health)
    {
        slider.value = health; 
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health; 

    }
}
