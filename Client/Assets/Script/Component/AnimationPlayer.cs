using UnityEngine;
using UnityEngine.UI;

public class AnimationPlayer : MonoBehaviour
{
    AnimationInfo curInfo;
    Sprite[] frames;
    public float speed = 0.1f;
    public int actionFrame = -1;
    //public UnityEvent frameEvent;

    private Image container;
    private int ticked;
    private float time;
    private bool doAnim;
    private int frameLock = -1;

    private void Awake()
    {
        container = GetComponent<Image>();
    }

    public void Play(AnimationInfo info, bool replay = false)
    {
        if (curInfo == info && !replay)
        {
            return;
        }

        Stop();
        curInfo = info;
        frames = Resources.LoadAll<Sprite>(info.resName);
        ticked = info.startFrame;
        time = 0;
        speed = info.duration / (info.endFrame - info.startFrame + 1);
        doAnim = true;
        container.sprite = frames[info.startFrame];
        container.preserveAspect = true;

        if (info.soundName != "")
        {
            AudioManager.Instance.PlaySoundByGO(AudioData.DATA[info.soundName],gameObject);
        }
        else
        {
            AudioManager.Instance.StopSoundByGO(gameObject);
        }
    }

    //固定再某一帧
    public void PlayOneFrame(AnimationInfo info, int tick, bool needSound = false)
    {
        if (tick < info.startFrame || tick > info.endFrame)
        {
            return;
        }

        Stop();
        frames = Resources.LoadAll<Sprite>(info.resName);
        container.sprite = frames[tick];

        if (needSound && info.soundName != "")
        {
            AudioManager.Instance.PlaySoundByGO(AudioData.DATA[info.soundName], gameObject);
        }
        else
        {
            AudioManager.Instance.StopSoundByGO(gameObject);
        }
    }

    public void Pause()
    {
        doAnim = false;
    }

    public void Resume()
    {
        doAnim = true;
    }

    public void Stop()
    {
        time = 0;
        doAnim = false;
        frameLock = -1;
        curInfo = null;
        //container.sprite = frames[0];
    }

    void Update()
    {
        if (doAnim)
        {
            time += Time.deltaTime;
            if (time > speed)
            {
                ticked++;
                if (ticked > curInfo.endFrame)
                    if (curInfo.loop)
                    {
                        ticked = curInfo.startFrame;
                    }
                    else
                    {
                        ticked = curInfo.endFrame;
                    }
                    
                else
                    time = 0;

                //if (ticked == actionFrame)
                //    frameEvent.Invoke();

                container.sprite = frames[ticked];
            }
        }
    }
}