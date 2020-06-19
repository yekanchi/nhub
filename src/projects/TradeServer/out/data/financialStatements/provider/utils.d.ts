import { IPeriodic } from "../index";
export declare function fetchReportPageContent(url: string, sheetId?: number): Promise<string>;
export declare namespace AppUtils {
    function ar2fa(str: string): string;
    function faDigits2En(str: string): string;
    function removeNonDigits(str: string): string;
    function parseJalaliDate(str: string): [number, number, number];
    function findJalaliDate(str: string): number[];
    function calculateSalMaliFromReportDateAndPeriod(rpYear: number, rpMonth: number, period: number): [number, number];
    function calculatePeriod(rpYear: number, rpMonth: number, smYear: number, smMonth: any): number;
    function calculateSm(value: IPeriodic): [number, number];
    function calculateProfitMargin(sale: number, cost: number): number;
    function calculatePercent(a: number, b: number, places?: number): number;
    function periodHash(year: number, month: number): number;
    function isSameDay(d1: Date, d2: Date): boolean;
    function isHoliday(d: Date): boolean;
}
//# sourceMappingURL=utils.d.ts.map