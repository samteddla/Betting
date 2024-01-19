

import { MatchResponse } from './match-response';
/** todo */
import {
    MatchResponse,
} from ".";


export interface BetResultResponse {

    matches?: Array<MatchResponse> | null;

    betAmount?: number;

    wonAmount?: number;

    totalWinCount?: number;

    cardId?: number;

    matchSelectionId?: number;

    matchTypeId?: number;
}
