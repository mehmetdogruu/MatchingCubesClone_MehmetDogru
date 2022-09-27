using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    //Bu script sayesinde IInteractor interfaceini characterController scriptinin saðýna Monobehacvior : yanýna eklememle artýk characterController scriptine dönüþtürdüm.
    //Interactor scripti daha önceki oyunlarda kullandýðým CollisionDetection scriptiyle týpatýp ayný iþi yapýyor.Çarpýþma kontrol.
    private IInteractor _controller;
    public bool canInteract;

    private void Awake()
    {
        _controller = GetComponentInParent<IInteractor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canInteract || !GameManager.instance.isPlaying) return;//Interactor canInteract true ise ve game managera göre oyun baþladýysa
        if (!other.CompareTag(tag)) return;// Interactor ve çarpýþtýðý objenin tagi ayný ise

        var hasInteractable = other.TryGetComponent<IInteractable>(out var interactable);//Interactorýn çarpýþtýðý objede IInteractable interface i varsa
        if (hasInteractable && interactable.IsInteractable) interactable.Interact(_controller);//IInteractabke interface i ile gelen INteract fonksiyonunu çalýþtýr
        //Neden böyle??? Topladýðým obje çarptýðým engel geçtiðim kapý bunlarýn hepsinin temelde tek bir iþi var onu da Interact fonksiyonun içine yazarak,OnTrigger ýn içinde sisteme farklý farklý scriptler buldurarak(catchimg) ve onlarýn içindeki fonksiyonlarý
        //teker teker inceletip çalýþtýrarak yük bindirmek istemedik.Bunu yapmamýzý saðlayan þe IInteractable interface i.IInteractoble interface sayesinde etkileþime gireceðim her objenin scriptine 2 satýr kodla eriþiyorum.
    }
}
