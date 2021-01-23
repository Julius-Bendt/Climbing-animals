using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Juto.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Juto.Audio;
public class UIManager : MonoBehaviour
{
    private string lastObj, toOpen;
    private string shopOpen, clawOpen;
    UIAnimation anim;
    UIAnimationFramework animFramework;
    public AnimationObjHolder[] animations;

    public Transform wallLeft1, wallLeft2, wallRight1, wallRight2;
    private SpriteRenderer[] walls;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI gameoverScoreText, gameoverCoin;

    public TextMeshProUGUI shopCoin;

    public ButtonScreenFill[] buttonFillers;

    public CustomToggle musicToggle, soundToggle, notificationToggle;

    private OfferManager offers;

    private int lastShopCoin;

    [System.Serializable]
    public class ButtonScreenFill
    {
        public string name;
        public RectTransform fill;
        public Image image;
        public Color on, off;
        public GameObject icon;
        public Vector3 size;

        public void Reset()
        {
            image.color = on;
            fill.localScale = Vector3.one;
            icon.SetActive(true);
        }
    }

    [System.Serializable]
    public struct AnimationObjHolder
    {
        public string name;
        public GameObject obj;
        public Button[] buttons;
    }

    public void Start()
    {
        anim = GetComponent<UIAnimation>();
        animFramework = GetComponent<UIAnimationFramework>();
        anim.Open("menu",0);
        offers = GetComponent<OfferManager>();

        offers.Init();


        List<SpriteRenderer> walls_renders = new List<SpriteRenderer>();

        walls_renders.Add(wallLeft1.GetComponent<SpriteRenderer>());
        walls_renders.Add(wallLeft2.GetComponent<SpriteRenderer>());
        walls_renders.Add(wallRight1.GetComponent<SpriteRenderer>());
        walls_renders.Add(wallRight2.GetComponent<SpriteRenderer>());
        walls = walls_renders.ToArray();

    }

    private void Update()
    {
        if(App.Instance.isPlaying)
        {
            scoreText.text = "Score\n<size=80%>" + ((App.Instance.Score) + (App.Instance.coinsPickedup * App.Instance.coinScoreIncrease));
        }
    }

    public ButtonScreenFill FindFiller(string name)
    {
        foreach (ButtonScreenFill item in buttonFillers)
        {
            if (item.name == name)
                return item;
        }

        return null;
    }

    public void ShowGameover()
    {
        ButtonScreenFill f = FindFiller("gameover");
        f.icon.SetActive(true);

        ButtonInteractive("gameover", true);
        _active("gameover", true);
        FindFiller("gameover_shop").Reset();
        FindFiller("gameover_claw").Reset();
        gameoverScoreText.text = "Score\n<size=60%>0";
        anim.Close("game",0);
        anim.Open("gameover", 3);

        //Move the walls
        StartCoroutine(MoveWalls(false));
    }

    #region shop
    public void ShowShop(string scene)
    {
        AudioController.PlaySound(App.Instance.soundDB.Click());
        anim.Close(scene, 0);
        ButtonInteractive(scene, false);
        shopOpen = scene+"_shop";

        ButtonScreenFill f = FindFiller(shopOpen);
        animFramework.Scale(f.fill, f.size, 0.5f, 0.05f, ShopFillOver);
        animFramework.Fade(f.image, f.on, 0.5f, 0.05f);
        f.icon.SetActive(false);

    }

    public void ShowSettings()
    {
        AudioController.PlaySound(App.Instance.soundDB.Click());
        _active("settings", true);
        anim.Close("menu", 4);
        ButtonInteractive("menu", false);
    }

    public void CloseSettings()
    {
        AudioController.PlaySound(App.Instance.soundDB.Click());
        anim.Close("settings", 5);
        ButtonInteractive("settings", false);
    }

    private void ShopFillOver()
    {
        ButtonScreenFill f = FindFiller(shopOpen);
        animFramework.Scale(f.fill, new Vector3(0, 0, 0), 0.5f, 0.05f, actuallyOpenShop);
        animFramework.Fade(f.image, f.off, 0.5f, 0.05f);
    }

    private void actuallyOpenShop()
    {
        ButtonScreenFill f = FindFiller(shopOpen);
        f.Reset();
        _active("shop", true);
        anim.Open("shop", 8);
    }

    public void BackToMenu(string open)
    {
        AudioController.PlaySound(App.Instance.soundDB.Click());
        anim.Close(open, 1);
    }

    #endregion

    #region claw

    public void ShowClaw(string scene)
    {
        anim.Close(scene, 0);
        ButtonInteractive(scene, false);
        shopOpen = scene + "_claw";

        ButtonScreenFill f = FindFiller(shopOpen);
        animFramework.Scale(f.fill, f.size, 0.5f, 0.05f, ClawFillOver);
        animFramework.Fade(f.image, f.on, 0.5f, 0.05f);
        f.icon.SetActive(false);

    }

    private void ClawFillOver()
    {
        ButtonScreenFill f = FindFiller(shopOpen);
        animFramework.Scale(f.fill, new Vector3(0, 0, 0), 0.5f, 0.05f, actuallyOpenClaw);
        animFramework.Fade(f.image, f.off, 0.5f, 0.05f);
    }

    private void actuallyOpenClaw()
    {
        ButtonScreenFill f = FindFiller(shopOpen);
        f.icon.SetActive(true);
        _active("claw", true);
        anim.Open("claw", 0);
    }

    #endregion

    IEnumerator MoveWalls(bool open)
    {
        float time = 0.5f;
        float timeElapsed = 0;

        Vector3 moveToL1 = new Vector3(-5, wallLeft1.localPosition.y, 0);
        Vector3 moveToL2 = new Vector3(-5, wallLeft2.localPosition.y, 0);
        Vector3 moveToR1 = new Vector3(5, wallRight1.localPosition.y, 0);
        Vector3 moveToR2 = new Vector3(5, wallRight2.localPosition.y, 0);

        Vector3 wr1 = wallRight1.localPosition;
        Vector3 wr2 = wallRight2.localPosition;
        Vector3 wl1 = wallLeft1.localPosition;
        Vector3 wl2 = wallLeft2.localPosition;
        Color to = (open) ? new Color(0.6415094f, 0.6415094f, 0.6415094f) : Color.clear;
        Color from = (open) ? Color.clear : new Color(0.6415094f, 0.6415094f, 0.6415094f);

        if (!open)
        {
            moveToL1 = new Vector3(-6.5f, wallLeft1.localPosition.y, 0);
            moveToL2 = new Vector3(-6.5f, wallLeft2.localPosition.y, 0);
            moveToR1 = new Vector3(6.5f, wallRight1.localPosition.y, 0);
            moveToR2 = new Vector3(6.5f, wallRight2.localPosition.y, 0);
        }
            

        while(time > timeElapsed)
        {
            timeElapsed += Time.deltaTime;

            wallLeft1.localPosition = Vector3.Lerp(wl1, moveToL1, timeElapsed / time);
            wallLeft2.localPosition = Vector3.Lerp(wl2, moveToL2, timeElapsed / time);
            wallRight1.localPosition = Vector3.Lerp(wr1, moveToR1, timeElapsed / time);
            wallRight2.localPosition = Vector3.Lerp(wr2, moveToR2, timeElapsed / time);

            foreach (SpriteRenderer sprite in walls)
            {
                sprite.color = Color.Lerp(from, to, timeElapsed / time);
            }

            yield return null;
        }

        yield return null;
    }

    #region menu start game
    public void StartGame()
    {
        AudioController.PlaySound(App.Instance.soundDB.Click());

        //close menu
        anim.Close("menu", 0);
        ButtonInteractive("menu", false);


        ButtonScreenFill f = FindFiller("menu_start");
        animFramework.Scale(f.fill, f.size, 0.5f, 0.05f, MenuFillInOver);
        animFramework.Fade(f.image, f.on, 0.5f, 0.05f);
        f.icon.SetActive(false);
    }

    private void MenuFillInOver()
    {
        ButtonScreenFill f = FindFiller("menu_start");
        animFramework.Scale(f.fill, new Vector3(0,0,0), 0.5f, 0.05f, ActuallyStartGame);
        animFramework.Fade(f.image, f.off, 0.5f, 0.05f);
    }
    private void ReplayFillInOver()
    {
        ButtonScreenFill f = FindFiller("gameover");
        animFramework.Scale(f.fill, new Vector3(0, 0, 0), 0.5f, 0.05f, ActuallyStartGame);
        animFramework.Fade(f.image, Color.clear, 0.5f, 0.05f);
    }

    public void Replay()
    {
        AudioController.PlaySound(App.Instance.soundDB.Click());
        //close menu
        anim.Close("gameover", 0);
        ButtonInteractive("gameover", false);
        ButtonScreenFill f = FindFiller("gameover");
        animFramework.Scale(f.fill, new Vector3(6, 6, 6), 0.5f, 0.05f, ReplayFillInOver);
        animFramework.Fade(f.image, new Color(0.8901961f, 0.8431373f, 0.1098039f, 1), 0.5f, 0.05f);
        f.icon.SetActive(false);
    }

    private void ActuallyStartGame()
    {
        ButtonScreenFill f = FindFiller("menu_start");
        f.icon.SetActive(true);
        _active("game", true);
        anim.Open("game", 0);
        App.Instance.music.ChangeTrack(App.Instance.soundDB.GameTrack());
        StartCoroutine(MoveWalls(true));
        App.Instance.StartGame();
    }
    #endregion

    public void ButtonInteractive(string name, bool active)
    {
        foreach (AnimationObjHolder a in animations)
        {
            if (a.name == name)
            {
                foreach (Button button in a.buttons)
                {
                    button.interactable = active;
                };
            }

        }
    }

    
    public void _active(string name, bool active)
    {
        foreach (AnimationObjHolder a in animations)
        {
            if (a.name == name)
            {
                a.obj.SetActive(active);
            }
               
        }
    }

    public void AnimationOver(UIAnimation.UIAnimationEvent _event)
    {

        switch(_event.identifier)
        {
            case 1:
                _active("menu", true);
                ButtonInteractive("menu", true);
                FindFiller("menu_shop").Reset();
                FindFiller("menu_start").Reset();
                FindFiller("menu_claw").Reset();
                anim.Open("menu",0);
                break;
            case 3:
                animFramework.TextValue(gameoverScoreText, "Score\n<size=60%>{0}", 0, (int)(App.Instance.Score), 1f, 0f);
                animFramework.TextValue(gameoverCoin, "<sprite=6> {0}",0,App.Instance.coinsEarned,1);
                break;
            case 4:
                anim.Open("settings_cg",7);
                ButtonInteractive("settings", true);
                break;
            case 7:
                anim.Open("settings", 0);
                musicToggle.SetValue(App.Instance.settings.musicEnabled);
                soundToggle.SetValue(App.Instance.settings.sfxEnabled);
                notificationToggle.SetValue(App.Instance.settings.notification);
                break;
            case 5:
                anim.Close("settings_cg",6);
                break;
            case 6:
                _active("menu", true);
                _active("settings", false);
                ButtonInteractive("menu", true);
                anim.Open("menu", 0);
                break;
            case 8:
                //coin anim
                UpdateShopCoin();
                break;
            case 9:
                anim.Open("claw", 0);
                break;
        }
    }

    public void UpdateShopCoin()
    {
        animFramework.TextValue(shopCoin, "<sprite=6> {0}", lastShopCoin, App.Instance.coins, 1);
        lastShopCoin = App.Instance.coins;
    }
}
