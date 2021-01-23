using UnityEngine;
using System.IO;
using UnityEngine.Events;
using Juto;


public class App : Singleton<App>
{
    // (Optional) Prevent non-singleton constructor use.
    protected App() { }



    public int coins
    {
        get
        {
            return stats.coins;
        }
        set
        {
            stats.coins = value;
        }
    }

    private float _score;
    public float Score
    {
        get
        {
            return (int)_score;
        }
        private set
        {
            _score = value;
        }

    }

    public bool isPlaying = false;

    public bool isNewGame = false;

    public Settings settings;
    public UIManager ui;
    public Music music;

    public Transform player;

    public SoundDatabase soundDB;
    public AnimalDatabase animalDB;
    public AchievementDatabase achievementDB;
    public AdManager admanager;

    public PlayerStats stats;


    public int wallScoreIncrease = 50, coinScoreIncrease = 400, timeScoreIncrease = 1500;

    public int coinsEarned = 0, coinsPickedup;

    private void Start()
    {
        music = FindObjectOfType<Music>();
        ui = FindObjectOfType<UIManager>();
        achievementDB = FindObjectOfType<AchievementDatabase>();
        Load();
        music.ChangeTrack(App.Instance.soundDB.MenuTrack());
    }

    private void Update()
    {
        if(isPlaying)
        {
            float pitch = Mathf.Clamp(0.008f*(Score*0.001f) + 0.85f,0.85f,1.3f);
            music.ChangePitch(pitch);
            Score += (Time.deltaTime * timeScoreIncrease * (ToAddMultiplier() + 0.5f)) + (Score * 0.001f);
        }
    }
    
    public void AddScore(int amount)
    {
        float toAdd = amount * ToAddMultiplier();

        Score += (int)amount;
    }

    private float ToAddMultiplier()
    {
        float y = player.position.y;
        float r = 1;

        if(y > 0)
            r = Mathf.Lerp(1, 1.5f, y / 7.5f);
        else
            r = Mathf.Lerp(1, 0.5f, y / -11f);
        return r;


    }

    public void Die()
    {
        if(isPlaying)
        {
            isPlaying = false;
            ui.ShowGameover();
            music.DieEffect();
            admanager.ShowAd();

            //update stats
            stats.totalScore += (int)Score;

            if (Score > stats.highscore)
                stats.highscore = (int)Score;


            if(achievementDB != null)
                achievementDB.CheckForUnlocks(stats);

            coinsEarned = (int)(Score * 0.007489f) + (coinsPickedup*50);
            coinsPickedup = 0;

            coins += coinsEarned;

            GameObject[] g = GameObject.FindGameObjectsWithTag("Obstacle");

            int count = g.Length;

            for (int i = 0; i < count; i++)
            {
                Destroy(g[i], 0);
            }
        }

        App.Instance.Save();
    }


    public void StartGame()
    {
        Score = 0;
        isPlaying = true;
        player.transform.localPosition = new Vector3(0, 0, 3);
        player.GetComponent<PlayerController>().Unshatter();
    }



    public void Save()
    {
        settings.coins = coins;
        Serialization.Save(Application.persistentDataPath + "/settings.json", settings);
    }



    public void Load()
    {
            
        settings = new Settings();

        if (File.Exists(Application.persistentDataPath + "/settings.json"))
        {
            settings = Serialization.Load<Settings>(Application.persistentDataPath + "/settings.json");
            coins = settings.coins;
        }
        else
        {
            isNewGame = true;
        }
    }

    private void OnDestroy()
    {
        Save();
    }

    void OnApplicationQuit()
    {
        Save();
    }
}

