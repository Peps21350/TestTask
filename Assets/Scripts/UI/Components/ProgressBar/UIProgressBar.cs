using UnityEngine;
using UnityEngine.UI;


public class UIProgressBar : MonoBehaviourBase
{
  [SerializeField] private Slider     slider = null;
  [SerializeField] private UITextMesh text   = null;


  public void init( int cur_value, int max_value )
  {
    slider.value = cur_value;
    slider.maxValue = max_value;
    
    text.setText( $"{cur_value}/{max_value}" );
  }
}