using UnityEngine;
using UnityEngine.UI;

public class ColorTransitioner : MonoBehaviour
{
    [SerializeField]
    Image _image = default;//�w�i�摜

    [SerializeField]
    Color _to;//���̐F�ɑJ�ڂ���

    [SerializeField]
    float _duration = 1f;//�J�ڎ���(�b)

    Color _from;

    float _elapsed = 0;
    /// <summary>
    /// �w�i�F�̑J�ڂ��������Ă��邩�ǂ���
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
    /// �w�i�F�̑J�ڏ������J�n����
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
    /// ���݂̔w�i�F�̑J�ڏ������X�L�b�v����
    /// </summary>
    public void Skip()
    {
        _elapsed = _duration;
    }
}
