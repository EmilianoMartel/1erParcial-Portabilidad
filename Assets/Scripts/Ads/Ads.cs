using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public abstract class Ads
{
    protected AdsData p_data;

    public AdsData Data { set { p_data = value; } }
    public AdType Type { get { return p_data.AdType; } }

    public virtual void Show() { }
}
