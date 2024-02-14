using UnityEngine;
using UnityEngine.UI;


[RequireComponent( typeof( RawImage ) )]
public class ImageScroller : MonoBehaviourBase
{
  [SerializeField, Range( 0, 10 )] private float scrollSpeed = 0.1f;
  [SerializeField, Range( -1, 1 )] private float xDirection  = 1;
  [SerializeField, Range( -1, 1 )] private float yDirection  = 1;
  
  [SerializeField] private RawImage image = null;


  private void Update()
  {
    image.uvRect = new Rect( image.uvRect.position + new Vector2( -xDirection * scrollSpeed, yDirection * scrollSpeed ) * Time.deltaTime, image.uvRect.size );
  }
}