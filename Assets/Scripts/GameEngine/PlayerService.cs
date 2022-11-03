using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService
{
    private Player _player;
    private Vector3 GetPlayerPosition => _player.transform.position;

    private static PlayerService _instance;

    public static PlayerService Instance
    {
        get
        {
                return _instance;
        }
    }

    public PlayerService(Player player)
    {
        _player = player;
    }
    private Vector3 GetPlayerDirection(Vector3 from)
    {
        return (_player.transform.position - from);
    }

}
