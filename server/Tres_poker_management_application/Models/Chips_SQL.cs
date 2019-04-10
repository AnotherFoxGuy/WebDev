using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tres_poker_management_application.Models
{
    public class Chips_SQL : Model
    {
        public void EditChips(int? Profile_ID, Chips model)
        {
            var serial = JsonConvert.SerializeObject(model);
            string sql = "UPDATE Gameprofile SET Chips = @0 WHERE Profile_ID = @1";
            update(sql, serial, Profile_ID);
            DeepStreamConnector.Instance.UpdateRecord("Chips", "Chips", serial);
        }
    }
}