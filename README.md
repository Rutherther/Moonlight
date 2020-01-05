# NtCore

NtCore aims to make NosTale .NET Application developer life easier by giving them access to a complete & easy to use API allowing them to interact with (almost) everything in the game  
NtCore can be used with local client (injected .dll) or remote client (clientless)
<br><br>
![Codacy grade](https://img.shields.io/codacy/grade/d7ecbcba4d48445f8a7e12f1bb4fb8e7?style=flat-square)
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/Roxeez/NtCore/Main.Legacy?style=flat-square)
![GitHub top language](https://img.shields.io/github/languages/top/Roxeez/NtCore?style=flat-square)
![GitHub commit activity](https://img.shields.io/github/commit-activity/m/Roxeez/NtCore?style=flat-square)
![GitHub](https://img.shields.io/github/license/Roxeez/NtCore?style=flat-square)
![Maintenance](https://img.shields.io/maintenance/yes/2020?style=flat-square)

## Getting Started

- Clone
- Open solution
- Build
- Create a new .NET library project targeting .NET Framework 4.7+
- Add NtCore.dll & Costura Fody as dependency
- Install DllExport to your project using DllExport.bat
- Build your application
- Copy your generated .dll and NtNative.dll to your NosTale folder
- Inject your generated .dll to NosTale process
- Enjoy

> <sub><sup>Since NtCore use only packets (no memory reading) for compatibility with local & remote client, your dll need to be injected BEFORE selecting your character</sub></sup>

## Example
> This example code will make character move to dropped item when they spawn & pick up them.  
You can find a full example [HERE](https://github.com/Roxeez/NtCore.Example)
```csharp
public class MyApplication
{
    public void Run()
    {
        Kernel32.AllocConsole();

        IClientManager clientManager = NtCoreAPI.GetClientManager();
        IEventManager eventManager = NtCoreAPI.GetEventManager();
        ICommandManager commandManager = NtCoreAPI.GetCommandManager();
        
        IClient localClient = clientManager.CreateLocalClient();
        
        eventManager.RegisterEventListener<MyListener>(localClient);
        commandManager.RegisterCommandHandler<MyCommandHandler>();
        
        string command;
        do
        {
            command = Console.ReadLine();
        } 
        while (command != "exit");
    }
}

public class MyCommandHandler : ICommandHandler
{
    [Command("MapInfo")]
    public async void OnMapInfoCommand(ICharacter sender)
    {
        IMap map = sender.Map;

        await sender.ReceiveChatMessage($"Id: {map.Id}", ChatMessageColor.GREEN);
        await sender.ReceiveChatMessage($"Monsters: {map.Monsters.Count()}", ChatMessageColor.GREEN);
        await sender.ReceiveChatMessage($"Players: {map.Players.Count()}", ChatMessageColor.GREEN);
        await sender.ReceiveChatMessage($"Npcs: {map.Npcs.Count()}", ChatMessageColor.GREEN);
        await sender.ReceiveChatMessage($"Drops: {map.Drops.Count()}", ChatMessageColor.GREEN);
    }
    
    [Command("SelectClosestEntity")]
    public async void OnSelectClosestEntityCommand(ICharacter sender)
    {
        IMap map = sender.Map;
        IMonster closestMonster = map.Monsters.OrderBy(x => x.Position.GetDistance(sender.Position)).FirstOrDefault();

        if (closestMonster == null)
        {
            await sender.ReceiveChatMessage("Can't find monster in range.", ChatMessageColor.RED);
            return;
        }
        
        await sender.ShowBubbleMessage(@"/!\ TARGET /!\", closestMonster);
        await sender.SelectEntity(closestMonster);
    }
}

public class MyListener : IEventListener
{
    [Handler]
    public async void OnTargetMove(TargetMoveEvent e)
    {
        ICharacter character = e.Character;
        ITarget target = character.Target;

        await character.Move(target.Entity.Position);
        await character.ShowBubbleMessage("Following target.");
    }
}
```

### Prerequisites

- **.NET Framework 4.7**
- **DllExport** (More information [HERE](https://github.com/3F/DllExport))
- **Costura Fody**

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Contributors
* **Roxeez**
* **Pumba98**

## License

This project is licensed under the GPL-3.0 License - see the [LICENSE.md](LICENSE.md) file for details
