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
        /// <summary>
        /// the method for initializing the players, it will ask for the name of each player and add them to the list of players.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="players"></param>
        internal static void InitializePlayers(int number, List<Player> players)
        {
            for (int i = 0; i < number; i++) // Loop through the number of players to initialize each one
            {
                Console.Clear();
                Console.Write($"Enter name for Player {i + 1}: ");
                string name = Console.ReadLine() ?? $"Player{i + 1}";

                // Create a new Player object with the entered name and default values, then add it to the players list
                players.Add(new Player
                {
                    Name = name,
                    Level = 1,
                    CardsOnHand = new List<Card>()
                });
            }
        }

        /// <summary>
        /// this method will give each player the starting cards, it will draw 4 cards from the door deck and 4 cards from the treasure deck and add them to the player's hand.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="players"></param>
        /// <param name="doorDeck"></param>
        /// <param name="treasureDeck"></param>
        internal static void GiveStartingCards(int count, List<Player> players, DoorDeck doorDeck, TreasureDeck treasureDeck)
        {
            foreach (Player player in players) // Loop through each player to give them their starting cards
            {
                for (int i = 0; i < count; i++) // Draw cards from the door deck and add them to the player's hand
                {
                    if (doorDeck.Cards.Count > 0)
                        player.CardsOnHand.Add(doorDeck.Draw());
                }

                for (int i = 0; i < count; i++) // Draw cards from the treasure deck and add them to the player's hand
                {
                    if (treasureDeck.Cards.Count > 0)
                        player.CardsOnHand.Add(treasureDeck.Draw());
                }
            }
        }

        /// <summary>
        /// Displays the main menu options
        /// </summary>
        internal static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("=== MAIN MENU ===");
            Console.WriteLine("C = Check players");
            Console.WriteLine("T = Start turns");
            Console.WriteLine("Q = Quit");
        }

        /// <summary>
        /// this method will read the user's input and return the corresponding action based on the key pressed
        /// </summary>
        /// <returns></returns>
        internal static MainMenuAction GetMainMenuAction()
        {
            var key = Console.ReadKey().Key; 

            // Use a switch expression to return the appropriate MainMenuAction based on the key pressed
            return key switch 
            {
                ConsoleKey.C => MainMenuAction.CheckPlayers,
                ConsoleKey.T => MainMenuAction.StartTurns,
                ConsoleKey.Q => MainMenuAction.Quit,
                _ => MainMenuAction.None
            };
        }

        /// <summary>
        /// this method will start the main menu phase, it will display the main menu and wait for the user's input.
        /// </summary>
        /// <param name="players"></param>
        /// <param name="doorDeck"></param>
        /// <param name="treasureDeck"></param>
        /// <returns></returns>
        internal static MainMenuAction RunMainMenu(List<Player> players)
        {
            while (true) 
            {
                DisplayMainMenu(); 
                var action = GetMainMenuAction(); 

                switch (action) 
                {
                    case MainMenuAction.CheckPlayers: 
                        return MainMenuAction.CheckPlayers;

                    case MainMenuAction.StartTurns: 
                        return MainMenuAction.StartTurns;

                    case MainMenuAction.Quit: 
                        return MainMenuAction.Quit;

                    case MainMenuAction.None: // If the user selects an invalid option, display an error message and prompt again
                        Console.WriteLine("Invalid selection. Please try again.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        /// <summary>
        /// this method will display the turn actions for the current player, it will show the player's name, level and the available actions they can take during their turn.
        /// </summary>
        /// <param name="currentPlayer"></param>
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

        /// <summary>
        /// this method will start the turn phase, it will loop through the players and allow them to take their turn
        /// </summary>
        /// <param name="players"></param>
        /// <param name="doorDeck"></param>
        /// <param name="treasureDeck"></param>
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
                    case ConsoleKey.M: // If the player presses "M", clear the console and show the player's hand
                        Console.Clear();
                        ShowHand(currentPlayer);
                        Console.ReadKey();
                        break;

                    case ConsoleKey.L: // If the player presses "L", clear the console and allow the player to play a card from their hand
                        Console.Clear();
                        PlayCard(currentPlayer, players, players.Count, null);
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D: // If the player presses "D", clear the console and allow the player to draw a card from the door deck and apply its effect
                        Console.Clear();
                        KickOpenDoor(currentPlayer, doorDeck);
                        Console.ReadKey();
                        break;

                    case ConsoleKey.R: // If the player presses "R", clear the console and allow the player to discard a card from their hand
                        Console.Clear();
                        DiscardCard(currentPlayer);
                        Console.ReadKey();
                        break;

                    case ConsoleKey.N: // If the player presses "N", End that player's turn and move to the next player
                        bool hasTooManyCards = currentPlayer.CardsOnHand.Count > 5;

                        // Only move to the next player if the current player doesn't have more than 5 cards in hand
                        currentPlayerIndex = !hasTooManyCards ? (currentPlayerIndex + 1) % players.Count : currentPlayerIndex;

                        if (hasTooManyCards)
                        {
                            Console.WriteLine("You have more than 5 cards in hand. Please discard down to 5 before ending your turn.");
                            Console.ReadKey();
                        }
                        break;

                    case ConsoleKey.P: // If the player presses "P", move to the previous player in the list
                        currentPlayerIndex = (currentPlayerIndex - 1 + players.Count) % players.Count;
                        break;

                    case ConsoleKey.E: // If the player presses "E", end the turn phase and return to the main menu
                        return;

                    case ConsoleKey.C: // If the player presses "C", clear the console and display the information of all players
                        Console.Clear();
                        DisplayPlayerInfo(players);
                        Console.ReadKey();
                        break;

                    case ConsoleKey.Q: // If the player presses "Q", clear the console, display a goodbye message, and exit the game
                        Environment.Exit(0);
                        break;
                }

                if (currentPlayer.Level >= 10) // Check if the current player has reached level 10 or higher, which means they win the game
                {
                    Console.Clear();
                    Console.WriteLine($"{currentPlayer.Name} wins the game!");
                    Console.ReadKey();
                    return;
                }
            }
        }

        /// <summary>
        /// this method will display the player's hand, it will show the name of each card and its details
        /// </summary>
        /// <param name="player"></param>
        internal static void ShowHand(Player player)
        {
            Console.WriteLine($"{player.Name}'s Hand:\n");

            if (player.CardsOnHand.Count == 0) // If the player has no cards in hand, display a message indicating that the hand is empty
            {
                Console.WriteLine("(Empty)");
                return;
            }

            for (int i = 0; i < player.CardsOnHand.Count; i++) // Loop through each card in the player's hand and display its details
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
                    return $"ONE SHOT\nName: {o.OneShotName}\nEffect: {o.Description}";
            }

            return card.GetType().Name; // Fallback for unknown card types
        }

        /// <summary>
        /// this method will allow the player to play a card from their hand, it will ask the player to select a card and then it will apply the effect of the card based on its type. 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="players"></param>
        /// <param name="numberOfPlayers"></param>
        internal static void PlayCard(Player player, List<Player> players, int numberOfPlayers, CombatState? combat)
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
                case EquipmentCard e: // If the card is an EquipmentCard
                    if (player.EquippedItems.ContainsKey(e.Slot) && player.EquippedItems[e.Slot] != null)
                    {
                        EquipmentCard? currentlyEquipped = player.EquippedItems[e.Slot]; // Get the currently equipped item in that slot
                        player.EquipmentBattleBonus -= currentlyEquipped.BattleBonus; 

                        Console.WriteLine($"Unequipped {currentlyEquipped.EqupipmentName} from {e.Slot} slot."); 
                    }

                    player.EquippedItems[e.Slot] = e; 
                    player.EquipmentBattleBonus += e.BattleBonus; 
                    break;

                case CurseCard c: // If the card is a CurseCard
                    Console.WriteLine("Who do you want to play the curse on?");
                    Player targetPlayer;

                    for (int i = 0; i < numberOfPlayers; i++) 
                    {
                        Console.WriteLine($"{i + 1}. {players[i].Name}");
                    }

                    int selectedIndex = Console.ReadKey().KeyChar - '1'; // Read the key pressed by the user, convert it to an index by subtracting 1
                    if (selectedIndex >= 0 && selectedIndex < players.Count) 
                    {
                        targetPlayer = players[selectedIndex]; 
                        c.ApplyCurseEffect(targetPlayer); 
                    }
                    break;

                case OneShotCard o: // If the card is a OneShotCard, check if there is an active combat to apply the one-shot effect
                    if (combat != null)
                    {
                        Console.WriteLine("One-shot card played");
                        ApplyOneShotEffect(o, combat); 
                    }
                    else
                    {
                        Console.WriteLine("One-shot cards can only be played during combat.");
                    }
                    break;

                case RaceCard r: // If the card is a RaceCard
                    player.Race = r.RaceName;
                    break;

                case ClassCard cl: // If the card is a ClassCard
                    player.PlayerClass = cl.ClassName; 
                    break;

                case MonsterCard m: // If the card is a MonsterCard
                    Console.WriteLine($"Played a {m.MonsterName}");
                    //TODO: add logic for playing monster cards like for adding to someone else's fight or if you do not get a monster when opening a door you can play yourself.
                    break;
            }
        }
        internal static void ApplyOneShotEffect(OneShotCard o, CombatState combat)
        {
            switch (o.TargetType) 
            {
                case OneShotTargetType.Player:
                    combat.PlayerBuff += o.Modifier; 
                    break;
                case OneShotTargetType.Enemy: 
                    combat.EnemyDebuff += o.Modifier; 
                    break;
                case OneShotTargetType.InstantWin: 
                    combat.AutoWin = true; 
                    break;
            }
        }

        /// <summary>
        /// this method will allow the player to select a card from their hand, it will ask the player to enter the number of the card they want to play and then it will return the selected card. If the player enters an invalid number, it will return null.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        internal static Card? SelectCard(Player player)
        {
            ShowHand(player);
            Console.Write("\nEnter card number to play: "); 

            if (int.TryParse(Console.ReadLine(), out int index)) // Read the player's input and try to parse it as an integer index
            {
                index--; 

                if (index >= 0 && index < player.CardsOnHand.Count) 
                {
                    return player.CardsOnHand[index]; 
                }
            }

            return null; // If the input is invalid (not a number or out of range), return null to indicate an invalid selection
        }

        /// <summary>
        /// this method will allow the player to discard a card from their hand, it will ask the player to enter the number of the card they want to discard and then it will remove the selected card from the player's hand. If the player enters an invalid number, it will do nothing.
        /// </summary>
        /// <param name="player"></param>
        internal static void DiscardCard(Player player)
        {
            ShowHand(player); 

            Console.Write("\nEnter card number to discard: "); 

            if (int.TryParse(Console.ReadLine(), out int index)) // Read the player's input and try to parse it as an integer index
            { 
                index--; 

                if (index >= 0 && index < player.CardsOnHand.Count) 
                {
                    player.CardsOnHand.RemoveAt(index); 
                    Console.WriteLine("Card discarded.");
                }
            }
        }

        /// <summary>
        /// this method will allow the player to kick open a door, it will draw a card from the door deck and then it will apply the effect of the card based on its type
        /// </summary>
        /// <param name="player"></param>
        /// <param name="doorDeck"></param>
        internal static void KickOpenDoor(Player player, DoorDeck doorDeck)
        {
            if (doorDeck.Cards.Count == 0) 
            {
                doorDeck.CreateDeck(95);
            }

            Card drawn = doorDeck.Draw(); 

            Console.WriteLine("You drew:");
            Console.WriteLine(GetCardDetails(drawn)); 
            Console.WriteLine();

            if (drawn is MonsterCard monster) // Check if the drawn card is a MonsterCard, if so, start combat with that monster
            {
                Console.WriteLine("Monster fight!");

                StartCombat(player, monster); 

                Console.WriteLine($"You are now Level {player.Level}");
            }
            else
            {
                player.CardsOnHand.Add(drawn); // If the drawn card is not a MonsterCard, add it to the player's hand
                Console.WriteLine("Card added to hand.");
            }
        }

        internal static void StartCombat(Player player, MonsterCard monster)
        {
            CombatState combat = new CombatState(); // Initialize a new CombatState object to keep track of the player's buffs, enemy debuffs, and auto-win status during combat

            while (true) 
            {
                Console.Clear();

                int playerStrength = player.Level + combat.PlayerBuff + player.EquipmentBattleBonus; 
                int monsterStrength = monster.MonsterLevel + combat.EnemyDebuff; 

                Console.WriteLine($"Fighting {monster.MonsterName}");
                Console.WriteLine($"Your strength: {playerStrength}");
                Console.WriteLine($"Monster strength: {monsterStrength}");
                Console.WriteLine();

                Console.WriteLine("P = Play card");
                Console.WriteLine("R = Give up");

                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.P) 
                {
                    PlayCard(player, new List<Player> { player }, 1, combat); 
                }
                else if (key == ConsoleKey.R)
                {
                    Console.WriteLine("\nYou get attacked");
                    monster.MonsterBadStuff(player);
                    return;
                }

                if (combat.AutoWin || playerStrength > monsterStrength) 
                {
                    Console.WriteLine("\nYou win!");
                    player.Level++;
                    Console.ReadKey();
                    return;
                }
            }
        }

        /// <summary>
        /// this method will display the information of all players, it will show the name, level
        /// </summary>
        /// <param name="players"></param>
        internal static void DisplayPlayerInfo(List<Player> players)
        {
            foreach (var player in players) // Loop through each player in the list and display their name, level, race, and class
                Console.WriteLine($"{player.Name} - Level {player.Level} - Race: {player.Race} - Class: {player.PlayerClass}");
        }

        /// <summary>
        /// this method will prompt the user to enter the number of players and then validate if it is a valid number
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// this method will validate the player's input for the number of players
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static int ValidatePlayerAmount(string input)
        {
            if (!int.TryParse(input, out int numberOfPlayers) || numberOfPlayers <= 0) 
            {
                Console.WriteLine("Invalid number of players.");
                Console.ReadKey();
                Console.Clear();
                return -1;
            }

            return numberOfPlayers;
        }
    }
    internal class CombatState // This class is used to keep track of the player's buffs, enemy debuffs, and auto-win status during combat
    {
        public int PlayerBuff { get; set; }
        public int EnemyDebuff { get; set; }
        public bool AutoWin { get; set; }
    }

    internal enum MainMenuAction // This enum is used to represent the different actions that can be selected from the main menu
    {
        CheckPlayers,
        StartTurns,
        Quit,
        None
    }
}
