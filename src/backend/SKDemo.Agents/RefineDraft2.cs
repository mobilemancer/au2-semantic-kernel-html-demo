using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Diagnostics;

namespace SKDemo.Agents
{
    class RefineDraft2
    {

        public RefineDraft2()
        {
            var builder = Kernel.CreateBuilder();

            // using OpenAI
            var openAIKey = Environment.GetEnvironmentVariable("OpenAIKey");
            Debug.Assert(!string.IsNullOrEmpty(openAIKey), "OpenAIKey environment variable is not set.");

            builder.AddOpenAIChatCompletion(
                     "gpt-4o-mini",             // OpenAI Model name
                     openAIKey);                // OpenAI API Key


            var kernel = builder.Build();
            kernel.ImportPluginFromType<MarkdownToHTML>();

        }
    }

    public sealed class MarkdownToHTML
    {
        [KernelFunction, Description("Convert markdown to HTML")]
        public static string Convert([Description("Markdown text to convert")] string markdown) =>
            new MarkdownSharp.Markdown().Transform(markdown);
    }

}
