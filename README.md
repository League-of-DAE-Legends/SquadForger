# SquadForge - ARAM Team Builder for League of Legends

Resolve the no rerolls issue in custom ARAM games with this handy tool! This GitHub repository houses a C#-based solution that takes the teams participating in a custom ARAM match as input and dynamically generates a curated pool of 15 champions for each team. The generated champion pools are then seamlessly shared on Discord, facilitating efficient decision-making among team members.

## Features:

- **Champion Pool Generation:** Automatically creates a diverse pool of X amount of champions for each team participating in the custom ARAM game.
- **Discord Integration:** Streamlines communication by posting the generated champion pools directly to a designated Discord channel, allowing teams to review and deliberate on their choices.

## Project setup: Extending the functionality

If you would like to use SquadForger's Discord integration, you must integrate your own Discord Webhook into the project. You can achieve that by doing the following: 
1. Create a `PrivateData.config` file in your directory
   
![image](https://github.com/League-of-DAE-Legends/SquadForger/assets/88614889/35061fb2-1a2a-45b2-a43a-43352dea7959)

2. Add your `Webhook ID` to `PrivateData.config`

![image](https://github.com/League-of-DAE-Legends/SquadForger/assets/88614889/f72ea40e-4404-4511-84e8-a8817edd4b6d)

3. Set the `Copy to output directory` property to `Copy always`

![Gif3](https://github.com/League-of-DAE-Legends/SquadForger/assets/88614889/e61f07ed-4135-4d18-baa5-7efd4e6adb3e)

From this point on the project is ready to be extended :)

## Usage:

1. Input the participating teams.
2. Run the script to generate champion pools.
3. Discord integration automatically shares the pools.
4. Teams discuss and finalize their 5 chosen champions.
5. Enter the blind pick ARAM lobby and enjoy the game!
