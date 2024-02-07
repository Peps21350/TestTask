using UnityEngine;
using UnityEngine.Serialization;


public class UIButtonBase : ButtonBase
{
  #region Serialize Fields
  [FormerlySerializedAs( "img_icon" )]
  [Header( nameof( UIButtonBase ) )] 
  [SerializeField] protected SpriteRenderer spr_renderer       = null;
  [SerializeField] protected UITextMesh     txt_title          = null;
  [SerializeField] private   RectTransform  rect_transform_btn = null;

  [SerializeField] private Color color_normal   = new Color( 1.0f, 1.0f, 1.0f, 1.0f );
  [SerializeField] private Color color_pressed  = new Color( 0.6f, 0.6f, 0.6f, 1.0f );
  [SerializeField] private Color color_disabled = new Color( 0.8f, 0.8f, 0.8f, 0.5f );
  #endregion

  
  #region Public Methods
  public void setSpriteIcon( Sprite sprite )
  {
    initComponents();
    
    if ( !spr_renderer )
      return;

    spr_renderer.enabled = sprite != null;
    spr_renderer.sprite = sprite;
  }

  public void setTitle( string title_text )
  {
    initComponents();

    if ( !txt_title )
      return;

    txt_title.enabled = !string.IsNullOrWhiteSpace( title_text );
    txt_title.setText( title_text, true );
  }

  public void setTitle( string title_text, Vector4 margin )
  {
    setTitle( title_text );

    if ( txt_title )
      txt_title.margin = margin;
  }

  public void setTitle( string title_text, Vector3 position )
  {
    setTitle( title_text );

    if ( txt_title )
      txt_title.rectTransform.anchoredPosition = position;
  }
  public void setTitleColor( Color color )
  {
    initComponents();

    if ( !txt_title )
      return;

    txt_title.color = color;
  }
  #endregion

  #region Protected Methods
  protected override void setButtonState( ButtonState button_state = ButtonState.NORMAL )
  {
    crossFadeColor( button_state );
    crossFadeScale( button_state );
  }
  #endregion

  #region Private Methods
  private void crossFadeColor( ButtonState button_state )
  {
    Color color = (isInteractable && isInteractableInHierarchy)
      ? (button_state == ButtonState.PRESSED ? color_pressed : color_normal)
      : color_disabled;
    
// todo
  }

  private void crossFadeScale( ButtonState button_state )
  {
    if ( !rect_transform_btn )
      return;
  }
  #endregion
}
