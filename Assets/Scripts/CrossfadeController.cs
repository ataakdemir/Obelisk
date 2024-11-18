using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrossfadeController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    public void StartCrossfade()
    {
        StartCoroutine(FadeEffect());
    }

    private IEnumerator FadeEffect()
    {
        fadeImage.raycastTarget = true;
        Color color = fadeImage.color;

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(0, 1, t);
            fadeImage.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(1f); 

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(1, 0, t);
            fadeImage.color = color;
            yield return null;
        }

        fadeImage.raycastTarget = false;
    }
}
