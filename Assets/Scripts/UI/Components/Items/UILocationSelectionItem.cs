using UnityEngine;
using UnityEngine.UI;


public class UILocationSelectionItem : PoolObject
{
  [SerializeField] private UIProgressBar ui_progress_bar   = null;
  [SerializeField] private Image         ui_location_image = null;
  [SerializeField] private UITextMesh    ui_location_text  = null;


  public void init( int cur_value, int max_value, string background_name )
  {
    ui_progress_bar.init( cur_value, max_value );

    ui_location_image.sprite = spriteManager.atlas_location.getSprite( background_name );
  }
}