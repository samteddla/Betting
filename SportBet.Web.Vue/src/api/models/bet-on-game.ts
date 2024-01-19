

import { MatchRequest } from './match-request';
/** todo */
import {
    MatchRequest,
} from ".";


export interface BetOnGame {

    selectionId?: number;

    matchTypeId?: number;

    matches?: Array<MatchRequest> | null;

    amount?: number;
}
