using UnityEngine;
using System.Collections;

public class Comp_UI_Timer : MonoBehaviour
{
    public enum COUNTER_DIRECTION
    {
        UP,
        DOWN
    }
    public COUNTER_DIRECTION eCounter_direction;
    public GameObject[] digit_counters;
    
    public float counter_current;

    public int min_value;
    public int max_value;

    private int counter_direction;
    private Comp_UI_Digit[] comp_timers = new Comp_UI_Digit[4];
    private int private_counter_int;
    private int[] dividers = { 1, 10, 60, 600 }; // MAX size for the counter == 4 digits
    private int[] timer;

    void Awake()
    {
        int timer_size = 0;
        for (int i = 0; i < digit_counters.Length; ++i)
        {
            if (digit_counters[i].activeInHierarchy)
                timer_size++;
        }

        timer = new int[timer_size];
        counter_current = max_value;
        counter_direction = -1;
        if (eCounter_direction == COUNTER_DIRECTION.UP)
        {
            counter_current = min_value;
            counter_direction = 1;
        }

        for (int i = 0; i < digit_counters.Length; ++i)
        {
            comp_timers[i] = digit_counters[i].GetComponent<Comp_UI_Digit>();
        }
    }

	void Update ()
    {
        ComputeCounter();
	}

    void ComputeCounter()
    {
        if (counter_current < min_value || counter_current > max_value)
            return;

        counter_current += Time.deltaTime * counter_direction;
        float aux = counter_current;

        for (int i = timer.Length-1; i >= 0; --i)
        {
            timer[i] = (int)(aux / dividers[i]);
            aux = (int)aux % dividers[i];
            comp_timers[i].setDigit(timer[i]);
        }
    }
}
