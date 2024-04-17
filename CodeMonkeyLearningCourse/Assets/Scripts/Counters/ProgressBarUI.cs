using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject progressObjectGO;
    private IHasProgress progressObject;
    [SerializeField] private Image Image;

    private void Start() {
        progressObject = progressObjectGO.GetComponent<IHasProgress>();
        if (progressObject == null) {
            Debug.LogError("GameObject : " + progressObjectGO.name + " does not have a component that implements IHasProgress");
            return;
        }


        progressObject.OnProgressChange += ProgessObject_OnProgressChange;
        Image.fillAmount = 0;
        Hide();
    }

    private void ProgessObject_OnProgressChange(object sender, IHasProgress.OnProgressChangeEventArgs e) { 
        Image.fillAmount = e.progressNormalized;
        if (e.progressNormalized >= 1f || e.progressNormalized <= 0f) { 
            Hide();
        }
        else {
            Show();
        }
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }
}
