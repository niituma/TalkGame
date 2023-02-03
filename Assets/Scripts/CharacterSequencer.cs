using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSequencer : MonoBehaviour
{
    [SerializeField]
    CharacterColorTransitioner _colorTransitioner = default;

    [SerializeField]
    Image[] _images;

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
        if (_images is null or { Length: 0 }) { return; }

        if (_currentIndex + 1 < _images.Length)
        {
            _currentIndex++;
            _colorTransitioner?.Play(Color.white, _images[_currentIndex], true);
            for(int i = 0; i <= _currentIndex; i++)
            {
                if(i == _currentIndex) { continue; }
                _colorTransitioner?.Play(Color.gray, _images[i], false);
            }
        }
    }
}
