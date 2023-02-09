using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EventSample : MonoBehaviour
{
    [SerializeField]
    private Actor _actor = default;
    [SerializeField]
    private MessagePrinter _messgePrinter = default;
    [SerializeField] string[] talks;
    [SerializeField]
    Image[] _Characters;
    [SerializeField]
    private Image _image;

    private void Start()
    {
        StartCoroutine(RunAsync());
    }

    private IEnumerator RunAsync()
    {
        while (true)
        {
            var cts = new CancellationTokenSource();
            StartCoroutine(CancelIfClicked(cts));
            yield return _actor.Fade(_image, Color.black, 2, 1, cts.Token); // 2秒かけてフェードアウト
            StartCoroutine(_actor.Fade(_Characters[0], Color.white, 1, 1, cts.Token));
            yield return _messgePrinter.ShowMessage(talks[0], cts.Token);

            yield return WaitClick(); // クリックを待つ
            yield return null; // 直前の GetMouseButtonDown が連続しないように1フレーム待つ

            cts = new CancellationTokenSource();
            StartCoroutine(CancelIfClicked(cts));
            yield return _actor.Fade(_image, Color.white, 2, 0, cts.Token); // ２秒かけてフェードイン
            StartCoroutine(_actor.Fade(_Characters[0], Color.gray, 0, 1, cts.Token));
            StartCoroutine(_actor.Fade(_Characters[2], Color.white, 1, 1, cts.Token));
            yield return _messgePrinter.ShowMessage(talks[1], cts.Token);

            yield return WaitClick(); // クリックを待つ
            yield return null;
        }
    }

    private IEnumerator CancelIfClicked(CancellationTokenSource cts)
    {
        while (!IsSkipRequested()) { yield return null; }
        cts.Cancel();
    }

    private IEnumerator WaitClick()
    {
        while (!IsSkipRequested()) { yield return null; }
    }

    private static bool IsSkipRequested()
    {
        return Input.GetMouseButtonDown(0);
    }
}
