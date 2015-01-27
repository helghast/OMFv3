using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Comp_UI_Counter : MonoBehaviour
{
    public GameObject[] digit_counters;
    public int counter_current;

    public int min_value;
    public int max_value;
    public int start_value;

    private Comp_UI_Digit[] comp_digit = new Comp_UI_Digit[7];
    private int private_counter_int;
    private int[] dividers = { 1, 10, 100, 1000, 10000, 100000, 1000000 }; // MAX size for the counter == 7 digits
    private int[] counter;

    private Text comp_Text;

    void Awake()
    {
        comp_Text = GameObject.Find("Meters_text").GetComponent<Text>();
        int timer_size = 0;
        for (int i = 0; i < digit_counters.Length; ++i)
        {
            if (digit_counters[i].activeInHierarchy)
                timer_size++;
        }

        counter = new int[timer_size];
        counter_current = start_value;
      

        for (int i = 0; i < digit_counters.Length; ++i)
        {
            comp_digit[i] = digit_counters[i].GetComponent<Comp_UI_Digit>();
        }
    }

    void Update()
    {
        //computeCounter();
        comp_Text.text = counter_current.ToString();
    }

    public void setCounter(int new_value)
    {
        counter_current = new_value;
        computeCounter();
    }

    public void addToCounter(int value)
    {
        counter_current += value;
        computeCounter();
    }

    private void computeCounter()
    {
        if (counter_current < min_value || counter_current > max_value)
            return;

        int aux = counter_current;

        for (int i = counter.Length - 1; i >= 0; --i)
        {
            counter[i] = (int)(aux / dividers[i]);
            aux = (int)aux % dividers[i];
            comp_digit[i].setDigit(counter[i]);
        }
        
        for (int i = counter.Length - 1; i >= 0; --i)
        {
            if (counter[i] == 0)
            {
                digit_counters[i].SetActive(false);
            }
            else
            {
                digit_counters[i].SetActive(true);
                break;
            }
        }
    }
}
