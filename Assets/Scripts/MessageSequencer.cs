using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MessageSequencer : MonoBehaviour
{
    [SerializeField]
    private MessagePrinter _printer = default;
    [SerializeField]
    private string[] _messages = default;
    int _currentIndex = -1;

    void Start()
    {
        MoveNext();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_printer.IsPrinting) { _printer.Skip(); }
            else{ MoveNext();}
        }
    }

    private void MoveNext()
    {
        if (_messages is null or { Length: 0 }) { return; }

        if(_currentIndex + 1 < _messages.Length)
        {
            _currentIndex++;
            _printer?.ShowMessage(_messages[_currentIndex]);
        }
    }

    //private void ShowMessage(string message)
    //{
    //    _textUi.text = message;
    //}
}
