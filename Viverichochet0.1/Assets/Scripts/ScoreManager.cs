using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int scoreP1;
    public static int scoreP2;
    private int round;
    Text textP1;
    Text textP2;
    //    bool finished = false;
    //  bool done = true;

    void Awake()
    {
        //  print("awake" + round);
        //  print("awake" + finished);
        textP1 = GameObject.Find("ScoreText_P1").GetComponent<Text>();
        textP2 = GameObject.Find("ScoreText_P2").GetComponent<Text>();
        if (scoreP1 == 5 || scoreP2 == 5) { scoreP2 = 0; scoreP1 = 0; }
        round = scoreP1 + scoreP2;
        
        //    print(score + " " + finished);
        //score = 0;
    }


    // Update is called once per frame
    void Update()
    {
        textP1.text = "Score: " + scoreP1;
        textP2.text = "Score: " + scoreP2;

        if (scoreP1 == 5 || scoreP2 == 5)
        {
            SceneManager.LoadScene(2);
        }
        if (scoreP1 == round + 1 || scoreP2 == round + 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

    }

}
