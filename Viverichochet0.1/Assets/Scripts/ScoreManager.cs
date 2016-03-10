using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int scoreP1;
    public static int scoreP2;
    private int round;
    public CollisionManager p1Hit;
    public CollisionManager p2Hit;
    Text textP1;
    Text textP2;
    //    bool finished = false;
    //  bool done = true;


    //runs everytime the scene restarts
    void Awake()
    {
        //  print("awake" + round);
        //  print("awake" + finished);
        textP1 = GameObject.Find("p1Score").GetComponent<Text>();
        textP2 = GameObject.Find("p2Score").GetComponent<Text>();
        //   if (scoreP1 == 5 || scoreP2 == 5) { scoreP2 = 0; scoreP1 = 0; }
        print(p1Hit.hit);
        print(p2Hit.hit);
       // round = scoreP1 + scoreP2;
        
        //    print(score + " " + finished);
        //score = 0;
    }


    // Update is called once per frame
    void Update()
    {
        //print(p1Hit.hit);
        //print(p2Hit.hit);
        if (p1Hit.hit) { scoreP2 += 1; print("Player 2 Scored:" + scoreP2); }
        
        if (p2Hit.hit) { scoreP1 += 1; print("Player 1 Scored:" + scoreP1); }
        textP1.text = "Score: " + scoreP1;
        textP2.text = "Score: " + scoreP2;

        if (scoreP1 == 5 || scoreP2 == 5)
        {
            SceneManager.LoadScene(2);
        }
        if (p1Hit.hit || p2Hit.hit)
        {
            print("Player 1 Score " +scoreP1); print("Player 2 Score" +scoreP2); print("Round #"+round);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);


        }

    }

}
