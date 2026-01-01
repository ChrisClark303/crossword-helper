using Microsoft.AspNetCore.Mvc;

namespace CrosswordHelper.Api
{
    public static class RouteBuilderExtensions
    {
        private static readonly Dictionary<string, Delegate> _helpRoutes = new()
        {
            { "/help/anagram-indicators", ([FromServices] ICrosswordHelperService helperService) => helperService.GetAnagramIndicators()},
            { "/help/removal-indicators", ([FromServices] ICrosswordHelperService helperService) => helperService.GetRemovalIndicators()},
            { "/help/reversal-indicators", ([FromServices] ICrosswordHelperService helperService) => helperService.GetReversalIndicators()},
            { "/help/container-indicators", ([FromServices] ICrosswordHelperService helperService) => helperService.GetContainerIndicators()},
            { "/help/letter-selection-indicators", ([FromServices] ICrosswordHelperService helperService) => helperService.GetLetterSelectionIndicators()},
            { "/help/homophone-indicators", ([FromServices] ICrosswordHelperService helperService) => helperService.GetHomophoneIndicators()},
            { "/help/substitution-indicators",([FromServices] ICrosswordHelperService helperService) => helperService.GetSubstitutionIndicators()},
            { "/help/hidden-word-indicators",([FromServices] ICrosswordHelperService helperService) => helperService.GetHiddenWordIndicators()}
        };

        public static void BuildRoutes(this WebApplication app)
        {
            foreach (var routeDetails in _helpRoutes)
            {
                var url = routeDetails.Key;
                var routeDelegate = routeDetails.Value;
                app.MapGet(url, routeDelegate);
            }

            app.MapGet("/help/{crosswordClue}", (string crosswordClue, [FromServices] ICrosswordHelperService helperService)
                => helperService.Help(crosswordClue))
               .WithName("GetCrosswordHelp");
        }
    }
}
