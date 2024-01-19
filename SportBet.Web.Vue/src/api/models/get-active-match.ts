

import { SelectionMatchResponse } from './selection-match-response';
/** todo */
import {
    SelectionMatchResponse,
} from ".";


export interface GetActiveMatch {

    matchSelectionId?: number;

    name?: string | null;

    description?: string | null;

    activeUntil?: Date;

    matches?: Array<SelectionMatchResponse> | null;
}
