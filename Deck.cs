using System;
using System.Collections.Generic;
using System.Linq;

namespace DSI.Deck
{
    public class Deck
    {
        public List<Card> CardSet; //{ get; set; }

        //Random Number generation variables
        private static readonly Random rand = new Random();
        private static readonly object syncLock = new object();

        /// <summary>
        /// Generates random number within the range of min to max-1
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>Random Number Integer</returns>
        public int RandomNumber(int min, int max)
        {
            lock(syncLock)
            {
                return rand.Next(min, max);
            }
        }


        /// <summary>
        /// Deck Constructor
        /// </summary>
        public Deck()
        {
            CardSet = new List<Card>();
            for (int i = 0; i < 4; i++)         //Suit values 0-3 (0-clubs, 1-diamonds, 2-hearts, 3-spades)
            {
                for (int j = 1; j < 14; j++)    //Card values 1-13 (values ordered from 2 to Ace, Ace is high)
                {
                    CardSet.Add(new Card(i, j));
                }
            }
        }

        /// <summary>
        /// Shuffle the deck.
        /// </summary>
        public void Shuffle()
        {
            for (int i = 0; i < CardSet.Count; i++)
            {
                //FisherYates in-place shuffle
                var temp = CardSet[i];
                var index = RandomNumber(0, CardSet.Count);
                CardSet[i] = CardSet[index];
                CardSet[index] = temp;
            }
        }


        /// <summary>
        /// Draw a the specified number of cards from the deck.
        /// </summary>
        /// <param name="howMany">how many cards to draw.</param>
        /// <returns>the list of drawn cards</returns>
        public List<Card> Draw(int howMany)
        {
            List<Card> userHand = new List<Card>();
                    
            for (int i = 0; i < howMany; i++)
            {
                int index = RandomNumber(0, CardSet.Count);

                userHand.Add(new Card((int)CardSet[index].CardSuit, (int)CardSet[index].CardValue));
                CardSet.RemoveAt(index);
            }

            return userHand;
        }

        /// <summary>
        /// Function asks user for howMany cards to draw and validates input
        /// </summary>
        /// <returns>valid value for howMany cards to draw</returns>
        public int sizeOfHand()
        {
            int numCards = 0;
            bool validateInput = false;

            Console.WriteLine("Input the number of cards you wish to draw: ");
            while (validateInput == false)
            {
                validateInput = int.TryParse(Console.ReadLine(), out numCards);
                if (!validateInput)
                {
                    Console.WriteLine("ERROR: Input not valid. Try Again.");
                    Console.WriteLine("Input Number: ");
                }
                else
                {
                    if (numCards > 52 || numCards < 1)
                    {
                        validateInput = false;
                        Console.WriteLine("ERROR: Input not between 1 and 52. Try Again.");
                        Console.WriteLine("Input Number: ");
                    }
                    else
                        validateInput = true;
                }
            }

            return numCards;
        }


        /// <summary>
        /// Sort cards by Suit then by Value
        /// </summary>
        /// <param name="userHand">List of cards drawn</param>
        /// <returns>cards that were drawn</returns>
        public List<Card> sortCards(List<Card> userHand)
        {
            List<Card> sortedHand = userHand.OrderBy(CardSuit => CardSuit.CardSuit).ThenBy(CardSuit => CardSuit.CardValue).ToList();
            return sortedHand;
        }

        /// <summary>
        /// Draw the specified number of cards from the deck and return them in sorted order (first by suit, then by face value).
        /// </summary>
        /// <param name="howMany">how many cards to draw.</param>
        /// <returns>a list of drawn cards, sorted by suit and face value (rank)</returns>
        public List<Card> DrawSorted(int howMany)
        {
            Random rand = new Random();
            List<Card> userHand = new List<Card>();
   
            for (int i = 0; i < howMany; i++)
            {
                int index = RandomNumber(0, CardSet.Count);

                userHand.Add(new Card((int)CardSet[index].CardSuit, (int)CardSet[index].CardValue));
                CardSet.RemoveAt(index);
            }

            userHand = sortCards(userHand);

            return userHand;
        }
    }
}
