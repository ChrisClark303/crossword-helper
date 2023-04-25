using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace CrosswordHelper.Api
{
    public static class RouteBuilderExtensions
    {
        private static Dictionary<string, KeyValuePair<string, Delegate>> _routeDictionary = new Dictionary<string, KeyValuePair<string, Delegate>>()
        {
            { "/help/anagram-indicators", new KeyValuePair<string, Delegate>("GetAnagramIndicators", ([FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.GetAnagramIndicators();
            })},
            { "/help/removal-indicators", new KeyValuePair<string, Delegate>("GetRemovalIndicators", ([FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.GetRemovalIndicators();
            })},
            { "/help/reversal-indicators", new KeyValuePair<string, Delegate>("GetReversalIndicators", ([FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.GetReversalIndicators();
            })},
            { "/help/container-indicators", new KeyValuePair<string, Delegate>("GetContainerIndicators", ([FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.GetContainerIndicators();
            })},
            { "/help/letter-selection-indicators", new KeyValuePair<string, Delegate>("GetLetterSelectionIndicators", ([FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.GetLetterSelectionIndicators();
            })},
            { "/help/homophone-indicators", new KeyValuePair<string, Delegate>("GetHomophoneIndicators", ([FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.GetHomophoneIndicators();
            })},
            { "/help/substitution-indicators", new KeyValuePair<string, Delegate>("GetSubstitutionIndicators", ([FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.GetSubstitutionIndicators();
            })},
            { "/help/hidden-word-indicators", new KeyValuePair<string, Delegate>("GetHiddenWordIndicators", ([FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.GetHiddenWordIndicators();
            })}
        };

        public static void BuildRoutes(this WebApplication app)
        {
            foreach (var routeDetails in _routeDictionary)
            {
                var url = routeDetails.Key;
                var routeName = routeDetails.Value.Key;
                var routeDelegate = routeDetails.Value.Value;
                app.MapGet(url, routeDelegate).WithName(routeName);
            }

            app.MapGet("/help/{crosswordClue}", (string crosswordClue, [FromServices] ICrosswordHelperService helperService) =>
            {
                return helperService.Help(crosswordClue);
            }).WithName("GetCrosswordHelp");
        }
    }
}
