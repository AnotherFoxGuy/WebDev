using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Tres_poker_management_application.Models
{
    public class Rules_SQL : Model
    {
        public IEnumerable<dynamic> GetProfileList()
        {
            string sql = "SELECT Profile_ID, profilename FROM Gameprofile";
            var results = read(sql);
            return results;
        }

        public void FindProfile(int? Profile_ID, Gameprofile model)
        {
            string sql = "SELECT profilename, Rules FROM Gameprofile WHERE Profile_ID = @0";
            var result = find(sql, Profile_ID);

            model.Profilename = result[0]["Profilename"];

            if (!result[0]["Rules"].Equals(DBNull.Value))
                model.Rules = result[0]["Rules"];
            else
                model.Rules = null;
        }

        public string GetRules(int Profile_ID)
        {
            string sql = "SELECT profilename, Rules FROM Gameprofile WHERE Profile_ID = @0";
            var result = find(sql, Profile_ID);

            return result[0]["Rules"] is DBNull ? "" : result[0]["Rules"];
        }

        public void EditRules(int? Profile_ID, Gameprofile model)
        {
            DeepStreamConnector.Instance.UpdateRecord("Rules" , "Rules", model.Rules);
            string sql = "UPDATE Gameprofile SET Rules = @0 WHERE Profile_ID = @1";
            update(sql, model.Rules, Profile_ID);
        }
    }
}