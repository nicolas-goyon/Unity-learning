using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image Image;

    private void Start() {
        cuttingCounter.OnCuttingCounterChange += CuttingCounter_OnCuttingCounterChange;
        Image.fillAmount = 0;
        Hide();
    }

    private void CuttingCounter_OnCuttingCounterChange(object sender, CuttingCounter.OnCuttingCounterChangeEventArgs e) {
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
