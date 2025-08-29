using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _comboText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Slider _comboSlider;
    
    private void Start()
    {
        _comboText.text = "x1";
        _comboSlider.value = 0;
    }

    public void ScoreChange(int points)
    {
        _scoreText.text = points.ToString();
    }

    public void SliderChange(int comboIncreaser)
    {
        _comboSlider.value = comboIncreaser;
    }

    public void ComboChange(int combo)
    {
        _comboText.text = $"x{combo}";
    }
}
