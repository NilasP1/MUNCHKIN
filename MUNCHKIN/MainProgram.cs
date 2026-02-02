using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN
{
    internal class MainProgram
    {
        static int numberOfPlayers;
        static List<Player> players = new List<Player>();
        public static void Run()
        {
            Console.WriteLine("Hello and welcome to Munchkin! How many players will be playing today?");
            numberOfPlayers = int.Parse(Console.ReadLine());
            
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.WriteLine($"Enter the name of player {i + 1}:");
                string playerName = Console.ReadLine();
                players.Add(new Player { Name = playerName });
            }

            Console.WriteLine("All players have been registered. Let the game begin!");

            if(Console.ReadKey().Key == ConsoleKey.C)
            {
                CheckOnPlayers();
            }
            else if(Console.ReadKey().Key == ConsoleKey.D)
            {
                ShowBothDecks();
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
            DoorCard doorCard = new DoorCard();
            TreasureCard treasureCard = new TreasureCard();
            Console.WriteLine("Door Deck:");
            foreach (var card in doorCard.doorDeck)
            {
                if (card is MonsterCard monster)
                {
                    Console.WriteLine($"Monster: {monster.MonsterName}, Level: {monster.MonsterLevel}");
                }
                else if (card is CurseCard)
                {
                    Console.WriteLine("Curse Card");
                }
                else if (card is RaceCard)
                {
                    Console.WriteLine("Race Card");
                }
                else if (card is ClassCard)
                {
                    Console.WriteLine("Class Card");
                }
            }

            Console.WriteLine("\nTreasure Deck:");
            foreach (var card in treasureCard.treasureDeck)
            {
                if(card is EquipmentCard equipment)
                {
                    Console.WriteLine($"Equipment: {equipment.EqupipmentName}, Equipment slot: {equipment.EquipmentSlot}, Battle bonus: {equipment.BattleBonus}");
                }
                else if(card is OneShotCard)
                {
                    Console.WriteLine("OneShotCard");
                }
            }
        }
    }
}
