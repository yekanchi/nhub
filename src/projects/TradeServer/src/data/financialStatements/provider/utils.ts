import {stringify} from "query-string";
import * as persianJs from 'persianjs';
import {IPeriodic} from "../index";

export async function fetchReportPageContent(url: string, sheetId?: number): Promise<string> {
    if (sheetId !== undefined) {
        const qs = stringify({sheetId});
        url += '&' + qs;
    }
    const res = await fetch(url);
    return await res.text();
}



export namespace AppUtils {
    export function ar2fa(str: string): string {
        if (str == null || str.length === 0) {
            return str;
        }
        return persianJs(str).arabicChar().toString();
    }

    export function faDigits2En(str: string): string {
        if (str == null || str.length === 0) {
            return str;
        }
        return persianJs(str).toEnglishNumber().toString();
    }

    export function removeNonDigits(str: string): string {
        return str.replace(/[^0-9]/g, '');
    }

    // example: "1398/11/15" => [1398, 11, 15]
    export function parseJalaliDate(str: string): [number, number, number] {
        return str.split('/').map(Number) as [number, number, number];
    }

    export function findJalaliDate(str: string): number[] {
        const matches = str.match(/(\d{4})\/(\d{2})\/(\d{2})/);
        if (matches) {
            return [parseInt(matches[1]), parseInt(matches[2]), parseInt(matches[3])];
        } else {
            return [];
        }
    }

    export function calculateSalMaliFromReportDateAndPeriod(rpYear: number, rpMonth: number, period: number): [number, number] {
        const smMonth = rpMonth + (12 - period);
        const smYear = rpYear;
        if (smMonth > 12) {
            return [smYear + 1, smMonth - 12];
        } else {
            return [smYear, smMonth];
        }
    }

    export function calculatePeriod(rpYear: number, rpMonth: number, smYear: number, smMonth): number {
        return 12 - ((smYear - rpYear) * 12 + smMonth - rpMonth);
    }

    export function calculateSm(value: IPeriodic): [number, number] {
        const remaining = 12 - value.period;
        let smYear = value.rpYear, smMonth = value.rpMonth + remaining;
        if (smMonth > 12) {
            smMonth -= 12;
            smYear += 1;
        }
        return [smYear, smMonth];
    }

    export function calculateProfitMargin(sale: number, cost: number) {
        return (sale - cost) / sale;
    }

    // a + p% = b
    export function calculatePercent(a: number, b: number, places: number = 0): number {
        return parseFloat(((b / a - 1) * 100).toFixed(places));
    }

    export function periodHash(year: number, month: number): number {
        return year * 100 + month;
    }

    export function isSameDay(d1: Date, d2: Date): boolean {
        return d1.getFullYear() === d2.getFullYear() &&
            d1.getMonth() === d2.getMonth() &&
            d1.getDate() === d2.getDate();
    }

    export function isHoliday(d: Date): boolean {
        return d.getDay() === 4 || d.getDay() === 5;
    }
}