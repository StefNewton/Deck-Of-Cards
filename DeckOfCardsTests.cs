using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSI.Deck;
using System.Collections.Generic;

namespace DeckOfCards.Tests
{
    [TestClass]
    public class DeckTests
    {
        public Deck deckOfCards = new Deck();

        [TestMethod]
        public void DeckInstance_TestInstanceCardCount_Return52()
        {
            int cardCount = deckOfCards.CardSet.Count;
            Assert.AreEqual(52, cardCount);
        }

        [TestMethod]
        public void Shuffle_TestCardShuffle_ReturnDeckNotEqualToOriginal()
        {
            bool isDifferent = false;

            //New instances of new Deck will equal any other instance by length
            //and type. Shuffle will adjust the order of the list.
            Deck tempDeck = new Deck();
            
            tempDeck.Shuffle();

           for(int i = 0; i < tempDeck.CardSet.Count; i++)
            {
                if(tempDeck.CardSet[i].CardSuit != deckOfCards.CardSet[i].CardSuit)
                {
                    //If a suit at any position in tempDeck does not match that of deckOfCards,
                    //then we can state that the deck was successfully shuffled by atleast 1 card
                    isDifferent = true;
                    break;
                }
            }

           //If this test passes, then newDeck is different in order than that of deckOfCards
            Assert.IsTrue(isDifferent);
        }

        [TestMethod]
        public void Draw_TestCardCountDrawn_ReturnHandCountEqualToHowMany()
        {
            int count = 37; //Tested for 0, 5, 52, 15, 37
            List<Card> actual = deckOfCards.Draw(count);

            Assert.AreEqual(actual.Count, count);       //Correctly draws specified amount
            CollectionAssert.AllItemsAreUnique(actual); // All cards are unique
        }

        [TestMethod]
        public void DrawSorted_TestCardCountDrawn_ReturnCardCountEqualToHowMany()
        {
            int count = 37; //Tested for 0, 5, 52, 15, 37
            List<Card> actual = deckOfCards.DrawSorted(count);

            Assert.AreEqual(actual.Count, count);       //Correctly draws specified amount
            CollectionAssert.AllItemsAreUnique(actual); // All cards are unique
        }

        [TestMethod]
        public void sortCards_TestSort_ReturnSortedDeck()
        {
            Deck tempActual = new Deck();
            bool cardsMatch = false;

            List<Card> actual = new List<Card>() { new Card(2,1), new Card(0, 11), new Card(0,1), new Card(3,6) };
            // 2 of hearts, queen of clubs, 2 of clubs, 7 of spades
            

            List<Card> expected = new List<Card>() { new Card(0, 1), new Card(0, 11), new Card(2, 1), new Card(3, 6) };
            // 2 of clubs, queen of clubs, 2 of hearts, 7 of spades


            tempActual.CardSet = actual;
            actual = tempActual.sortCards(actual);

            for(int i = 0; i < actual.Count; i++)
            {
                if (actual[i].CardSuit == expected[i].CardSuit && actual[i].CardValue == expected[i].CardValue)
                    cardsMatch = true;
                else
                {
                    cardsMatch = false;
                    break;
                }

            }

            Assert.IsTrue(cardsMatch);
        }

        [TestMethod]
        public void randomNumber_TestUniformRandomness_ReturnStardardDev()
        {
            // TESTING RANDOMNESS using an example of a 'fair' die and statistical analysis
            // This is more of a test of independence / unbias by the random number generator

            // A die, like our range of cards is a discrete random variable where
            // any roll (or random number generated) will equal a 1,2,3,4,5, or 6
            // in this example we will be doing numOfTrials 'rolls' or random number generations
            // and any roll or generation has an equal chance of being a number 1-6

            //Observed frequecy for each side of the die will be
            // side : observed Freq --> 1 = one, 2 = two, and so on.
            //Expected frequency for a fair die with a sample of 100 is
            // 1 = 16.66, 2 = 16.66, and so on. 
            // With each event being independent.

            // In an effort to prove un-bias/independence by the random number generator I will use the
            // chi-squared test with a significance level of 0.05. 

            Deck temp = new Deck();
            int discreteRandomNumber = 0, one = 0, two = 0, three = 0, four = 0, five = 0, six = 0;
            int numOfTrials = 10000; //You can easily test any number of trials by changing numOfTrials

            for (int i = 0; i < numOfTrials; i++)
            {
                discreteRandomNumber = temp.RandomNumber(1, 7); //Generate numbers 1-6

                if (discreteRandomNumber == 1)
                    one++;
                else if (discreteRandomNumber == 2)
                    two++;
                else if (discreteRandomNumber == 3)
                    three++;
                else if (discreteRandomNumber == 4)
                    four++;
                else if (discreteRandomNumber == 5)
                    five++;
                else
                    six++;

            }
           
            double ChiCriticalValue= 11.07; // Using degree of freedom = (6-1) = 5 and significance level of .05 http://sites.stat.psu.edu/~mga/401/tables/Chi-square-table.pdf
            double expected = numOfTrials/6.0;

           //Chi-squared = SumOf[((observed - expected)^2)/expected]
            double totalChiSquared = (Math.Pow((one - expected), 2.0)/expected) + (Math.Pow((two - expected), 2.0) / expected) 
                + (Math.Pow((three - expected), 2.0) / expected) + (Math.Pow((four - expected), 2.0) / expected)
                + (Math.Pow((five - expected), 2.0) / expected) + (Math.Pow((six - expected), 2.0) / expected);

            //Asserting that if the totalChiSquared < ChiCriticalValue, then at a 0.05 level of significance (95%) we can accept the null hypothesis
            //and say that our random number generator is fair or unbiased.

            bool isBiased = true; //Assume first that the data set is biased
            if (totalChiSquared < ChiCriticalValue)
                isBiased = false; //If total < critical, then isBiased is false

            Assert.IsFalse(isBiased);
        }

    }
}
