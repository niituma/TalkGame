using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public interface IAwaiter<T> // 結果を受け取る側のためのインターフェイス
{
    /// <summary>
    /// 処理が終了したかどうか。
    /// </summary>
    bool IsCompleted { get; }

    /// <summary>
    /// 処理の結果。
    /// </summary>
    T Result { get; }
}

class Awaiter<T> : IAwaiter<T> // 結果を設定する側の実装
{
    public bool IsCompleted { get; private set; }

    public T Result { get; private set; }

    /// <summary>
    /// 処理を終了して結果を設定する。
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
    /// スキップ判定用のトークンを返す。
    /// </summary>
    public SkipRequestToken Token
        => new SkipRequestToken(this);

    /// <summary>
    /// スキップを要求されている場合は true。
    /// </summary>
    public bool IsSkipRequested { get; private set; }

    /// <summary>
    /// スキップを要求する。
    /// </summary>
    public void Skip() { IsSkipRequested = true; }
}

public struct SkipRequestToken
{
    private SkipRequestSource _source;

    public SkipRequestToken(SkipRequestSource source)
        => _source = source;

    /// <summary>
    /// スキップを要求されている場合は true。
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
        "選択肢1",
        "選択肢2",
        "選択肢3",
        };
        Debug.Log("選択肢を表示して入力を待ちます。");
        yield return WaitForSelection(selection, out var awaiter);
        Debug.Log($"選択肢結果は {selection[awaiter.Result]} でした。"); ;
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
        // 選択肢を表示して、押されるの待つ処理
        while (true)
        {
            for (var i = 0; i < _buttons.Length; i++)
            {
                if (_buttons[i] == eventSystem.currentSelectedGameObject)
                {
                    awaiter.SetResult(i); // 処理を終了・結果を設定
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
            Debug.Log("マウスのボタン入力を待ちます");
            yield return WaitForMouseButtonDown(out var awaiter);

            // Awaiter の終了後は、必ず結果が保証されている
            Debug.Log($"マウスの{awaiter.Result}ボタンが押されました");
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
        // どのマウスボタンが押されたのか、結果を返したい。
        while (true)
        {
            for (var i = 0; i < 3; i++)
            {
                if (Input.GetMouseButtonDown(i))
                {
                    awaiter.SetResult(i); // 処理を終了・結果を設定
                    yield break;
                }
            }

            yield return null;
        }
    }
}