using UnityEngine;

public class LocalSave : MonoBehaviour
{
  #region Private Fields
  private const string KEY_PLAYER_POINTS     = "player_points";
  private const string KEY_POINTS_MULTIPLIER = "player_multiplier";
  #endregion

  
  #region Public Methods
  public static float playerPoints
  {
    get => getFloat( KEY_PLAYER_POINTS, 0 );
    set => setFloat( KEY_PLAYER_POINTS, value );
  }
  
  public static float pointsMultiplier
  {
    get => getFloat( KEY_POINTS_MULTIPLIER, 0.001f );
    set => setFloat( KEY_POINTS_MULTIPLIER, value );
  }
  #endregion

  #region Private Methods
  private static void setInt( string key, int value )
    => PlayerPrefs.SetInt( key, value );

  private static int getInt( string key, int value )
    => PlayerPrefs.GetInt( key, value );
  
  private static void setFloat( string key, float value )
    => PlayerPrefs.SetFloat( key, value );

  private static float getFloat( string key, float value )
    => PlayerPrefs.GetFloat( key, value );
  #endregion
}
