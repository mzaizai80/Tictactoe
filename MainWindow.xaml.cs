using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tictactoe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        /// <summary>
        /// Holds the current results of cellls in the game
        /// </summary>
        private Marktype[] mResult;

        /// <summary>
        /// True if it is a player 1's turn (X) or player 2's turn (O)
        /// </summary>

        private bool mPlayer1Turn;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool mGameEnded;


        #endregion



        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
        
        NewGame();

        }

        #endregion

        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>

        private void NewGame()
        {
            // Create a new blank array of free cells
            mResult = new Marktype[9];

            for (int i = 0; i < mResult.Length; i++)
                mResult[1] = Marktype.Free;

            // Make sure Player 1 starts yhe game
            mPlayer1Turn = true;

            // Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button=>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            // Make sure the game hasn't finished
            mGameEnded= false;

        }


        /// <summary>
        /// Handels a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The event of the click</param>

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to a button 

            var button = (Button)sender;

            // Find the buttons position in the array
            var colmun = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = colmun + ( row * 3);

            // Don't do anything if the cell already has a value in it
            if (mResult[index] != Marktype.Free)
                return;

            // Set the cell value based on which players turn it is
            /*if (mPlayer1Turn)
                mResult[index] = Marktype.Cross;
            else
                mResult[index] = Marktype.Nought;
            * Sortcut 
            */
            
            mResult[index] = mPlayer1Turn ? Marktype.Cross : Marktype.Nought;
            // 

            // Set button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            // Change nought to green
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Goldenrod;

            // Toggle the players trun
            /*
             if (mPlayer1Turn)
                mPlayer1Turn = false;
            else
                mPlayer1Turn= true;
            * Sortcut 
            */
            mPlayer1Turn ^= true;

            // Check for winner

            CheckForWinner();


        }


        /// <summary>
        /// Checks if ther is a winner of a 3 line straight
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void CheckForWinner()
        {

            #region Horizontal
            // check for horizontal wins
            // 
            //  -  Row 0 
            // 

            // var same= (mResult[0] & mResult[1] & mResult[2])== mResult[0];
            if (mResult[0] != Marktype.Free && (mResult[0] & mResult[1] & mResult[2]) == mResult[0])
            {
                // Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            // check for horizontal wins
            // 
            //  -  Row 1 
            // 
            if (mResult[3] != Marktype.Free && (mResult[3] & mResult[4] & mResult[5]) == mResult[3])
            {
                // Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            // check for horizontal wins
            // 
            //  -  Row 2 
            // 
            if (mResult[6] != Marktype.Free && (mResult[6] & mResult[7] & mResult[8]) == mResult[6])
            {
                // Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            #endregion

            #region Vetiical

            // check for vertical wins
            // 
            //  -  Column 0 
            // 
            if (mResult[0] != Marktype.Free && (mResult[0] & mResult[3] & mResult[6]) == mResult[0])
            {
                // Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            // check for vertical wins
            // 
            //  -  Column 1 
            // 
            if (mResult[1] != Marktype.Free && (mResult[1] & mResult[4] & mResult[7]) == mResult[1])
            {
                // Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            // check for vertical wins
            // 
            //  -  Column 2 
            // 
            if (mResult[2] != Marktype.Free && (mResult[2] & mResult[5] & mResult[8]) == mResult[2])
            {
                // Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button2_2.Background = Button2_2.Background = Button2_2.Background = Brushes.Green;
            }


            #endregion



            #region Diagonal

            // check for Diagonal wins
            // 
            //  -  Top Left - Bottom Right
            // 
            if (mResult[0] != Marktype.Free && (mResult[0] & mResult[4] & mResult[8]) == mResult[0])
            {
                // Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            // check for Diagonal wins
            // 
            //  -  Top Left - Bottom Right
            // 
            if (mResult[2] != Marktype.Free && (mResult[2] & mResult[4] & mResult[6]) == mResult[2])
            {
                // Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }


            #endregion

            #region No Winner
            // Check for no winner and full board
            if (!mResult.Any(result => result == Marktype.Free))
            {
                // Game ended
                mGameEnded =true;

                // Turn all cells Orange
                Container.Children.Cast<Button>().ToList().ForEach(Button =>
                {
                    Button.Background = Brushes.DarkOrchid;
                });
                
            }

            #endregion

        }


    }
}
