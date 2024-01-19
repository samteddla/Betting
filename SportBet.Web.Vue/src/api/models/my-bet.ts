

/** todo */
import {
    
} from ".";


export interface MyBet {

    betCardId?: number;

    matchId?: number;

    homeTeam?: string | null;

    home?: string | null;

    awayTeam?: string | null;

    away?: string | null;

    matchType?: string | null;

    outcomeId?: number;

    outcomeName?: string | null;

    createdAt?: Date;

    matchSelectionId?: number;

    matchSelectionName?: string | null;

    matchSelectionDescription?: string | null;
}
