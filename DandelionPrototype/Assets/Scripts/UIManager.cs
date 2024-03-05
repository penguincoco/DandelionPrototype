using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    [Header("UI Variables")]
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private string[] prompts;
    private int currentPromptCounter;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    public void SetPromptText()
    {
        promptText.text = prompts[currentPromptCounter];
    }

    public void SetPromptText(string textToShow)
    {
        promptText.text = textToShow;
    }

    public void ClearPromptText()
    {
        promptText.text = "";
    }

    public void IncreasePromptCounter()
    {
        currentPromptCounter++;
    }

    public void DecreasePromptCounter()
    {
        currentPromptCounter--;
    }
}
