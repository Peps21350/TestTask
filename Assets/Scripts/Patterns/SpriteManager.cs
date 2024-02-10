using System;


public class SpriteManager : MonoBehaviourSingleton<SpriteManager>
{
  #region Public Fields
  public readonly CachedAtlas atlas_furniture = new( "Furniture" );
  public          CachedAtlas atlas_location  = new( "Location" );
  #endregion

  public event Action onLocationAtlasLoaded = () => { };

  protected override void Awake()
  {
    base.Awake();

    DontDestroyOnLoad( this );

    StartCoroutine( atlas_location.loadResourcesAsync( _ => onLocationAtlasLoaded?.Invoke() ) );
    atlas_furniture.loadResources();
  }
}