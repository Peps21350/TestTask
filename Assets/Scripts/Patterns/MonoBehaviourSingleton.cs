using UnityEngine;


public class MonoBehaviourSingleton<T> : MonoBehaviour
  where T : MonoBehaviourSingleton<T>
{
  public static bool isRegistered => instance != null;
  
  #region Unity Methods
  protected virtual void Awake()
  {
    tryToRegisterSingleton();
  }
  
  protected virtual void Start()
  {
    onCreateService();
  }
  #endregion

  #region Singleton Logic
  public static T Instance
  {
    get
    {
      if ( instance == null )
      {
        instance = new GameObject().AddComponent<T>();
        instance.gameObject.name = $"{instance.GetType().Name}_Singleton";
      }

      return instance;
    }
  }

  private static T instance = null;

  private void tryToRegisterSingleton()
  {
    if ( instance != null )
    {
      Debug.LogError( $"Trying to register Singleton object {name} but its already exists ({Instance.name}). There should be only one instance of the object on the Scene!" );
      return;
    }

    instance = (T)this;
  }
  
  protected virtual void onCreateService() {}
  #endregion
}