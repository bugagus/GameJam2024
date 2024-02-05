using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] float holdTime;
    private InputControls _input;
    [SerializeField] private MorseCode nextGoblinMorse;
    private float lastStartTime;
    private float lastEndTime;

    private void Awake()
    {
        _input = new InputControls();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDestroy()
    {
        _input.Disable();
    }

    private void Start()
    {
        _input.Controls.Tick.started += ctx => StartTick(ctx);   
        _input.Controls.Tick.canceled += ctx => EndTick(ctx);    
    }

    private void StartTick(InputAction.CallbackContext context)
    {
        Debug.Log("Tick started");
        lastStartTime = Time.time;
    }

    private void EndTick(InputAction.CallbackContext context)
    {
        Debug.Log("Tick ended");
        lastEndTime = Time.time;
        CheckLetter();
    }

    private void CheckLetter()
    {
        char typedLetter;
        char desiredLetter = nextGoblinMorse.GetCurrentLetter(); 
        typedLetter = (lastEndTime - lastStartTime < holdTime)? 'E' : 'T';
        if(typedLetter == desiredLetter)
        {
            nextGoblinMorse.NextLetter();
        }
        else{
            nextGoblinMorse.ResetWord();
        }   
    }

    public void SetNextGoblin(MorseCode g)
    {
        nextGoblinMorse = g;
    }

}
