# SquadForge - ARAM Team Builder for League of Legends

Resolve the no rerolls issue in custom ARAM games with this tool! This GitHub repository houses a C#-based solution that takes the teams participating in a custom ARAM match as input and dynamically generates a pool of 15 champions for each team. The generated champion pools are then displayed and can be shared on Discord.

## Features:

- **Champion Pool Generation:** Automatically generate X amount of champions for each team.
- **Discord Integration:** Post the generated champion pools directly to a designated Discord channel.

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

### 1. Input the participating teams.
This can be done manually, or by parsing a csv. Currently we have 1 simple version of CSV parser, but you can add your own parser by implementing ```ITeamNamesRepository``` interface.
This is what our ```CSVTeamParser.cs``` can handle:
![image](https://github.com/League-of-DAE-Legends/SquadForger/assets/88614889/11b3f6a5-50be-4df0-bb64-fa4879677bc5)

Very simple :)

![Gif3](https://github.com/League-of-DAE-Legends/SquadForger/assets/88614889/73ade216-d078-4258-b2ca-eb73419a3787)


### 2. Run the script to generate champion pools.
Here we have 2 options:

**Safe Generate**
- Uses a local json that contains info about the champions. (Up to date with patch 14.1.1)

**Custom Generate**
- Gets the champion info directly from Riot's Data Dragons. Take a look at the [Data Dragons](https://developer.riotgames.com/docs/lol) documentation for more info.
- Needs a valid patch number to do so

![Gif3](https://github.com/League-of-DAE-Legends/SquadForger/assets/88614889/1473c960-2998-4f26-aa63-606f67d53c36)

### 3. Discord integration automatically shares the pools.
- If you didn't specify a Discord webhook in ```PrivateData.config```, then this will do nothing
- If you did specify this, ```Send to Discord``` button will produce the following result:

![image](https://github.com/League-of-DAE-Legends/SquadForger/assets/88614889/ab73f3d0-e9ec-4451-8ef2-679b9dbc859c)
