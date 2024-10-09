using UnityEngine;
using UnityEngine.UI;

public class BloodBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBloodBar(float currentValue, float maxValue)
    {
        _slider.value = currentValue / maxValue;
    }
}
