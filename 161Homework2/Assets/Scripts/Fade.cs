using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    public static Fade i;

    public bool fadeInProgress;

    public GameObject fadePanel;
    public Image fadePanelImage;
    Color color = new Color(0,0,0,0);

    public bool fadeOut;
    private float fadeOutSpeed;
    public bool fadeIn;
    private float fadeInSpeed;
    public float defaultSpeed;

	string sceneToLoad;
	bool loadScene;

    // Use this for initialization
    void Awake ()
	{
	    i = this;
	}

    public void FadeOutStart()
    {
        color = new Color(0,0,0,0);
        fadePanel.SetActive(true);
        fadeOut = true;
        fadeIn = false;
        fadeOutSpeed = defaultSpeed;
        fadeInProgress = true;
        StartCoroutine("FadeOut");
    }

	public void FadeOutStart(string scene){
		loadScene = true;
		sceneToLoad = scene;
		FadeOutStart ();
	}

    IEnumerator FadeOut()
    {
        while (color.a <= 0.95f)
        {
			color.a += Time.fixedDeltaTime*fadeOutSpeed;
            fadePanelImage.color = color;
            if (color.a > 0.95f)
            {
				if (loadScene) {
					FadeOutEnd (sceneToLoad);
				} else {
					FadeOutEnd ();
				}
                yield return null;

            }
            yield return null;
        }
    }


    public void FadeOutEnd()
    {
        fadeOut = false;
        fadeInProgress = false;
    }

	public void FadeOutEnd(string scene){
		FadeOutEnd ();
		SceneManager.LoadScene (scene);
	}

    public void FadeInStart()
    {
        fadePanel.SetActive(true);
        color = new Color(0,0,0,1);
        fadeIn = true;
        fadeOut = false;
        fadeInSpeed = defaultSpeed;
        fadeInProgress = true;
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        while (color.a >= 0.05f)
        {
			color.a -= Time.fixedDeltaTime * fadeInSpeed;
            fadePanelImage.color = color;
            if (color.a < 0.05f)
            {
                FadeInEnd();
                yield return null;
            }
            yield return null;
        }
    }

    public void FadeInEnd()
    {
        fadeIn = false;
        fadePanel.SetActive(false);
        fadeInProgress = false;
    }
}
