using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static event Action<Enemy> OnEnemyDied;

    public static void ReportEnemyDied(Enemy enemy)
    {
        OnEnemyDied?.Invoke(enemy);
    }

    public static event Action<List<BaseItemData>, float, string> OnStageClear;

    public static void ReportStageClear(List<BaseItemData> reward, float clearTime, string header)
    {
        OnStageClear?.Invoke(reward, clearTime, header);
        
    }
}