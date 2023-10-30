using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        if (!_slider)
        {
            Debug.LogError("No slider found on FloatingHealthBar");
            enabled = false;
            return;
        }
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _slider.value = currentHealth / maxHealth;
        if (_fillImage) _fillImage.enabled = currentHealth > 0;
    }
}
