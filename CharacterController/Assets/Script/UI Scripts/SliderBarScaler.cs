using UnityEngine;
using UnityEngine.UI;

public class SliderBarScaler : MonoBehaviour
{
    public Slider mainSlider;
    public Slider previewSlider;

    private float sliderWidth;

    RectTransform mainSliderRect;
    RectTransform previewSliderRect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainSliderRect = mainSlider.GetComponent<RectTransform>();
        previewSliderRect = previewSlider.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSliderSize();
    }

    public void UpdateSliderSize()
    {
        sliderWidth = mainSlider.maxValue * 25;
        mainSliderRect.sizeDelta = new Vector2(sliderWidth, mainSliderRect.sizeDelta.y);
        previewSliderRect.sizeDelta = new Vector2(sliderWidth, previewSliderRect.sizeDelta.y);
    }
}
