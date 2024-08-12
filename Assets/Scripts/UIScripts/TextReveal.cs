using System.Collections;
using TMPro;
using UnityEngine;

public class TextReveal : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public GameObject _continueText;
    public GameObject _buttons;
    public TextMeshProUGUI ggs;
    public float revealSpeed = 0.05f;

    bool _keepGoing = false;
    string _displayText;
    string[] Texts =
    {"You were too late...",
    "By the time you got back...",
    "The cage was already in the UHaul..."
  };
    int _countingTexts=0;

    void Start()
    {
      if(_buttons !=null)
      {
        _buttons.SetActive(false);
      }
      _continueText = GameObject.Find("EPanel");
      if (_continueText!=null)
      {
        _continueText.SetActive(false);
      }
      else {
        Debug.Log("Panel not found");
      }
      textMeshPro = GameObject.Find("GameOverText").GetComponent<TextMeshProUGUI>();
        if (textMeshPro != null)
        {
            textMeshPro.text = "";
            StartCoroutine(RevealText(Texts[_countingTexts]));
        }
        else
        {
            Debug.LogError("TextMeshPro component is not assigned.");
        }
        ggs = GameObject.Find("ContText").GetComponent<TextMeshProUGUI>();
        if(ggs != null)
        {
          ggs.text = "";
        }
    }
    void Update()
    {
      if(_keepGoing)
      {
        if(Input.GetKey(KeyCode.E))
        {
          _keepGoing = false;
          _continueText.SetActive(false);
          StartCoroutine(RevealText(Texts[_countingTexts]));
        }
      }
      if(_countingTexts ==3)
      {
        _continueText.SetActive(false);
        StartCoroutine(FinalText());
      }
    }

    IEnumerator RevealText(string text)
    {
        for (int i = 0; i <= text.Length; i++)
        {
            textMeshPro.text = text.Substring(0, i); // Update the text to show up to the i-th letter
            yield return new WaitForSeconds(revealSpeed); // Wait for the specified amount of time
        }
        _keepGoing = true;
        _continueText.SetActive(true);
        _countingTexts++;
    }
    IEnumerator FinalText()
    {
      string question = "Continue?";
      for (int i =0; i <= question.Length; i++)
      {
        ggs.text = question.Substring(0,i);
        yield return new WaitForSeconds(revealSpeed);
      }//after continue pops up, buttons popup
      _buttons.SetActive(true);
    }
}
