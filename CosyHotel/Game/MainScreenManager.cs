using System;
using Screens.Handling;
using Screens.Menus;

namespace CosyHotel.Game
{
    /// <summary>
    /// MainScreenManager, handles the top level screens of the game
    /// </summary>
    public class MainScreenManager : ScreenHandler
    {
        /// <summary>
        /// Main Screen Manager constructor
        /// </summary>
        public MainScreenManager(MainMenu startScreen, SettingsMenu settingsScreen, GameMain gameScreen)
        {

            //main menu
            startScreen.Play += (_, _) => ChangeScreen(gameScreen);
            startScreen.Settings += (_, _) => ChangeScreen(settingsScreen);
            startScreen.Exit += (_, _) => Environment.Exit(0); //exit game

            //settings menu
            settingsScreen.Back += (_, _) => ChangeScreen(startScreen);

            //game menu
            gameScreen.Back += (_, _) => ChangeScreen(startScreen);

            ChangeScreen(startScreen);
        }
    }
}
