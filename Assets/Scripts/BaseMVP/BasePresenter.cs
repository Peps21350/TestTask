public abstract class BasePresenter<M, V>
  where M : BaseModel, new()
  where V : PoolObject
{
  protected M model;
  protected V view;

  public virtual void init()
  {
    subscribe();
  }

  public virtual void setup( M model, V view )
  {
    this.model = model;
    this.view  = view;
  }
  
  protected virtual void subscribe() { }
}