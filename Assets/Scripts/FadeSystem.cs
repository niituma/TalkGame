using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeSystem : MonoBehaviour
{
    [SerializeField]
    Image _panel;
    [SerializeField]
    Image[] _images;
    [SerializeField]
    float _fadeTime;
    [SerializeField]
    MessagePrinter _printer;
    Color _fadecolor = default;

    Coroutine skip;
    float _time;

    bool _fadeing = false;

    private void Update()
    {
        //if (_fadeing) { _panel.color = _panel.canvasRenderer.GetColor(); }

        if (Input.GetMouseButtonDown(0))
        {
            if (_panel is null) { return; }

            if (_fadeing) { Skip(); }
            else
            {
                //_panel.canvasRenderer.SetColor(_panel.color);
                _printer?.ShowMessage("FadeäJén");
                if (_panel.color.a == 0)
                    Fade(0, 0, 0, 1);
                else
                {
                    Fade(0, 0, 0, 0);
                    if (_images[0].gameObject.activeSelf)
                    {
                        _images[0].gameObject.SetActive(false);
                        _images[1].gameObject.SetActive(true);
                    }
                    else
                    {
                        _images[0].gameObject.SetActive(true);
                        _images[1].gameObject.SetActive(false);
                    }
                }
            }
        }

        if (_fadeing && _panel is not null && _panel.color == _fadecolor)
        {
            _fadeing = false;
            _printer?.ShowMessage("FadeèIóπ");
        }
    }
    public void Fade(float red, float green, float blue, float alfa)
    {
        _fadeing = true;
        _fadecolor = new Color(red, green, blue, alfa);
        //_panel.CrossFadeColor(_fadecolor, _fadeTime, true, true);
        skip = StartCoroutine(StartFade(red, green, blue, alfa));
    }

    public IEnumerator StartFade(float red, float green, float blue, float alfa)
    {
        while (true)
        {
            yield return null;
            _time += Time.deltaTime;
            if (alfa == 0)
            {
                var a = 1.0f - _time / _fadeTime;
                _panel.color = new Color(red, green, blue, a);
                if (a <= 0) { break; }
            }

            if (alfa == 1)
            {
                var a = _time / _fadeTime;
                _panel.color = new Color(red, green, blue, a);
                if (a >= 1) { break; }
            }
        }
        _time = 0;
        _panel.color = _fadecolor;
    }

    public void Skip()
    {
        StopCoroutine(skip);
        _panel.color = _fadecolor;
        //_panel.canvasRenderer.SetColor(new Color(0, 0, 0, 1));
    }
}
