using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(Collider))]
public class Riddles : MonoBehaviour
{


    [SerializeField] private GameObject riddleCanvas;
    [SerializeField] private GameObject deliveryCanvas;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject interactPrompt;

    [SerializeField] private string[] acceptedAnswers;
    [SerializeField] private bool containsMatch = true;
    [SerializeField] private bool lockAfterSolved = true;

    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private KeyCode closeKey = KeyCode.Escape;

    public UnityEvent onOpened;   // e.g. freeze player movement, unlock cursor
    public UnityEvent onClosed;   // e.g. resume movement, lock cursor
    public UnityEvent onSolved;   // e.g. open a door

    private bool playerInside;
    private bool isOpen;
    private bool solved;

    private void Start()
    {
        if (riddleCanvas != null) riddleCanvas.SetActive(false);
        if (interactPrompt != null) interactPrompt.SetActive(false);
    }

    private void OnEnable()
    {
        if (inputField != null) inputField.onSubmit.AddListener(CheckAnswer);
    }

    private void OnDisable()
    {
        if (inputField != null) inputField.onSubmit.RemoveListener(CheckAnswer);
    }

    private void Update()
    {
        if (!isOpen)
        {
            if (playerInside && !(solved && lockAfterSolved) && Input.GetKeyDown(interactKey))
                OpenRiddle();
        }
        else if (Input.GetKeyDown(closeKey))
        {
            CloseRiddle();
        }
    }

    private void OpenRiddle()
    {
        isOpen = true;
        riddleCanvas.SetActive(true);
        deliveryCanvas.SetActive(false);
        if (interactPrompt != null) interactPrompt.SetActive(false);

        inputField.text = "";

        // wait a frame so the "E" keypress that opened the UI doesn't get typed into the field
        StartCoroutine(FocusNextFrame());

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        onOpened?.Invoke();
    }

    private IEnumerator FocusNextFrame()
    {
        yield return null;
        inputField.text = "";
        inputField.ActivateInputField();
        inputField.Select();
    }

    private void CloseRiddle()
    {
        isOpen = false;
        inputField.DeactivateInputField();
        riddleCanvas.SetActive(false);
        deliveryCanvas.SetActive(true);

        if (interactPrompt != null && playerInside && !(solved && lockAfterSolved))
            interactPrompt.SetActive(true);

        onClosed?.Invoke();
    }

    // Called when the player presses Enter in the input field.
    private void CheckAnswer(string input)
    {
        if (IsCorrect(input))
        {
            solved = true;
            onSolved?.Invoke();
            if (lockAfterSolved) CloseRiddle();
        }
        else
        {
            inputField.text = "";
            inputField.ActivateInputField(); // keep typing after a wrong guess
        }
    }

    private bool IsCorrect(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;

        string guess = input.Trim().ToLowerInvariant();

        foreach (string answer in acceptedAnswers)
        {
            if (string.IsNullOrWhiteSpace(answer)) continue;
            string key = answer.Trim().ToLowerInvariant();

            if (containsMatch ? guess.Contains(key) : guess == key)
                return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInside = true;
        if (interactPrompt != null && !isOpen && !(solved && lockAfterSolved))
            interactPrompt.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInside = false;
        if (interactPrompt != null) interactPrompt.SetActive(false);
        if (isOpen) CloseRiddle(); // auto-close if they walk away
    }
}