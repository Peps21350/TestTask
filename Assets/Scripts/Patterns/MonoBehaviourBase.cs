using UnityEngine;

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
#endif


public abstract class MonoBehaviourBase : MonoBehaviour
{
  protected static PoolManager         poolManager     => MonoBehaviourSingleton<PoolManager>.Instance;
  protected static ViewManager         viewManager     => MonoBehaviourSingleton<ViewManager>.Instance;
  protected static ClickableBaseGlobal clickableGlobal => MonoBehaviourSingleton<ClickableBaseGlobal>.Instance;
  
  public RectTransform rect_transform => transform as RectTransform;
  
  private bool was_inited_components = false;
  
  protected void initComponents()
  {
    if ( was_inited_components )
      return;

    initMyComponents();

    was_inited_components = true;
  }
  
  protected virtual void initMyComponents() {}
  
  protected virtual void OnDestroy() {}
}
