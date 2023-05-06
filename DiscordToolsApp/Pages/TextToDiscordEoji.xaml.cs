using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordToolsApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextToDiscordEoji : ContentPage
    {
        string input = "";
        List<string> outputList = new List<string>();
        public TextToDiscordEoji()
        {
            InitializeComponent();
        }
        private async void DiscordButton_Clicked(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://bit.ly/3NmBFDO");
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                input = e.NewTextValue ?? "";
                if (input != null)
                {
                    if (e.NewTextValue.Length > ((string.IsNullOrEmpty(e.OldTextValue)) ? 0 : e.OldTextValue.Length))
                    {
                        var outres = ConvertTextToEmoji(input.Substring(input.Length - 1)[0]);
                        if (!string.IsNullOrEmpty(outres))
                            outputList.Add(outres);
                        else
                            Input.Text = Input.Text.Remove(Input.Text.Length - 1, 1);
                    }
                    else if (e.NewTextValue.Length < e.OldTextValue.Length)
                    {
                        if (e.NewTextValue.Length != outputList.Count)
                            outputList.RemoveAt(outputList.Count - 1);
                    }
                    Output.Text = string.Join("", outputList);
                }
                else Output.Text = "";
            }
            catch { }
        }
        private string ConvertTextToEmoji(char input)
        {
            outputList = new List<string>();
            var emojiMap = new Dictionary<char, string>
            {
                {'a', ":regional_indicator_a:"},
                {'b', ":regional_indicator_b:"},
                {'c', ":regional_indicator_c:"},
                {'d', ":regional_indicator_d:"},
                {'e', ":regional_indicator_e:"},
                {'f', ":regional_indicator_f:"},
                {'g', ":regional_indicator_g:"},
                {'h', ":regional_indicator_h:"},
                {'i', ":regional_indicator_i:"},
                {'j', ":regional_indicator_j:"},
                {'k', ":regional_indicator_k:"},
                {'l', ":regional_indicator_l:"},
                {'m', ":regional_indicator_m:"},
                {'n', ":regional_indicator_n:"},
                {'o', ":regional_indicator_o:"},
                {'p', ":regional_indicator_p:"},
                {'q', ":regional_indicator_q:"},
                {'r', ":regional_indicator_r:"},
                {'s', ":regional_indicator_s:"},
                {'t', ":regional_indicator_t:"},
                {'u', ":regional_indicator_u:"},
                {'v', ":regional_indicator_v:"},
                {'w', ":regional_indicator_w:"},
                {'x', ":regional_indicator_x:"},
                {'y', ":regional_indicator_y:"},
                {'z', ":regional_indicator_z:"},
                {'0', ":zero:"},
                {'1', ":one:"},
                {'2', ":two:"},
                {'3', ":three:"},
                {'4', ":four:"},
                {'5', ":five:"},
                {'6', ":six:"},
                {'7', ":seven:"},
                {'8', ":eight:"},
                {'9', ":nine:"},
                {'!', ":exclamation:"},
                {'#', ":hash:"},
                {'*', ":asterisk:"},
                {'?', ":question:"},
                {':', ":colon:"},
            };
            var sb = new StringBuilder();

            char c = char.Parse(input.ToString().ToLower());

            if (emojiMap.TryGetValue(c, out string emoji))
            {
                sb.Append(emoji + " ");
            }
            else if (c == ' ')
            {
                sb.Append(":blue_square:");
            }
            else
                sb.Append("");

            return sb.ToString();
        }

        public static string ConvertEmojiToString(string input)
        {
            var emojiMap = new Dictionary<string, string>
            {
                {":regional_indicator_a:", "a"},
                {":regional_indicator_b:", "b"},
                {":regional_indicator_c:", "c"},
                {":regional_indicator_d:", "d"},
                {":regional_indicator_e:", "e"},
                {":regional_indicator_f:", "f"},
                {":regional_indicator_g:", "g"},
                {":regional_indicator_h:", "h"},
                {":regional_indicator_i:", "i"},
                {":regional_indicator_j:", "j"},
                {":regional_indicator_k:", "k"},
                {":regional_indicator_l:", "l"},
                {":regional_indicator_m:", "m"},
                {":regional_indicator_n:", "n"},
                {":regional_indicator_o:", "o"},
                {":regional_indicator_p:", "p"},
                {":regional_indicator_q:", "q"},
                {":regional_indicator_r:", "r"},
                {":regional_indicator_s:", "s"},
                {":regional_indicator_t:", "t"},
                {":regional_indicator_u:", "u"},
                {":regional_indicator_v:", "v"},
                {":regional_indicator_w:", "w"},
                {":regional_indicator_x:", "x"},
                {":regional_indicator_y:", "y"},
                {":regional_indicator_z:", "z"},
                {":zero:", "0"},
                {":one:", "1"},
                {":two:", "2"},
                {":three:", "3"},
                {":four:", "4"},
                {":five:", "5"},
                {":six:", "6"},
                {":seven:", "7"},
                {":eight:", "8"},
                {":nine:", "9"},
                {":exclamation:", "!"},
                {":hash:", "#"},
                {":asterisk:", "*"},
                {":question:", "?"},
                {":colon:", ":"},
            };

            foreach (var emoji in emojiMap.Keys)
            {
                input = input.Replace(emoji, emojiMap[emoji]);
            }

            return input;
        }

    }
}