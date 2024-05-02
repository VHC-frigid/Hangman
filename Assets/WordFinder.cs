using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordFinder : MonoBehaviour
{

    //initialise a new list of strings
    public List<string> possibleWords = new List<string>();

    public Text wordsDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        TextAsset allWords = Resources.Load("words") as TextAsset;

        string[] words = allWords.text.Split(',');

        //for each entry in the wrods array
        foreach (string currentWord in words)
        {
            if (currentWord.Length > 3)
            {
                possibleWords.Add(currentWord);
            }
        }
        
        //possibleWords = new List<string>(words);
    }

    public void SearchString(string inputString)
    {
        //contin all teh words we find that have the input string inside
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

}
