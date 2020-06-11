using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigUI : MonoBehaviour
{
    User user;

    Slider sliderRed;
    Slider sliderGreen;
    Slider sliderBlue;
    private void Awake()
    {
        user = GameObject.Find("User").GetComponent<User>();
        sliderRed = GameObject.Find("SliderRed").GetComponent<Slider>();
        sliderGreen = GameObject.Find("SliderGreen").GetComponent<Slider>();
        sliderBlue = GameObject.Find("SliderBlue").GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Color c = GetComponentInChildren<Renderer>().material.color;

        c = user.PlayerColor;

        sliderRed.value = c.r;
        sliderGreen.value = c.g;
        sliderBlue.value = c.b;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BackButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void SliderChange()
    {
        Color c = new Color(sliderRed.value, sliderGreen.value, sliderBlue.value);

        GetComponentInChildren<Renderer>().material.color = c;

        UserColor(c);
    }

    void UserColor(Color c)
    {
        user.PlayerColor = c;
    }
}
