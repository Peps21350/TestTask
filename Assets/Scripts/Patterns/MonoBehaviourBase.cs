using UnityEngine;


public abstract class MonoBehaviourBase : MonoBehaviour
{
  protected static PoolManager         poolManager     => MonoBehaviourSingleton<PoolManager>.Instance;
  protected static ViewManager         viewManager     => MonoBehaviourSingleton<ViewManager>.Instance;
  protected static SpriteManager       spriteManager   => MonoBehaviourSingleton<SpriteManager>.Instance;
  protected static ClickableBaseGlobal clickableGlobal => MonoBehaviourSingleton<ClickableBaseGlobal>.Instance;
  
  
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
