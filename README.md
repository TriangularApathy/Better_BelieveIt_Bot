
# Better-BelieveIt-Bot

Hello!

This is a WIP Discord bot that I'm making for myself and friends. 

## Current Bot Features

I will attempt to update this as new features are added.

- It will show up as online in your server (wow, crazy!)

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

For development, the *Bot Token* is stored using the secret manager and accessed via the ConfigurationBuilder.

[Visual Studio]
- Right click on the project and select *Manage User Secrets*
- Add your bot token using the following format: `"BotToken" : "YourTokenHere"`

[.NET CLI]
- `dotnet user-secrets init`
- `dotnet user-secrets set "BotToken" "YourTokenHere"`

After this, you should be able to build/run the project and have your bot active and ready in your server!
## Authors

- [@TriangularApathy](https://github.com/TriangularApathy)

- [@Austin-Marlatt](https://github.com/Austin-Marlatt)

