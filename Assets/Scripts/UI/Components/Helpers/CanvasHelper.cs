using System.Collections;
using UnityEngine;

public static class CanvasHelper
{
  public static void setVisible( this CanvasGroup canvas_group, bool set_visible )
    => canvas_group.alpha = set_visible ? 1.0f : 0.0f;
  
  public static IEnumerator fadeCanvasAlpha( this CanvasGroup canvas_group, float target_alpha, float fade_duration = 0.5f )
  {
    float currentAlpha = canvas_group.alpha;

    float timeElapsed = 0f;

    while ( timeElapsed < fade_duration )
    {
      float alpha = Mathf.Lerp(currentAlpha, target_alpha, timeElapsed / fade_duration);
      canvas_group.alpha = alpha;

      timeElapsed += Time.deltaTime;

      yield return null;
    }

    canvas_group.alpha = target_alpha;
  }
  
}
