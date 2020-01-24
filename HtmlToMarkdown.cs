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
            
            
            // Create a config to use with the conversion.
            var config = new Config
            {
                GithubFlavored = function.Parameters.GithubFlavored, // generate GitHub flavoured markdown, supported for BR, PRE and table tags
                RemoveComments = function.Parameters.RemoveComments, // will ignore all comments
                SmartHrefHandling = function.Parameters.SmartHrefHandling, // remove markdown output for links where appropriate
                UnknownTags = (Config.UnknownTagsOption)Enum.Parse(typeof(Config.UnknownTagsOption), function.Parameters.UnknownTags), // Include the unknown tag completely in the result (default as well)
                WhitelistUriSchemes = function.Parameters.WhitelistUriSchemes, //Specify which schemes (without trailing colon) are to be allowed
                TableWithoutHeaderRowHandling = (Config.TableWithoutHeaderRowHandlingOption)Enum.Parse(typeof(Config.TableWithoutHeaderRowHandlingOption), function.Parameters.TableWithoutHeaderRowHandling), //handle table without header rows
            };

            // Create Converter Object
            var converter = new Converter(config);


            try // Attempt to convert the HTML
            {
                resultData = converter.Convert(function.InputData);
            }
            catch (ArgumentException exception) //Throw an error if it doesn't work
            {
                errorMessage = exception.ToString();
            }
        }
    }
}