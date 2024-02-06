using UnityEngine;
using UnityEngine.UI;


public static class ScrollRectExtensions
{
  private static int countCornersEnter( this RectTransform rect, RectTransform target )
  {
    Vector3[] rect_corners = new Vector3[4];
    rect.GetWorldCorners( rect_corners );

    Vector3[] target_corners = new Vector3[4];
    target.GetWorldCorners( target_corners );

    Rect view_port_rect = new Rect( rect_corners[0], rect_corners[2] - rect_corners[0] );

    int visible_corners = 0;
    for ( int i = 0; i < target_corners.Length; i++ )
    {
      if ( view_port_rect.Contains( target_corners[i] ) )
        ++visible_corners;
    }

    return visible_corners;
  }

  public static bool isFullyEnter( this ScrollRect scroll_rect, RectTransform target ) =>
    isFullyEnter( scroll_rect.viewport, target );

  public static bool isFullyEnter( this RectTransform rect, RectTransform target ) =>
    countCornersEnter( rect, target ) == 4; // True if all 4 corners are visible

  public static bool isEnter( this ScrollRect scroll_rect, RectTransform target ) =>
    isEnter( scroll_rect.viewport, target ); // True if any corners are visible

  public static bool isEnter( this RectTransform rect, RectTransform target ) =>
    countCornersEnter( rect, target ) > 0; // True if any corners are visible

  /*public static Vector2 getSnapToPositionToBringChildIntoView( this ScrollRectFaster scroll_rect, Vector2 child_position, Vector2 half_size_and_start_spacing, UIScrollSpawner.ScrollToPosition scroll_to_position, bool clamp_value = true )
  {
    child_position *= -1;

    if ( scroll_rect.vertical )
    {
      switch ( scroll_to_position )
      {
      case UIScrollSpawner.ScrollToPosition.UPPER:
        if ( scroll_rect.scrollDirection == 1 )
          child_position.y -= half_size_and_start_spacing.y;
        else
          child_position.y += scroll_rect.viewport.rect.height - half_size_and_start_spacing.y;
        break;
      case UIScrollSpawner.ScrollToPosition.MIDDLE:
        if ( scroll_rect.scrollDirection == 1 )
          child_position.y -= scroll_rect.viewport.rect.height / 2.0f;
        else
          child_position.y += scroll_rect.viewport.rect.height / 2.0f;
        break;
      case UIScrollSpawner.ScrollToPosition.LOWER:
        if ( scroll_rect.scrollDirection == 1 )
          child_position.y -= scroll_rect.viewport.rect.height - half_size_and_start_spacing.y;
        else
          child_position.y += half_size_and_start_spacing.y;
        break;
      }

      if ( scroll_rect.scrollDirection == 1 )
        child_position.y = Mathf.Clamp( child_position.y, 0.0f, scroll_rect.content.sizeDelta.y - scroll_rect.viewport.rect.height );
      else
        child_position.y = Mathf.Clamp( child_position.y, -scroll_rect.content.sizeDelta.y, 0.0f );
    }

    if ( scroll_rect.horizontal )
    {
      switch ( scroll_to_position )
      {
      case UIScrollSpawner.ScrollToPosition.UPPER:
        if ( scroll_rect.scrollDirection == 1 )
          child_position.x -= half_size_and_start_spacing.x;
        else
          child_position.x += scroll_rect.viewport.rect.width - half_size_and_start_spacing.x;
        break;
      case UIScrollSpawner.ScrollToPosition.MIDDLE:
        if ( scroll_rect.scrollDirection == 1 )
          child_position.x -= scroll_rect.viewport.rect.width / 2.0f;
        else
          child_position.x += scroll_rect.viewport.rect.width / 2.0f;
        break;
      case UIScrollSpawner.ScrollToPosition.LOWER:
        if ( scroll_rect.scrollDirection == 1 )
          child_position.x -= scroll_rect.viewport.rect.width - half_size_and_start_spacing.x;
        else
          child_position.x += half_size_and_start_spacing.x;
        break;
      }
    }

    if ( !clamp_value )
      return child_position;

    if ( scroll_rect.scrollDirection == 1 )
      child_position.x = Mathf.Clamp( child_position.x, 0.0f, -scroll_rect.content.sizeDelta.x );
    else
      child_position.x = Mathf.Clamp( child_position.x, scroll_rect.viewport.rect.width - scroll_rect.content.sizeDelta.x, 0.0f );

    return child_position;
  }*/
}
