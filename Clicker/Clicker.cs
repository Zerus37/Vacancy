using UnityEngine;
using UnityEngine.Events;

public class Clicker : MonoBehaviour
{
    public static UnityEvent<int> OnScoreUpdate = new UnityEvent<int>();

    public static UnityEvent<float> OnPowerUpdate = new UnityEvent<float>();
    public static UnityEvent<float> OnCritChanceUpdate = new UnityEvent<float>();
    public static UnityEvent<float> OnCritMultyUpdate = new UnityEvent<float>();
    public static UnityEvent<float> OnSuperMultyUpdate = new UnityEvent<float>();
    public static UnityEvent<float> OnSuperCooldownUpdate = new UnityEvent<float>();
    public static UnityEvent<float> OnAutoCooldownUpdate = new UnityEvent<float>();

    public static UnityEvent<float> ShowAutoClick = new UnityEvent<float>();
    public static UnityEvent<float> ShowSuperClick = new UnityEvent<float>();

    public static Clicker instance;

    [SerializeField] private float score = 0f;

    //10 * х*х*x формула цены апгрейда где х = любая характеристика
    [SerializeField] private float power = 1f;
    [SerializeField] private float critChance = 1f;
    [SerializeField] private float critMulty = 2f;
    [SerializeField] private float superMulty = 2f;
    [SerializeField] private float superCooldown = 1f;
    [SerializeField] private float autoCooldown = 1f;

    [SerializeField] public Vector3 autoPosition;

    private float superCurrentCooldown = 0f;
    private float autoCurrentCooldown = 1f;

    private bool superClickReady = false;

    public float power_
    {
        get => power;
    }

    public float critChance_
    {
        get => critChance;
    }

    public float critMulty_
    {
        get => critMulty;
    }

    public float superMulty_
    {
        get => superMulty;
    }

    public float superCooldown_
    {
        get => superCooldown;
    }

    public float autoCooldown_
    {
        get => autoCooldown;
    }

    public static float StatToCost(float stat)
	{
        return stat * stat * stat * 5;
    }

    void Start()
    {
        instance = this;

        score = (float)PlayerPrefs.GetInt("score", 0);

        power = PlayerPrefs.GetFloat("power", power);
        critChance = PlayerPrefs.GetFloat("critChance", critChance);
        critMulty = PlayerPrefs.GetFloat("critMulty", critMulty);
        superMulty = PlayerPrefs.GetFloat("superMulty", superMulty);
        superCooldown = PlayerPrefs.GetFloat("superCooldown", superCooldown);
        autoCooldown = PlayerPrefs.GetFloat("autoCooldown", autoCooldown);
	}

    void Update()
    {
		if (autoCurrentCooldown > 0)
		{
            autoCurrentCooldown -= Time.deltaTime;
        }
		else
		{
            autoCurrentCooldown = autoCooldown;
            Click(false, true);
        }

        ShowAutoClick.Invoke(autoCurrentCooldown/autoCooldown);

        if (superCurrentCooldown > 0)
        {
            superCurrentCooldown -= Time.deltaTime;
            ShowSuperClick.Invoke(superCurrentCooldown/superCooldown);
        }
        else
        {
            superClickReady = true;
        }

    }

    public void UpdateAllStats()
	{
        OnPowerUpdate.Invoke(power);
        OnCritChanceUpdate.Invoke(critChance);
        OnCritMultyUpdate.Invoke(critMulty);
        OnSuperMultyUpdate.Invoke(superMulty);
        OnSuperCooldownUpdate.Invoke(superCooldown);
        OnAutoCooldownUpdate.Invoke(autoCooldown);

        Click(false, true);
    }

    public void ManualClick()
	{
        Click(superClickReady, false);
    }

    public void Click(bool isSuper, bool isAuto)
    {
        float addScore = power;

        if (isSuper)
		{
            superClickReady = false;
            superCurrentCooldown = superCooldown;

            addScore *= superMulty;
        }

		if (critChance == 100f)
        {
            addScore *= critMulty;
        }
		else if (Random.Range(0, 100) < critChance)
		{
            addScore *= critMulty;
        }

        score += addScore;

		if (isAuto)
        {
            NumbersUI.Instance.AddText((int)addScore, autoPosition);
        }
		else
        {
            NumbersUI.Instance.AddText((int)addScore, Input.mousePosition);
        }

        RefreshScore();
    }

    // КОД - ГОВНО
    // Идёт далее, более элегантного решения не нашел. Искал 10 минут.
    // 10 * х*х*x формула цены апгрейда. Где х = любая характеристика
    public void UpPower()
	{
        if (PaymentByScore(power))
        {
            power += 1f;
            RefreshScore();
            OnPowerUpdate.Invoke(power);
            PlayerPrefs.SetFloat("power", power);
        }
    }

    public void UpCritChance()
    {
		if (critChance == 100f)
		{
            return;
		}

		if (PaymentByScore(critChance))
        {
            critChance += 1f;
            RefreshScore();
            OnCritChanceUpdate.Invoke(critChance);

            PlayerPrefs.SetFloat("critChance", critChance);
        }
    }

    public void UpCritMulty()
    {
        if (PaymentByScore(critMulty*2))
        {
            critMulty += 0.5f;
            RefreshScore();
            OnCritMultyUpdate.Invoke(critMulty);
            PlayerPrefs.SetFloat("critMulty", critMulty);
        }
    }

    public void UpSuperMulty()
    {
        if (PaymentByScore(superMulty*2))
        {
            superMulty += 0.5f;
            RefreshScore();
            OnSuperMultyUpdate.Invoke(superMulty);
            PlayerPrefs.SetFloat("superMulty", superMulty);
        }
    }

    public void UpSuperCooldown()
    {
        if (superCooldown <= 5f)
        {
            return;
        }

        float cost = 31f - superCooldown;

        if (PaymentByScore(cost))
        {
            superCooldown -= 0.5f;
            RefreshScore();
            OnSuperCooldownUpdate.Invoke(superCooldown);
            PlayerPrefs.SetFloat("superCooldown", superCooldown);
        }
    }

    public void UpAutoCooldown()
    {
		if (autoCooldown <= 1f)
		{
            return;
		}

        float cost = 31f - autoCooldown;

        if (PaymentByScore(cost))
        {
            autoCooldown -= 0.5f;
            RefreshScore();
            OnAutoCooldownUpdate.Invoke(autoCooldown);
            PlayerPrefs.SetFloat("autoCooldown", autoCooldown);
        }
    }

    private bool PaymentByScore(float stat)
    {
        float cost = StatToCost(stat);

        if (cost > score)
        {
            return false;
        }
		else
        {
            score -= cost;
            return true;
        }
	}

    private void RefreshScore()
    {
        int intScore = (int)score;

        PlayerPrefs.SetInt("score", intScore);
        OnScoreUpdate.Invoke(intScore);
    }
}
