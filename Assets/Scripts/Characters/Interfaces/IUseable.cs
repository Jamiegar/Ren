using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUseable
{
    public void StartUsing(Vector2 touchedPosition);
    public void UsingUpdate(Vector2 touchedPosition);
    public void StopUsing(Vector2 touchedPosition);
}
