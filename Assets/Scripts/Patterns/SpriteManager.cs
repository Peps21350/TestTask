
  public class SpriteManager : MonoBehaviourSingleton<SpriteManager>
  {
    #region Public Fields
    public readonly CachedAtlas atlas_furniture = new CachedAtlas( "Furniture" );
    public          CachedAtlas atlas_location  = new CachedAtlas( "Location" );
    #endregion

    protected override void Awake()
    {
      base.Awake();
      
      atlas_location.loadResources();
      atlas_furniture.loadResources();
    }
  }
  