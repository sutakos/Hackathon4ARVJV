public class changeForm : MonoBehaviour
{
    /*
        script attacher a un objet qui va changer de taille pour un temps defini et de taille defini.
    */
    [SerializeField] private Vector3 sizeToChangeTo = new Vector3(2f, 2f, 2f); // taille a changer
    [SerializeField] private float timeToChangeSize = 1f; // temps pour changer de taille
    private Vector3 originalSize; // taille originale de l'objet

    private void Start()
    {
        originalSize = transform.localScale; // recuperer la taille originale de l'objet
    }
    public void ChangeSize()
    {
        transform.localScale = sizeToChangeTo; // changer la taille de l'objet
        Invoke("ResetSize", timeToChangeSize); // appeler la fonction ResetSize apres le temps defini
    }
    private void ResetSize()
    {
        transform.localScale = originalSize; // restaurer la taille originale de l'objet
    }
}