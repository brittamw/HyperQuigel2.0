using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public int startingHealt = 50;
    public int currentHealth;

	public Text healthPoints;


    // public Slider healtSlider; // falls wir einen EP-Balken haben wollen
    public AudioClip deathClip;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    bool isDead;
    bool damaged;
    
    // Use this for initialization
	void Awake () {
        currentHealth = startingHealt;
	}
	
	// Update is called once per frame
	void Update () {
       // Bei Schaden Bild rot aufleuchten lassen
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
		healthPoints.text = currentHealth.ToString();
	
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        //playerAudio.clip = deathClip;
        //playerAudio.Play();


    }
}
