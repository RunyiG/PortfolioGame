using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets itemAssets { get; private set; }

    private void Awake()
    {
        itemAssets = this;
    }

    public Transform ItemWorldPf;

    public Sprite HoneySprite;
    public Sprite AppleSprite;
    public Sprite OrangeSprite;
    public Sprite IceSprite;
    public Sprite HoneyRAppleSpr;
    public Sprite AppleOrange;
    public Sprite OrangeHoney;

}
