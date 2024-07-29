using MarkdownSharp;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace SKDemo.Agents._2_RefineDraft
{
    public class MarkdownToHTMLPlugin
    {
        [KernelFunction, Description("Convert markdown to HTML")]
        public static string Convert([Description("Markdown text to convert")] string markdown) =>
            new Markdown().Transform(markdown);
    }
}
