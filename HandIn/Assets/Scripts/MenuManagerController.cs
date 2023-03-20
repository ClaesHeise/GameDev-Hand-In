using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerController : MonoBehaviour
{
  private static GameManagerController GM;

  private List<Menu> menus;
  private Menu currentMenu;
  private Stack<string> perviousMenus;


  public static MenuManagerController Instance { get; private set; }

  // Start is called before the first frame update
  void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(this);
    }
    else
    {
      Instance = this;
      Instance.perviousMenus = new Stack<string>();
      DontDestroyOnLoad(Instance);
    }
  }

  void Start()
  {
    if (GM == null) GM = GameManagerController.Instance;
  }

  // Update is called once per frame
  void Update()
  {
    // Check if the game is paused
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (GM.gameState == GameState.PAUSED)
      {
        GM.SetGameState(GameState.GAME);
        Time.timeScale = 1;
      }
      else if (GM.gameState == GameState.GAME)
      {
        GM.SetGameState(GameState.PAUSED);
        Time.timeScale = 0;
        GoToMenu("PauseMenu");
      }
    }
  }


  public void AddMenu(Menu menu)
  {
    if (menus == null)
    {
      menus = new List<Menu>();
    }
    if (!menu.isMainMenu)
    {
      menu.Disable();
    }
    else
    {
      currentMenu = menu;
      menu.Enable();
    }
    Debug.Log($"Menu {menu.menuName} added");
    menus.Add(menu);
  }

  private bool TryGetMenu(string menuName, out Menu menu)
  {
    menu = menus.Find(m => m.menuName.ToLower().Equals(menuName.ToLower()));
    return menu != null;
  }

  public void GoToMenu(string menuName)
  {
    var perviousMenu = currentMenu;
    if (!TryGetMenu(menuName, out currentMenu))
    {
      Debug.LogError($"Menu {menuName} not found");
      return;
    }

    if (currentMenu.isMainMenu)
    {
      perviousMenus.Clear();
    }
    else if (perviousMenu != null)
    {
      perviousMenus.Push(perviousMenu.menuName);
    }
    perviousMenu.Disable();
    currentMenu.Enable();
  }

  public void GoBack()
  {
    if (perviousMenus.TryPop(out var newMenu))
    {
      if (!TryGetMenu(newMenu, out var nextMenu))
      {
        Debug.LogError($"Menu {newMenu} not found");
        return;
      };
      currentMenu.Disable();
      currentMenu = nextMenu;
      currentMenu.Enable();
    };
  }

  public void DisableAllMenus()
  {
    foreach (var menu in menus)
    {
      menu.Disable();
    }
  }
}
