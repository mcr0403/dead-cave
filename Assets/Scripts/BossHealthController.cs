using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public static BossHealthController instance;

    private void Awake()
    {
        instance = this;
    }

    public Slider bossHealthSlider;

    public int curretnHealth;

    public BossBattle theBoss;

    // Start is called before the first frame update
    void Start()
    {
        bossHealthSlider.maxValue = curretnHealth;
        bossHealthSlider.value = curretnHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damegeAmount)
    {
        curretnHealth -= damegeAmount;
        if (curretnHealth <= 0)
        {
            curretnHealth = 0;

            theBoss.EndBattle();

            AudioManager.instance.PlaySFX(0);
        } else
        {
            AudioManager.instance.PlaySFX(1);
        }

        bossHealthSlider.value = curretnHealth;

    }

}
