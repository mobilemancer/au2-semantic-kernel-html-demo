using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SKDemo.Agents._2_RefineDraft;
using System.Diagnostics;

namespace SKDemo.Agents
{
    public class RefineDraftUsingPlugins
    {
        private Kernel _kernel;
        public RefineDraftUsingPlugins()
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
        }

        public async Task<string> Run(string draft)
        {
            var prompt = @"{{$input}}

You are responsible to turning drafts of job descriptions into a perfect job advert formated in HTML.
You will only respond with the finished HTML, and no other comments.";

            var summarize = _kernel.CreateFunctionFromPrompt(prompt, 
                executionSettings: new OpenAIPromptExecutionSettings { 
                    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions, 
                    MaxTokens = 5000 });

            var result = await _kernel.InvokeAsync(summarize, new()
            {
                ["input"] = draft
            });

            Console.WriteLine(result);
            
            return result.ToString();
        }
    }



}
