using Azure.AI.OpenAI;
using Azure;

namespace AzureOpenAI {

    internal class Program
    {
        private static readonly OpenAIClient _client =
            new(new Uri(Constants.RESOURCE_ENDPOINT), new AzureKeyCredential(Constants.API_KEY));

        private static List<KeyValuePair<int, string>> _options = new() { 
            new KeyValuePair<int, string>(1,"Completion"),
            new KeyValuePair<int, string>(2,"Chat Completion"),
            new KeyValuePair<int, string>(3,"Embeddings"),
            new KeyValuePair<int, string>(4,"Langchain")
        };

        private static void Main(string[] args)
        {
            Console.WriteLine("Wellcome to Azure OpenAI by C#. Ten thousand times easier than python!");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("Choose an option bellow:");
            WriteOptions();
            var option = Console.ReadLine();
            while (!ConsoleInputIsValid(option))
            {
                if (string.IsNullOrEmpty(option))
                {
                    Console.WriteLine("Please, option can't be null. Choose one:");
                    WriteOptions();
                    option = Console.ReadLine();
                }
                else
                {
                    var isNumeric = int.TryParse(option, out int n);
                    if (!isNumeric || n > _options.Count)
                    {
                        Console.WriteLine($"Wrong option! Please provide a number between 1 and {_options.Count}");
                    }
                    WriteOptions();
                    option = Console.ReadLine();
                }
               //LoopPropmpt(Convert.ToInt32(option));
            }
            LoopPropmpt(Convert.ToInt32(option));

            Console.ReadLine();
        }

        #region-- Utils --
        private static void LoopPropmpt(int option)
        {
            Console.Write("Your query?: ");
            var prompt = Console.ReadLine();
            if (string.IsNullOrEmpty(prompt))
            {
                Console.WriteLine("Your query can't be null!");
                Console.Write("Try again [Y/N]?: ");
                var yesNo = Console.ReadLine();
                while (!ConsoleInputIsValid(yesNo))
                {
                    Console.WriteLine("Please, inform Y or N");
                }
                if (yesNo.ToUpper() == "Y")
                {
                    LoopPropmpt(option);
                }
                else
                {
                    Environment.Exit(0);
                }
            }

            if (option == 1)
            {
                GetCompletionResponse(prompt);
            }
            else if (option == 2)
            {
                GetChatCompletionsResponse(prompt);
            }
        }

        private static void WriteOptions()
        {
            foreach (var option in _options)
            {
                Console.WriteLine($"{option.Key} - {option.Value}");
            }
            Console.Write("Whitch one?: ");
        }

        private static bool ConsoleInputIsValid(object? value)
        {
            if (value == null) return false;
            var isNumeric = int.TryParse((string?)value, out int n);
            return isNumeric 
                ? n >= 1 && n <= _options.Count
                : ((string)value).ToUpper() == "Y" || ((string)value).ToUpper() == "N";
        }
        #endregion

        #region-- AI Completions --
        private static void GetCompletionResponse(string prompt)
        {
            try
            {
                string deploymentName = "gpt-35-turbo";
                Response<Completions> completionsResponse = _client.GetCompletions(deploymentName, prompt);
                string completion = completionsResponse.Value.Choices[0].Text;
                Console.WriteLine($"AI Response: {completion}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
        }

        private static void GetChatCompletionsResponse(string prompt)
        {
            try
            {
                string deploymentName = "gpt-35-turbo";
                List<ChatMessage> tt = new()
                {
                    new ChatMessage(ChatRole.System, "Você é um assistente para dar respostas sábias no estilo do mestre Yoda."),
                    new ChatMessage(ChatRole.Assistant, "Nascer novamente deverá. Burro de mais nesta vida você é!"),
                    new ChatMessage(ChatRole.User, prompt)
                };
                var bbb = new ChatCompletionsOptions(tt);
                Response<ChatCompletions> completionsResponse = _client.GetChatCompletions(deploymentName, bbb);
                string completion = completionsResponse.Value.Choices[0].Message.Content;
                Console.WriteLine($"AI Response: {completion}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
        }
        #endregion
    }
}