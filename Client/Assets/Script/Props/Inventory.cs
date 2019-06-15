using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Inventory
{
    private Dictionary<int, PropData> m_allProps = new Dictionary<int, PropData>();
    

    public void AddProp(PropData data)
    {

    }

    public PropData GetProp(int id)
    {
        if (m_allProps.ContainsKey(id))
        {
            return m_allProps[id];
        }

        return null;
    }
}
