using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]

public class TutorialMode : MonoBehaviour
{
    public GameObject popupBox;
    public Animator animator;
    public TMP_Text popupText;
    public GameObject BaseBee;
    public GameObject ThrowPowerUp;
    public GameObject KickPowerUp;
    private float startTime;
    private float tutorialStep;
    private Rigidbody rbody;
    private Vector3 old_position;
    private Animator player;
    private List<GameObject> powerUp;

    public void Popup(string text)
    {
        popupBox.SetActive(true);
        popupText.text = text;
        animator.SetTrigger("pop");
    }

    public void ClosePop()
    {
        popupBox.SetActive(false);
        animator.SetTrigger("close");
    }
    private float DeltaTime()
    {
        return Time.time - startTime;
    }

    private void nextStep()
    {
        tutorialStep += 1;
        startTime = Time.time;
    }

    private void Start()
    {
        tutorialStep = 0;
        startTime = Time.time;
        rbody = GetComponent<Rigidbody>();
        player = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name != "Tutorial") GetComponent<TutorialMode>().enabled = false;
        powerUp = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pickup"));
    }
    private void Update()
    {
        if (DeltaTime() > 2)
        {
            switch (tutorialStep)
            {
                case 0:
                    Popup("Hello SpaceBee!");
                    nextStep();
                    break;
                case 1:
                    nextStep();
                    break;
                case 2:
                    Popup("Test your legs. WASD or Thumbstick");
                    old_position = transform.position;
                    nextStep();
                    break;
                case 3:
                    if ((old_position - transform.position).magnitude > 5)
                    {
                        nextStep();
                        Popup("Great!");
                    }
                    break;
                case 4:
                    ClosePop();
                    nextStep();
                    break;
                case 5:
                    Popup("Test gravity. Space or Gamepad South. Double jumps are possible.");
                    nextStep();
                    break;
                case 6:
                    if (rbody.velocity.magnitude > 1)
                    {
                        nextStep();
                        Popup("Success!");
                    }
                    break;
                case 7:
                    ClosePop();
                    nextStep();
                    break;
                case 8:
                    Popup("Test your agility by dodging. Left Shift or Gamepad East.");
                    nextStep();
                    break;
                case 9:
                    if (player.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
                    {
                        Popup("Quick Dashing!");
                        nextStep();
                    }
                    break;
                case 10:
                    ClosePop();
                    nextStep();
                    break;
                case 11:
                    Popup("Check your fighting skills. Left Mouse Click or Gamepad West.");
                    nextStep();
                    break;
                case 12:
                    if (player.GetCurrentAnimatorStateInfo(0).IsName("Punches"))
                    {
                        Popup("Strong Hook!");
                        nextStep();
                    }
                    break;
                case 13:
                    ClosePop();
                    nextStep();
                    break;
                case 14:
                    Popup("Choose powerups for special abilities. Walk over and pickup a kick powerup.");
                    Instantiate(KickPowerUp, new Vector3(-65.0f, 13.4f, -28.5f), Quaternion.identity);
                    nextStep();
                    break;
                case 15:
                    powerUp = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pickup"));
                    if (powerUp.Count <= 5)
                    {
                        nextStep();
                    }
                    break;
                case 16:
                    Popup("Check your new kick ability. Right Mouse Click or Gamepad North.");
                    nextStep();
                    break;
                case 17:
                    if (player.GetCurrentAnimatorStateInfo(0).IsName("Kicks"))
                    {
                        Popup("Nice Highkick!");
                        nextStep();
                    }
                    break;
                case 18:
                    ClosePop();
                    nextStep();
                    break;
                case 19:
                    Popup("Walk over and pickup a throw powerup. Only one powerup can be active in a real game, so choose carefully.");
                    Instantiate(ThrowPowerUp, new Vector3(-111.3f, 13.4002861f, -28.4000008f), Quaternion.identity);
                    nextStep();
                    break;
                case 20:
                    powerUp = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pickup"));
                    if (powerUp.Count <= 0)
                    {
                        nextStep();
                    }
                    break;
                case 21:
                    Popup("Check your new throw ability. F key or Right Trigger.");
                    nextStep();
                    break;
                case 22:
                    if (player.GetCurrentAnimatorStateInfo(0).IsName("Throw"))
                    {
                        Popup("Strike!");
                        nextStep();
                    }
                    break;
                case 23:
                    ClosePop();
                    nextStep();
                    break;
                case 24:
                    Popup("The goal is to kill wasps and save the Queen Bee! Test you fighting skills!");
                    nextStep();
                    break;
                case 25:
                    if (DeltaTime() > 2)
                    {
                        Instantiate(BaseBee, new Vector3(-90.5999985f, 9.0002861f, -52.2000008f), Quaternion.identity);
                        nextStep();
                        ClosePop();
                        BaseBee = GameObject.FindGameObjectWithTag("Enemy");
                    }
                    break;
                case 26:
                    // check if bee dead
                    if (BaseBee.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Die"))
                    {
                        nextStep();
                    }
                    break;
                case 27:
                    Popup("You're ready to begin! Good Luck!");
                    nextStep();
                    break;
                case 28:
                    if (DeltaTime() > 5)
                    {
                        nextStep();
                        ClosePop();
                    }
                    break;
                case 29:
                    SceneManager.LoadScene("Level_1");
                    break;
                default:
                    break;
            }
        }


    }
}
