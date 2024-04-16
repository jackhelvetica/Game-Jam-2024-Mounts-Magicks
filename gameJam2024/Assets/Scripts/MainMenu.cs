using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private int pageNo = 0;
    public Button playButton;
    public Button tutorialButton;
    public Button creditsButton;
    public Button prevButton;
    public Button nextButton;
    public Button tutCloseButton;
    public Button credCloseButton;
    public GameObject tutorialPanel;
    public GameObject page1;
    public GameObject page2;
    public GameObject creditsPanel;

    public void Play()
    {
        SceneManager.LoadScene("Level");
        FindObjectOfType<AudioManagerScript>().Play("Button2");
        FindObjectOfType<AudioManagerScript>().Stop("Main Menu");
    }

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
        FindObjectOfType<AudioManagerScript>().Play("Button1");
    }

    public void Story()
    {
        SceneManager.LoadScene("Story");
        FindObjectOfType<AudioManagerScript>().Play("Button1");
    }

    public void Quit()
    {
        Debug.Log("Quit game");
        Application.Quit();
        FindObjectOfType<AudioManagerScript>().Play("Button2");
    }

    public void Credits()
    {
        creditsPanel.SetActive(true);
        credCloseButton.Select();
        FindObjectOfType<AudioManagerScript>().Play("Button1");
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        creditsButton.Select();
        FindObjectOfType<AudioManagerScript>().Play("Button1");
    }

    public void Tutorial()
    {
        tutorialPanel.SetActive(true);
        page1.SetActive(true);
        nextButton.Select();
        PageManager();
        FindObjectOfType<AudioManagerScript>().Play("Button1");
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        tutorialButton.Select();
        FindObjectOfType<AudioManagerScript>().Play("Button1");
    }

    public void NextPage()
    {
        pageNo++;
        PageManager();
        FindObjectOfType<AudioManagerScript>().Play("Button1");
    }

    public void PrevPage()
    {
        pageNo--;
        PageManager();
        FindObjectOfType<AudioManagerScript>().Play("Button1");
    }

    public void PageManager()
    {
        if (pageNo == 0)
        {
            prevButton.interactable = false;
            nextButton.interactable = true;
            nextButton.Select();
            page1.SetActive(true);
            page2.SetActive(false);

            //Create a new navigation
            Navigation NewNav = new Navigation();
            NewNav.mode = Navigation.Mode.Explicit;

            //Set what you want to be selected on down, up, left or right;
            NewNav.selectOnUp = nextButton.GetComponent<Button>();
            NewNav.selectOnDown = nextButton.GetComponent<Button>();
            NewNav.selectOnLeft = nextButton.GetComponent<Button>();
            NewNav.selectOnRight = nextButton.GetComponent<Button>();

            //Assign the new navigation to your desired button or ui Object
            tutCloseButton.GetComponent<Button>().navigation = NewNav;
        }
        else if (pageNo == 1)
        {
            prevButton.interactable = true;
            nextButton.interactable = false;
            prevButton.Select();
            page1.SetActive(false);
            page2.SetActive(true);
           
            //Create a new navigation
            Navigation NewNav = new Navigation();
            NewNav.mode = Navigation.Mode.Explicit;

            //Set what you want to be selected on down, up, left or right;
            NewNav.selectOnUp = prevButton.GetComponent<Button>();
            NewNav.selectOnDown = prevButton.GetComponent<Button>();
            NewNav.selectOnLeft = prevButton.GetComponent<Button>();
            NewNav.selectOnRight = prevButton.GetComponent<Button>();

            //Assign the new navigation to your desired button or ui Object
            tutCloseButton.GetComponent<Button>().navigation = NewNav;
        }
    }
}
