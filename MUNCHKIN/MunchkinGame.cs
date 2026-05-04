using MUNCHKIN.Cards;
using MUNCHKIN.Cards.DoorCards;
using MUNCHKIN.Cards.TreasureCards;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

namespace MUNCHKIN
{
    public class MunchkinGame
    {
        private int numberOfPlayers;
        private List<Player> players = new List<Player>();

        private DoorDeck doorDeck;
        private TreasureDeck treasureDeck;
        private Random rand = new Random();

        public MunchkinGame()
        {
            doorDeck = new DoorDeck(rand);
            treasureDeck = new TreasureDeck(rand);
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Munchkin!");

            numberOfPlayers = MunchkinHelpers.PromptForValidPlayerAmount(); //Asks the user for the number of players and validates the input

            MunchkinHelpers.InitializePlayers(numberOfPlayers, players); //Initilizes the players

            doorDeck.CreateDeck(95); //Creates the door deck with 95 cards
            treasureDeck.CreateDeck(73); //Creates the treasure deck with 73 cards

            MunchkinHelpers.GiveStartingCards(4, players, doorDeck, treasureDeck); //Gives each player 4 cards from the door deck and 4 cards from the treasure deck

            Console.Clear();
            Console.WriteLine("Game setup complete!");
            Console.ReadKey();

            while (true) 
            {
                var action = MunchkinHelpers.RunMainMenu(players); //Uses a method to display and return the user's choice from the main menu

                switch (action) 
                {
                    case MainMenuAction.StartTurns:
                        MunchkinHelpers.StartTurnPhase(players, doorDeck, treasureDeck); 
                        break;
                    case MainMenuAction.Quit:
                        Environment.Exit(0); 
                        break;
                    case MainMenuAction.CheckPlayers:
                        Console.Clear();
                        MunchkinHelpers.DisplayPlayerInfo(players); 
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}