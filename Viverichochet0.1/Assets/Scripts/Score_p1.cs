using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Score_p1 : MonoBehaviour {

    public static int score;
    private int round;
    Text text;


    void Awake()
    {
        print("awake" + round);
        text = GetComponent<Text>();
        round = score;
        //score = 0;
    }
	
	
	// Update is called once per frame
	void Update () {
        text.text = "Score " + score;

        if (score == round+1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }
	}
}
