using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;

    private float _scoreValue;

    private void Update()
    {
        ShowScore();
    }

    public void SetScore(float value)
    {
        _scoreValue = value;
    }

    private void ShowScore()
    {
        _score.text = _scoreValue.ToString("F0");
    }
}
