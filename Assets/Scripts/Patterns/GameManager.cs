using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviourSingleton<GameManager>
{
  protected override void Awake()
  {
    base.Awake();

    DontDestroyOnLoad( this );
  }

  protected override void onCreateService()
  {
    spriteManager.onLocationAtlasLoaded += changeScene;
  }

  private void changeScene()
  {
    StartCoroutine( change() );
    IEnumerator change()
    {
      yield return new WaitForSeconds( 2.0f );
      yield return SceneManager.LoadSceneAsync( 1 );
      init();
    }
  }

  private void init()
  {
    viewManager.addLocationView();
  }
}