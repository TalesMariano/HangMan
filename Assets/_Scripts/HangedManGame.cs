using UnityEngine;
using UnityEngine.UI;

public class HangedManGame : MonoBehaviour
{
    // Jogo
    public string word = "Mario";
    
    [Space]
    
    // References
    public Text linhasForca;
    public Text textoLetrasUsadas;
    
    // Arrays
    public char[] wordArray;
    public char[] visualArray;
    public bool[] correctLetter;


    //Unity UI
    public string nextWord;
    
    void Start()
    {
        PrepareGame();
    }

    void ResetGame()
    {
        linhasForca.text = "";
        textoLetrasUsadas.text = "";
    }
    
    [ContextMenu("PrepareGame")]
    void PrepareGame()
    {
        wordArray = word.ToCharArray();     // Transforma palavra chave em array

        int numLetters = wordArray.Length;    // Pega o número de letras
        
        //Inicializa Arrays
        visualArray = new char[numLetters];
        correctLetter = new bool[numLetters];
        
        // Popula arrays
        for (int i = 0; i < numLetters; i++)
        {
            visualArray[i] = '_';
            correctLetter[i] = false;
        }

        UpdateVisualWord(); // Atualiza visual
    }

    void Update()
    {
        // Recebe input do teclado
        if (Input.anyKeyDown)
        {
            //linhasForca.text = Input.inputString;
            //linhasForca.text = GetKeyboardLetter().ToString();
            
            CheckCharInWord(GetKeyboardLetter());
            AddLetterToUsedList(GetKeyboardLetter());
            UpdateVisualWord();
        }
    }
    
    void UpdateVisualWord()
    {
        PrintArrayChar(visualArray);    // Preenche text element com array de resultado
    }
    
    void CheckCharInWord(char c)
    {
        for (int i = 0; i < wordArray.Length; i++)
        {
            if (CompareChars(wordArray[i], c))
            {
                correctLetter[i] = true;
                visualArray[i] = wordArray[i];
            }
        }
    }
    
    void PrintArrayChar(char[] array)
    {
        // Clear Text
        linhasForca.text = "";
        
        for (int i = 0; i < array.Length; i++)
        {
            linhasForca.text += array[i] + " ";
        }
    }
    
    char GetKeyboardLetter()
    {
        foreach (char c in Input.inputString)
        {
            if (char.IsLetter(c))
            {
                return c;
            } 
        }

        return '\0';
    }

    bool CompareChars(char a, char b)
    {
        return char.ToUpper(a) == char.ToUpper(b);
    }

    void AddLetterToUsedList(char c)
    {
        if(c == '\0')
            return;
        
        // Check for repeats
        if(textoLetrasUsadas.text.Contains(c.ToString()))
            return;
        
        textoLetrasUsadas.text += c + " ";
    }

    public void SetNextWord(string newWord)
    {
        nextWord = newWord;
    }
    
    public void ButtonUISetWord()
    {
        word = nextWord;
        
        ResetGame();
        
        PrepareGame();
    }
}
