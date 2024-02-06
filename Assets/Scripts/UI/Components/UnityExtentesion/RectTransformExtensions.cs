using UnityEngine;
using System;


public static class RectTransformExtensions
{
  private static Vector3[] local_corners = new Vector3[4];
  public static Vector2 getLocalCorner( this RectTransform transform, Corner corner )
  {
    // (1) - UPPER_LEFT | (2) - UPPER_RIGHT
    // (0) - LOWER_LEFT | (3) - LOWER_RIGHT
    transform.GetLocalCorners( local_corners );
    switch ( corner )
    {
    case Corner.UPPER_LEFT:    return local_corners[1];
    case Corner.UPPER_CENTER:  return new Vector2( 0.0f, local_corners[1].y );
    case Corner.UPPER_RIGHT:   return local_corners[2];
    case Corner.MIDDLE_LEFT:   return new Vector2( local_corners[1].x, 0.0f );
    case Corner.MIDDLE_CENTER: return new Vector2( (local_corners[2].x - local_corners[1].x) / 2.0f, (local_corners[3].y - local_corners[2].y) / 2.0f );
    case Corner.MIDDLE_RIGHT:  return new Vector2( local_corners[3].x, 0.0f );
    case Corner.LOWER_LEFT:    return local_corners[0];
    case Corner.LOWER_CENTER:  return new Vector2( 0.0f, local_corners[3].y );
    case Corner.LOWER_RIGHT:   return local_corners[3];

    default: throw new ArgumentOutOfRangeException( nameof( corner ), corner, "Invalid Corner value" );
    }
  }

  public static float getOffset( this RectTransform rect_transform, RectTransform.Axis axis )
  {
    return rect_transform.offsetMin[(int)axis] - rect_transform.offsetMax[(int)axis];
  }

  public static Vector2 getOffset( this RectTransform rect_transform )
  {
    return rect_transform.offsetMin - rect_transform.offsetMax;
  }

  public static void setAnchor( this RectTransform rect_transform, Corner corner )
  {
    getAnchor( corner, out Vector2 anchor_min, out Vector2 anchor_max );
    rect_transform.anchorMin = anchor_min;
    rect_transform.anchorMax = anchor_max;
  }

  public static void getAnchor( Corner corner, out Vector2 anchor_min, out Vector2 anchor_max )
  {
    switch ( corner )
    {
    case Corner.UPPER_LEFT:
      anchor_min = Vector2.up;
      anchor_max = Vector2.up;
      return;
    case Corner.UPPER_CENTER:
      anchor_min = new Vector2( 0.5f, 1.0f );
      anchor_max = new Vector2( 0.5f, 1.0f );
      return;
    case Corner.UPPER_RIGHT:
      anchor_min = Vector2.one;
      anchor_max = Vector2.one;
      return;
    case Corner.MIDDLE_LEFT:
      anchor_min = new Vector2( 0.0f, 0.5f );
      anchor_max = new Vector2( 0.0f, 0.5f );
      return;
    case Corner.MIDDLE_CENTER:
      anchor_min = new Vector2( 0.5f, 0.5f );
      anchor_max = new Vector2( 0.5f, 0.5f );
      return;
    case Corner.MIDDLE_RIGHT:
      anchor_min = new Vector2( 1.0f, 0.5f );
      anchor_max = new Vector2( 1.0f, 0.5f );
      return;
    case Corner.LOWER_LEFT:
      anchor_min = Vector2.zero;
      anchor_max = Vector2.zero;
      return;
    case Corner.LOWER_CENTER:
      anchor_min = new Vector2( 0.5f, 0.0f );
      anchor_max = new Vector2( 0.5f, 0.0f );
      return;
    case Corner.LOWER_RIGHT:
      anchor_min = Vector2.right;
      anchor_max = Vector2.right;
      return;

    default:
      anchor_min = new Vector2( 0.5f, 0.5f );
      anchor_max = new Vector2( 0.5f, 0.5f );
      return;
    }
  }

  public static void setPivot( this RectTransform rect_transform, Corner corner )
  {
    getAnchor( corner, out Vector2 anchor_min, out Vector2 _ );
    rect_transform.pivot = anchor_min;
  }

  public static Vector3 getPositionOnEllipse( this RectTransform rect_trans, Transform target, float angle )
  {
    Quaternion rot = Quaternion.LookRotation( target.position - rect_trans.position, rect_trans.up );
    Vector3 point = new Vector3(
        Mathf.Sin( Mathf.Deg2Rad * angle ) * rect_trans.rect.width / 2.0f
      , 0.0f
      , Mathf.Cos( Mathf.Deg2Rad * angle ) * rect_trans.rect.height / 2.0f
    );

    return rect_trans.position + (rot * point);
  }

  public static void setLeft( this RectTransform rt, float left )
  {
    rt.offsetMin = new Vector2( left, rt.offsetMin.y );
  }

  public static void setRight( this RectTransform rt, float right )
  {
    rt.offsetMax = new Vector2( -right, rt.offsetMax.y );
  }

  public static void setTop( this RectTransform rt, float top )
  {
    rt.offsetMax = new Vector2( rt.offsetMax.x, -top );
  }

  public static void setBottom( this RectTransform rt, float bottom )
  {
    rt.offsetMin = new Vector2( rt.offsetMin.x, bottom );
  }
}

#region Public Enums
public enum Corner : byte
{
  UPPER_LEFT    = 0,
  UPPER_CENTER  = 1,
  UPPER_RIGHT   = 2,
  MIDDLE_LEFT   = 3,
  MIDDLE_CENTER = 4,
  MIDDLE_RIGHT  = 5,
  LOWER_LEFT    = 6,
  LOWER_CENTER  = 7,
  LOWER_RIGHT   = 8
}
#endregion
