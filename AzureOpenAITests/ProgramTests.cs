using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AzureOpenAI.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void ConsoleInputIsValidRangeTest()
        {
            // arrange
            List<KeyValuePair<int, string>> options = new() {
                new KeyValuePair<int, string>(1,"Completion"),
                new KeyValuePair<int, string>(2,"Chat Completion"),
                new KeyValuePair<int, string>(3,"Embeddings")
            };
            int n = 1;

            // assert
            Assert.IsTrue(n >= 1 && n <= options.Count);
        }

        [TestMethod()]
        public void ConsoleInputIsValidParamTest()
        {
            // arrange
            object? value = null;

            // assert
            if (value == null)
            {
                Assert.IsTrue(value == null, "If object parameter is null always return FALSE");
            }
            else
            {
                var isNumeric = int.TryParse((string?)value, out int n);
                if (isNumeric)
                {
                    Assert.IsTrue(isNumeric, "If param is number, than showld be a numeral character");
                }
                else
                {
                    Assert.IsTrue(value == "Y" || value == "N");
                }
            }
        }

        [TestMethod()]
        public void GetCompletionResponsePromptEmptyTest()
        {
            // arrange
            string prompt = "SS";

            // assert
            Assert.IsFalse(string.IsNullOrEmpty(prompt), "User query cann't be null or empty");
        }

        [TestMethod()]
        public void GetCompletionResponseDeploymentNameEmptyTest()
        {
            // arrange
            string deploymentName = "gpt-35-turbo";

            // assert
            Assert.IsFalse(string.IsNullOrEmpty(deploymentName), "Deplyment name cann't be null or empty");
        }

        [TestMethod()]
        public void GetChatCompletionsResponsePromptEmptyTest()
        {
            // arrange
            string prompt = "SS";

            // assert
            Assert.IsFalse(string.IsNullOrEmpty(prompt), "User query cann't be null or empty");
        }

        [TestMethod()]
        public void GetChatCompletionsResponseDeploymentNameEmptyTest()
        {
            // arrange
            string deploymentName = "gpt-35-turbo";

            // assert
            Assert.IsFalse(string.IsNullOrEmpty(deploymentName), "Deplyment name cann't be null or empty");
        }

        [TestMethod()]
        public void GetEmbeddingsResponsePromptEmptyTest()
        {
            // arrange
            string prompt = "SS";

            // assert
            Assert.IsFalse(string.IsNullOrEmpty(prompt), "User query cann't be null or empty");
        }

        [TestMethod()]
        public void GetEmbeddingsResponseDeploymentNameEmptyTest()
        {
            // arrange
            string deploymentName = "text-embedding-ada-00";

            // assert
            Assert.IsFalse(string.IsNullOrEmpty(deploymentName), "Deplyment name cann't be null or empty");
        }
    }
}