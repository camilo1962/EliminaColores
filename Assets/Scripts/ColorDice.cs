using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Drawing;
using UnityEngine.U2D;

public class ColorDice : MonoBehaviour
{

    // Array of dice sides sprites to load from Resources folder
    //public GameObject[] diceSides;
     public Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
     private Image diceImg;
    //private GameObject diceImg;

    // Use this for initialization
    private void Start()
    {

        // Assign Renderer component
        diceImg = GetComponent<Image>();

        // Load dice sides sprites to array from DiceSides subfolder of Resources folder
        //diceSides = Resources.LoadAll<Sprite>("DiceColour/");
    }

    // If you left click over the dice then RollTheDice coroutine is started
    public void OnMouseDown()
    {
        StartCoroutine("RollTheDice");
    }

    // Coroutine that rolls the dice
    public IEnumerator RollTheDice()
    {
        // Variable to contain random dice side number.
        // It needs to be assigned. Let it be 0 initially
        SoundManager.Instance.PlaySfxOneShot(EAudioSource.AUDIO_SOURCE_SFX, EGameSfx.SFX_CLICK);
        int randomDiceSide = 0;


    // Lado final o valor que lee el dado al final de la rutina
    //int finalSide = 0;
    //Debug.Log("¡¡Dados de color lanzados!!");
    // Bucle para cambiar de lado los dados aleatoriamente
    // antes de que aparezca el lado final. 20 iteraciones aquí.
    x: for (int i = 0; i <= 20; i++)
        {
            // Pick up random value from 0 to 5 (All inclusive)
            randomDiceSide = Random.Range(0, 6);

            //Debug.Log("Hey!!!");
            //Coloca el sprite en la cara superior del dado desde una matriz según un valor aleatorio
            diceImg.sprite = diceSides[randomDiceSide];

            // Pause before next itteration
            yield return new WaitForSeconds(0.05f);
        }



        //Lista de verificación si el color de los dados está en el tablero.

        bool Cell_check = IsValidDiceColor(randomDiceSide);
        Debug.Log("el color existe: " + Cell_check);
        if (Cell_check == false)
        {
            goto x;
        }



        // Debug.Log("SUCCESS");
        GameController.Instance.diceRollColorTag = (EDiceTag)randomDiceSide;

        // Asignando el lado final para que puedas usar este valor más adelante en tu juego
        // para el movimiento del jugador, por ejemplo
        //finalSide = randomDiceSide + 1;

        // Show final dice value in Console
        // Debug.Log("Colour Dice: " + (EDiceTag)randomDiceSide);



    }

    bool IsValidDiceColor(int col)
    {
        GameObject obj = GameController.Instance.list.FirstOrDefault(x => x.GetComponent<PointerTester>().Tag.Equals((EDiceTag)col));
        //Debug.Log("Test=> " + obj == null +" *** "+obj != null);
        return (obj != null ? true : false);
    }

}
