using System;
using System.Collections.Generic;


/// <summary>
/// 为一次性使用道具。使用后立即消失
/// </summary>
class InstantProp : PropBase
{
    public override void OnPropEndUsing()
    {
        base.OnPropEndUsing();

        gameObject.SetActive(false);
        m_uiBtn.SetActive(false);
        m_isOver = true;
    }
}