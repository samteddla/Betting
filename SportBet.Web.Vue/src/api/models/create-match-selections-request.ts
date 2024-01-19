

/** todo */
import {
    
} from ".";


export interface CreateMatchSelectionsRequest {

    name?: string | null;

    description?: string | null;

    activeUntil?: Date;

    matches?: Array<number> | null;

    matchesTypes?: Array<number> | null;
}
