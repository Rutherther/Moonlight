This is a fork of Roxeez/Moonlight. I migrated it to .NET Standard so it can be used on Linux as well as Windows.

# Moonlight

Moonlight aims to make NosTale .NET Application developer life easier by giving them access to a complete & easy to use API allowing them to interact with (almost) everything in the game  
Moonlight can be used with local client (injected .dll) or remote client (clientless)
<br><br>
![Codacy grade](https://img.shields.io/codacy/grade/d7ecbcba4d48445f8a7e12f1bb4fb8e7)
![AppVeyor](https://img.shields.io/appveyor/build/Roxeez/Moonlight)
![AppVeyor tests](https://img.shields.io/appveyor/tests/Roxeez/Moonlight)
![GitHub commit activity](https://img.shields.io/github/commit-activity/m/Roxeez/Moonlight)
![GitHub](https://img.shields.io/github/license/Roxeez/Moonlight)

## Getting Started

### .NET Framework with local client

- Create a C# .dll project targeting .NET Framework 4.7+
- Add Moonlight dependency
- Install DllExport to your project and create your export function (cf. DllExport wiki)
- Install Costura.Fody to your project so you don't have to copy all dependencies to NosTale folder
- Build your project
- Create database.db using Moonlight.Toolkit CLI
- Copy previously generated database.db to a subfolder named Moonlight in your NosTale folder
- Copy your generated .dll & MoonlightCore to your NosTale folder
- Copy SQLitePCLRaw.batteries_v2.dll, SQLitePCLRaw.provider.dynamic_cdecl.dll, SQLitePCLRaw.core.dll, SQLitePCLRaw.nativelibrary.dll to NosTale folder
  - if you encounter SqliteException with inner exception of "The path is not of a legal form." check that you added these files to NosTale folder
- Copy runtimes folder
- Inject your .dll using an injector supporting custom export function.

> Moonlight is a packet based lib, so if you want everything to work correctly using local client, it should be injected before character selection.

### .NET Core with remote client

> Moonlight remote client currently supports only Gameforge official client with Gameforge auth

- Create a C# .NET Core console project
- Add Moonlight dependency
- Use GameforgeApi to obtain sessionToken (You can use GenerateInstallationId for unique id generation based on email and password)
  - First call GetAuthToken
  - Then use GetAccounts and select account you want to use
  - Call GetSessionToken for account you want to use
- Initializa MoonlightAPI and NosTale
- Use NosTale.InitLogin to obtain instance of NosTaleLogin
- Use NosTale login to Connect to server you want and then Login using correct parameters
- Register listener to event NostaleLogin.ServersReceived
- You may connect to a channel using NosTaleLogin.ConnectToWorld with Channels received in your listener
  - You will obtain NosTaleWorld
- Register listener to event NosTaleWorld.CharactersListReceived
- You may Select a character from one of the received
- Finally send StartGame to start the game

## Example
>Example application can be found here : https://github.com/Roxeez/Moonlight.Example
```csharp
private async Task BotLoop()
{
    while (IsRunning)
    {
        IEnumerable<Monster> monsters;
        Skill zoneSkill;
        do
        {
            zoneSkill = Configuration.UsedSkills.FirstOrDefault(x => !x.IsOnCooldown);
            monsters = Client.Character.Map.Monsters
                .Where(x => x.Vnum == MonsterConstants.SoftPii)
                .Where(x => x.Position.IsInRange(Client.Character.Position, Radius))
                .OrderBy(x => x.Position.GetDistance(Client.Character.Position));
            
            Monster closestPod = await GetClosestPod();
            if (closestPod == null)
            {
                return;
            }

            await Client.Character.Attack(closestPod);
        } 
        while ((monsters.Count() < 10 || zoneSkill == null) && IsRunning);

        if (monsters.Count() < 10)
        {
            return;
        }
        
        await Client.Character.UseSkillOn(zoneSkill, monsters.First());
        await Task.Delay(100);
    }
}

private async Task<Monster> GetClosestPod()
{
    Monster pod;
    do
    {
        pod = Client.Character.Map.Monsters
            .Where(x => x.Vnum == MonsterConstants.SoftPiiPod)
            .Where(x => x.Position.IsInRange(Client.Character.Position, Radius))
            .OrderBy(x => x.Position.GetDistance(Client.Character.Position))
            .FirstOrDefault();
        
        await Task.Delay(100);
    } 
    while (pod == null && IsRunning);

    return pod;
}
```

### Prerequisites

- **.NET Framework 4.7** for local client
- **.NET Core** for remote client

## Contributors
* **Roxeez**
* **Rutherther**

### Special thanks

* **Pumba98** for helping me with some C++/RE related stuff

### License

This project is licensed under the GPL-3.0 License - see the [LICENSE.md](LICENSE.md) file for details
