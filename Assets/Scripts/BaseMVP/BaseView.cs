using System.Collections;
using UnityEngine;


public class BaseView<M, P, V> : PoolObject
  where M : BaseModel, new()
  where P : BasePresenter<M, V>, new()
  where V : PoolObject
{
  [Header(nameof(BaseView<M, P, V>))]
  [SerializeField] private UITextMesh   text_title           = null;
  [SerializeField] private UIButtonBase ui_btn_close         = null;
  [SerializeField] private UITextMesh   text_tap_to_continue = null;
  [SerializeField] private V            cur_object           = null;
  [Space]
  
  private   M model;
  protected P presenter;
  
  
  public virtual IEnumerator init()
  {
    presenter = new P();
    
    subscribe();
    
    model = new M();
    
    presenter.setup( model, cur_object );
    
    presenter.init();
    model.init();

    yield break;
  }

  protected virtual void subscribe() { }
  
  #region initMyComponents Methods
  protected override void initMyComponents()
  {
    base.initMyComponents();

    if ( ui_btn_close != null )
    {
      ui_btn_close.onClick += _ => onClickBtnClose();
      ui_btn_close.setTitle( GameText.btn_back );
    }
  }
  #endregion

  #region Protected Methods
  protected virtual void onClickBtnClose()
  {
    closeView();
  }

  protected virtual void closeView()
  { 
    despawn();
  }

  protected void setTitle( string title, bool force_auto_size = false )
  {
    if ( text_title )
      text_title.setText( title, force_auto_size );
  }

  protected void setContinueTextEnabled( bool is_enabled )
  {
    if ( text_tap_to_continue )
      text_tap_to_continue.enabled = is_enabled;
  }
  #endregion
}