using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageBiggen : MonoBehaviour
{
  [SerializeField] RectTransform _image;
  [SerializeField] GameObject _textPanel;
  [SerializeField] GameObject _yourdidit;
  [SerializeField] GameObject _buttons;
  int i = 0;
  Vector3 _scaleGoal = new Vector3(4.6f,4.6f,4.6f);
  Vector3 _initScale = new Vector3(0.1f,0.1f,0.1f);


    // Start is called before the first frame update
    void Start()
    {
      _image.localScale = _initScale;
      _textPanel.SetActive(false);
      _yourdidit.SetActive(false);
      _buttons.SetActive(false);
      StartCoroutine(Pictures());
      Time.timeScale = 1f;
    }
    public IEnumerator Pictures()
    {
      while(i<50)
      {
        _image.localScale = Vector3.Slerp(_initScale, _scaleGoal, i/50f);
         yield return null;
         yield return null;
         i++;
      }
      yield return new WaitForSeconds(3f);
      _textPanel.SetActive(true);
      yield return new WaitForSeconds(4f);
      _yourdidit.SetActive(true);
      yield return new WaitForSeconds(3f);
      _buttons.SetActive(true);
    }
}
