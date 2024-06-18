using UnityEngine;
using System.Collections;

public class OpenMaterialsShop : MonoBehaviour
{
    public GameObject materialsShop;
    public GameObject buttonMaterialsShop;
    public Dialogue materialsShopDialogue;

    void Start()
    {
        materialsShop.SetActive(false);
        buttonMaterialsShop.SetActive(false);
    }

    private void Update()
    {
        ActivateGameObjectBasedOnDays();
    }

    public void ToggleMaterialsShop()
    {
        materialsShop.SetActive(!materialsShop.activeSelf);
    }

    public void CloseMaterialsShop()
    {
        materialsShop.SetActive(false);
    }

    void ActivateGameObjectBasedOnDays()
    {
        if (GameManager.days >= 2 && buttonMaterialsShop != null)
        {
            buttonMaterialsShop.SetActive(true);
            if (GameManager.days == 2)
            {
                StartCoroutine(MaterialsShopDialogue());
            }
        }
    }

    private IEnumerator MaterialsShopDialogue()
    {
        yield return null;
        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<DialogueManager>().StartDialogue(materialsShopDialogue);
    }
}
