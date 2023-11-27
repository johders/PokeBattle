using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pra.Pe1.PokeBattle.Core.Entities
{
    public class SelectedBagItem
    {

        public List<BagItem> SelectedBagItems { get; set; }

        public SelectedBagItem()
        {
            SelectedBagItems = new List<BagItem>();
           
        }

    }
}
