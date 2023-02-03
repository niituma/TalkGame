using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.VersionControl;
using UnityEngine;

public class BackGroundSequencer : MonoBehaviour
{
    [SerializeField]
    ColorTransitioner _colorTransitioner = default;

    [SerializeField]
    Color[] _colors;

    int _currentIndex = -1;
    void Start()
    {
        MoveNext();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_colorTransitioner is { IsConpleted: false }) { _colorTransitioner.Skip(); }
            else { MoveNext(); }
        }
    }

    private void MoveNext()
    {
        if(_colors is null or { Length: 0 }) { return; }

        if (_currentIndex + 1 < _colors.Length)
        {
            _currentIndex++;
            _colorTransitioner?.Play(_colors[_currentIndex]);
        }
    }
}
