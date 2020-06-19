"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var jsdom_1 = require("jsdom");
var AppUtils_1 = require("../../../utils/AppUtils");
function parseN10ReportFromPageContent(letter, content) {
    var document = new jsdom_1.JSDOM(content).window.document;
    var elem, text;
    // اصلاحیه
    var amend = false, amendedTracingNumber = 0, amendmentDesc = '';
    elem = document.getElementById('ctl00_cphBody_ucNavigateToNextPrevLetter_hlPrevVersion');
    if (elem) {
        amend = true;
        var matched = elem.textContent.match(/\( (\d+) \)/);
        if (matched) {
            amendedTracingNumber = parseInt(matched[1]);
        }
        elem = document.getElementById('ctl00_cphBody_ucNavigateToNextPrevLetter_lblCorrectionDesc');
        amendmentDesc = elem.textContent;
    }
    // وضعیت حسابرسی
    var audited = false;
    elem = document.getElementById('ctl00_lblIsAudited');
    if (elem) {
        text = elem.textContent.trim();
        if (text.length > 0) {
            audited = elem.textContent.indexOf('ن') === -1;
        }
    }
    // سرمایه
    var capital = 0;
    elem = document.getElementById('ctl00_lblListedCapital');
    if (elem) {
        text = AppUtils_1.AppUtils.faDigits2En(elem.textContent.trim());
        capital = parseInt(AppUtils_1.AppUtils.removeNonDigits(text));
    }
    // دوره
    var period = 0, reportMonth = 0, reportYear = 0;
    elem = document.getElementById('ctl00_lblPeriod');
    if (elem) {
        text = AppUtils_1.AppUtils.faDigits2En(elem.textContent.trim());
        period = parseInt(AppUtils_1.AppUtils.removeNonDigits(text));
    }
    elem = document.getElementById('ctl00_lblPeriodEndToDate');
    if (elem) {
        text = AppUtils_1.AppUtils.faDigits2En(elem.textContent.trim());
        var _a = AppUtils_1.AppUtils.parseJalaliDate(text), year = _a[0], month = _a[1];
        reportMonth = month;
        reportYear = year;
    }
    // سال مالی
    var smMonth = 0, smYear = 1;
    elem = document.getElementById('ctl00_lblYearEndToDate');
    if (elem) {
        text = AppUtils_1.AppUtils.faDigits2En(elem.textContent.trim());
        var _b = AppUtils_1.AppUtils.parseJalaliDate(text), year = _b[0], month = _b[1];
        smMonth = month;
        smYear = year;
    }
    // تلفیقی
    var talfighi = false;
    elem = document.getElementById('ctl00_lblReportName');
    if (elem) {
        text = AppUtils_1.AppUtils.ar2fa(elem.textContent);
        talfighi = text.indexOf('تلفیقی') !== -1;
    }
    return {
        tracingNumber: letter.tracingNumber,
        symbol: letter.symbol,
        companyName: letter.companyName,
        sendTime: letter.sendTime,
        publishTime: letter.publishTime,
        url: letter.url,
        attachmentUrl: letter.attachmentUrl,
        amend: amend,
        amendedTracingNumber: amendedTracingNumber,
        amendmentDesc: amendmentDesc,
        audited: audited,
        capital: capital,
        period: period,
        rpMonth: reportMonth,
        rpYear: reportYear,
        smMonth: smMonth,
        smYear: smYear,
        talfighi: talfighi,
    };
}
exports.parseN10ReportFromPageContent = parseN10ReportFromPageContent;
//# sourceMappingURL=n10ReportDataProvider.js.map