using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HelloWorld
{

    struct Player
    {
        public string name;
        public int health;
        public int damage;
        public int defense;

    }

    struct Item
    {
        public string name;
        public int statBoost;
        public int durability;
    }

    class Game
    {
        bool _gameOver = false;
        Player player1;
        Player player2;
        public int levelScaleMax = 0;
        Item greatSword;
        Item shortSword;
        //Run the game
        public void Run()
        {
            Start();

            while(_gameOver == false)
            {
                Update();
            }

            End();

        }
        //This function handles the battles for our ladder. roomNum is used to update the our opponent to be the enemy in the current room. 
        //turnCount is used to keep track of how many turns it took the player to beat the enemy
        bool StartBattle(int roomNum, ref int turnCount)
        {
            //initialize default enemy stats
            int enemyHealth = 0;
            int enemyAttack = 0;
            int enemyDefense = 0;
            string enemyName = "";
            //Changes the enemy's default stats based on our current room number. 
            //This is how we make it seem as if the player is fighting different enemies
            switch (roomNum)
            {
                case 0:
                    {
                        enemyHealth = 100;
                        enemyAttack = 20;
                        enemyDefense = 5;
                        enemyName = "Wizard";
                        break;
                    }
                case 1:
                    {
                        enemyHealth = 80;
                        enemyAttack = 30;
                        enemyDefense = 5;
                        enemyName = "Troll";
                        break;
                    }
                case 2:
                    {
                        
                        enemyHealth = 200;
                        enemyAttack = 40;
                        enemyDefense = 10;
                        enemyName = "Giant";
                        break;
                    }
            }

            //Loops until the player or the enemy is dead
            while(player1.health > 0 && enemyHealth > 0)
            {
                //Displays the stats for both charactersa to the screen before the player takes their turn
                PrintStats(player1.name, player1.health, player1.damage, player1.defense);
                PrintStats(enemyName, enemyHealth, enemyAttack, enemyDefense);

                //Get input from the player
                char input;
                GetInput(out input, "Attack", "Defend");
                //If input is 1, the player wants to attack. By default the enemy blocks any incoming attack
                if(input == '1')
                {
                    BlockAttack(ref enemyHealth, player1.damage, enemyDefense);
                    Console.Clear();
                    Console.WriteLine("You dealt " + (player1.damage - enemyDefense) + " damage.");
                    Console.Write("> ");
                    Console.ReadKey();
                    Console.Clear();

                    //After the player attacks, the enemy takes its turn. Since the player decided not to defend, the block attack function is not called.
                    player1.health -= enemyAttack;
                    Console.WriteLine(enemyName + " dealt " + enemyAttack + " damage.");
                    Console.Write("> ");
                    Console.ReadKey();
                    turnCount++;
                }
                //If the player decides to defend the enemy just takes their turn. However this time the block attack function is
                //called instead of simply decrementing the health by the enemy's attack value.
                else
                {
                    BlockAttack(ref player1.health, enemyAttack, player1.defense);
                    Console.WriteLine(enemyName + " dealt " + (enemyAttack - player1.defense) + " damage.");
                    Console.Write("> ");
                    Console.ReadKey();
                    turnCount++;
                    Console.Clear();
                }
                
                
            }
            //Return whether or not our player died
            return player1.health != 0;

        }
        //Decrements the health of a character. The attack value is subtracted by that character's defense
        void BlockAttack(ref int opponentHealth, int attackVal, int opponentDefense)
        {
            int damage = attackVal - opponentDefense;
            if(damage < 0)
            {
                damage = 0;
            }
            opponentHealth -= damage;
        }
        //Scales up the player's stats based on the amount of turns it took in the last battle

        
        void UpgradeStats(int turnCount)
        {
            //Subtract the amount of turns from our maximum level scale to get our current level scale
            int scale = levelScaleMax - turnCount;
            if(scale <= 0)
            {
                scale = 1;
            }
            player1.health += 10 * scale;
            player1.damage *= scale;
            player1.defense *= scale;
        }

        //Allows the player to manually improve stats
        void UpgradeStats(ref int health, ref int damage, ref int defense)
        {
            char input = ' ';
            while (input != '1' && input != '2' && input != '3')
            {
                Console.WriteLine("Welcome to the shop, traveler! What would you like?");
                Console.WriteLine("1. Increase Health");
                Console.WriteLine("2. Increase Damage");
                Console.WriteLine("3. Increase Defense");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '1':
                        {
                            health += 35;
                            Console.WriteLine("\nYou increased your health by 35 points.");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                    case '2':
                        {
                            damage += 16;
                            Console.WriteLine("\nYou increased your damage by 16 points.");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                    case '3':
                        {
                            defense += 20;
                            Console.WriteLine("\nYou increased your defense by 20 points");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("\nI'm sorry, I don't understand your choice. Please try again.");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                }
            }
        }

        //Gets input from the player
        //Out's the char variable given. This variables stores the player's input choice.
        //The parameters option1 and option 2 displays the players current chpices to the screen
        void GetInput(out char input,string option1, string option2, string query)
        {
            Console.WriteLine(query);
            //Initialize input
            input = ' ';
            //Loop until the player enters a valid input
            while(input != '1' && input != '2')
            {
                Console.WriteLine("1." + option1);
                Console.WriteLine("2." + option2);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
            }
                
        }
       
        void GetInput(out char input, string option1, string option2)
        {
            //Initialize input
            input = ' ';
            //Loop until the player enters a valid input
            while (input != '1' && input != '2')
            {
                Console.WriteLine("1." + option1);
                Console.WriteLine("2." + option2);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
            }
        }
        //Prints the stats given in the parameter list to the console
        void PrintStats(string name, int health, int damage, int defense)
        {
            Console.WriteLine("\n" + name);
            Console.WriteLine("Health: " + health);
            Console.WriteLine("Damage: " + damage);
            Console.WriteLine("Defense: " + defense);
        }

        void PrintStats(Player player)
        {
            Console.WriteLine("\n" + player1.name);
            Console.WriteLine("Health: " + player1.health);
            Console.WriteLine("Damage: " + player1.damage);
            Console.WriteLine("Defense: " + player1.defense);
        }

        //This is used to progress through our game. A recursive function meant to switch the rooms and start the battles inside them.
        void ClimbLadder(int roomNum)
        {
            //Displays context based on which room the player is in
            switch (roomNum)
            {
                case 0:
                    {
                        Console.WriteLine("A wizard blocks your path");
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("A troll stands before you");
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("A giant has appeared!");
                        break;
                    }
                default:
                    {
                        _gameOver = true;
                        return;
                    }
            }
            int turnCount = 0;
            //Starts a battle. If the player survived the battle, level them up and then proceed to the next room.
            if(StartBattle(roomNum, ref turnCount))
            {
                UpgradeStats(turnCount);
                ClimbLadder(roomNum + 1);
                Console.Clear();
            }
            _gameOver = true;

        }

        //Displays the character selection menu. 
        void SelectCharacter()
        {
            char input = ' ';
            //Loops until a valid option is choosen
            while(input != '1' && input != '2' && input != '3')
            {
                //Prints options
                Console.WriteLine("Welcome! Please select a character.");
                Console.WriteLine("1.Sir Kibble");
                Console.WriteLine("2.Gnojoel");
                Console.WriteLine("3.Joedazz");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                //Sets the players default stats based on which character was picked
                switch (input)
                {
                    case '1':
                        {
                            player1.name = "Sir Kibble";
                            player1.health = 120;
                            player1.defense = 10;
                            player1.damage = 20;
                            break;
                        }
                    case '2':
                        {
                            player1.name = "Gnojoel";
                            player1.health = 40;
                            player1.defense = 2;
                            player1.damage = 70;
                            break;
                        }
                    case '3':
                        {
                            player1.name = "Joedazz";
                            player1.health = 200;
                            player1.defense = 5;
                            player1.damage = 25;
                            break;
                        }
                    //If an invalid input is selected display and input message and input over again.
                    default:
                        {
                            Console.WriteLine("Invalid input. Press any key to continue.");
                            Console.Write("> ");
                            Console.ReadKey();
                            break;
                        }
                }
                Console.Clear();
            }
            //Prints the stats of the choosen character to the screen before the game begins to give the player visual feedback
            PrintStats(player1.name, player1.health, player1.damage, player1.defense);
            Console.WriteLine("Press any key to continue.");
            Console.Write("> ");
            Console.ReadKey();
            Console.Clear();
        }
        //Sets the values of the players
        void IntializeCharacter()
        {
            player1.name = "Player1";
            player1.defense = 10;
            player1.health = 100;

            player2.name = "Player 2";
            player2.defense = 10;
            player2.health = 100; 
        }

        //Choose between singleplayer or mutliplayer
        void GetModeChoice()
        {
            char input = ' ';
            GetInput(out input, "Singleplayer", "Multiplayer", "Select your game type.");
            if (input == '1')
            {
                Console.Clear();
                SelectCharacter();
                ClimbLadder(0);
            }
            else
            {
                Console.Clear();
                EquipItems();
                MultiplayerBattle();
            }
        }

        //Sets the vaules of items
        void IntializeItems()
        {
            greatSword.name = "GreatSword";
            greatSword.durability = 10;
            greatSword.statBoost = 15;

            shortSword.name = "ShortSword";
            shortSword.durability = 15;
            shortSword.statBoost = 10;
        }

        //Allows player to equip the items
        void EquipItems()
        {
            char input = ' ';
            GetInput(out input, "GreatSword", "ShortSword", "Player 1, choose a weapon.");
            if (input == '1')
            {
                player1.damage = 30;
            }
            else
            {
                player1.damage = 25;
            }
            Console.Clear();
            GetInput(out input, "GreatSword", "ShortSword", "Player 2, choose a weapon");
            if (input == '1')
            {
                player2.damage = 30;
            }
            else
            {
                player2.damage = 25;
            }
        }

        //Allows multiplayer battle mode
        void MultiplayerBattle()
        {
            while (player1.health > 0 && player2.health > 0)
            {
                PrintStats(player1);
                PrintStats(player2);

                char input;
                GetInput(out input, "Attack", "Defend", "Player one turn");
                if(input == '1')
                {
                    BlockAttack(ref player2.health, player1.damage, player2.defense);
                    Console.Clear();
                    Console.WriteLine("You dealt " + (player1.damage - player2.defense) + " damage.");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    BlockAttack(ref player1.health, player2.damage, player1.defense);
                    Console.WriteLine(player2.name + " dealt " + (player2.damage - player1.defense) + " damage.");
                    Console.ReadKey();
                    Console.Clear();
                }
                Console.Clear();
                if(player2.health <= 0)
                {
                    break;
                }
                PrintStats(player1);
                PrintStats(player2);

                GetInput(out input, "Attack", "Defend", "Player two turn");
                if(input == '1')
                {
                    BlockAttack(ref player1.health, player2.damage, player1.defense);
                    Console.Clear();
                    Console.WriteLine("You dealt " + (player2.damage - player1.defense) + " damage.");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    BlockAttack(ref player2.health, player1.damage, player2.defense);
                    Console.Clear();
                    Console.WriteLine(player2.name + " dealt " + (player1.damage - player2.defense) + " damage.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            _gameOver = true;
        }

        //Performed once when the game begins
        public void Start()
        {
            IntializeCharacter();
        }

        //Repeated until the game ends
        public void Update()
        {
            GetModeChoice();  
        }

        //Performed once when the game ends
        public void End()
        {
            //If the player died print death message
            if(player1.health <= 0)
            {
                Console.WriteLine("Failure");
                return;
            }
            //Print game over message
            Console.WriteLine("Congrats");
        }
    }
}
