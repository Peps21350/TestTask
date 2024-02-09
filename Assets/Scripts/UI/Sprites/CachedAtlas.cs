using UnityEngine;
using UnityEngine.U2D;
using System;
using System.Linq;
using System.Collections.Generic;
using Object = UnityEngine.Object;


[Serializable]
public class CachedAtlas : CachedObject<SpriteAtlas>
{
  #region Private Fields
  private readonly HashSet<MonoBehaviourBase> reference_count = new HashSet<MonoBehaviourBase>();
  private          Dictionary<string, Sprite> cached_sprites  = null;
  #endregion


  #region Public Methods
  public SpriteAtlas loadResources( MonoBehaviourBase mono_behaviour_base )
  {
    reference_count.Add( mono_behaviour_base );

    return base.loadResources();
  }

  public void dispose( MonoBehaviourBase mono_behaviour_base )
  {
    if ( !reference_count.Contains( mono_behaviour_base ) )
      return;

    reference_count.Remove( mono_behaviour_base );
    if ( reference_count.Count == 0 )
    {
      reference_count.Clear();
      destroy();
    }
  }

  public Sprite getSprite( string sprite_name, string fallback_sprite_name = null )
    => getSprite( cachedObj, sprite_name, fallback_sprite_name );
  #endregion

  #region Protected Methods
  protected override void onRelease( SpriteAtlas asset )
  {
    if ( cached_sprites != null && cached_sprites.Values.Count > 0 )
    {
      Sprite sprite = cached_sprites.Values.First();
      if ( sprite != null )
        Resources.UnloadAsset( sprite.texture );

      foreach ( KeyValuePair<string, Sprite> it in cached_sprites )
        Object.Destroy( it.Value );

      cached_sprites.Clear();
    }

    if ( asset != null && isResourcesAsset )
      Resources.UnloadAsset( asset );
  }
  #endregion

  #region Private Methods
  private Sprite getSprite( SpriteAtlas sprite_atlas, string sprite_name, string fallback_sprite_name )
  {
    Sprite getSprite( string sprite_name )
    {
      Sprite sprite = null;

      if ( cached_sprites == null )
        cached_sprites = new Dictionary<string, Sprite>();
      else
      if ( cached_sprites.TryGetValue( sprite_name, out sprite ) )
        return sprite;

      sprite = sprite_atlas.GetSprite( sprite_name );
      if ( sprite != null )
      {
        sprite.name = sprite_name;
        cached_sprites[sprite_name] = sprite;
      }

      return sprite;
    }

    if ( sprite_atlas == null )
    {
      Debug.LogError( $"SpriteAtlas IsNull. getSprite( {nameof( sprite_name )} {sprite_name}, {nameof( fallback_sprite_name )} {fallback_sprite_name}). return null." );
      return null;
    }

    if ( string.IsNullOrWhiteSpace( sprite_name ) )
    {
      if ( string.IsNullOrEmpty( fallback_sprite_name ) )
        return null;

      Debug.LogWarning( $"{sprite_atlas}. sprite_name IsNull. return {fallback_sprite_name}." );
      sprite_name = fallback_sprite_name;
    }

    Sprite sprite = getSprite( sprite_name );
    if ( sprite == null )
    {
      Debug.LogWarning( $"Sprite {sprite_name} Is not inside the ({sprite_atlas}). return {fallback_sprite_name}." );
      if ( !string.IsNullOrEmpty( fallback_sprite_name ) )
        sprite = getSprite( fallback_sprite_name );
    }

    return sprite;
  }
  #endregion

  #region Public Constructor
  public CachedAtlas( string cached_name )
    : base( cached_name )
  {
  }
  #endregion
}
