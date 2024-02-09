using UnityEngine;

public class LocalSave : MonoBehaviour
{
  #region Private Fields
  private const string KEY_GAME_STATE = "game_state";
  #endregion

  
  #region Public Methods
  public static string curGameState
  {
    get => getString( KEY_GAME_STATE, string.Empty );
    set => setString( KEY_GAME_STATE, value );
  }
  #endregion

  #region Private Methods
  private static void setString( string key, string value )
    => PlayerPrefs.SetString( key, value );

  private static string getString( string key, string value )
    => PlayerPrefs.GetString( key, value );
  #endregion
}
