
# Better-BelieveIt-Bot

Hello!

This is a WIP Discord bot that I'm making for myself and friends. 

## Current Bot Features

I will attempt to update this as new features are added.

- It will show up as online in your server (wow, crazy!)
- It will register (2) commands to the server you provided with `"TestingGuildID"`
- - These commands are:
	- `ping`: the bot will reply to the message with "Pong!"
	- `match_add`: takes two ints as arguments and returns the sum

## Planned Additions

Because this is meant to be for fun, a lot of the additions will include ridiculous commands meant to troll friends.

- Integrated GitHub requests/issues
- Soundboard/quote generator
- Economy system

## Requirements

It is assumed that you have already created your Bot within Discord's developer portal [https://discord.com/developers/] and have added it to a server.

If not, this guide should be sufficient [https://discord.com/developers/docs/quick-start/getting-started]





## Deployment

I will attempt to update this each time the setup changes.

For development, the *Bot Token* and your *Testing Guild ID* is stored using the secret manager and accessed via the ConfigurationBuilder.

[Visual Studio]
- Right click on the project and select *Manage User Secrets*
- Add your bot token using the following format: `"BotToken" : "YourTokenHere"`
- Add your Guild ID using the following format: `"TestingGuildID" : "YourGuildID"`

[.NET CLI]
- `dotnet user-secrets init`
- `dotnet user-secrets set "BotToken" "YourTokenHere"`
- `dotnet user-secrets set "TestingGuildID" "YourGuildID"`

[VS Code]
- [VS Code User Secrets Package](https://marketplace.visualstudio.com/items?itemName=Reptarsrage.vscode-manage-user-secrets)

After this, you should be able to build/run the project and have your bot active and ready in your server!
## Authors

- [@TriangularApathy](https://github.com/TriangularApathy)

- [@Austin-Marlatt](https://github.com/Austin-Marlatt)

