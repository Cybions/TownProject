using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FarmableResource : MonoBehaviour
{
    [SerializeField]
    private Image Icon;
    private bool isIconDisplayed = false;
    private Tweener movementTweener;
    // Start is called before the first frame update
    void Start()
    {
        movementTweener = Icon.transform.DOScale(Vector3.zero, 0f);
    }

    private void Update()
    {
        if (isIconDisplayed)
        {
            Icon.transform.LookAt(Camera.main.transform);
        }
    }

    public void DisplayIcon()
    {
        movementTweener = Icon.transform.DOScale(Vector3.one, .8f).SetEase(Ease.OutBack);
        isIconDisplayed = true;
    }

    public void HideIcon()
    {
        movementTweener = Icon.transform.DOScale(Vector3.zero, .8f).SetEase(Ease.InBack).OnComplete(delegate { isIconDisplayed = false; });     
    }
    
    public void FarmResource()
    {
        GetComponent<SphereCollider>().enabled = false;
        movementTweener.Kill();
        Icon.transform.DOScale(Vector3.zero, 0f);
        if (CameraManager.Instance.OldFarmableResources.Contains(this))
        {
            CameraManager.Instance.OldFarmableResources.Remove(this);
        }
        if(CameraManager.Instance.ClosestFR = this) { CameraManager.Instance.ClosestFR = null; }
        transform.DOScale(0, .5f).SetEase(Ease.Linear);
        transform.DOMove(PlayerController.Instance.transform.position, .3f).SetEase(Ease.Linear).OnComplete(delegate { Destroy(gameObject); });
    }
}
