import {stringify} from "query-string";

export async function fetchReportPageContent(url: string, sheetId?: number): Promise<string> {
    if (sheetId !== undefined) {
        const qs = stringify({sheetId});
        url += '&' + qs;
    }
    const res = await fetch(url);
    return await res.text();
}