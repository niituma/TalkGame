using UnityEngine;
using UnityEngine.UI;

public class ColorTransitioner : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        if(_image is null) { return; }

        _elapsed += Time.deltaTime;
        if (_elapsed < _duration)
        {
            _image.color = Color.Lerp(_from, _to, _elapsed / _duration);
        }
        else
        {
            _image.color = _to;
        }
    }

    /// <summary>
    /// ”wŒiF‚Ì‘JˆÚˆ—‚ğŠJn‚·‚é
    /// </summary>
    /// <param name="color"></param>
    public void Play(Color color)
    {
        if(_image is null) { return; }

        _from = _image.color;
        _to = color;
        _elapsed = 0;
    }

    /// <summary>
    /// Œ»İ‚Ì”wŒiF‚Ì‘JˆÚˆ—‚ğƒXƒLƒbƒv‚·‚é
    /// </summary>
    public void Skip()
    {
        _elapsed = _duration;
    }
}
