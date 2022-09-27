using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    //Bu script sayesinde IInteractor interfaceini characterController scriptinin sa��na Monobehacvior : yan�na eklememle art�k characterController scriptine d�n��t�rd�m.
    //Interactor scripti daha �nceki oyunlarda kulland���m CollisionDetection scriptiyle t�pat�p ayn� i�i yap�yor.�arp��ma kontrol.
    private IInteractor _controller;
    public bool canInteract;

    private void Awake()
    {
        _controller = GetComponentInParent<IInteractor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canInteract || !GameManager.instance.isPlaying) return;//Interactor canInteract true ise ve game managera g�re oyun ba�lad�ysa
        if (!other.CompareTag(tag)) return;// Interactor ve �arp��t��� objenin tagi ayn� ise

        var hasInteractable = other.TryGetComponent<IInteractable>(out var interactable);//Interactor�n �arp��t��� objede IInteractable interface i varsa
        if (hasInteractable && interactable.IsInteractable) interactable.Interact(_controller);//IInteractabke interface i ile gelen INteract fonksiyonunu �al��t�r
        //Neden b�yle??? Toplad���m obje �arpt���m engel ge�ti�im kap� bunlar�n hepsinin temelde tek bir i�i var onu da Interact fonksiyonun i�ine yazarak,OnTrigger �n i�inde sisteme farkl� farkl� scriptler buldurarak(catchimg) ve onlar�n i�indeki fonksiyonlar�
        //teker teker inceletip �al��t�rarak y�k bindirmek istemedik.Bunu yapmam�z� sa�layan �e IInteractable interface i.IInteractoble interface sayesinde etkile�ime girece�im her objenin scriptine 2 sat�r kodla eri�iyorum.
    }
}
