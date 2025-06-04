using System;
using Godot;

public static class StaticEventManager
{
    public static Action StartGame;
    public static Action ReturnToMainMenu;
    public static Action GameOver;
    public static Action PauseGame;
    public static Action ResumeGame;
    public static Action StartNewGame;

    public static Action FoodCollide;
    public static Action<Tail> TailAdded;


    public static void BroadcastStartNewGame()
    {
        GD.Print("invoke event start new game");
        StartNewGame?.Invoke();
    }

    public static void BroadcastGameOverEvent()
    {
        GD.Print("invoke event game over");
        GameOver?.Invoke();
    }
    
    public static void BroadcastStartGameEvent()
    {
        GD.Print("invoke event starting game");
        StartGame?.Invoke();
    }

    public static void BroadcastReturnToMainMenuEvent()
    {
        GD.Print("invoke event returning main menu");
        ReturnToMainMenu?.Invoke();
    }

    public static void BroadcastFoodCollideEvent()
    {
        GD.Print("invoke event food collide");
        FoodCollide?.Invoke();
    }

    public static void BroadcastTailAddedEvent(Tail tail)
    {
        GD.Print("invoke event tail added");
        TailAdded?.Invoke(tail);
    }

    public static void BroadcastPauseGameEvent()
    {
        GD.Print("invoke event game pause");
        PauseGame?.Invoke();
    }
    
    public static void BroadcastResumeGameEvent()
    {
        GD.Print("invoke event game resume");
        ResumeGame?.Invoke();
    }
    
}
