# SquadForger - ARAM Team Builder for League of Legends

Resolve the no rerolls issue in custom ARAM games with this tool! This GitHub repository houses a C#-based solution that takes the teams participating in a custom ARAM match as input and dynamically generates a pool of 15 champions for each team. The generated champion pools are then displayed and can be shared on Discord.

## Features:

- **Champion Pool Generation:** Automatically generate X amount of champions for each team.
- **Discord Integration:** Post the generated champion pools directly to a designated Discord channel.

## Usage:

### 1. Download the [Latest Release](https://github.com/League-of-DAE-Legends/SquadForger/releases/latest)

### 2. Open ```SquadForger.exe```

### 3. Input the participating teams.
This can be done manually, or by parsing a csv. Currently we have 1 simple version of CSV parser, but you can add your own parser by implementing ```ITeamNamesRepository``` interface.
This is what our ```CSVTeamParser.cs``` can handle:

![image](https://github.com/League-of-DAE-Legends/SquadForger/assets/88614889/11b3f6a5-50be-4df0-bb64-fa4879677bc5)



![Gif3](https://github.com/League-of-DAE-Legends/SquadForger/assets/88614889/8c693f03-9d70-4a44-acb6-b2cff3f226af)



### 4. Run the script to generate champion pools.
Here we have 2 options:

**Safe Generate**
- Uses a local json that contains info about the champions. (Up to date with patch 14.1.1)

**Custom Generate**
- Gets the champion info directly from Riot's Data Dragons. Take a look at the [Data Dragons](https://developer.riotgames.com/docs/lol) documentation for more info.
- Needs a valid patch number to do so

![Gif3](https://github.com/League-of-DAE-Legends/SquadForger/assets/88614889/4452e50b-61bd-4c55-89af-974c99327cbc)

### 5. Add Discord Webhook ID
If you are not familiar with Webhooks, [this article](https://hookdeck.com/webhooks/platforms/how-to-get-started-with-discord-webhooks) will come in handy :)

![image](https://github.com/user-attachments/assets/126e19a1-2a23-427f-85f1-7a40237439b9)


### 6. Discord integration automatically shares the pools.
- If you didn't specify a Discord webhook in ```PrivateData.config```, then this will do nothing
- If you did specify this, ```Send to Discord``` button will produce the following result:

![image](https://github.com/League-of-DAE-Legends/SquadForger/assets/88614889/ab73f3d0-e9ec-4451-8ef2-679b9dbc859c)
