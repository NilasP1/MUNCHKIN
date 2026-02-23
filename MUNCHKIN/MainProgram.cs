using System;
using System.Collections.Generic;

namespace MUNCHKIN
{
    internal class MainProgram
    {
        static int numberOfPlayers;
        static List<Player> players = new List<Player>();

        static DoorDeck doorDeck = new DoorDeck();
        static TreasureDeck treasureDeck = new TreasureDeck();

        public static void Run()
        {
            Console.WriteLine("Hello and welcome to Munchkin! How many players will be playing today?");
            if (!int.TryParse(Console.ReadLine(), out numberOfPlayers) || numberOfPlayers <= 0)
            {
                Console.WriteLine("Invalid number of players.");
                return;
            }

            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.WriteLine($"Enter the name of player {i + 1}:");
                string playerName = Console.ReadLine() ?? $"Player{i + 1}";
                // Initialize lists to avoid null warnings
                players.Add(new Player
                {
                    Name = playerName,
                    equipedEquipmentCards = new List<EquipmentCard>(),
                    cardsOnHand = new List<Card>()
                });
            }

            // Create decks via the deck objects (do not instantiate abstract base card types)
            doorDeck.CreateDeck(50);      // pick an appropriate amount or implement deck factory logic
            treasureDeck.CreateDeck(50);

            Console.WriteLine("All players have been registered. Let the game begin!");
            Console.WriteLine("Press C to check players, D to show decks.");

            ConsoleKey key = Console.ReadKey().Key;

            // Main game loop to allow user interaction until they choose to quit
            while (true)
            {
                Console.WriteLine("Press C to check players, D to show decks, or Q to quit.");
                key = Console.ReadKey().Key;
                Console.WriteLine();
                if (key == ConsoleKey.C)
                {
                    CheckOnPlayers();
                }
                else if (key == ConsoleKey.D)
                {
                    ShowBothDecks();
                }
                else if (key == ConsoleKey.Q)
                {
                    Console.WriteLine("Thanks for playing Munchkin! Goodbye!");
                    break;
                }
            }
        }

        static public void CheckOnPlayers()
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.WriteLine($"Player {i + 1}: {players[i].Name}, Level: {players[i].level}");
            }
        }

        static public void ShowBothDecks()
        {
            Console.WriteLine("Door Deck:");

            // Use the deck field name declared in DoorDeck (signature shows 'cards')
            foreach (var card in doorDeck.cards)
            {
                if (card is MonsterCard monster)
                {
                    Console.WriteLine($"Monster: {monster.MonsterName}, Level: {monster.MonsterLevel}");
                }
                else if (card is CurseCard cursecard)
                {
                    Console.WriteLine($"Curse Card: {cursecard.CurseName}, Effect: {cursecard.CurseEffect}");
                }
                else if (card is RaceCard racecard)
                {
                    Console.WriteLine($"Race Card: {racecard.RaceName}, Ability: {racecard.RaceAbility}");
                }
                else if (card is ClassCard classcard)
                {
                    Console.WriteLine($"Class Card: {classcard.ClassName}, Ability: {classcard.ClassAbility}");
                }
            }

            Console.WriteLine("\nTreasure Deck:");

            foreach (var card in treasureDeck.cards)
            {
                if (card is EquipmentCard equipment)
                {
                    Console.WriteLine($"Equipment: {equipment.EqupipmentName}, Slot: {equipment.EquipmentSlot}, Bonus: {equipment.BattleBonus}");
                }
                else if (card is OneShotCard oneshotcard)
                {
                    Console.WriteLine($"OneShot Card: {oneshotcard.OneShotName}, Effect: {oneshotcard.OneShotEffect}");
                }
            }
        }
    }
}
