using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SKDemo.Agents._2_RefineDraft;
using System.Diagnostics;

namespace SKDemo.Agents._4_MultiAgent
{
    public class MultiAgent
    {
        private static Kernel _kernel;
        private static ChatHistory _history;
        private static IChatCompletionService _chatCompletionService;
        private static OpenAIPromptExecutionSettings _openAIPromptExecutionSettings;

        public MultiAgent()
        {
            if (_kernel == null)
            {
                var builder = Kernel.CreateBuilder();

                // using OpenAI
                var openAIKey = Environment.GetEnvironmentVariable("OpenAIKey");
                Debug.Assert(!string.IsNullOrEmpty(openAIKey), "OpenAIKey environment variable is not set.");

                builder.AddOpenAIChatCompletion(
                         "gpt-4o-mini",             // OpenAI Model name
                         openAIKey);                // OpenAI API Key

                builder.Plugins.AddFromType<JobAddCopywriterPlugin>();
                builder.Plugins.AddFromType<MarkdownToHTMLPlugin>();

                _kernel = builder.Build();
                //kernel.ImportPluginFromType<MarkdownToHTMLPlugin>();

                // Create chat history
                _history = new ChatHistory();

                // Get chat completion service
                _chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();

                // Enable auto function calling
                _openAIPromptExecutionSettings = new()
                {
                    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
                };
            }



}
