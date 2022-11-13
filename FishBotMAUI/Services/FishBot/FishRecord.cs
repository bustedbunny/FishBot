using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace FishBot;

[Serializable]
public partial class FishRecord : ObservableObject
{
    [ObservableProperty]
    private string name;

    private float probability;

    public float Probability
    {
        get => probability;
        set
        {
            value = Math.Clamp(value, 0f, 1f);
            SetProperty(ref probability, value);
        }
    }

}