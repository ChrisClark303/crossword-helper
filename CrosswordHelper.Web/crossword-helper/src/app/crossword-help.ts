export class CrosswordHelp {
    originalClue: string;
    wordDetails:[WordDetails];
    // {
    //     "wordDetails": [
    //       {
    //         "originalWord": "string",
    //         "potentialReplacements": [
    //           "string"
    //         ],
    //         "couldBeAnagramIndicator": true,
    //         "couldBeContainerIndicator": true,
    //         "couldBeReversalIndicator": true,
    //         "couldBeSeparator": true
    //       }
    //     ],
    //     "originalClue": "string"
    //   }
}

export class WordDetails {
    originalWord: string;
    potentialReplacements: [string];
    couldBeAnagramIndicator: boolean;
    couldBeContainerIndicator: boolean;
    couldBeRemovalIndicator: boolean;
    couldBeReversalIndicator: boolean;
    couldBeHomophoneIndicator: boolean;
    couldBeLetterSelectionIndicator: boolean;
    couldBeSeparator: boolean;
    couldBeSubstitutionIndicator: boolean;
}
