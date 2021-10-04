using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _curentScore;
    [SerializeField] private Text _bestScore;

    private void Update()
    {
        _curentScore.text = GameManager.gameManager.CurentScore.ToString();
        _bestScore.text = GameManager.gameManager.BestScore.ToString();
    }
}