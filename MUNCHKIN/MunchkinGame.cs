using System;
using System.Collections.Generic;
using System.Numerics;

namespace MUNCHKIN
{
    public class MunchkinGame
    {
        int numberOfPlayers;
        List<Player> players = new List<Player>();

        private DoorDeck doorDeck;
        private TreasureDeck treasureDeck;
        Random rand = new Random();

        public MunchkinGame()
        {
            doorDeck = new DoorDeck(rand);
            treasureDeck = new TreasureDeck(rand);
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Munchkin!");
            Console.Write("How many players? ");

            if (!int.TryParse(Console.ReadLine(), out numberOfPlayers) || numberOfPlayers <= 0)
            {
                Console.WriteLine("Invalid number of players.");
                return;
            }

            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.Clear();
                Console.Write($"Enter name for Player {i + 1}: ");
                string name = Console.ReadLine() ?? $"Player{i + 1}";

                players.Add(new Player
                {
                    Name = name,
                    level = 1,
                    CardsOnHand = new List<Card>()
                });
            }

            doorDeck.CreateDeck(95);
            treasureDeck.CreateDeck(73);

            foreach (Player player in players)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (doorDeck.cards.Count > 0)
                        player.CardsOnHand.Add(doorDeck.Draw());
                } 
                
                for (int i = 0; i < 4; i++)
                {
                    if (treasureDeck.cards.Count > 0)
                        player.CardsOnHand.Add(treasureDeck.Draw());
                }
            } 

            Console.Clear();
            Console.WriteLine("Game setup complete!");
            Console.ReadKey();

            MainMenu();
        }

        private void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MAIN MENU ===");
                Console.WriteLine("C = Check players");
                Console.WriteLine("T = Start turns");
                Console.WriteLine("Q = Quit");

                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.C)
                {
                    Console.Clear();
                    CheckOnPlayers();
                    Console.ReadKey();
                }
                else if (key == ConsoleKey.T)
                {
                    StartTurnPhase();
                }
                else if (key == ConsoleKey.Q)
                {
                    Console.Clear();
                    Console.WriteLine("Goodbye!");
                    break;
                }
            }
        }

        private void StartTurnPhase()
        {
            int currentPlayerIndex = 0;

            while (true)
            {
                Console.Clear();
                var currentPlayer = players[currentPlayerIndex];
                var nextPlayer = players[(currentPlayerIndex + 1) % players.Count];

                Console.WriteLine($"--- {currentPlayer.Name}'s Turn ---");
                Console.WriteLine($"Level: {currentPlayer.level}");
                Console.WriteLine();
                Console.WriteLine("M = Show hand");
                Console.WriteLine("L = Play card");
                Console.WriteLine("D = Kick open door");
                Console.WriteLine("R = Discard card");
                Console.WriteLine("N = Next player");
                Console.WriteLine("P = Previous player");
                Console.WriteLine("E = End turn phase");
                Console.WriteLine("Q = Quit game");

                var key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.M:
                        Console.Clear();
                        ShowHand(currentPlayer);
                        Console.ReadKey();
                        break;

                    case ConsoleKey.L:
                        Console.Clear();
                        PlayCard(currentPlayer, nextPlayer);
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D:
                        Console.Clear();
                        KickOpenDoor(currentPlayer);
                        Console.ReadKey();
                        break;

                    case ConsoleKey.R:
                        Console.Clear();
                        DiscardCard(currentPlayer);
                        Console.ReadKey();
                        break;

                    case ConsoleKey.N:
                        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
                        break;

                    case ConsoleKey.P:
                        currentPlayerIndex = (currentPlayerIndex - 1 + players.Count) % players.Count;
                        break;

                    case ConsoleKey.E:
                        return;

                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private void ShowHand(Player player)
        {
            Console.WriteLine($"{player.Name}'s Hand:\n");

            if (player.CardsOnHand.Count == 0)
            {
                Console.WriteLine("(Empty)");
                return;
            }

            for (int i = 0; i < player.CardsOnHand.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {GetCardDetails(player.CardsOnHand[i])}");
                Console.WriteLine("-----------------------------------");
            }
        }

        private string GetCardDetails(Card card)
        {
            if (card is MonsterCard m)
                return $"MONSTER\nName: {m.MonsterName}\nLevel: {m.MonsterLevel}";

            if (card is CurseCard c)
                return $"CURSE\nName: {c.CurseName}\nEffect: {c.CurseEffect}";

            if (card is RaceCard r)
                return $"RACE\nName: {r.RaceName}\nAbility: {r.RaceAbility}";

            if (card is ClassCard cl)
                return $"CLASS\nName: {cl.ClassName}\nAbility: {cl.ClassAbility}";

            if (card is EquipmentCard e)
                return $"EQUIPMENT\nName: {e.EqupipmentName}\nSlot: {e.Slot}\nBonus: +{e.BattleBonus}\nEffect: {e.specialEffect}";

            if (card is OneShotCard o)
                return $"ONE SHOT\nName: {o.OneShotName}\nEffect: {o.OneShotEffect}";

            if (card is TreasureCard t)
                return $"TREASURE\nGold Value: {t.GoldValue}";

            return card.GetType().Name;
        }

        private void PlayCard(Player player, Player nextplayer)
        {
            ShowHand(player);

            Console.Write("\nEnter card number to play: ");

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                index--;

                if (index >= 0 && index < player.CardsOnHand.Count)
                {
                    Card card = player.CardsOnHand[index];

                    Console.WriteLine("\nPlaying:");
                    Console.WriteLine(GetCardDetails(card));

                    if (card is EquipmentCard equipment)
                    {
                        player.EquippedItems[equipment.Slot] = equipment;
                    }
                    else if (card is CurseCard curse)
                    {
                        Console.WriteLine("Who do you want to play the curse on?");
                        Player targetPlayer;

                        for (int i = 0; i < numberOfPlayers; i++)
                        {
                            Console.WriteLine($"{i+1}. {players[i].Name}");
                        }

                        int selectedIndex = Console.ReadKey().KeyChar - '1';
                        if (selectedIndex >= 0 && selectedIndex < players.Count)
                        {
                            targetPlayer = players[selectedIndex];
                            curse.ApplyCurseEffect(targetPlayer);
                        }
                    }

                    player.CardsOnHand.RemoveAt(index);
                }
            }
        }

        private void DiscardCard(Player player)
        {
            ShowHand(player);

            Console.Write("\nEnter card number to discard: ");

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                index--;

                if (index >= 0 && index < player.CardsOnHand.Count)
                {
                    player.CardsOnHand.RemoveAt(index);
                    Console.WriteLine("Card discarded.");
                }
            }
        }

        private void KickOpenDoor(Player player)
        {
            if (doorDeck.cards.Count == 0)
            {
                Console.WriteLine("Door deck is empty!");
                return;
            }

            Card drawn = doorDeck.Draw();

            Console.WriteLine("You drew:");
            Console.WriteLine(GetCardDetails(drawn));
            Console.WriteLine();

            if (drawn is MonsterCard monster)
            {
                Console.WriteLine("Monster fight!");
                Console.WriteLine("You defeat it automatically (for now).");
                player.level++;
                Console.WriteLine($"You are now Level {player.level}");
            }
            else
            {
                player.CardsOnHand.Add(drawn);
                Console.WriteLine("Card added to hand.");
            }
        }

        private void CheckOnPlayers()
        {
            foreach (var player in players)
                Console.WriteLine($"{player.Name} - Level {player.level}");
        }

        
    }
}