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

            numberOfPlayers = MunchkinHelpers.PromptForValidPlayerAmount();

            MunchkinHelpers.InitializePlayers(numberOfPlayers, players);

            doorDeck.CreateDeck(95);
            treasureDeck.CreateDeck(73);

            MunchkinHelpers.GiveStartingCards(4, players, doorDeck, treasureDeck);

            Console.Clear();
            Console.WriteLine("Game setup complete!");
            Console.ReadKey();

            while (true)
            {
                var action = MunchkinHelpers.RunMainMenu(players);

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