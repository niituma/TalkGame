using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    public IEnumerator Fade(Image image, Color color, float time, float alfa, CancellationToken ct)
    {
        Debug.Log($"Actor FadeIn: time={time}", this);
        color.a = alfa == 1 ? 0 : 1;
        image.color = new Color(image.color.r, image.color.g, image.color.b, color.a);

        // color のアルファ値を徐々に 1 に近づける処理
        var elapsed = 0F;
        var from = image.color;
        while (!ct.IsCancellationRequested && elapsed < time)
        {
            elapsed += Time.deltaTime;
            color.a = alfa == 1 ? elapsed / time : 1 - elapsed / time;
            image.color = Color.Lerp(from, color, elapsed / time); ;
            yield return null;
        }

        color.a = alfa;
        image.color = color;
        yield return null;
    }

}