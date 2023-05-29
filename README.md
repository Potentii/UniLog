# UniLog

> Logger for Unity

## Installing

- Inside Unity, open the `Unity Package Manager` window: `Window` > `Package Manager`
- Click the `+` icon on the top left
- Select `Add package from git URL...`
- Paste this repo Git URL: `https://github.com/Potentii/UniLog.git`
- Unity will start downloading this repo as a dependency

If you are using a custom `.asmdef` file:
- Open your `.asmdef` files on the Unity editor
- Go to the `Assembly Definition References` list
- Click on the `+` to add another reference
- Click on the missing reference slot
- Select `Potentii.UniLog.Core` on the search dialog
- Click `Apply` on the `.asmdef` editor

---

## Using

Inside a MonoBehaviour you can just call it statically like this:

```csharp
using UnityEngine;
using Potentii.UniLog.Core;

public class GameManager : MonoBehaviour{
    
    public void Start(){
    
        string userId = getUserId();
        
        UniLog.Info(this, "GAME_STARTED", "Game just started", ("userId", userId));
        
    }
    
}

```


---

## License

[MIT](LICENSE)
