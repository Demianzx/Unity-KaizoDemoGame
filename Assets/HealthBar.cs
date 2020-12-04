using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour {

    public Image health;
    float hp, maxhp = 100f;

	// Use this for initialization
	void Start () {
        hp = maxhp;
	}
	
	// Update is called once per frame
	public void TakeDamage(float amount)
    {
        hp = Mathf.Clamp(hp - amount, 0f, maxhp);
        health.transform.localScale = new Vector2(hp / maxhp, 1);
    }
    private void FixedUpdate()
    {
        if (hp <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
