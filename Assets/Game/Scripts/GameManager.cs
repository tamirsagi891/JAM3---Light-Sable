using System;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private DoorBehaviour door;
    [SerializeField] private GameObject keys;
    [SerializeField] private UIManager UI;
    [SerializeField] private GameObject FadeOut;
    [Range(0,99)][SerializeField] private int baseAmmo;
    [SerializeField] private Light2D globalLight;
    #endregion

    #region Fields
    
    private static GameManager _shared;
    private GameState _state;
    private KeysState _keysState;
    private bool _endScene = false;
    private float _endSceneDelay = 2;

    #endregion

    #region Animation Tags

   #endregion

   #region MonoBehaviour

   private void Awake()
   {
       if (_shared == null)
       {
           _shared = this;
           Init();
       }
       else Destroy(this);
        AudioManager.PlayStartSceneMusic();
       SetDefaultVal();
   }

   private void Start()
   {
       AudioManager.PlayGameLoopMusic();
   }


   private void Update()
   {
       switch (_state)
       {
           case GameState.START:
               break;
           case GameState.MID:
               break;
            case GameState.END:
                break;
           default:
               break;
       }

       if (BossState)
           globalLight.intensity = 0.1f;
       else
           globalLight.intensity = 0f;

       if (Input.GetKeyDown(KeyCode.Escape))
           SceneManager.LoadScene("End Scene");
       
       if (_endScene)
           EndScene();

   }

   #endregion

   #region Properties
   
   
   public static GameState State
   {
       get => _shared._state;
       set => _shared._state = value;
   }
   
   public static KeysState KeysState
   {
       get => _shared._keysState;
   }
   
   public int Ammo { get; set; }
   public int DeathCounter { get; set; }
   public bool BossState { get; set; }
   
   #endregion
   

   #region Methods

   private void Init()
   {
       _keysState = KeysState.ZERO;
       foreach (Transform key in keys.gameObject.transform)
       {
           key.gameObject.SetActive(true);
       }
   }
   
   private void Lose()
   {
       _state = GameState.END;
   }

   private void SetDefaultVal()
   {
       Ammo = baseAmmo;
       DeathCounter = 0;
       BossState = false;
   }

   public static void UnlockOneKey()
   {
       _shared._keysState += 1;
       _shared.door.UnlockOneKey();
       _shared.UI.AddKey();
   }

   public void EndScene()
   {
       if (_endSceneDelay == 2)
       {
           _endScene = true;
           FadeOut.SetActive(true);
           
       }
       if (_endSceneDelay > 0)
           _endSceneDelay -= Time.deltaTime;
       else
           SceneManager.LoadScene("End Scene");
       
   }
   
   #endregion

   
}
