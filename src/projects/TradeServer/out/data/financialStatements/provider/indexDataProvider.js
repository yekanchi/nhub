"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CodalIndexDataProvider = void 0;
const query_string_1 = require("query-string");
const JDate = require("jalali-date");
const AppUtils_1 = require("utils/AppUtils");
class CodalIndexDataProvider {
    async fetchN10Letters(symbol) {
        const category = 1; // اطلاعات و صورت مالی سالانه
        const letterType = 6; // اطلاعات و صورت‌های مالی میاندوره‌ای
        return CodalIndexDataProvider.fetchLetters(symbol, category, letterType, CodalIndexDataProvider.rawLetterToN10Letter);
    }
    async fetchN3xLetters(symbol) {
        const category = 3; // گزارش عملکرد ماهانه
        const letterType = 58; // گزارش فعالیت ماهانه
        return CodalIndexDataProvider.fetchLetters(symbol, category, letterType, CodalIndexDataProvider.rawLetterToN3xLetter);
    }
    static async fetchLetters(symbol, category, letterType, mapper) {
        let pageNumber = 1;
        let totalPages = 0;
        const letters = [];
        do {
            const qs = query_string_1.stringify({
                Symbol: symbol,
                Category: category,
                LetterType: letterType,
                Childs: false,
                Mains: true,
                PageNumber: pageNumber,
            });
            const response = await fetch('https://search.codal.ir/api/search/v2/q?' + qs);
            const json = await response.json();
            if (totalPages === 0) {
                totalPages = json.Page;
            }
            letters.push(...json.Letters.map(mapper));
            pageNumber += 1;
        } while (pageNumber <= totalPages);
        return letters;
    }
    static rawLetterToN10Letter(rawLetter) {
        const title = AppUtils_1.AppUtils.faDigits2En(rawLetter.Title);
        const [rpYear, rpMonth] = AppUtils_1.AppUtils.findJalaliDate(title);
        let period = 12;
        let matches = title.match(/(\d+) ماهه/);
        if (matches) {
            period = parseInt(matches[1]);
        }
        const audited = title.indexOf('(حسابرسی شده)') !== -1;
        const consolidated = title.indexOf('تلفیقی') == -1;
        let code = 0;
        if (rawLetter.LetterCode) {
            let codeStr = AppUtils_1.AppUtils.faDigits2En(rawLetter.LetterCode);
            code = parseInt(AppUtils_1.AppUtils.removeNonDigits(codeStr));
        }
        let url = null;
        if (rawLetter.Url) {
            url = 'https://codal.ir' + rawLetter.Url;
        }
        let attachmentUrl = null;
        if (rawLetter.AttachmentUrl) {
            attachmentUrl = 'https://codal.ir' + rawLetter.AttachmentUrl;
        }
        return {
            tracingNumber: rawLetter.TracingNo,
            symbol: AppUtils_1.AppUtils.ar2fa(rawLetter.Symbol),
            companyName: AppUtils_1.AppUtils.ar2fa(rawLetter.CompanyName),
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
    static rawLetterToN3xLetter(rawLetter) {
        const title = AppUtils_1.AppUtils.faDigits2En(rawLetter.Title);
        const [rpYear, rpMonth] = AppUtils_1.AppUtils.findJalaliDate(title);
        let code = 0;
        if (rawLetter.LetterCode) {
            let codeStr = AppUtils_1.AppUtils.faDigits2En(rawLetter.LetterCode);
            code = parseInt(AppUtils_1.AppUtils.removeNonDigits(codeStr));
        }
        let url = null;
        if (rawLetter.Url) {
            url = 'https://codal.ir' + rawLetter.Url;
        }
        let attachmentUrl = null;
        if (rawLetter.AttachmentUrl) {
            attachmentUrl = 'https://codal.ir' + rawLetter.AttachmentUrl;
        }
        return {
            tracingNumber: rawLetter.TracingNo,
            symbol: AppUtils_1.AppUtils.ar2fa(rawLetter.Symbol),
            companyName: AppUtils_1.AppUtils.ar2fa(rawLetter.CompanyName),
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
    static parseCodalDateTimeToMillis(str) {
        str = AppUtils_1.AppUtils.faDigits2En(str);
        const parts = str.split(' ');
        const dateParts = AppUtils_1.AppUtils.parseJalaliDate(parts[0]);
        const timeParts = parts[1].split(':').map(Number);
        const date = JDate.toGregorian(...dateParts);
        date.setHours(...timeParts);
        return date.getTime();
    }
}
exports.CodalIndexDataProvider = CodalIndexDataProvider;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiaW5kZXhEYXRhUHJvdmlkZXIuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyIuLi8uLi8uLi8uLi9zcmMvZGF0YS9maW5hbmNpYWxTdGF0ZW1lbnRzL3Byb3ZpZGVyL2luZGV4RGF0YVByb3ZpZGVyLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7OztBQUNBLCtDQUF1QztBQUN2QyxxQ0FBcUM7QUFDckMsNkNBQXdDO0FBcUJ4QyxNQUFhLHNCQUFzQjtJQUUvQixLQUFLLENBQUMsZUFBZSxDQUFDLE1BQWM7UUFDaEMsTUFBTSxRQUFRLEdBQUcsQ0FBQyxDQUFDLENBQUssNkJBQTZCO1FBQ3JELE1BQU0sVUFBVSxHQUFHLENBQUMsQ0FBQyxDQUFHLHNDQUFzQztRQUM5RCxPQUFPLHNCQUFzQixDQUFDLFlBQVksQ0FBQyxNQUFNLEVBQUUsUUFBUSxFQUFFLFVBQVUsRUFBRSxzQkFBc0IsQ0FBQyxvQkFBb0IsQ0FBQyxDQUFDO0lBQzFILENBQUM7SUFFRCxLQUFLLENBQUMsZUFBZSxDQUFDLE1BQWM7UUFDaEMsTUFBTSxRQUFRLEdBQUcsQ0FBQyxDQUFDLENBQUssc0JBQXNCO1FBQzlDLE1BQU0sVUFBVSxHQUFHLEVBQUUsQ0FBQyxDQUFFLHNCQUFzQjtRQUM5QyxPQUFPLHNCQUFzQixDQUFDLFlBQVksQ0FBQyxNQUFNLEVBQUUsUUFBUSxFQUFFLFVBQVUsRUFBRSxzQkFBc0IsQ0FBQyxvQkFBb0IsQ0FBQyxDQUFDO0lBQzFILENBQUM7SUFFTyxNQUFNLENBQUMsS0FBSyxDQUFDLFlBQVksQ0FBb0IsTUFBYyxFQUFFLFFBQWdCLEVBQUUsVUFBa0IsRUFBRSxNQUF5QjtRQUNoSSxJQUFJLFVBQVUsR0FBRyxDQUFDLENBQUM7UUFDbkIsSUFBSSxVQUFVLEdBQUcsQ0FBQyxDQUFDO1FBQ25CLE1BQU0sT0FBTyxHQUFRLEVBQUUsQ0FBQztRQUN4QixHQUFHO1lBQ0MsTUFBTSxFQUFFLEdBQUcsd0JBQVMsQ0FBQztnQkFDakIsTUFBTSxFQUFFLE1BQU07Z0JB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