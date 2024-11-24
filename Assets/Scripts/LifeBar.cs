using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;


    public void SetMaxLife(int life)
    {
        slider.maxValue = life;
        slider.value = life;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetLife(int life)
    {
        slider.value = life;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
