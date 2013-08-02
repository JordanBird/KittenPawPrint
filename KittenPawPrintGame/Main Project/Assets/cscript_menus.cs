﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cscript_navigation : MonoBehaviour 
{
	cscript_master master;
	
	Texture2D blankWhiteTexture = new Texture2D(1, 1);
	Texture2D blankBlackTexture = new Texture2D(1, 1);
	
	//Main Menu Variables
	public GUISkin game;
	
	Game[] games = new Game[3];
	
	int position = 0;
	
	//Create Game Variables
	public GUISkin correctAnswer;
	public GUISkin incorrectAnswer;
	
	string gameName = "Add Game Name Here";
	string authorName = "Add Author Here";
	string mulitpleChoiceQuestion = "Add Multiple Choice Question Here";
	
	int selectedCreateGame = 0;
	
	List<Answer> answers = new List<Answer>();

	public void Init(cscript_master m)
	{
		master = m;
	}
	
	// Use this for initialization
	void Start ()
	{
		blankWhiteTexture.SetPixel (0, 0, Color.white);
		blankWhiteTexture.Apply();
			
		blankBlackTexture.SetPixel (0, 0, Color.black);
		blankBlackTexture.Apply();
		
		correctAnswer = Resources.Load ("GUI Skins/GUISkin_Correct_Answer") as GUISkin;
		incorrectAnswer = Resources.Load ("GUI Skins/GUISkin_Incorrect_Answer") as GUISkin;
		game = Resources.Load ("GUI Skins/GUISkin_Main_Menu_Games") as GUISkin;
		
		games[0] = new Game("Multiplication Football", "Top Notch", "Football", "Which number is a multiple of 4?", null);
		games[1] = new Game("Fly to the Noun", "Top Notch", "Plane", "Which words are nouns?", null);
		games[2] = new Game("Word Train", "Top Notch", "Train", "Which objects begin with the letter S?", null);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnGUI()
	{
		switch (master.gameState)
		{
			case cscript_master.GameState.MainMenu:
				MainMenuGUI ();
				break;
			case cscript_master.GameState.Help:
				HelpGUI ();
				break;
			case cscript_master.GameState.About:
				AboutGUI ();
				break;
			case cscript_master.GameState.CreateGame:
				CreateGameGUI ();
				break;
			case cscript_master.GameState.Playing:
				break;
				
		}
	}
	
	private void MainMenuGUI()
	{
		GUI.Label(new Rect(Screen.width / 2 - 50, 10, 100, 50), "Main Menu");
		
		if (GUI.Button (new Rect(10, 10, 100, 30), "About"))
			master.gameState = cscript_master.GameState.About;
		
		if (GUI.Button (new Rect(Screen.width - 110, 10, 100, 30), "Help"))
			master.gameState = cscript_master.GameState.Help;
		
		if (GUI.Button (new Rect(Screen.width / 2 - 50, Screen.height - 40, 100, 30), "Create Game"))
			master.gameState = cscript_master.GameState.CreateGame;
		
		float inc = (Screen.width - 40) / 3;
		
		//Playable Games
		
		for (int i = 0; i < 3; i++)
		{
			GUI.DrawTexture (new Rect(i * inc + 9 + (i * 10), 59, inc + 2, Screen.height - 178), blankBlackTexture);
			GUI.DrawTexture (new Rect(i * inc + 10 * (i + 1), 60, inc, Screen.height - 180), blankWhiteTexture);
			
			GUI.Label (new Rect(i * inc + 10 * (i + 1), 60, inc, Screen.height - 180), games[i].name, game.label);
			GUI.Label (new Rect(i * inc + 10 * (i + 1), Screen.height - 160, inc, 20), "By " + games[i].author, game.label);
		}
	}
	
	private void HelpGUI()
	{
		GUI.Label(new Rect(Screen.width / 2 - 50, 10, 100, 50), "Help");
		
		if (GUI.Button (new Rect(10, 10, 100, 30), "< Back"))
			master.gameState = cscript_master.GameState.MainMenu;
	}
	
	private void AboutGUI()
	{
		GUI.Label(new Rect(Screen.width / 2 - 50, 10, 100, 50), "About");
		
		if (GUI.Button (new Rect(10, 10, 100, 30), "< Back"))
			master.gameState = cscript_master.GameState.MainMenu;
	}
	
	private void CreateGameGUI()
	{
		GUI.Label(new Rect(Screen.width / 2 - 60, 10, 120, 50), "Create your Game");
		
		if (GUI.Button (new Rect(10, 10, 100, 30), "< Back"))
			master.gameState = cscript_master.GameState.MainMenu;
		
		if (GUI.Button (new Rect(Screen.width - 110, 10, 100, 30), "Help"))
			master.gameState = cscript_master.GameState.Help;
		
		gameName = GUI.TextField (new Rect((Screen.width / 4 - 10) / 2, 60, Screen.width / 4, 20), gameName);
		authorName = GUI.TextField (new Rect((Screen.width / 4 - 10) * 2.5f, 60, Screen.width / 4, 20), authorName);
		
		GUIContent[] g = new GUIContent[3];
		
		g[0] = new GUIContent("Football");
		g[1] = new GUIContent("Plane");
		g[2] = new GUIContent("Train");
		
		float centre = (Screen.width / 4);
		
		selectedCreateGame = GUI.SelectionGrid (new Rect(centre, 100, Screen.width / 2 - 10, 60), selectedCreateGame, g, 3);
		
		if (GUI.Button (new Rect(Screen.width / 2 - 75, 170, 150, 40), "Add Background"))
		{
			//Add Background Code	
		}
		
		mulitpleChoiceQuestion = GUI.TextArea (new Rect((Screen.width / 4 - 10) * 1.5f, 220, Screen.width / 4, 20), mulitpleChoiceQuestion);
		
		//Add Answer System
		int position = 0;
		
		for (int i = 0; i < answers.Count; i++)
		{
			GUISkin temp;
			
			if (answers[i].correct == true)
				temp = correctAnswer;
			else
				temp = incorrectAnswer;
			
			if (GUI.Button (new Rect(10 * (i + 1) + i * 100, 250, 100, 100), answers[i].text, temp.button))
			{
				if (answers[i].correct == true)
					answers[i].correct = false;
				else
					answers[i].correct = true;
			}
			
			position++;
		}
		
		if (GUI.Button (new Rect(10 * (position + 1) + position * 100, 250, 100, 100), "Add Answer"))
			answers.Add (new Answer(true, "Answer: " + position));
		
		GUI.Label (new Rect(20, 360, Screen.width - 40, 20), "Tap on object once for incorrect answer, double tap for correct, flick away to delete");
		
		if (GUI.Button (new Rect(Screen.width / 2 - 25, 390, 50, 25), "Save"))
		{
			//Save Code	
		}
	}
}
