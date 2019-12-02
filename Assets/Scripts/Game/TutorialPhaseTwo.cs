using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPhaseTwo : MonoBehaviour {

    public GameObject professor;
    public GameObject[] texts;
    public GameObject textBaloon;
    public GameObject fanToSpawn;
    public GameObject shootText;

    public GameObject blackLayer;
    public GameObject button;

    public PlayerPhaseTwo player;

    private int actualText = 0;
    private Animator profAnimator;
    private float minTime = 3f;
    private float lastTimeChanged;

    void Start() {
        GameManager.DeactiveSpawners();
        GameState.Instance.InTutorial();
        GameInput.Instance.PauseFunction = false;
        player.CanShoot = false;
        lastTimeChanged = Time.time;
        profAnimator = professor.GetComponent<Animator>();
        blackLayer.SetActive(true);
        StartText();
    }

    void Update() {
        if (actualText == 2 && !fanToSpawn.activeSelf) {
            StopCoroutine(FANReaction());
            actualText++;
            Three();
        }

        if (GameInput.Instance.Next && button.activeSelf)
            LoadNextText();

        if (GameInput.Instance.Pause) {
            End();
        }
    }

    public void StartText() {
        actualText = 0;
        Zero();
    }

    public void LoadNextText() {
        if (Time.time - lastTimeChanged < minTime)
            return;

        lastTimeChanged = Time.time;
        actualText++;
        switch (actualText) {
            case 1: One(); break;
            case 2: Two(); break;
            case 4: End(); break;
        }
    }

    void Zero() {
        profAnimator.Play("Entrance");
        StartCoroutine(ZeroRoutine());
    }

    void One() {
        button.SetActive(false);
        profAnimator.speed = 1;
        texts[actualText - 1].SetActive(false);
        texts[actualText].SetActive(true);
        profAnimator.Play("Talking");
        StartCoroutine(StopTalking());
    }

    void Two() {
        button.SetActive(false);
        texts[actualText - 1].SetActive(false);
        fanToSpawn = ObjectPool.Instantiate(fanToSpawn, new Vector2(1.5f, 0.5f), fanToSpawn.transform.rotation);
        StartCoroutine(FANReaction());
    }

    void Three() {
        player.CanShoot = false;
        button.SetActive(false);
        texts[actualText - 1].SetActive(false);
        texts[actualText].SetActive(true);
        profAnimator.Play("Talking");
        StartCoroutine(StopTalking());
    }

    IEnumerator ZeroRoutine() {
        yield return new WaitForSeconds(3.0f);
        textBaloon.SetActive(true);
        texts[actualText].SetActive(true);
        profAnimator.Play("Talking");
        StartCoroutine(StopTalking());
    }

    IEnumerator FANReaction() {
        yield return new WaitForSeconds(2.0f);
        texts[actualText].SetActive(true);
        profAnimator.speed = 1;
        profAnimator.Play("Talking");
        player.CanShoot = true;
        yield return new WaitForSeconds(1.0f);
        shootText.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        profAnimator.speed = 0;
    }

    IEnumerator StopTalking() {
        yield return new WaitForSeconds(4.0f);
        button.SetActive(true);
        profAnimator.speed = 0;
    }

    void End() {
        StopAllCoroutines();
        professor.SetActive(false);
        textBaloon.SetActive(false);
        blackLayer.SetActive(false);
        player.CanShoot = true;

        GameInput.Instance.PauseFunction = true;

        gameObject.GetComponent<GameManager>().LetsStart();
        GameState.Play();
        GameManager.ActiveSpawners();

        this.enabled = false;
    }
}
