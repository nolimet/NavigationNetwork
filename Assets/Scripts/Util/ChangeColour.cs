using UnityEngine;
using System.Collections;

public class ChangeColour : MonoBehaviour {

	public float duration = 10.0f;
	
	public float maxAlpha = 0.2f;
	
	private bool updown = false;
	
	public KeyCode KeyBind;

    // Update is called once per frame

    void Update () {
		
		float substract=1/duration;

        Color textureColor = renderer.material.color;
		
		if(textureColor.a > maxAlpha && updown==false)
		{
        	textureColor.a -= substract*Time.deltaTime;
		}
		
		if(textureColor.a < 1&& updown)
		{
        	textureColor.a += substract*Time.deltaTime;
		}
		if(Input.GetKeyDown(KeyBind))
		{
				updown=!updown;
		}
        renderer.material.color = textureColor;

    }
	public void ChangeOpacity()
	{
		updown=!updown;
	}
	
	public void changeColour(int InRed, int InGreen, int InBlue, int InAlpha=255)
	{
	 Color textureColor = renderer.material.color;
		
		Debug.Log (InRed);
		Debug.Log (InGreen);
		Debug.Log (InBlue);
		
		float blue=0.003921568627451f*InBlue;
		float red=0.003921568627451f*InRed;
		float green=0.003921568627451f*InGreen;
		float alpha=0.003921568627451f*InAlpha;
		
		Debug.Log (red);
		Debug.Log (green);
		Debug.Log (blue);
		
		Debug.Log (1/255);
		
		textureColor.b = blue;
		textureColor.g = green;
		textureColor.r = red;
		textureColor.a = alpha;
		
        renderer.material.color = textureColor;
	}
}
