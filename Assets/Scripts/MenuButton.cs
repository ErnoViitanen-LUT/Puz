using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public MenuController menuController;
    public Animator animator;
    public int thisIndex;
    string url = "https://github.com/ErnoViitanen-LUT/Puz";
    bool submitted = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(menuController.index == thisIndex)
      {
        animator.SetBool ("selected", true);
        if(Input.GetAxis ("Submit") == 1){
          animator.SetBool ("pressed", true);          
          if (!submitted) {
            submitted = true;
             Invoke("ButtonPress",0.25f);
          }
        }else if (animator.GetBool ("pressed")){
          animator.SetBool ("pressed", false);		        
        }
      }else{
        animator.SetBool ("selected", false);
      }          
    }
    void ButtonPress(){
      if(thisIndex == 0) {
        SceneManager.LoadScene(1);        
      }
      if(thisIndex == 2) {
        Application.OpenURL(url);
        
      }
    }
}
