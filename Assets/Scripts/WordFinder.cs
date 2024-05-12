using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WordFinder : MonoBehaviour
{

    //initialise a new list of strings
    public List<string> possibleWords = new List<string>();

    public Text wordsDisplay;
    public Text incorrectWord;
    public int maxIncorrectguesses = 8;
    public Text subTitle;
    public Text answer;
    public GameObject goPanel;

    private string currentWord;
    private List<char> guessedLetters = new List<char>();
    private int incorrectGuesses = 0;

    bool gameOver;

    // Start is called before the first frame update
    void Start()
    {

        gameOver = false;
        goPanel.SetActive(false);
        
        TextAsset allWords = Resources.Load("words") as TextAsset;

        if (allWords == null || string.IsNullOrEmpty(allWords.text))
        {
            Debug.LogError("failed to load word list");
            return;
        }
        
        string[] words = allWords.text.Split(',');

        //for each entry in the word array
        foreach (string word in words)
        {
            if (word.Length > 3)
            {
                possibleWords.Add(word);
            }
        }

        if (possibleWords.Count == 0)
        {
            Debug.LogError("List not bloody working");
            return;
        }

        currentWord = possibleWords[Random.Range(0, possibleWords.Count)];
        
        UpdateWordDisplay();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SearchString(string inputString)
    {
        //contin all the words we find that have the input string inside
        string wordsFound = "";
        wordsDisplay.text = "";

        //iterate through all our possible words
        foreach (string currentWord in possibleWords)
        {
            //if the current wod contains the int string
            if (currentWord.Contains(inputString))
            {
                wordsFound += currentWord + ",";
            }
        }

        wordsFound = wordsFound.Remove(wordsFound.LastIndexOf(','));
        wordsDisplay.text = wordsFound;
    }

    public void SearchCharacters(string inputCharacters)
    {
        string wordsFound = "";

        char[] inputCharArray = inputCharacters.ToCharArray();

        foreach (string currentWord in possibleWords)
        {
            //track whether the word has all the characters or not
            bool containsAllSoFar = true;

            foreach (char character in inputCharArray)
            {
                //if the current word doesn't contain one of the characters
                if (!currentWord.Contains(character))
                {
                    containsAllSoFar = false;
                    break; //<-- end the iteration early
                }
            }

            if (!containsAllSoFar)
            {
                continue; //<-- stop this iteration and continue with the next iteration
            }

            wordsFound += currentWord + ",";
        }

        //update wordsFound to remove everything after the last comma
        wordsFound = wordsFound.Remove(wordsFound.LastIndexOf(','));
        wordsDisplay.text = wordsFound;
    }

    void UpdateWordDisplay()
    {
        string displayText = "";

        foreach (char c in currentWord)
        {
            if (guessedLetters.Contains(c))
            {
                displayText += c + " ";

            }
            else
            {
                displayText += "_ ";
            }
        }

        wordsDisplay.text = displayText;
    }

    void CheckGuess(char guess)
    {
        guessedLetters.Add(guess);

        if (!currentWord.Contains(guess.ToString()))
        {
            incorrectGuesses++;
            incorrectWord.text = "Incorrect Try " + incorrectGuesses;

            if (incorrectGuesses >= maxIncorrectguesses)
            {
                answer.text = currentWord;
                goPanel.SetActive(true);
                subTitle.text = "Le Fail";
                
                //Debug.Log("you lose the currnt word was " + currentWord);

            }
        }

        UpdateWordDisplay();

        if (WordGuessed())
        {
            subTitle.text = "You Win";
            //Debug.Log("win");
        }
    }

    bool WordGuessed()
    {
        foreach (char c in currentWord)
        {
            if (!guessedLetters.Contains(c))
            {
                return false;
            }
        }
        return true;
    }

    // Function to call when a letter button is clicked
    public void GuessSubmitted(string guess)
    {
        // Check if the input is a single letter
        if (guess.Length == 1)
        {
            char guessedLetter = guess.ToLower()[0];
            // Check if the letter has already been guessed
            if (!guessedLetters.Contains(guessedLetter))
            {
                // Process the guess
                CheckGuess(guessedLetter);
            }
            else
            {
                Debug.Log("You already guessed that letter!");
            }
        }
        else
        {
            Debug.Log("Please enter a single letter.");
        }

    }
}

