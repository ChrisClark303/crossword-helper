using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace CrosswordHelper.Api
{
    public static class Routes
    {
        public static Dictionary<string, Delegate> HelpRoutes = new Dictionary<string, Delegate>()
        {
            { "/help/anagram-indicators", ([FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.GetAnagramIndicators();
            }},
            { "/help/removal-indicators", ([FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.GetRemovalIndicators();
            }},
            { "/help/reversal-indicators", ([FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.GetReversalIndicators();
            }},
            { "/help/container-indicators", ([FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.GetContainerIndicators();
            }},
            { "/help/letter-selection-indicators", ([FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.GetLetterSelectionIndicators();
            }},
            { "/help/homophone-indicators", ([FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.GetHomophoneIndicators();
            }},
            { "/help/substitution-indicators",([FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.GetSubstitutionIndicators();
            }},
            { "/help/hidden-word-indicators",([FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.GetHiddenWordIndicators();
            }}
        };
    }

    public static class RouteBuilderExtensions
    {
        public static void BuildRoutes(this WebApplication app)
        {
            foreach (var routeDetails in Routes.HelpRoutes)
            {
                var url = routeDetails.Key;
                var routeDelegate = routeDetails.Value;
                app.MapGet(url, routeDelegate);
            }

            app.MapGet("/help/{crosswordClue}", (string crosswordClue, [FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.Help(crosswordClue);
            }).WithName("GetCrosswordHelp");
        }
    }
}
