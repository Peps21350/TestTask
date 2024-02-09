using UnityEngine;
using UnityEngine.UI;


public class UIProgressBar : MonoBehaviourBase
{
  [SerializeField] private Slider     slider = null;
  [SerializeField] private UITextMesh text   = null;


  public void init( int cur_value, int max_value )
  {
    slider.maxValue = max_value;
    slider.value = cur_value;
    
    text.setText( $"{cur_value}/{max_value}" );
  }

  public void deinit()
  {
    slider.value = 0;
    slider.maxValue = 0;
    
    text.setText( string.Empty );
  }
}