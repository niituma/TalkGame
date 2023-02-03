using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] string[] talks;
    int count = 0;
    bool _istalk;
    Coroutine talk;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && count < talks.Length)
        {
            if (_istalk)
            {
                StopCoroutine(talk);
                _text.text = talks[count];
                count++;
                _istalk = false;
                return;
            }
            talk = StartCoroutine(Dialogue());
        }
    }
    IEnumerator Dialogue()
    {
        _istalk = true;
        _text.text = string.Empty;
        foreach (var word in talks[count])
        {
            _text.text += word;
            yield return new WaitForSeconds(0.1f);
        }
        count++;
        _istalk = false;
    }
}
