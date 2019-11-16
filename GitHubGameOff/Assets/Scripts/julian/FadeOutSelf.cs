
using System.Collections;
using UnityEngine;

public class FadeOutSelf : MonoBehaviour
{
    public float duration;

    // Use this for initialization
    void Start()
    {
        Fade(duration, new Color(0, 0, 0, 0));
    }

    private IEnumerator Fade(float durationSeconds, Color target)
    {
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        float startTime = Time.time;
        float progress;
        bool fading = true;

        while (fading)
        {
            yield return new WaitForEndOfFrame();
            progress = Time.time - startTime;
            if (meshRenderer != null)
            {
                meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, target, progress / durationSeconds);
            }
            else
            {
                fading = false;
            }


            if (progress >= durationSeconds)
            {
                fading = false;
            }
        }
        Destroy(gameObject);

        yield break;
    }
}
