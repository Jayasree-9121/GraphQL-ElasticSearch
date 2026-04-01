export interface TransactionLogs {
    ErrorCategory: string;
    Technologies: string;
    InvestigationSteps: string;
    SearchQueries: string;
    ErrorType: string;
    ErrorSummary: string;
    PossibleCauses: Array<string>;
    OccurrenceConditions: Array<string>;
    BusinessImpact: string;
    Severity: string;
    FixSuggestions: Array<string>;
    RecommendedFix: string;
}

