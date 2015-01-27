using UnityEngine;
using System.Collections;

public class cFrog 
{
    public Comp_IA_Frog comp_ia_frog;
    public bool isEnabled;
    public bool isVisible;
    public int index;
    public Transform _transform;
    public Renderer _rendererHead;
    public Renderer _rendererBody;
    public Collider _collider;
    public GameObject _gameObject;

    public cFrog(Comp_IA_Frog comp_ia_frog)
    {
        this.comp_ia_frog = comp_ia_frog;
        _transform = comp_ia_frog.transform;
        _rendererHead = comp_ia_frog.transform.Find("grp_FROG/Head").renderer;
        _rendererBody = comp_ia_frog.transform.Find("grp_FROG/Body").renderer;
        _collider = comp_ia_frog.collider;
        _gameObject = comp_ia_frog.gameObject;
    }

    public void Enable()
    {
        _rendererHead.enabled = true;
        _rendererBody.enabled = true;
        _collider.enabled = true;
        isEnabled = true;
        isVisible = false;
        comp_ia_frog.comp_Frog_Manager.enabled_frogs++;
        comp_ia_frog.WakeUp();
    }

    // Marca la moneda como inactiva
    public void Disable()
    {
        isEnabled = false;
        isVisible = false;
        _rendererHead.enabled = false;
        _rendererBody.enabled = false;
        _collider.enabled = false;
    }

    // Elimina (reciclandola) la moneda de la escena devolviendola a la pool
    public void DeleteFrog()
    {
        //Disable();
        comp_ia_frog.comp_Frog_Manager.DeleteFrog(index);
    }
	
}
