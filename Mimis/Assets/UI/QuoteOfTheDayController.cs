using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Attach to a GameObject that has a UIDocument component.
/// Assign the UIDocument in the Inspector — the controller will
/// wire up the button and cycle through quotes automatically.
/// </summary>
[RequireComponent(typeof(UIDocument))]
public class QuoteOfTheDayController : MonoBehaviour
{
    // ── Quotes ────────────────────────────────────────────────────────────────

    private static readonly List<(string text, string author)> Quotes = new()
    {
        (
            "The purpose of life is to live it, to taste experience to the utmost, to reach out eagerly and without fear for newer and richer experience.",
            "Eleanor Roosevelt"
        ),
        (
            "In the middle of every difficulty lies opportunity.",
            "Albert Einstein"
        ),
        (
            "Life is not measured by the number of breaths we take, but by the moments that take our breath away.",
            "Maya Angelou"
        ),
        (
            "To live is the rarest thing in the world. Most people just exist.",
            "Oscar Wilde"
        ),
        (
            "The good life is one inspired by love and guided by knowledge.",
            "Bertrand Russell"
        ),
        (
            "Life is what happens to you while you're busy making other plans.",
            "John Lennon"
        ),
        (
            "You only live once, but if you do it right, once is enough.",
            "Mae West"
        ),
        (
            "Do not go where the path may lead; go instead where there is no path and leave a trail.",
            "Ralph Waldo Emerson"
        ),
        (
            "In three words I can sum up everything I've learned about life: it goes on.",
            "Robert Frost"
        ),
        (
            "Life is either a daring adventure or nothing at all.",
            "Helen Keller"
        ),
    };

    // ── Exaltations ───────────────────────────────────────────────────────────

    private static readonly List<string> Exaltations = new()
    {
        "What a gift — to be alive, to feel, to begin again.",
        "Today holds a thousand beautiful possibilities.",
        "Breathe it in — this moment is yours entirely.",
        "Every sunrise is a promise kept by the universe.",
        "You are here. That alone is extraordinary.",
        "The day is young and so is your spirit.",
        "Greet this moment with open hands.",
    };

    // ── State ─────────────────────────────────────────────────────────────────

    private int _quoteIndex   = 0;
    private int _exaltIndex   = 0;

    // UI references
    private Label  _dayLabel;
    private Label  _exaltation;
    private Label  _quoteText;
    private Label  _quoteAuthor;
    private Button _nextBtn;

    // ── Unity Lifecycle ───────────────────────────────────────────────────────

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _dayLabel    = root.Q<Label>("day-label");
        _exaltation  = root.Q<Label>("exaltation");
        _quoteText   = root.Q<Label>("quote-text");
        _quoteAuthor = root.Q<Label>("quote-author");
        _nextBtn     = root.Q<Button>("next-quote-btn");

        // Seed with today's date
        _dayLabel.text = DateTime.Now.ToString("dddd · MMMM d");

        // Random start
        _quoteIndex  = UnityEngine.Random.Range(0, Quotes.Count);
        _exaltIndex  = UnityEngine.Random.Range(0, Exaltations.Count);

        DisplayCurrent();

        _nextBtn.clicked += OnNextClicked;
    }

    private void OnDisable()
    {
        if (_nextBtn != null)
            _nextBtn.clicked -= OnNextClicked;
    }

    // ── Logic ─────────────────────────────────────────────────────────────────

    private void OnNextClicked()
    {
        _quoteIndex  = (_quoteIndex  + 1) % Quotes.Count;
        _exaltIndex  = (_exaltIndex  + 1) % Exaltations.Count;
        DisplayCurrent();
    }

    private void DisplayCurrent()
    {
        var (text, author) = Quotes[_quoteIndex];
        _quoteText.text   = text;
        _quoteAuthor.text = $"— {author}";
        _exaltation.text  = Exaltations[_exaltIndex];
    }
}
