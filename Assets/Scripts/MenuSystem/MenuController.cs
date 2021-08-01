using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public MenuController Instance { get { return instance; } }
    MenuController instance;

    public MenuPage startPage;
    public MenuPage optionsPage;
    public MenuPage creditsPage;
    public MenuPage pausePage;

    public List<MenuPage> extraPages;

    void Start()
    {
        instance = this;

        OnTitle();
    }

    public virtual void OnStart(string startSceneName)
    {
        SceneController sceneController = FindObjectOfType<SceneController>();
        sceneController?.OnLoadScene(startSceneName);
    }

    public virtual void OnBackToMainMenu(string menuSceneName)
    {
        SceneController sceneController = FindObjectOfType<SceneController>();
        sceneController?.OnLoadScene(menuSceneName);
    }

    public virtual void OnTitle()
    {
        startPage?.gameObject.SetActive(true);
        optionsPage?.gameObject.SetActive(false);
        creditsPage?.gameObject.SetActive(false);
        pausePage?.gameObject.SetActive(false);

        foreach(MenuPage page in extraPages)
        {
            page?.gameObject.SetActive(false);
        }
    }

    public virtual void OnOptions()
    {
        optionsPage?.gameObject.SetActive(true);
        startPage?.gameObject.SetActive(false);
        creditsPage?.gameObject.SetActive(false);
        pausePage?.gameObject.SetActive(false);

        foreach (MenuPage page in extraPages)
        {
            page?.gameObject.SetActive(false);
        }
    }

    public virtual void OnCredits()
    {
        creditsPage?.gameObject.SetActive(true);
        startPage?.gameObject.SetActive(false);
        optionsPage?.gameObject.SetActive(false);
        pausePage?.gameObject.SetActive(false);

        foreach (MenuPage page in extraPages)
        {
            page?.gameObject.SetActive(false);
        }
    }

    public virtual void OnPause()
    {
        pausePage?.gameObject.SetActive(true);
        startPage?.gameObject.SetActive(false);
        optionsPage?.gameObject.SetActive(false);
        creditsPage?.gameObject.SetActive(false);

        foreach (MenuPage page in extraPages)
        {
            page?.gameObject.SetActive(false);
        }
    }

    public virtual void OnActivatePage(string pageName)
    {
        startPage?.gameObject.SetActive(false);
        optionsPage?.gameObject.SetActive(false);
        creditsPage?.gameObject.SetActive(false);
        pausePage?.gameObject.SetActive(false);

        foreach (MenuPage page in extraPages)
        {
            if (page.name == pageName) page?.gameObject.SetActive(true);
            else page?.gameObject.SetActive(false);
        }
    }
}
