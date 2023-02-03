using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public interface IAwaiter<T> // ���ʂ��󂯎�鑤�̂��߂̃C���^�[�t�F�C�X
{
    /// <summary>
    /// �������I���������ǂ����B
    /// </summary>
    bool IsCompleted { get; }

    /// <summary>
    /// �����̌��ʁB
    /// </summary>
    T Result { get; }
}

class Awaiter<T> : IAwaiter<T> // ���ʂ�ݒ肷�鑤�̎���
{
    public bool IsCompleted { get; private set; }

    public T Result { get; private set; }

    /// <summary>
    /// �������I�����Č��ʂ�ݒ肷��B
    /// </summary>
    public void SetResult(T result)
    {
        Result = result;
        IsCompleted = true;
    }
}
public class SkipRequestSource
{
    /// <summary>
    /// �X�L�b�v����p�̃g�[�N����Ԃ��B
    /// </summary>
    public SkipRequestToken Token
        => new SkipRequestToken(this);

    /// <summary>
    /// �X�L�b�v��v������Ă���ꍇ�� true�B
    /// </summary>
    public bool IsSkipRequested { get; private set; }

    /// <summary>
    /// �X�L�b�v��v������B
    /// </summary>
    public void Skip() { IsSkipRequested = true; }
}

public struct SkipRequestToken
{
    private SkipRequestSource _source;

    public SkipRequestToken(SkipRequestSource source)
        => _source = source;

    /// <summary>
    /// �X�L�b�v��v������Ă���ꍇ�� true�B
    /// </summary>
    public bool IsSkipRequested => _source.IsSkipRequested;
}
public class Sample : MonoBehaviour
{
    [SerializeField]
    Button[] _buttons;
    [SerializeField] 
    EventSystem eventSystem;

    private void Start()
    {
        StartCoroutine(RunAsync());
    }

    private IEnumerator Run()
    {
        var selection = new string[]
        {
        "�I����1",
        "�I����2",
        "�I����3",
        };
        Debug.Log("�I������\�����ē��͂�҂��܂��B");
        yield return WaitForSelection(selection, out var awaiter);
        Debug.Log($"�I�������ʂ� {selection[awaiter.Result]} �ł����B"); ;
    }

    public IEnumerator WaitForSelection(string[] messages, out IAwaiter<int> awaiter)
    {
        var result = new Awaiter<int>();
        var e = WaitForSelection(messages, result);
        awaiter = result;
        return e;
    }

    private IEnumerator WaitForSelection(string[] messages, Awaiter<int> awaiter)
    {
        foreach(var message in messages)
        {

        }
        // �I������\�����āA�������̑҂���
        while (true)
        {
            for (var i = 0; i < _buttons.Length; i++)
            {
                if (_buttons[i] == eventSystem.currentSelectedGameObject)
                {
                    awaiter.SetResult(i); // �������I���E���ʂ�ݒ�
                    yield break;
                }
            }

            yield return null;
        }
    }

    private IEnumerator RunAsync()
    {
        while (true)
        {
            Debug.Log("�}�E�X�̃{�^�����͂�҂��܂�");
            yield return WaitForMouseButtonDown(out var awaiter);

            // Awaiter �̏I����́A�K�����ʂ��ۏ؂���Ă���
            Debug.Log($"�}�E�X��{awaiter.Result}�{�^����������܂���");
            yield return null;
        }
    }

    private IEnumerator WaitForMouseButtonDown(out IAwaiter<int> awaiter)
    {
        var awaiterImpl = new Awaiter<int>();
        var e = WaitForMouseButtonDown(awaiterImpl);
        awaiter = awaiterImpl;
        return e;
    }

    private IEnumerator WaitForMouseButtonDown(Awaiter<int> awaiter)
    {
        // �ǂ̃}�E�X�{�^���������ꂽ�̂��A���ʂ�Ԃ������B
        while (true)
        {
            for (var i = 0; i < 3; i++)
            {
                if (Input.GetMouseButtonDown(i))
                {
                    awaiter.SetResult(i); // �������I���E���ʂ�ݒ�
                    yield break;
                }
            }

            yield return null;
        }
    }
}