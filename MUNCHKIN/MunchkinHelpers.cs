using MUNCHKIN.Cards;
using MUNCHKIN.Cards.DoorCards;
using MUNCHKIN.Cards.TreasureCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN
{
    internal static class MunchkinHelpers
    {

        internal static void InitializePlayers(int number, List<Player> players)
        {
            for (int i = 0; i < number; i++)
            {
                Console.Clear();
                Console.Write($"Enter name for Player {i + 1}: ");
                string name = Console.ReadLine() ?? $"Player{i + 1}";

                players.Add(new Player
                {
                    Name = name,
                    Level = 1,
                    CardsOnHand = new List<Card>()
                });
            }
        }

        internal static void GiveStartingCards(int count, List<Player> players, DoorDeck doorDeck, TreasureDeck treasureDeck)
        {
            foreach (Player player in players)
            {
                for (int i = 0; i < count; i++)
                {
                    if (doorDeck.Cards.Count > 0)
                        player.CardsOnHand.Add(doorDeck.Draw());
                }

                for (int i = 0; i < count; i++)
                {
                    if (treasureDeck.Cards.Count > 0)
                        player.CardsOnHand.Add(treasureDeck.Draw());
                }
            }
        }

        internal static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("=== MAIN MENU ===");
            Console.WriteLine("C = Check players");
            Console.WriteLine("T = Start turns");
            Console.WriteLine("Q = Quit");
        }

        internal static MainMenuAction GetMainMenuAction()
        {
            var key = Console.ReadKey().Key;
            return key switch
            {
                ConsoleKey.C => MainMenuAction.CheckPlayers,
                ConsoleKey.T => MainMenuAction.StartTurns,
                ConsoleKey.Q => MainMenuAction.Quit,
                _ => MainMenuAction.None
            };
        }

        internal static bool StartMainMenuPhase(List<Player> players, DoorDeck doorDeck, TreasureDeck treasureDeck)
        {
            while (true)
            {
                DisplayMainMenu();
                var action = GetMainMenuAction();

                switch (action)
                {
                    case MainMenuAction.CheckPlayers:
                        Console.Clear();
                        DisplayPlayerInfo(players);
                        Console.ReadKey();
                        break;
                    case MainMenuAction.StartTurns:
                        StartTurnPhase(players, doorDeck, treasureDeck);
                        break;
                    case MainMenuAction.Quit:
                        return false;
                    case MainMenuAction.None:
                        Console.WriteLine("Invalid selection. Please try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        internal static void ShowTurnActions(Player currentPlayer)
        {
            Console.WriteLine($"--- {currentPlayer.Name}'s Turn ---");
            Console.WriteLine($"Level: {currentPlayer.Level}");
            Console.WriteLine();
            Console.WriteLine("M = Show hand");
            Console.WriteLine("L = Play card");
            Console.WriteLine("D = Kick open door");
            Console.WriteLine("R = Discard card");
            Console.WriteLine("N = Next player");
            Console.WriteLine("P = Previous player");
            Console.WriteLine("E = End turn phase");
            Console.WriteLine("C = Check players");
            Console.WriteLine("Q = Quit game");
        }

        internal static void StartTurnPhase(List<Player> players, DoorDeck doorDeck, TreasureDeck treasureDeck)
        {
            int currentPlayerIndex = 0;

            while (true)
            {
                Console.Clear();
                var currentPlayer = players[currentPlayerIndex];

                ShowTurnActions(currentPlayer);
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
                        PlayCard(currentPlayer, players, players.Count);
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D:
                        Console.Clear();
                        KickOpenDoor(currentPlayer, doorDeck);
                        Console.ReadKey();
                        break;

                    case ConsoleKey.R:
                        Console.Clear();
                        DiscardCard(currentPlayer);
                        Console.ReadKey();
                        break;

                    case ConsoleKey.N:
                        bool hasTooManyCards = currentPlayer.CardsOnHand.Count > 5;

                        // Only move to the next player if the current player doesn't have more than 5 cards in hand
                        currentPlayerIndex = !hasTooManyCards ? (currentPlayerIndex + 1) % players.Count : currentPlayerIndex;

                        if (hasTooManyCards)
                        {
                            Console.WriteLine("You have more than 5 cards in hand. Please discard down to 5 before ending your turn.");
                            Console.ReadKey();
                        }
                        break;

                    case ConsoleKey.P:
                        currentPlayerIndex = (currentPlayerIndex - 1 + players.Count) % players.Count;
                        break;

                    case ConsoleKey.E:
                        return;

                    case ConsoleKey.C:
                        Console.Clear();
                        DisplayPlayerInfo(players);
                        Console.ReadKey();
                        break;

                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        internal static void ShowHand(Player player)
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

        /// <summary>
        /// Returns a string with the details of the card, formatted based on its type.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        internal static string GetCardDetails(Card card)
        {
            switch (card)
            {
                case MonsterCard m:
                    return $"MONSTER\nName: {m.MonsterName}\nLevel: {m.MonsterLevel}";

                case CurseCard c:
                    return $"CURSE\nName: {c.CurseName}\nEffect: {c.CurseEffect}";

                case RaceCard r:
                    return $"RACE\nName: {r.RaceName}\nAbility: {r.RaceAbility}";

                case ClassCard cl:
                    return $"CLASS\nName: {cl.ClassName}\nAbility: {cl.ClassAbility}";

                case EquipmentCard e:
                    return $"EQUIPMENT\nName: {e.EqupipmentName}\nSlot: {e.Slot}\nBonus: +{e.BattleBonus}\nEffect: {e.specialEffect}";

                case OneShotCard o:
                    return $"ONE SHOT\nName: {o.OneShotName}\nEffect: {o.OneShotEffect}";

                case TreasureCard t:
                    return $"TREASURE\nGold Value: {t.GoldValue}";
            }

            return card.GetType().Name; // Fallback for unknown card types
        }

        internal static void PlayCard(Player player, List<Player> players, int numberOfPlayers)
        {
            ShowHand(player);
            Console.Write("\nEnter card number to play: ");
            Card? selectedCard = SelectCard(player);
            if (selectedCard == null)
            {
                Console.WriteLine("Invalid card selection.");
                return;
            }

            Console.WriteLine("\nPlaying:");
            Console.WriteLine(GetCardDetails(selectedCard));
            player.CardsOnHand.Remove(selectedCard);

            switch (selectedCard)
            {
                case EquipmentCard e:
                    player.EquippedItems[e.Slot] = e;
                    break;

                case CurseCard c:
                    Console.WriteLine("Who do you want to play the curse on?");
                    Player targetPlayer;

                    for (int i = 0; i < numberOfPlayers; i++)
                    {
                        Console.WriteLine($"{i + 1}. {players[i].Name}");
                    }

                    int selectedIndex = Console.ReadKey().KeyChar - '1';
                    if (selectedIndex >= 0 && selectedIndex < players.Count)
                    {
                        targetPlayer = players[selectedIndex];
                        c.ApplyCurseEffect(targetPlayer);
                    }
                    break;

                case OneShotCard o:
                    Console.WriteLine("One-shot card played");
                    // TODO: Implement one-shot effect logic here
                    break;

                case RaceCard r:
                    player.Race = r.RaceName;
                    break;

                case ClassCard cl:
                    player.PlayerClass = cl.ClassName;
                    break;

                case MonsterCard m:
                    Console.WriteLine($"Played a {m.MonsterName}");
                    //TODO: add logic for playing monster cards like for adding to someone else's fight or if you do not get a monster when opening a door you can play yourself.
                    break;
            }
        }

        internal static Card? SelectCard(Player player)
        {
            ShowHand(player);
            Console.Write("\nEnter card number to play: ");

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                index--;

                if (index >= 0 && index < player.CardsOnHand.Count)
                {
                    return player.CardsOnHand[index];
                }
            }

            return null;
        }

        internal static void DiscardCard(Player player)
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

        internal static void KickOpenDoor(Player player, DoorDeck doorDeck)
        {
            if (doorDeck.Cards.Count == 0)
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
                player.Level++;
                Console.WriteLine($"You are now Level {player.Level}");
            }
            else
            {
                player.CardsOnHand.Add(drawn);
                Console.WriteLine("Card added to hand.");
            }
        }

        internal static void DisplayPlayerInfo(List<Player> players)
        {
            foreach (var player in players)
                Console.WriteLine($"{player.Name} - Level {player.Level} - Race: {player.Race} - Class: {player.PlayerClass}");
        }

        internal static int PromptForValidPlayerAmount()
        {
            while (true)
            {
                Console.Write("How many players? ");
                string input = Console.ReadLine() ?? "";
                int numberOfPlayers = ValidatePlayerAmount(input);
                if (numberOfPlayers > 0)
                    return numberOfPlayers;
            }
        }

        internal static int ValidatePlayerAmount(string input)
        {
            if (!int.TryParse(input, out int numberOfPlayers) || numberOfPlayers <= 0)
            {
                Console.WriteLine("Invalid number of players.");
                return -1;
            }

            return numberOfPlayers;
        }
    }

    internal enum MainMenuAction
    {
        CheckPlayers,
        StartTurns,
        Quit,
        None
    }
}
