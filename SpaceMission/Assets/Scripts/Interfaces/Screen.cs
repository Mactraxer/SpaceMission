using System;

public interface IScreen
{
    public void Show();
    public event Action WillDismis;
    
}

public interface ILaunchSettings { }