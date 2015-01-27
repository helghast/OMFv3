using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Comp_UI_Digit : MonoBehaviour
{
    private Image img;
    public Comp_HUD_Sprites comp_HUD_sprites;

	void Start ()
    {
        comp_HUD_sprites = GameObject.Find("HUD_Sprites").GetComponent<Comp_HUD_Sprites>();
        img = GetComponent<Image>();
	}

    public void setDigit(int new_digit)
    {
        img.sprite = comp_HUD_sprites.numbers[new_digit];
    }
}
