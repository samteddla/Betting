

import { MyBetMatchExtend } from './my-bet-match-extend';
/** todo */
import {
    MyBetMatchExtend,
} from ".";


export interface MyBetExtende {

    betCardId?: number;

    createdAt?: Date;

    matchSelectionId?: number;

    matchSelectionName?: string | null;

    matchSelectionDescription?: string | null;

    betAmount?: number;

    wonAmount?: number;

    totalWinCount?: number;

    matchType?: string | null;

    matches?: Array<MyBetMatchExtend> | null;
}
