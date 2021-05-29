﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Completed
{
	using System.Collections.Generic;		//Allows us to use Lists. 
	using UnityEngine.UI;					//Allows us to use UI.
	
	public class GameManager : MonoBehaviour
	{

		[SerializeField] internal float levelStartDelay = 2f;                        //Time to wait before starting level, in seconds.
		[SerializeField] internal float turnDelay = 0.1f;                            //Delay between each Player turn.
		[SerializeField] internal int playerFoodPoints = 100;                      //Starting value for Player food points.
		internal int currentFoodCount = 0;
		[SerializeField] internal int maxFoodCap = 2;                            //Starting value for Player food points.
		[SerializeField] internal string foodCapMessage ="Reached to maximum food capacity";
		public static GameManager instance = null;				//Static instance of GameManager which allows it to be accessed by any other script.
		[HideInInspector] internal bool playersTurn = true;       //Boolean to check if it's players turn, hidden in inspector but public.

		public int level = 1;                                   //Current level number, expressed in game as "Day 1".

		[SerializeField]
		internal List<BoardManager> LevelBoardList;
		
		private Text levelText;									//Text to display current level number.
		private Text scoreText;									//Text to display current level number.
		private GameObject levelImage;							//Image to block out level as levels are being set up, background for levelText.
		private BoardManager boardScript;						//Store a reference to our BoardManager which will set up the level.
		private SavePrefData savePrefScript;						//Store a reference to our BoardManager which will set up the level.
		private List<Enemy> enemies;							//List of all Enemy units, used to issue them move commands.
		private bool enemiesMoving;								//Boolean to check if enemies are moving.
		private bool doingSetup = true;                         //Boolean to check if we're setting up board, prevent Player from moving during setup.

		internal bool isGameWin = false;
		internal bool isDead = false;
		
		
		//Awake is always called before any Start functions
		void Awake()
		{
            //Check if instance already exists
            if (instance == null)

                //if not, set instance to this
                instance = this;

            //If instance already exists and it's not this:
            else if (instance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);	
			
			//Sets this to not be destroyed when reloading scene
			DontDestroyOnLoad(gameObject);
			
			//Assign enemies to a new List of Enemy objects.
			enemies = new List<Enemy>();

			//Get a component reference to the attached SavePrefData script
			savePrefScript = GetComponent<SavePrefData>();

			//Get a component reference to the attached BoardManager script
			//--boardScript = GetComponent<BoardManager>();

			//Call the InitGame function to initialize the first level 
			InitGame();
		}

        //this is called only once, and the paramter tell it to be called only after the scene was loaded
        //(otherwise, our Scene Load callback would be called the very first load, and we don't want that)
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static public void CallbackInitialization()
        {
            //register the callback to be called everytime the scene is loaded
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        //This is called each time a scene is loaded.
        static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
			print("OnSceneLoaded " + instance.level);
			instance.level++;
			instance.InitGame();
        }

		
		//Initializes the game for each level.
		void InitGame()
		{
			print("Init=" + level);
			//if(level> LevelBoardList.Count)
			//{
			//	level = 1;
			//}
			

			//Get a reference to our image LevelImage by finding it by name.
			levelImage = GameObject.Find("LevelImage");

			//Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
			levelText = GameObject.Find("LevelText").GetComponent<Text>();
			
			scoreText = GameObject.Find("ScoreBoard").GetComponent<Text>();


			if (GameWin())
			{
				level--;
				GameOver();
				return;
			}
			//else
			//{
			//	instance.level++;
			//}

			boardScript = LevelBoardList[level-1].GetComponent<BoardManager>();

			//While doingSetup is true the player can't move, prevent player from moving while title card is up.
			doingSetup = true;

			//Set the text of levelText to the string "Day" and append the current level number.
			levelText.text = "Day " + level;

			scoreText.text = "";


			//Set levelImage to active blocking player's view of the game board during setup.
			if(levelImage)
				levelImage.SetActive(true);

			//Call the HideLevelImage function with a delay in seconds of levelStartDelay.
			Invoke("HideLevelImage", levelStartDelay);
			
			//Clear any Enemy objects in our List to prepare for next level.
			enemies.Clear();
			
			//Call the SetupScene function of the BoardManager script, pass it current level number.
			boardScript.SetupScene(level);
			
		}
		
		
		//Hides black image used between levels
		void HideLevelImage()
		{
			//Disable the levelImage gameObject.
			levelImage.SetActive(false);
			
			//Set doingSetup to false allowing player to move again.
			doingSetup = false;
		}
		
		//Update is called every frame.
		void Update()
		{
			//Check that playersTurn or enemiesMoving or doingSetup are not currently true.
			if(playersTurn || enemiesMoving || doingSetup)
				
				//If any of these are true, return and do not start MoveEnemies.
				return;
			
			//Start moving enemies.
			StartCoroutine (MoveEnemies ());
		}
		
		//Call this to add the passed in Enemy to the List of Enemy objects.
		public void AddEnemyToList(Enemy script)
		{
			//Add Enemy to List enemies.
			enemies.Add(script);
		}
		
		public bool GameWin()
		{
			if (level > LevelBoardList.Count)
			{
				isGameWin = true;
			}
			else
			{
				isGameWin = false;
			}

			return isGameWin;
		}

		void saveScore()
		{
			int prefVal = savePrefScript.loadPrefs(SavePrefData.savePrefInstance.scoreKey);

			if (prefVal < level)
				SavePrefData.savePrefInstance.SavePrefs(SavePrefData.savePrefInstance.scoreKey, level);
		}
		//GameOver is called when the player reaches 0 food points
		public void GameOver()
		{

			print("GameOver " + level);

			saveScore();

			int val = savePrefScript.loadPrefs(SavePrefData.savePrefInstance.scoreKey);

			if (isGameWin)
			{
				levelText.text = "You Win !!!";
			}
			else if(isDead)
			{
				//Set levelText to display number of levels passed and game over message
				levelText.text = "After " + level + " days, you starved.";
			}


			scoreText.text = "Score Board:\n\n Score: "+(level)+" Days. \n Best: "+ val + " Days.";

			//Enable black background image gameObject.
			levelImage.SetActive(true);
			
			//Disable this GameManager.
			enabled = false;
		}
		
		//Coroutine to move enemies in sequence.
		IEnumerator MoveEnemies()
		{
			//While enemiesMoving is true player is unable to move.
			enemiesMoving = true;
			
			//Wait for turnDelay seconds, defaults to .1 (100 ms).
			yield return new WaitForSeconds(turnDelay);
			
			//If there are no enemies spawned (IE in first level):
			if (enemies.Count == 0) 
			{
				//Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
				yield return new WaitForSeconds(turnDelay);
			}
			
			//Loop through List of Enemy objects.
			for (int i = 0; i < enemies.Count; i++)
			{
				//Call the MoveEnemy function of Enemy at index i in the enemies List.
				enemies[i].MoveEnemy ();
				
				//Wait for Enemy's moveTime before moving next Enemy, 
				yield return new WaitForSeconds(enemies[i].moveTime);
			}
			//Once Enemies are done moving, set playersTurn to true so player can move.
			playersTurn = true;
			
			//Enemies are done moving, set enemiesMoving to false.
			enemiesMoving = false;
		}
	}
}

