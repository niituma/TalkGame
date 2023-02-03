using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterColorTransitioner : MonoBehaviour
{
    [SerializeField]
    Image _image = default;//”wŒi‰æ‘œ

    [SerializeField]
    Color _to;//‚±‚ÌF‚É‘JˆÚ‚·‚é

    [SerializeField]
    float _duration = 1f;//‘JˆÚŠÔ(•b)

    Color _from;

    float _elapsed = 0;
    /// <summary>
    /// ”wŒiF‚Ì‘JˆÚ‚ªŠ®—¹‚µ‚Ä‚¢‚é‚©‚Ç‚¤‚©
    /// </summary>
    public bool IsConpleted => _image is null ? false : _image.color == _to;

    // Start is called before the first frame update
    void Start()
    {
        if (_image is null) { return; }
        _from = _image.color;
    }

    /// <summary>
    /// ”wŒiF‚Ì‘JˆÚˆ—‚ğŠJn‚·‚é
    /// </summary>
    /// <param name="color"></param>
    public void Play(Color color, Image image, bool transition)
    {
        if (_image is null) { return; }
        _image = image;
        _from = _image.color;
        _to = color;
        _elapsed = transition ? 0 : _duration;
        StartCoroutine(FadeStart());
    }

    IEnumerator FadeStart()
    {
        var elapsed = _elapsed;
        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            _image.color = Color.Lerp(_from, _to, elapsed / _duration);

            yield return null;
        }
        _image.color = _to;

    }

    /// <summary>
    /// Œ»İ‚Ì”wŒiF‚Ì‘JˆÚˆ—‚ğƒXƒLƒbƒv‚·‚é
    /// </summary>
    public void Skip()
    {
        _elapsed = _duration;
    }
}
