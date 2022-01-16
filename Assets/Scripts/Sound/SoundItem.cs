using UnityEngine;

public abstract class SoundItem : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;

    public string Label => _label;
    public int Price => _price;
    public bool IsBuyed { get; set; }
}
