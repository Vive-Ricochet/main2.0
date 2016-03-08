using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Score_p1 : MonoBehaviour {

    public static int score;
    private int round;
    Text text;
//    bool finished = false;
  //  bool done = true;

    void Awake()
    {
      //  print("awake" + round);
      //  print("awake" + finished);
        text = GetComponent<Text>();
        round = score;

    //    print(score + " " + finished);
        //score = 0;
    }
	
	
	// Update is called once per frame
	void Update () {
        text.text = "Score: " + score;

        if (score == 5)
        {
            score += 1;
            SceneManager.LoadScene(1);
        }
        if (score == round+1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }
        
	}
}
