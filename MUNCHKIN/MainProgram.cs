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
                    equipedEquipmentCards = new List<EquipmentCard>(),
                    cardsOnHand = new List<Card>()
                });
            }

            doorDeck.CreateDeck(50);
            treasureDeck.CreateDeck(50);

            foreach (var player in players)
            {
                while (player.cardsOnHand.Count < 8 && doorDeck.cards.Count > 0)
                    player.cardsOnHand.Add(doorDeck.Draw());
            }

            Console.Clear();
            Console.WriteLine("Game setup complete!");
            Console.ReadKey();

            MainMenu();
        }

        static void MainMenu()
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

        static void StartTurnPhase()
        {
            int currentPlayerIndex = 0;

            while (true)
            {
                Console.Clear();
                var currentPlayer = players[currentPlayerIndex];

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
                        PlayCard(currentPlayer);
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

        static void ShowHand(Player player)
        {
            Console.WriteLine($"{player.Name}'s Hand:\n");

            if (player.cardsOnHand.Count == 0)
            {
                Console.WriteLine("(Empty)");
                return;
            }

            for (int i = 0; i < player.cardsOnHand.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {GetCardDetails(player.cardsOnHand[i])}");
                Console.WriteLine("-----------------------------------");
            }
        }

        static string GetCardDetails(Card card)
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
                return $"EQUIPMENT\nName: {e.EqupipmentName}\nSlot: {e.EquipmentSlot}\nBonus: +{e.BattleBonus}\nEffect: {e.specialEffect}";

            if (card is OneShotCard o)
                return $"ONE SHOT\nName: {o.OneShotName}\nEffect: {o.OneShotEffect}";

            if (card is TreasureCard t)
                return $"TREASURE\nGold Value: {t.GoldValue}";

            return card.GetType().Name;
        }

        static void PlayCard(Player player)
        {
            ShowHand(player);

            Console.Write("\nEnter card number to play: ");

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                index--;

                if (index >= 0 && index < player.cardsOnHand.Count)
                {
                    Card card = player.cardsOnHand[index];

                    Console.WriteLine("\nPlaying:");
                    Console.WriteLine(GetCardDetails(card));

                    if (card is EquipmentCard equipment)
                    {
                        player.equipedEquipmentCards.Add(equipment);
                        Console.WriteLine("Equipment equipped!");
                    }

                    player.cardsOnHand.RemoveAt(index);
                }
            }
        }

        static void DiscardCard(Player player)
        {
            ShowHand(player);

            Console.Write("\nEnter card number to discard: ");

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                index--;

                if (index >= 0 && index < player.cardsOnHand.Count)
                {
                    player.cardsOnHand.RemoveAt(index);
                    Console.WriteLine("Card discarded.");
                }
            }
        }

        static void KickOpenDoor(Player player)
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
                player.cardsOnHand.Add(drawn);
                Console.WriteLine("Card added to hand.");
            }
        }

        static void CheckOnPlayers()
        {
            foreach (var player in players)
                Console.WriteLine($"{player.Name} - Level {player.level}");
        }
    }
}