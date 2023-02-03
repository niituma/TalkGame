using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class MessagePrinter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textUi = default;

    private string _message = "";

    [SerializeField]
    private float _speed = 1.0F;

    int[] _alphaArray = new int[100];

    private float _elapsed = 0; // 文字を表示してからの経過時間
    private float _interval; // 文字毎の待ち時間
    private int _currentIndex = -1;

    public bool IsPrinting
    {
        get
        {
            // TODO: ここにコードを書く
            return _currentIndex + 1 != _message.Length;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (_textUi is null) { return; }
        _textUi.text = "";
        //if (_message is null or { Length: 0 }) { return; }
        //_interval = _speed / _message.Length;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (_textUi is not null && _message is not null && _currentIndex + 1 < _message.Length)
    //    {
    //        _elapsed += Time.deltaTime;
    //        if (_elapsed > _interval)
    //        {
    //            _elapsed = 0;
    //            _currentIndex++;
    //            _alphaArray[_currentIndex] = 10;
    //        }
    //    }

    //    if(_message is null or { Length: 0 } || _alphaArray[_message.Length - 1] >= 255) { return; }

    //    _textUi.text = "";
    //    for (int i = 0; i < _currentIndex + 1; i++)
    //    {
    //        if (_alphaArray[i] < 250)
    //        {
    //            _alphaArray[i] += 5;
    //        _textUi.text += $"<alpha=#{_alphaArray[i].ToString("x2")}>" + _message[i];
    //        }
    //        else
    //        {
    //            _alphaArray[i] = 255;
    //            _textUi.text += _message[i];
    //        }
    //    }
    //}

    public IEnumerator ShowMessage(string message, CancellationToken ct)
    {
        _textUi.text = "";
        _currentIndex = -1;
        _interval = _speed / message.Length;

        while (!ct.IsCancellationRequested && _alphaArray[message.Length - 1] <= 255)
        {
            if (_currentIndex + 1 <= message.Length)
            {
                _elapsed += Time.deltaTime;
                if (_elapsed > _interval)
                {
                    _elapsed = 0;
                    _currentIndex++;
                    _alphaArray[_currentIndex] = 10;
                }
            }

            _textUi.text = "";
            for (int i = 0; i < _currentIndex; i++)
            {
                if (_alphaArray[i] < 250)
                {
                    _alphaArray[i] += 5;
                    _textUi.text += $"<alpha=#{_alphaArray[i].ToString("x2")}>" + message[i];
                }
                else
                {
                    _alphaArray[i] = 255;
                    _textUi.text += message[i];
                }
            }

            yield return null;
        }

        _textUi.text = "";
        for (int i = 0; i < message.Length; i++)
        {
            _alphaArray[i] = 255;
            _textUi.text += message[i];
        }
        yield return null;
    }

    public void ShowMessage(string message)
    {
        // TODO: ここにコードを書く
        for (int i = 0; i < _message.Length; i++)
        {
            _alphaArray[i] = 0;
        }
        _textUi.text = "";
        _currentIndex = -1;
        _message = message;
        _interval = _speed / message.Length;

    }
    public void Skip()
    {
        _textUi.text = "";
        for (int i = 0; i < _message.Length; i++)
        {
            _alphaArray[i] = 255;
            _textUi.text += _message[i];
        }
        _currentIndex = _message.Length - 1;
    }
}
