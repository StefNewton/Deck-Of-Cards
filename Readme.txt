*******************************************************
*  Name      :  Stefani Moore                      
*  Date      :  Feb. 4, 2017
*******************************************************


                 Read Me


*******************************************************
*  Description of the program
*******************************************************

Using C#, this program is a representation of a deck of 
cards. Unit tests have been created to test certain
functions within the program.

*******************************************************
*  Source files
*******************************************************

Name:  Card.cs
   Contains a main function for Console Application execution and
   defines a Card instance as having both a suit and a value.
   public enum CardSuit and CardValue included in this file
   to define values for both the CardSuit's and CardValue's.

Name:  Deck.cs
   Contains the definition for the Deck Class, which holds
   a list of Cards. (List chosen for its similarity to the
   c++ vector and ease of use due to basic STL functions).
   Functions to shuffle, draw, drawsorted, and to determine
   how many cards to draw are found in this file.


*******************************************************
*  Unit Test Project Files
*******************************************************
 
Name:	DeckOfCardsTests.cs
   Contains Unit Tests for public functions found in 
   Deck.cs  
   
*******************************************************
*  Circumstances of programs
*******************************************************

   The program runs successfully.  
   
   The program was developed and tested on Microsoft Visual
   Studios Community 2015. 


*******************************************************
*  How to build and run the program
*******************************************************

1. Uncompress the DeckOfCards Folder and place in the 
   Visual Studios Projects folder

2. Open the DeckOfCards project file in Visual Studios
   Example: File->Open->Project/Solution-> Find Projects Folder -> Select DeckOfCards
		-> Select DeckOf Cards again ->  Select DeckOfCards Visual C# project File
   C:\Users\Stefani\Documents\Visual Studio 2015\Projects\DeckOfCards\DeckOfCards

3. Run Unit Tests 
   Open Test Explorer window (if not already open)
	- Test -> Windows -> Test Explorer
   Select Run All

4. To Run as a Console Application
   Make sure Card.cs is open in Visual Studios tab is selected
   	-Hit F5 or Debug -> Start Debugging
 
  Note: Step 4 will only work if output type is 'Console Application'
  To change output type:
	- Project -> DeckOfCards Properties (Alt F7) -> Change Output type: to Console Application -> Save
