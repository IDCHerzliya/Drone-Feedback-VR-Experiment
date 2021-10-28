using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DroneController : MonoBehaviour
{
    private Queue<IEnumerator> _instructionQueue = new Queue<IEnumerator>();
    private bool _running = false;
    private bool _waitingAdminInput = false;
    private bool _watchRunning = false;

    [SerializeField]
    private Renderer targetRenderer;
    [SerializeField]
    private AudioSource targetAudioSource;
    [SerializeField]
    private Material[] textList;
    [SerializeField]
    private AudioClip[] audioClips;
    [SerializeField]
    private GameObject fog;
    private System.Diagnostics.Stopwatch _watch = new System.Diagnostics.Stopwatch();


    private void Update()
    {
        if (_instructionQueue.Count == 0 || _running) return;
        var v = _instructionQueue.Dequeue();
        StartCoroutine(v);
        _running = true;
    }

    public void Move(Vector3 toMove, float time, bool local)
    {
        _instructionQueue.Enqueue(MoveCoroutine(toMove, time, local));
    }


    public void Wait(float time)
    {
        _instructionQueue.Enqueue(WaitCoroutine(time));
    }

    public void AwaitAdminInput()
    {
        _instructionQueue.Enqueue(SetPaused(true));

    }

    public void AdminInputEvent(InputAction.CallbackContext cont)
    {
        if (!cont.performed || !_waitingAdminInput) return;
        _running = false;
        _waitingAdminInput = false;
    }

    public void AwaitUserInput()
    {
        _instructionQueue.Enqueue(AwaitUserCoroutine());
    }

    public void UserInputEvent(InputAction.CallbackContext cont)
    {
        if (!cont.performed || !_watchRunning) return;
        _watchRunning = false;
        _running = false;
        _watch.Stop();
        var s = "" + _watch.Elapsed;
        SimpleLogger.WriteToFile(s);
    }

    public void Rotate(Vector3 euler, float time)
    {
        _instructionQueue.Enqueue(RotateCoroutine(euler, time));
    }

    public void SetTexture(int textID)
    {
        _instructionQueue.Enqueue(SetTexCoroutine(textList[textID]));
    }

    public void PlayAudio(int clipID, float duration)
    {
        _instructionQueue.Enqueue(PlayAudioCoroutine(audioClips[clipID], duration));
    }

    public void SetFog(bool status)
    {
        fog.SetActive(status);
    }

    private IEnumerator MoveCoroutine(Vector3 toMove, float time, bool local)
    {
        var initPos = local ? transform.localPosition : transform.position;
        var endPos = toMove;


        float deltat = 0;
        while (deltat < time)
        {
            yield return new WaitForEndOfFrame();
            deltat += Time.deltaTime;
            var p = Vector3.Lerp(initPos, endPos, deltat / time);
            if (local) transform.localPosition = p;
            else transform.position = p;
        }
        yield return null;
        _running = false;
    }

    private IEnumerator WaitCoroutine(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        _running = false;
    }

    private IEnumerator RotateCoroutine(Vector3 euler, float time)
    {
        var q = transform.rotation;
        var p = Quaternion.Euler(euler);


        float deltat = 0;
        while (deltat < time)
        {
            yield return new WaitForEndOfFrame();
            deltat += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(q, p, deltat / time);
        }
        yield return null;
        _running = false;
    }

    private IEnumerator SetTexCoroutine(Material img)
    {
        targetRenderer.material = img;
        yield return null;
        _running = false;
    }

    private IEnumerator PlayAudioCoroutine(AudioClip clip, float delay)
    {
        targetAudioSource.clip = clip;
        targetAudioSource.Play();
        yield return new WaitForSecondsRealtime(delay);
        _running = false;
    }

    private IEnumerator SetPaused(bool status)
    {       
        _running = status;
        _waitingAdminInput = status;
        yield return null;
    }

    private IEnumerator AwaitUserCoroutine()
    {
        _watchRunning = true;
        _watch.Reset();
        _watch.Start();
        yield return null;
    }
}
