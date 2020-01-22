using ReverseMarkdown;
using System;

namespace Corepoint.Plugin
{
    internal class HtmlToMarkdown
    {
        public static void ConvertToMarkdown(Function function, out string resultData, out string errorMessage)
        {
            resultData = "";
            errorMessage = "";

            // Get the UnknownTags Enum
            string unknownTags = function.Parameters.UnknownTags;
            var unknownTagsOption = new Config.UnknownTagsOption();

            switch (unknownTags)
            {
                case "Bypass":
                    unknownTagsOption = Config.UnknownTagsOption.Bypass;
                    break;

                case "Drop":
                    unknownTagsOption = Config.UnknownTagsOption.Drop;
                    break;

                case "Raise":
                    unknownTagsOption = Config.UnknownTagsOption.Raise;
                    break;

                default: 
                    unknownTagsOption = Config.UnknownTagsOption.PassThrough;
                    break;
            }

            // Get the TablesWithoutHeaderRowHandling Enum
            string tableHeaderHandling = function.Parameters.TableWithoutHeaderRowHandling;
            var tableHandlingOption = new Config.TableWithoutHeaderRowHandlingOption(); 
            switch (tableHeaderHandling)
            {
                case "EmptyRow":
                    tableHandlingOption = Config.TableWithoutHeaderRowHandlingOption.EmptyRow;
                    break;

                default: 
                    tableHandlingOption = Config.TableWithoutHeaderRowHandlingOption.Default;
                    break;
            }

            // Create a config to use with the conversion.
            var config = new ReverseMarkdown.Config
            {
                GithubFlavored = function.Parameters.GithubFlavored, // generate GitHub flavoured markdown, supported for BR, PRE and table tags
                RemoveComments = function.Parameters.RemoveComments, // will ignore all comments
                SmartHrefHandling = function.Parameters.SmartHrefHandling, // remove markdown output for links where appropriate
                UnknownTags = unknownTagsOption, // Include the unknown tag completely in the result (default as well)
                WhitelistUriSchemes = function.Parameters.WhitelistUriSchemes, //Specify which schemes (without trailing colon) are to be allowed
                TableWithoutHeaderRowHandling = tableHandlingOption //handle table without header rows
            };

            
            string html = function.InputData;
            
            var converter = new Converter(config);


            try
            {
                resultData = converter.Convert(html);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}