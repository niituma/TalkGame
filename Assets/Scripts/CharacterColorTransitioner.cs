using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterColorTransitioner : MonoBehaviour
{
    [SerializeField]
    Image _image = default;//背景画像

    [SerializeField]
    Color _to;//この色に遷移する

    [SerializeField]
    float _duration = 1f;//遷移時間(秒)

    Color _from;

    float _elapsed = 0;
    /// <summary>
    /// 背景色の遷移が完了しているかどうか
    /// </summary>
    public bool IsConpleted => _image is null ? false : _image.color == _to;

    // Start is called before the first frame update
    void Start()
    {
        if (_image is null) { return; }
        _from = _image.color;
    }

    /// <summary>
    /// 背景色の遷移処理を開始する
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
    /// 現在の背景色の遷移処理をスキップする
    /// </summary>
    public void Skip()
    {
        _elapsed = _duration;
    }
}
