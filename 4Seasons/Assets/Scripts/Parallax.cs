using UnityEngine;

public class Parallax : MonoBehaviour
{
     
    private float startingPos, lengthOfSprite;// Prywatne zmienne do przechowywania początkowej pozycji obiektu oraz długości sprite'a.
    public float AmountOfParallax;  // Publiczna zmienna do kontrolowania ilości efektu paralaksy. Można ją dostosować w edytorze Unity.
    public Camera MainCamera;// Publiczna zmienna do odniesienia się do głównej kamery w scenie, co pozwala skryptowi reagować na jej ruch.

    private void Start() // Start jest wywoływany przed pierwszą aktualizacją klatki.
    {
        startingPos = transform.position.x; // Inicjalizuje początkową pozycję obiektu gry na jej obecną pozycję x.
        lengthOfSprite = 6.67f; // Ręcznie ustawia długość sprite'a. Można to obliczyć dynamicznie, jeśli jest potrzeba.
    }

    private void Update() // Update jest wywoływany raz na klatkę.
    {
        Vector3 cameraPosition = MainCamera.transform.position; // Przechwytuje pozycję kamery, aby użyć jej do obliczenia efektu paralaksy.
        float temp = cameraPosition.x * (1 - AmountOfParallax);  // Oblicza tymczasową wartość na podstawie pozycji x kamery, dostosowaną o ilość paralaksy. Pomaga to w określeniu, kiedy zresetować pozycję tła.
        float distance = cameraPosition.x * AmountOfParallax; // Oblicza dystans do przesunięcia tła, tworząc efekt paralaksy poprzez pomnożenie pozycji x kamery przez AmountOfParallax.

        Vector3 newPosition = new Vector3(startingPos + distance, 
        transform.position.y, transform.position.z);  // Określa nową pozycję obiektu gry, przesuwając go wzdłuż osi x, jednocześnie zachowując jego oryginalne pozycje y i z.
        transform.position = newPosition; // Stosuje obliczoną pozycję do obiektu gry.

        if (temp > startingPos + lengthOfSprite)  // Sprawdza, czy obiekt gry musi zostać zresetowany w lewo lub w prawo, aby stworzyć ciągły efekt przewijania.
        {
            startingPos += lengthOfSprite; // Jeśli kamera przemieściła się wystarczająco daleko w prawo, dostosowuje początkową pozycję w prawo.
        }
        else if (temp < startingPos - lengthOfSprite)
        {
            startingPos -= lengthOfSprite;  // Jeśli kamera przemieściła się wystarczająco daleko w lewo, dostosowuje początkową pozycję w lewo.
        }
    }
}
