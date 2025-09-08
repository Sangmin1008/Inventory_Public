using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ItemRegistrySO ItemRegistry;

    protected override void Awake()
    {
        base.Awake();
        ItemRegistry.Init();
    }
}
