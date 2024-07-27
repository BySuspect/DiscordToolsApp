using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordToolsApp.Helpers
{
    public static class JsonHelper
    {
        public static bool IsValidJson(string jsonString)
        {
            try
            {
                var json = JToken.Parse(jsonString);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string BeautifyJson(string jsonString)
        {
            try
            {
                JToken parsedJson = JToken.Parse(jsonString);
                string beautifiedJson = parsedJson.ToString(Formatting.Indented);
                return beautifiedJson;
            }
            catch (JsonReaderException)
            {
                return jsonString;
            }
            catch (Exception ex)
            {
                ApplicationService.ShowCustomAlert(
                    "Error!",
                    $"Something went wrong while trying beautify json data\n Error Message: {ex.Message}",
                    "Ok"
                );
                return jsonString;
            }
        }
    }
}
