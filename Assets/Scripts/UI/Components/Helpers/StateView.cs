using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;


namespace UnityEngine.UI
{
  public abstract class StateInstruction
  {
    public int state_index;

    public abstract void apply();
  }

  public abstract class StateInstruction<T> : StateInstruction where T : Object
  {
    [SerializeField] public T target;
  }

  [Serializable]
  public sealed class ActiveInstruction : StateInstruction<GameObject>
  {
    [FormerlySerializedAs( "IsActive" )] 
    [SerializeField] public bool isActive;

    public override void apply()
    {
      if ( target ) 
        target.SetActive( isActive );
    }
  }

  [Serializable]
  public sealed class EnableComponentInstruction : StateInstruction<MonoBehaviour>
  {
    [FormerlySerializedAs( "TargetObject" )] 
    [SerializeField] public GameObject targetObject;
    [FormerlySerializedAs( "IsEnable" )] 
    [SerializeField] public bool isEnable;

    public override void apply()
    {
      if ( target ) 
        target.enabled = isEnable;
    }
  }

  [Serializable]
  public sealed class CanvasGroupVisibilityInstruction : StateInstruction<CanvasGroup>
  {
    [FormerlySerializedAs( "IsVisible" )] 
    [SerializeField] public bool isVisible;

    public override void apply()
    {
      if ( target ) 
        target.alpha = isVisible ? 1.0f : 0.0f;
    }
  }

  [Serializable]
  public sealed class SpriteInstruction : StateInstruction<Image>
  {
    [FormerlySerializedAs( "Sprite" )] 
    [SerializeField] public Sprite sprite;

    public override void apply()
    {
      if ( target ) target.sprite = sprite;
    }
  }
  
  [Serializable]
  public sealed class SpriteMaterialInstruction : StateInstruction<Image>
  {
    [FormerlySerializedAs( "Material" )] 
    [SerializeField] public Material material;

    public override void apply()
    {
      if ( target ) target.material = material;
    }
  }

  [Serializable]
  public sealed class GraphicColorInstruction : StateInstruction<Graphic>
  {
    [FormerlySerializedAs( "Color" )] 
    [SerializeField] public Color color;

    public override void apply()
    {
      if ( target ) target.color = color;
    }
  }

  [Serializable]
  public sealed class RectTransformPositionInstruction : StateInstruction<RectTransform>
  {
    [FormerlySerializedAs( "Position" )] 
    [SerializeField] public Vector2 position;

    public override void apply()
    {
      if ( target ) 
        target.anchoredPosition = position;
    }
  }

  [Serializable]
  public sealed class RectTransformWidthInstruction : StateInstruction<RectTransform>
  {
    [FormerlySerializedAs( "Width" )] 
    [SerializeField] public float width;

    public override void apply()
    {
      if ( target ) 
        target.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, width );
    }
  }

  [Serializable]
  public sealed class RectTransformStretchWidthInstruction : StateInstruction<RectTransform>
  {
    [SerializeField] public float stretchWidth;

    public override void apply()
    {
      if ( target ) 
        target.sizeDelta = new Vector2( stretchWidth, target.sizeDelta.y );
    }
  }

  [Serializable]
  public sealed class RectTransformStretchHeightInstruction : StateInstruction<RectTransform>
  {
    [SerializeField] public float stretchHeight;

    public override void apply()
    {
      if ( target ) target.sizeDelta = new Vector2( target.sizeDelta.x, stretchHeight );
    }
  }

  [Serializable]
  public sealed class RectTransformTopInstruction : StateInstruction<RectTransform>
  {
    [SerializeField] public float rectTop;

    public override void apply()
    {
      if ( target ) 
        target.offsetMax = new Vector2( target.offsetMax.x, -rectTop );
    }
  }

  [Serializable]
  public sealed class RectTransformBottomInstruction : StateInstruction<RectTransform>
  {
    [SerializeField] public float rectBottom;

    public override void apply()
    {
      if ( target ) 
        target.offsetMin = new Vector2( target.offsetMin.x, rectBottom );
    }
  }

  [Serializable]
  public sealed class RectTransformHeightInstruction : StateInstruction<RectTransform>
  {
    [FormerlySerializedAs( "Height" )] 
    [SerializeField] public float height;

    public override void apply()
    {
      if ( target ) target.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, height );
    }
  }

  [Serializable]
  public sealed class RectTransformAnchorsInstruction : StateInstruction<RectTransform>
  {
    [FormerlySerializedAs( "AnchorMin" )] 
    [SerializeField] public Vector2 anchorMin;
    
    [FormerlySerializedAs( "AnchorMax" )] 
    [SerializeField] public Vector2 anchorMax;

    public override void apply()
    {
      if ( target )
      {
        target.anchorMin = anchorMin;
        target.anchorMax = anchorMax;
      }
    }
  }

  [Serializable]
  public sealed class RectTransformPivotInstruction : StateInstruction<RectTransform>
  {
    [FormerlySerializedAs( "Pivot" )] 
    [SerializeField] public Vector2 pivot;

    public override void apply()
    {
      if ( target ) target.pivot = pivot;
    }
  }

  [Serializable]
  public sealed class RectTransformRotationInstruction : StateInstruction<RectTransform>
  {
    [FormerlySerializedAs( "Rotation" )] 
    [SerializeField] public Vector3 rotation;

    public override void apply()
    {
      if ( target ) target.localEulerAngles = rotation;
    }
  }

  [Serializable]
  public sealed class RectTransformScaleInstruction : StateInstruction<RectTransform>
  {
    [FormerlySerializedAs( "Scale" )] 
    [SerializeField] public Vector3 scale;

    public override void apply()
    {
      if ( target ) target.localScale = scale;
    }
  }

  [Serializable]
  public sealed class TransformScaleInstruction : StateInstruction<Transform>
  {
    [FormerlySerializedAs( "Scale" )] 
    [SerializeField] public Vector3 scale;

    public override void apply()
    {
      if ( target ) target.localScale = scale;
    }
  }

  [Serializable]
  public sealed class AspectRatioFitterModeInstruction : StateInstruction<AspectRatioFitter>
  {
    [FormerlySerializedAs( "AspectMode" )] 
    [SerializeField] public AspectRatioFitter.AspectMode aspectMode;

    public override void apply()
    {
      if ( target ) target.aspectMode = aspectMode;
    }
  }

  [Serializable]
  public sealed class AspectRatioFitterRatioInstruction : StateInstruction<AspectRatioFitter>
  {
    [FormerlySerializedAs( "AspectRatio" )] 
    [SerializeField] public float aspectRatio;

    public override void apply()
    {
      if ( target ) target.aspectRatio = aspectRatio;
    }
  }

  public abstract class StateView : MonoBehaviour
  {
    [FormerlySerializedAs( "CurrentStateIndex" )]
    public int currentStateIndex;

    [FormerlySerializedAs( "ActiveInstructions" )]
    public List<ActiveInstruction> activeInstructions;

    [FormerlySerializedAs( "EnableComponentInstructions" )]
    public List<EnableComponentInstruction> enableComponentInstructions;

    [FormerlySerializedAs( "CanvasGroupVisibilityInstructions" )]
    public List<CanvasGroupVisibilityInstruction> canvasGroupVisibilityInstructions;

    [FormerlySerializedAs( "SpriteInstructions" )]
    public List<SpriteInstruction> spriteInstructions; 
    
    [FormerlySerializedAs( "SpriteMaterialInstructions" )]
    public List<SpriteMaterialInstruction> spriteMaterialInstructions;

    [FormerlySerializedAs( "GraphicColorInstructions" )]
    public List<GraphicColorInstruction> graphicColorInstructions;

    [FormerlySerializedAs( "RectTransformAnchorsInstructions" )]
    public List<RectTransformAnchorsInstruction> rectTransformAnchorsInstructions;

    [FormerlySerializedAs( "RectTransformPivotInstructions" )]
    public List<RectTransformPivotInstruction> rectTransformPivotInstructions;

    [FormerlySerializedAs( "RectTransformPositionInstructions" )]
    public List<RectTransformPositionInstruction> rectTransformPositionInstructions;

    [FormerlySerializedAs( "RectTransformWidthInstructions" )]
    public List<RectTransformWidthInstruction> rectTransformWidthInstructions;

    [FormerlySerializedAs( "RectTransformStretchWidthInstructions" )]
    public List<RectTransformStretchWidthInstruction> rectTransformStretchWidthInstructions;

    [FormerlySerializedAs( "RectTransformStretchHeightInstructions" )]
    public List<RectTransformStretchHeightInstruction> rectTransformStretchHeightInstructions;

    [FormerlySerializedAs( "RectTransformTopInstructions" )]
    public List<RectTransformTopInstruction> rectTransformTopInstructions;

    [FormerlySerializedAs( "RectTransformBottomInstructions" )]
    public List<RectTransformBottomInstruction> rectTransformBottomInstructions;

    [FormerlySerializedAs( "RectTransformHeightInstructions" )]
    public List<RectTransformHeightInstruction> rectTransformHeightInstructions;

    [FormerlySerializedAs( "RectTransformRotationInstructions" )]
    public List<RectTransformRotationInstruction> rectTransformRotationInstructions;

    [FormerlySerializedAs( "RectTransformScaleInstructions" )]
    public List<RectTransformScaleInstruction> rectTransformScaleInstructions;

    [FormerlySerializedAs( "TransformScaleInstructions" )]
    public List<TransformScaleInstruction> transformScaleInstructions;

    [FormerlySerializedAs( "AspectRatioFitterModeInstructions" )]
    public List<AspectRatioFitterModeInstruction> aspectRatioFitterModeInstructions;

    [FormerlySerializedAs( "AspectRatioFitterRatioInstructions" )]
    public List<AspectRatioFitterRatioInstruction> aspectRatioFitterRatioInstructions;

    public abstract void setState( object state, int index );
  }

  public abstract class StateView<T> : StateView
  {
    public override void setState( object state, int index )
    {
      currentStateIndex = index;
      setState( index );
    }
    
    public void setState( T state )
    {
      int index = Convert.ToInt32( state );

      applyCommonInstructions( index );
      applyTextInstructions( index );
      applyAspectRatioInstructions( index );
      applyRectTransformInstructions(index );
    }

    public void setState( int index )
    {
      applyCommonInstructions( index );
      applyTextInstructions( index );
      applyAspectRatioInstructions( index );
      applyRectTransformInstructions(index );
    }

    private void applyCommonInstructions(int index)
    {
      applyInstructions( activeInstructions, index );
      applyInstructions( enableComponentInstructions, index );
      applyInstructions( canvasGroupVisibilityInstructions, index );
      applyInstructions( spriteInstructions, index );
      applyInstructions( spriteMaterialInstructions, index );
    }

    private void applyTextInstructions( int index )
    {
      applyInstructions( graphicColorInstructions, index );
    }

    private void applyRectTransformInstructions( int index )
    {
      applyInstructions( rectTransformAnchorsInstructions, index );
      applyInstructions( rectTransformPivotInstructions, index );
      applyInstructions( rectTransformPositionInstructions, index );
      applyInstructions( rectTransformWidthInstructions, index );
      applyInstructions( rectTransformStretchWidthInstructions, index );
      applyInstructions( rectTransformStretchHeightInstructions, index );
      applyInstructions( rectTransformTopInstructions, index );
      applyInstructions( rectTransformBottomInstructions, index );
      applyInstructions( rectTransformHeightInstructions, index );
      applyInstructions( rectTransformRotationInstructions, index );
      applyInstructions( rectTransformScaleInstructions, index );
      applyInstructions( transformScaleInstructions, index );
    }

    private void applyAspectRatioInstructions( int index )
    {
      applyInstructions( aspectRatioFitterModeInstructions, index );
      applyInstructions( aspectRatioFitterRatioInstructions, index );
    }

    private void applyInstructions<T>( List<T> list, int index ) where T : StateInstruction
    {
      foreach ( T instruction in list.Where( instruction => instruction.state_index == index ) )
        instruction.apply();
    }
  }
}