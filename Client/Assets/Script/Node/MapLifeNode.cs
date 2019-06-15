using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLifeNode : MapNode
{
    protected enum MotionState
    {
        Idle,
        Run,
        Climb,
        Operate,
    }

    private int m_curHP = 1;
    private int m_maxHP = 1;

    protected int CurHP
    {
        get
        {
            return m_curHP;
        }

        set
        {
            if (value < 0)
            {
                value = 0;
            }
            m_curHP = value;
        }
    }

    protected int MaxHP
    {
        get
        {
            return m_maxHP;
        }

        set
        {
            if (value <= 0)
            {
                value = 1;
            }
            m_maxHP = value;
        }
    }

}
