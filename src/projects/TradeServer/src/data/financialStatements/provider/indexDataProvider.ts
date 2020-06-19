import {ILetter, IN10Letter, IN3xLetter} from "..";
import {stringify} from "query-string";
import * as JDate from 'jalali-date';
import {AppUtils} from "utils/AppUtils";
import {ICodalIndexDataProvider} from "./index";

interface IRawLetter {
    TracingNo: number,
    Symbol: string,
    CompanyName: string,
    Title: string,
    LetterCode: string,
    SentDateTime: string,
    PublishDateTime: string,
    Url: string,
    AttachmentUrl: string
}

interface ICodalIndexJson {
    Total: number,
    Page: number,
    Letters: IRawLetter[]
}

export class CodalIndexDataProvider implements ICodalIndexDataProvider {

    async fetchN10Letters(symbol: string): Promise<IN10Letter[]> {
        const category = 1;     // اطلاعات و صورت مالی سالانه
        const letterType = 6;   // اطلاعات و صورت‌های مالی میاندوره‌ای
        return CodalIndexDataProvider.fetchLetters(symbol, category, letterType, CodalIndexDataProvider.rawLetterToN10Letter);
    }

    async fetchN3xLetters(symbol: string): Promise<IN3xLetter[]> {
        const category = 3;     // گزارش عملکرد ماهانه
        const letterType = 58;  // گزارش فعالیت ماهانه
        return CodalIndexDataProvider.fetchLetters(symbol, category, letterType, CodalIndexDataProvider.rawLetterToN3xLetter);
    }

    private static async fetchLetters<T extends ILetter>(symbol: string, category: number, letterType: number, mapper: (IRawLetter) => T): Promise<T[]> {
        let pageNumber = 1;
        let totalPages = 0;
        const letters: T[] = [];
        do {
            const qs = stringify({
                Symbol: symbol,
                Category: category,
                LetterType: letterType,
                Childs: false,              // فقط شرکت اصلی
                Mains: true,
                PageNumber: pageNumber,
            });
            const response = await fetch('https://search.codal.ir/api/search/v2/q?' + qs);
            const json: ICodalIndexJson = await response.json();
            if (totalPages === 0) {
                totalPages = json.Page;
            }
            letters.push(...json.Letters.map(mapper));
            pageNumber += 1;
        } while (pageNumber <= totalPages);
        return letters;
    }

    private static rawLetterToN10Letter(rawLetter: IRawLetter): IN10Letter {
        const title: string = AppUtils.faDigits2En(rawLetter.Title);
        const [rpYear, rpMonth] = AppUtils.findJalaliDate(title);
        let period = 12;
        let matches = title.match(/(\d+) ماهه/);
        if (matches) {
            period = parseInt(matches[1]);
        }
        const audited = title.indexOf('(حسابرسی شده)') !== -1;
        const consolidated = title.indexOf('تلفیقی') == -1;

        let code: number = 0;
        if (rawLetter.LetterCode) {
            let codeStr: string = AppUtils.faDigits2En(rawLetter.LetterCode);
            code = parseInt(AppUtils.removeNonDigits(codeStr));
        }

        let url: string = null;
        if (rawLetter.Url) {
            url = 'https://codal.ir' + rawLetter.Url;
        }

        let attachmentUrl: string = null;
        if (rawLetter.AttachmentUrl) {
            attachmentUrl = 'https://codal.ir' + rawLetter.AttachmentUrl;
        }

        return {
            tracingNumber: rawLetter.TracingNo,
            symbol: AppUtils.ar2fa(rawLetter.Symbol),
            companyName: AppUtils.ar2fa(rawLetter.CompanyName),
            code: code,
            sendTime: CodalIndexDataProvider.parseCodalDateTimeToMillis(rawLetter.SentDateTime),
            publishTime: CodalIndexDataProvider.parseCodalDateTimeToMillis(rawLetter.PublishDateTime),
            url: url,
            attachmentUrl: attachmentUrl,
            rpYear: rpYear,
            rpMonth: rpMonth,
            period: period,
            audited: audited,
            consolidated: consolidated,
        };
    }

    private static rawLetterToN3xLetter(rawLetter: IRawLetter): IN3xLetter {
        const title: string = AppUtils.faDigits2En(rawLetter.Title);
        const [rpYear, rpMonth] = AppUtils.findJalaliDate(title);

        let code: number = 0;
        if (rawLetter.LetterCode) {
            let codeStr: string = AppUtils.faDigits2En(rawLetter.LetterCode);
            code = parseInt(AppUtils.removeNonDigits(codeStr));
        }

        let url: string = null;
        if (rawLetter.Url) {
            url = 'https://codal.ir' + rawLetter.Url;
        }

        let attachmentUrl: string = null;
        if (rawLetter.AttachmentUrl) {
            attachmentUrl = 'https://codal.ir' + rawLetter.AttachmentUrl;
        }

        return {
            tracingNumber: rawLetter.TracingNo,
            symbol: AppUtils.ar2fa(rawLetter.Symbol),
            companyName: AppUtils.ar2fa(rawLetter.CompanyName),
            code: code,
            sendTime: CodalIndexDataProvider.parseCodalDateTimeToMillis(rawLetter.SentDateTime),
            publishTime: CodalIndexDataProvider.parseCodalDateTimeToMillis(rawLetter.PublishDateTime),
            url: url,
            attachmentUrl: attachmentUrl,
            rpYear: rpYear,
            rpMonth: rpMonth,
            period: 1,
        };
    }

    private static parseCodalDateTimeToMillis(str: string): number {
        str = AppUtils.faDigits2En(str);
        const parts = str.split(' ');
        const dateParts = AppUtils.parseJalaliDate(parts[0]);
        const timeParts = parts[1].split(':').map(Number) as [number, number, number];
        const date: Date = JDate.toGregorian(...dateParts);
        date.setHours(...timeParts);
        return date.getTime();
    }
}