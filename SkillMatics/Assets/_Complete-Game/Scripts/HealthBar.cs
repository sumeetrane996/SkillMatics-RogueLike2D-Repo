using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Completed
{
    public class HealthBar : MonoBehaviour
    {
        public static HealthBar healthInstance;
        [SerializeField] GameObject player;
        [SerializeField] Slider slider;
        [SerializeField] Gradient gradient;
        [SerializeField] Image fill;

        // Start is called before the first frame update
        void Start()
        {
            healthInstance = this;
            int maxFoodVal=player.GetComponent<Player>().food;
            print(maxFoodVal);
            //int maxVal = player.GetComponent<Player>();
            SetMaxHealth((float)maxFoodVal);
        }

        private void Update()
        {
            int maxFoodVal = player.GetComponent<Player>().food;
            SetHealth((float)maxFoodVal);
        }
        public void SetMaxHealth(float health)
        {
            slider.maxValue = health;
            slider.value = health;
            fill.color = gradient.Evaluate(1f);
        }

        public void SetHealth(float health)
        {
            slider.value = health;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }

}
