using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Score_p2 : MonoBehaviour {

    public static int score;
    private int round;
    Text text;


    void Awake()
    {
        print("awake" + round);
        text = GetComponent<Text>();
        round = score;
        if (score == 5) round = 0;
        //score = 0;
    }


    // Update is called once per frame
    void Update()
    {
        text.text = "Score " + score;

        if (score == 5) 
        {
            round = 0;
            SceneManager.LoadScene(1);
            
        }

        if (score == round + 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}
