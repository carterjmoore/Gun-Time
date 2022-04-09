# Gun Time
## Project Description
This project is a first-person platformer game with shooter and puzzler aspects, developed using Unity, and designed to be played on computers. In the game, the player breaks into a facility to steal the Time Gun, a gun that can speed up or slow down time for whatever it shoots. The player must then use this time gun to defeat different enemies and complete platforming puzzles in order to escape the facility. There are several types of shootable environment objects and enemies which can be sped up or slowed down using the gun. Each shootable entity has 7 states (normal, 3 slow states, 3 speed states), and will behave differently depending on its current state. Environmental objects will decay from speed/slow states back towards the normal state over time, while enemies will remain in their current state unless shot.

The game contains seven levels, and more may be added in the future if development continues past the project due date. These levels are all unlocked by default, but should be played in the order in which they appear in the level-select. There is a main menu that provides a level-select and options menu, and the options menu can be also accessed during the game by pausing with 'ESC'. The pause menu also allows the player to return to the main menu. Upon completing a level, you can either go directly to the next level, or go back to the main menu. Upon death, you are given the option to replay the level, or return to main menu.

This game was made as part of a university-level Intro to Video Game Development course.

## How to Play
The game can be played on the Unity Play website at: *add link here*
Note that the game was developed for play with a 16:9 aspect ratio, so using other aspect ratios may cause UI/menu elements to be displayed strangely.

Alternatively, the project can be opened in Unity, and either ran directly in the editor, or an executable can be created via File > Build Settings > Build.
Note that the project was originally developed in Unity 2019.4.\*, so opening the project in another version of Unity may cause issues.

## Project Organization
All important files for the project can be found in the `Assets` folder in the main directory. Important subdirectories include:

- `Assets/Scripts` Contains all the C# scripts for the project.
- `Assets/Prefabs` Contains all prefabs for the project.
- `Assets/Scenes` Contains all the scenes for the project, and all the lighting for those scenes.
- `Assets/Materials` Contains all the materials used for the project.
- `Assets/Sounds` and `Assets/Resources` Contain all the sound files used in the project.

## Contributors
- [Carter Moore](https://github.com/carterjmoore)
- [Samuel Bain](https://github.com/LeumasNaib)
- [Brilliant Nguyen](https://github.com/YourFrostyFriend)
