using UnityEngine;

public class TutorialSlides : MonoBehaviour
{
    [SerializeField] GameObject[] slides;
    int currentSlide = 0;

    private void Start()
    {
        UpdateSlides();
    }

    public void NextSlide()
    {
        currentSlide++;
        if (currentSlide >= slides.Length) currentSlide = 0;
        UpdateSlides();
    }

    public void PreviousSlide()
    {
        currentSlide--;
        if (currentSlide < 0) currentSlide = slides.Length - 1;
        UpdateSlides();
    }

    void UpdateSlides()
    {
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(i == currentSlide);
        }
    }

    public bool IsLastSlide()
    {
        return currentSlide == slides.Length - 1;
    }

    
}
